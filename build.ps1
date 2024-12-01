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

try {
    Write-Host "Building Windows CLI..."
    cd $windowsCLI
    dotnet publish -c Release -r win-x64 --self-contained -o "$outputFolder\CLI-Windows"
    Compress-Archive -Path "$outputFolder\CLI-Windows\*" -DestinationPath "$outputFolder\$cliWindowsName.zip"
    Write-Host "Windows CLI build and packaging complete."
} catch {
    Write-Host "Windows CLI build failed. Skipping."
}

try {
    Write-Host "Building Linux CLI..."
    cd $linuxCLI
    dotnet publish -c Release -r linux-x64 --self-contained -o "$outputFolder\CLI-Linux"
    Compress-Archive -Path "$outputFolder\CLI-Linux\*" -DestinationPath "$outputFolder\$cliLinuxName.zip"
    Write-Host "Linux CLI build and packaging complete."
} catch {
    Write-Host "Linux CLI build failed. Skipping."
}

try {
    Write-Host "Building Windows GUI..."
    cd $windowsGUI
    dotnet publish -c Release -r win-x64 --self-contained -o "$outputFolder\GUI-Windows"
    Compress-Archive -Path "$outputFolder\GUI-Windows\*" -DestinationPath "$outputFolder\$guiWindowsName.zip"
    Write-Host "Windows GUI build and packaging complete."
} catch {
    Write-Host "Windows GUI build failed. Skipping."
}

Write-Host "Build process complete! Release ZIP files are located in $outputFolder."
