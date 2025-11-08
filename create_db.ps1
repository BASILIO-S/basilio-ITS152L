param(
  [string]$Server = "(localdb)\MSSQLLocalDB",
  [string]$User = "",
  [string]$Pass = "",
  [string]$OrderFile = ""
)

# PowerShell wrapper for create_db.cmd
$batchPath = Join-Path -Path $PSScriptRoot -ChildPath 'create_db.cmd'
if (-not (Test-Path $batchPath)) {
  Write-Host "Error: create_db.cmd not found at $batchPath"
  exit 1
}

# If user provided but not pass, prompt for secure password
if ($User -ne '' -and $Pass -eq '') {
  $secure = Read-Host -AsSecureString -Prompt 'SQL Password (input hidden)'
  $BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
  $Pass = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)
  [System.Runtime.InteropServices.Marshal]::ZeroFreeBSTR($BSTR) | Out-Null
}

# Build argument array for cmd.exe
$arguments = @('/c', '"' + $batchPath + '"')
if ($Server -ne '') { $arguments += '"' + $Server + '"' }
if ($User -ne '') { $arguments += '"' + $User + '"' }
if ($Pass -ne '') { $arguments += '"' + $Pass + '"' }
if ($OrderFile -ne '') { $arguments += '"' + $OrderFile + '"' }

Write-Host "Running: cmd.exe $($arguments -join ' ')"

# Start cmd.exe and let it inherit the console
$psi = New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName = 'cmd.exe'
$psi.Arguments = $arguments -join ' '
$psi.RedirectStandardOutput = $false
$psi.RedirectStandardError = $false
$psi.UseShellExecute = $true
$process = [System.Diagnostics.Process]::Start($psi)
$process.WaitForExit()
exit $process.ExitCode
