Param(
	[switch] $force=$false
	, $config= "Release"
	, [string[]] $frameworks = @("net6.0", "net7.0")
	, [string] $version = "v0.8.0"
)
. $PSScriptRoot\..\Run-TestSuite.ps1

if ($force) {
	Write-Output "Enforcing QA testing for DuckDB"
}

$rootUrl = "https://github.com/duckdb/duckdb/releases"
if ($null -eq $version) {
	$rootUrl = "$rootUrl/latest/download"
} else {
	$rootUrl = "$rootUrl/download/$version"
}

if (-not($env:PATH -like "7-zip")) {
	$env:PATH += ";C:\Program Files\7-Zip"
}

$duckPath = "C:/Program Files/DuckDB/"
Write-Output "Using '$duckPath' as DuckDB CLI installation folder"

$filesChanged = & git diff --name-only HEAD HEAD~1
if ($force -or ($filesChanged -like "*duckdb*")) {
	Write-Output "Deploying DuckDB testing environment"

	if (-not (Test-Path -Path $duckPath\duckdb.exe)) {
		Write-Output "`tDownloading DuckDB CLI from $rootUrl ..."
		Invoke-WebRequest "$rootUrl/duckdb_cli-windows-amd64.zip" -OutFile "$env:temp\duckdb_cli.zip"
		Write-Output "`tDuckDB CLI downloaded."
		Write-Output "`tExtracting from archive DuckDB CLI..."		
		& 7z e "$env:temp\duckdb_cli.zip" -o"$duckPath" -y | Out-Null
		Write-Output "`tDuckDB CLI extracted."		
	} else {
		Write-Output "`tDuckDB CLI already installed: skipping installation."
	}
	
	$alreadyDownloaded = $false
	foreach ($framework in $frameworks)
	{
		$binPath = "./../bin/$config/$framework/"
		if (-not (Test-Path -Path $binPath\duckdb.dll)) {
			Write-Output "`tInstalling DuckDB library ..."
			if (-not $alreadyDownloaded) {
				Write-Output "`t`tDownloading DuckDB library ..."
				Invoke-WebRequest "$rootUrl/libduckdb-windows-amd64.zip" -OutFile "$env:temp\libduckdb.zip"
				$alreadyDownloaded = $true
				Write-Output "`t`tDuckDB library downloaded."
			}
			Write-Output "`t`tExtracting from archive DuckDB library to '$binPath' ..."
			& 7z e "$env:temp\libduckdb.zip" -o"$binPath" -y
			Write-Output "`t`tArchive extracted."
			Write-Output "`tDuckDB library installed."
		} else {
			Write-Output "`tDuckDB library already installed: skipping installation."
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

		Write-Output "`tCreating database at $databasePath ..."
		if (-not($env:PATH -like $duckPath)) {
			$env:PATH += ";$duckPath"
		}
		(Get-Content ".\deploy-duckdb-database.sql") -replace '<path>', $databasePath | & duckdb.exe
		Write-Output "`tDatabase created."
	}

	# Running QA tests
	Write-Output "Running QA tests related to DuckDB"
	$testSuccessful = Run-TestSuite @("DuckDB") -config $config -frameworks $frameworks

	# Raise failing tests
	exit $testSuccessful
} else {
	return -1
}