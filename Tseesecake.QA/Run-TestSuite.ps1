Function Run-TestSuite {
	[CmdletBinding()]
	Param(
		[Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string[]] $categories
		, [string] $config = "Release"
		, [string[]] $frameworks = @("net6.0")
		, [string] $project = "Tseesecake.QA"
	)

	Begin {
		$testSuccessful = 0
		if ($env:APPVEYOR -eq "True") {
			$adapters = "--test-adapter-path:.", "--logger:Appveyor"
		}
	}

	Process {
		foreach ($framework in $frameworks) {
			$buildMsg = & dotnet build "..\..\$project" -c $config -f $framework --nologo
			if ($lastexitcode -ne 0) {
				Write-Warning "Cannot build the Test assembly! `r`n$($buildMsg -join "`r`n")"
			} else {
				foreach ($category in $categories) {
					Write-Output "`tRunning test-suite for $category ($framework)"
					$testArgs  = @("test", "..\..\$project")
					$testArgs += @("--filter", "`"TestCategory=$($category.Split("+") -join "`"`"&`"`"TestCategory=")`"")
					$testArgs += @("-c", $config)
					$testArgs += @("-f", $framework)
					$testArgs += @("--no-build", "--nologo")
					$testArgs += $adapters
					& dotnet $testArgs | Out-Host
					$testSuccessful += $lastexitcode
				}
			}
		}
    }

    End {
        return $testSuccessful
    }
}


