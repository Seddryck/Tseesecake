Param(
	[switch] $force=$false
	, [string] $databaseService= "MSSQL`$SQL2019"
	, [string] $config = "Release"
	, [string[]] $frameworks = @("net6.0", "net7.0")
)
. $PSScriptRoot\..\Windows-Service.ps1
. $PSScriptRoot\..\Docker-Container.ps1
. $PSScriptRoot\..\Run-TestSuite.ps1

if ($force) {
	Write-Host "Enforcing QA testing for Microsoft SQL Server"
}

$filesChanged = & git diff --name-only HEAD HEAD~1
if ($force -or ($filesChanged -like "*mssql*")) {
	Write-Host "Deploying Microsoft SQL Server testing environment"
	
	# Starting database Service
	if ($env:APPVEYOR -eq "True") {
		try { $previouslyRunning = Start-Windows-Service $databaseService }
		catch {
			Write-Warning "Failure to start a Windows service: $_"
			exit 1
		}
	} else {
		$previouslyRunning, $running = Deploy-Container -FullName "mssqlserver" -Nickname "mssql"
		if (!$previouslyRunning){
			Start-Sleep -s 10
		}
	}

	# Deploying database based on script
	Write-host "`tDeploying database ..."
	Write-host "`t`tCreating file to import ..."
	Get-Content "../WindEnergy.csv" -Raw | Out-File -Encoding UTF8NoBOM "../bin/WindEnergyBOM.csv"
	$fullPath = Get-Item "../bin/WindEnergyBOM.csv" | Resolve-Path | Convert-Path	
	Write-host "`t`tFile to import created at $fullPath"
	if ($env:APPVEYOR -eq "True") {
		Write-host "`t`tUsing local client"
		Write-host "`t`tCreating tables ..."
		& sqlcmd -U "sa" -P "Password12!" -S ".\SQL2019" -i ".\deploy-mssqlserver-database.sql"
		Write-host "`t`tTables created"
		Write-host "`t`tBulk copying data ..."
		& bcp WindEnergyStg in $fullPath -U sa -P Password12! -S localhost -d Energy -t "," -C 65001 -c -F2
		Write-host "`t`tData bulk copied"
		Write-host "`t`tPost deploying data ..."
		& sqlcmd -U "sa" -P "Password12!" -S ".\SQL2019" -i ".\deploy-mssqlserver-database-post.sql"
		Write-host "`t`tData post deployed..."
	} else {
		Write-host "`t`tCopying deployment scripts on container ..."
		& docker cp "./deploy-mssqlserver-database.sql" mssql:"./deploy-mssqlserver-database.sql"
		& docker cp "./deploy-mssqlserver-database-post.sql" mssql:"./deploy-mssqlserver-database-post.sql"
		
		Write-host "`t`tUsing $fullPath as raw source ..."
		Write-host "`t`tScript copied"
		Write-host "`t`tUsing remote client on the docker container and local BCP ..."
		& docker exec -it mssql /opt/mssql-tools/bin/sqlcmd "-Usa" "-PPassword12!" "-i./deploy-mssqlserver-database.sql"
		& bcp WindEnergyStg in $fullPath -U sa -P Password12! -S localhost -d Energy -t "," -C 65001 -c -F2
		& docker exec -it mssql /opt/mssql-tools/bin/sqlcmd "-Usa" "-PPassword12!" "-i./deploy-mssqlserver-database-post.sql"
	}
	Write-host "`tDatabase deployed"
	
	# Copying correct config
	Write-Host "`tConfiguring URI for instance ..."
	foreach ($framework in $frameworks)
	{
		$filePath = "$PSScriptRoot\..\bin\$config\$framework\Instance.txt"
		$serverUrl = if ($env:APPVEYOR -eq "True") { "localhost/SQL2019" } else { "localhost" }
		$serverUrl | Set-Content -NoNewline -Force $filePath
		Write-Host "`t`tConfigure value '$serverUrl' into $filePath"
	}
	Write-Host "`tURI for instance configured."
	

	# Running QA tests
	$testSuccessful = Run-TestSuite @("MsSqlServer") -config $config -frameworks $frameworks

	# Stopping database Service
	if (!$previouslyRunning) {
		if ($env:APPVEYOR -eq "True") {
			Stop-Windows-Service $databaseService
		} else {
			Remove-Container $running
		}
	}
	
	# Raise failing tests
	exit $testSuccessful
} else {
	return -1
}