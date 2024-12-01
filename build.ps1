# Prompt for the version number
$version = Read-Host "Enter the release version (e.g., 1.0.1)"

# Get the current directory as the project root
$projectRoot = Get-Location
$outputFolder = "$projectRoot\Release"

# Ensure the output folder exists
if (!(Test-Path -Path $outputFolder)) {
    New-Item -ItemType Directory -Path $outputFolder
}

# Build configurations
$windowsCLI = "$projectRoot\VEXOR.CLI"
$linuxCLI = "$projectRoot\VEXOR.CLI"
$windowsGUI = "$projectRoot\VEXOR.GUI"

# Define file naming convention
$cliWindowsName = "VEXOR.CLI.Windows.v$version"
$cliLinuxName = "VEXOR.CLI.Linux.v$version"
$guiWindowsName = "VEXOR.GUI.Windows.v$version"

# Helper function to handle warnings and errors
function SafeBuild {
    param (
        [string]$buildPath,
        [string]$runtime,
        [string]$outputSubFolder,
        [string]$zipName
    )
    try {
        Write-Host "Building for runtime: $runtime..."
        cd $buildPath
        dotnet publish -c Release -r $runtime --self-contained -o "$outputFolder\$outputSubFolder" 2>&1 | Tee-Object -Variable buildOutput

        # Check if the output folder contains any files
        if (Get-ChildItem -Path "$outputFolder\$outputSubFolder" -Recurse | Where-Object { $_.Name -match ".*" }) {
            Write-Host "$runtime build succeeded. Packaging files..."
            Compress-Archive -Path "$outputFolder\$outputSubFolder\*" -DestinationPath "$outputFolder\$zipName.zip"
        } else {
            Write-Host "$runtime build failed. Skipping packaging."
        }
    } catch {
        Write-Host "An error occurred during $runtime build. Skipping."
    }
}

# Build Windows CLI
SafeBuild -buildPath $windowsCLI -runtime "win-x64" -outputSubFolder "CLI-Windows" -zipName $cliWindowsName

# Build Linux CLI
SafeBuild -buildPath $linuxCLI -runtime "linux-x64" -outputSubFolder "CLI-Linux" -zipName $cliLinuxName

# Build Windows GUI
SafeBuild -buildPath $windowsGUI -runtime "win-x64" -outputSubFolder "GUI-Windows" -zipName $guiWindowsName

Write-Host "Build process complete! Release ZIP files are located in $outputFolder."
