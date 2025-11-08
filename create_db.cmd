@echo off
REM create_db.cmd
REM Creates the BasilioBlogDB database and runs the SQL scripts in the repository.
REM Usage: create_db.cmd [SQL_SERVER_INSTANCE] [SQL_USER] [SQL_PASS] [ORDER_FILE]
REM Example (Integrated): create_db.cmd "(localdb)\MSSQLLocalDB"
REM Example (SQL auth, prompt for password): create_db.cmd "localhost\SQLEXPRESS" root
REM Example (SQL auth, pass on CLI): create_db.cmd "localhost\SQLEXPRESS" root MyPassword order.txt

SETLOCAL ENABLEDELAYEDEXPANSION

:: Repo root (script folder)
SET REPO=%~dp0

:: Parameters
SET SERVER=%1
SET SQLUSER=%2
SET SQLPASS=%3
SET ORDERFILE=%4
IF "%SERVER%"=="" SET SERVER=(localdb)\MSSQLLocalDB

echo Using SQL Server: %SERVER%

:: Detect sqlcmd presence
where sqlcmd >nul 2>&1
IF ERRORLEVEL 1 (
  echo ERROR: "sqlcmd" was not found in PATH.
  echo Please install the SQL Server command-line utilities or add sqlcmd.exe to your PATH.
  echo See: https://learn.microsoft.com/sql/tools/sqlcmd-utility
  goto :END
)

:: Determine auth args. If SQL user provided but no password, prompt securely using PowerShell.
IF NOT "%SQLUSER%"=="" (
  IF "%SQLPASS%"=="" (
    echo SQL username provided but no password. Prompting for password (hidden)...
    for /f "usebackq delims=" %%P in (`powershell -NoProfile -Command "Add-Type -AssemblyName System.Security; $sec = Read-Host -AsSecureString -Prompt 'SQL Password'; $ptr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($sec); $plain = [Runtime.InteropServices.Marshal]::PtrToStringAuto($ptr); Write-Output $plain"`) do set SQLPASS=%%P
  )
  SET AUTHARGS=-U "%SQLUSER%" -P "%SQLPASS%"
  echo Using SQL auth as user: %SQLUSER%
) ELSE (
  SET AUTHARGS=-E
  echo Using Integrated Windows Authentication (Trusted Connection)
)

:: Quick connectivity test before creating DB
echo Testing connection to %SERVER% ...
sqlcmd -S "%SERVER%" %AUTHARGS% -Q "SELECT 1;" -b >nul 2>&1
IF ERRORLEVEL 1 (
  echo ERROR: Unable to connect to the SQL Server instance "%SERVER%" using the provided credentials.
  echo Common causes:
  echo  - SQL Server instance "%SERVER%" is not installed or not running.
  echo  - SQL Server Browser service is stopped (for named instances).
  echo  - Firewall blocking connections.
  echo  - SQL authentication not enabled, or credentials are incorrect.
  echo
  echo Please ensure the instance exists and is reachable. If you need to install SQL Server Express, see:
  echo   https://www.microsoft.com/en-us/sql-server/sql-server-downloads
  echo
  echo After installation/start, rerun this script.
  goto :END
)

echo Creating database BasilioBlogDB if it doesn't exist...
sqlcmd -S "%SERVER%" %AUTHARGS% -Q "IF DB_ID(N'BasilioBlogDB') IS NULL CREATE DATABASE BasilioBlogDB;" -b
IF ERRORLEVEL 1 (
  echo Failed to create database. Exiting.
  goto :END
)

echo Waiting a second for the new database to be available...
ping -n 2 127.0.0.1 >nul

:: Helper to execute a script file with auth args
:execScript
IF "%~1"=="" (
  echo execScript called without filename
  EXIT /B 1
)
echo Executing script: %~1
sqlcmd -S "%SERVER%" %AUTHARGS% -d BasilioBlogDB -i "%~1" -b
IF ERRORLEVEL 1 (
  echo Error executing %~1
  EXIT /B 1
)
EXIT /B 0

:: Execute scripts in order if ORDERFILE exists, otherwise run default sorted order
IF NOT "%ORDERFILE%"=="" (
  if exist "%ORDERFILE%" (
    echo Running ordered script list from %ORDERFILE% ...
    for /f "usebackq tokens=* delims=" %%L in ("%ORDERFILE%") do (
      set LINE=%%L
      rem skip empty lines and comments starting with #
      if not "!LINE!"=="" (
        set FIRSTCHAR=!LINE:~0,1!
        if not "!FIRSTCHAR!"=="#" (
          rem if path is relative (doesn't contain :), prefix with repo
          echo !LINE! | findstr /c:: >nul
          if errorlevel 1 (
            set SCRIPT_PATH=%REPO%!LINE!
          ) else (
            set SCRIPT_PATH=!LINE!
          )
          call :execScript "!SCRIPT_PATH!"
          if errorlevel 1 goto :END
        )
      )
    )
  ) ELSE (
    echo Specified order file not found: %ORDERFILE%
    goto :END
  )
) ELSE (
  echo Running table scripts from BasilioLe2\BasilioBlogDB\dbo ...
  for %%f in ("BasilioLe2\BasilioBlogDB\dbo\*.sql") do (
    call :execScript "%REPO%%%~f"
    if errorlevel 1 goto :END
  )

  echo Running stored procedure scripts from "BasilioLe2\BasilioBlogDB\Stored Procedures" ...
  for %%f in ("BasilioLe2\BasilioBlogDB\Stored Procedures\*.sql") do (
    call :execScript "%REPO%%%~f"
    if errorlevel 1 goto :END
  )
)

echo Database setup completed successfully.

:: Update BlogTestUI appsettings.json files to point to this database/instance (without embedding password)
echo Updating BlogTestUI connection string placeholders...
IF NOT "%SQLUSER%"=="" (
  SET JSON_CONNS=Data Source=%SERVER%;Initial Catalog=BasilioBlogDB;User ID=%SQLUSER%;Password=__REPLACE_ME__;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;MultiSubnetFailover=False
) ELSE (
  SET JSON_CONNS=Data Source=%SERVER%;Initial Catalog=BasilioBlogDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;MultiSubnetFailover=False
)

:: Update known appsettings.json locations (project copies)
SET APP1=%REPO%BasilioLe2\BlogTestUI\appsettings.json
SET APP2=%REPO%BasilioLe2\BasilioLE2\BlogTestUI\appsettings.json

IF EXIST "%APP1%" (
  echo Updating %APP1%
  (echo { & echo   "ConnectionStrings": { & echo     "SqlDB": "!JSON_CONNS!" & echo   } & echo }) > "%APP1%"
) ELSE (
  echo %APP1% not found, skipping.
)
IF EXIST "%APP2%" (
  echo Updating %APP2%
  (echo { & echo   "ConnectionStrings": { & echo     "SqlDB": "!JSON_CONNS!" & echo   } & echo }) > "%APP2%"
) ELSE (
  echo %APP2% not found, skipping.
)

:END
ENDLOCAL
pause
