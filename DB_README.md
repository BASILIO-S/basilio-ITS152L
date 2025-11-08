DB creation helper for BasilioBlogDB

What this does
- Adds a small Windows batch script `create_db.cmd` that:
  - Creates a database named `BasilioBlogDB` if it doesn't exist on the target SQL Server instance.
  - Executes all `.sql` files found in `BasilioLe2/BasilioBlogDB/dbo/` to create tables.
  - Executes all `.sql` files found in `BasilioLe2/BasilioBlogDB/Stored Procedures/` to create stored procedures.

Configuration change
- The project configuration has been updated to use SQL Server authentication with user `root` (password placeholder). Two configuration files were updated:
  - `BasilioLe2/BlogTestUI/appsettings.json`
  - `BasilioLe2/BasilioLE2/BlogTestUI/appsettings.json`
  - `BasilioLe2/BlogTestUI/App.config`
  - `BasilioLe2/BasilioLE2/BlogTestUI/App.config`

Connection string placeholder
- The connection strings now use the following format (password is intentionally set to `__REPLACE_ME__`):

  Data Source=localhost\\SQLEXPRESS;Initial Catalog=BasilioBlogDB;User ID=root;Password=__REPLACE_ME__;

How to set your password locally (do not commit secrets)
- Option A (local edit - quick): Open the appsettings.json or App.config in your local copy and replace `__REPLACE_ME__` with the real password. Don't commit it.

- Option B (environment variable - recommended): Update the projects to read the password from an environment variable or user secrets. Example (PowerShell) to set an env var for the current session:

  $env:DB_PASSWORD = "YourPasswordHere"

  Then update your app to build the connection string at runtime using the environment variable.

Running the DB creation script
- Use `create_db.cmd` or `create_db.ps1` to create the DB. The script will prompt for a password if you pass a username but no password:

  From PowerShell (recommended):
  .\create_db.ps1 -Server "localhost\SQLEXPRESS" -User root

  From cmd.exe:
  create_db.cmd "localhost\SQLEXPRESS" root

Security note
- Replace the placeholder with your real password only on your local machine. Rotate the password if you previously shared it. Do not commit real secrets to the repository.

Troubleshooting
- If the script reports `sqlcmd` not found, install the SQL Server command-line utilities: https://learn.microsoft.com/sql/tools/sqlcmd-utility
- If connection fails, ensure the SQL Server instance `localhost\SQLEXPRESS` exists and is running, Mixed Mode is enabled, and the `root` login exists and has the necessary permissions.
