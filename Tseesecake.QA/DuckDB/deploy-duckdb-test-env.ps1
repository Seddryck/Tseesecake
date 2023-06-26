Param(
	[switch] $force=$false
	, $config= "Release"
	, [string[]] $frameworks = @("net6.0", "net7.0")
)
. $PSScriptRoot\..\Run-TestSuite.ps1

if ($force) {
	Write-Host "Enforcing QA testing for DuckDB"
}

$rootUrl = "https://github.com/duckdb/duckdb/releases/latest/download"
if (-not($env:PATH -like "7-zip")) {
	$env:PATH += ";C:\Program Files\7-Zip"
}

$duckPath = "C:/Program Files/DuckDB/"
Write-Host "Using '$duckPath' as DuckDB CLI installation folder"

$filesChanged = & git diff --name-only HEAD HEAD~1
if ($force -or ($filesChanged -like "*duckdb*")) {
	Write-Host "Deploying DuckDB testing environment"

	if (-not (Test-Path -Path $duckPath\duckdb.exe)) {
		Write-Host "`tDownloading DuckDB CLI ..."
		Invoke-WebRequest "$rootUrl/duckdb_cli-windows-amd64.zip" -OutFile "$env:temp\duckdb_cli.zip"
		Write-Host "`tDuckDB CLI downloaded."
		Write-Host "`tExtracting from archive DuckDB CLI..."		
		& 7z e "$env:temp\duckdb_cli.zip" -o"$duckPath" -y | Out-Null
		Write-Host "`tDuckDB CLI extracted."		
	} else {
		Write-Host "`tDuckDB CLI already installed: skipping installation."
	}
	
	$alreadyDownloaded = $false
	foreach ($framework in $frameworks)
	{
		$binPath = "./../bin/$config/$framework/"
		if (-not (Test-Path -Path $binPath\duckdb.dll)) {
			Write-Host "`tInstalling DuckDB library ..."
			if (-not $alreadyDownloaded) {
				Write-Host "`t`tDownloading DuckDB library ..."
				Invoke-WebRequest "$rootUrl/libduckdb-windows-amd64.zip" -OutFile "$env:temp\libduckdb.zip"
				$alreadyDownloaded = $true
				Write-Host "`t`tDuckDB library downloaded."
			}
			Write-Host "`t`tExtracting from archive DuckDB library to '$binPath' ..."
			& 7z e "$env:temp\libduckdb.zip" -o"$binPath" -y
			Write-Host "`t`tArchive extracted."
			Write-Host "`tDuckDB library installed."
		} else {
			Write-Host "`tDuckDB library already installed: skipping installation."
		}
	}

	# Deploying database based on script
	foreach ($framework in $frameworks)
	{
		$binPath = "./../bin/$config/$framework"
		$databasePath = "$binPath/Customer.duckdb"
		if (Test-Path -Path $databasePath) {
			Remove-Item -Path $databasePath
		}

		Write-host "`tCreating database at $databasePath ..."
		if (-not($env:PATH -like $duckPath)) {
			$env:PATH += ";$duckPath"
		}
		(Get-Content ".\deploy-duckdb-database.sql") -replace '<path>', $databasePath | & duckdb.exe
		Write-host "`tDatabase created."
	}

	# Running QA tests
	Write-Host "Running QA tests related to DuckDB"
	$testSuccessful = Run-TestSuite @("DuckDB") -config $config -frameworks $frameworks

	# Raise failing tests
	exit $testSuccessful
} else {
	return -1
}