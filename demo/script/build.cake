
#tool "dotnet:?package=grate&version=1.7.4"
#r "..\..\src\Cake.grate\bin\Debug\net8.0\Cake.grate.dll"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Core-Functions")
.Does(() => 
{
   const string outputDirectory = "./output";
   CreateDirectory(outputDirectory);

   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local);Database=grate-core-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      CommandTimeout = 60,
      SchemaName = "demo",
      Drop = true,
      Silent = true,
      WithTransaction = true,
      DoNotStoreScriptsRunText = true,
      DatabaseType = "sqlserver",
      Environment = "Demo",
      OutputPath = outputDirectory,
      Version = "0.1.2.3",
      RepositoryPath = "RepositoryPath",
      SqlFilesDirectory = "./sqlfiles",
      Folders= "up=renamed_up;beforemigration=preparefordeploy",
      WarnOnOneTimeScriptChanges = true,
      WarnAndIgnoreOnOneTimeScriptChanges = true,
      RunAllAnyTimeScripts = true,
      DisableTokenReplacement = true
   });
});

Task("Admin")
.Does(() => 
{
   // Using ConnectionStringAdmin requires the database to already exist

   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local);Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      Silent = true
   });

   // Ensure file permissions allow access to the backup file from SQL Server
   var backupFile = new FilePath("./grate-admin-functions.bak")
      .MakeAbsolute(Context.Environment);

   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local);Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      ConnectionStringAdmin = "Server=(local);Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      CommandTimeoutAdmin = 60,
      Restore = backupFile.ToString(),
      Silent = true
   });
});

Task("DryRun")
.Does(() => 
{
   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local);Database=grate-dry-run;Trusted_Connection=True;TrustServerCertificate=true;",
      DryRun = true,
      Silent = true
   });
});

Task("Baseline")
.Does(() => 
{
   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local);Database=grate-baseline;Trusted_Connection=True;TrustServerCertificate=true;",
      Baseline = true,
      Silent = true
   });
});

Task("UserTokens")
.Does(() => 
{
   var settings = new GrateSettings()
   {
      ConnectionString = "Server=(local);Database=grate-usertokens;Trusted_Connection=True;TrustServerCertificate=true;",
      SqlFilesDirectory = "./sqlfiles_usertokens",
      Silent = true
   };

   settings.WithUserToken("MyToken", "Replaced");

   Grate(settings);
});


Task("Default")
   .IsDependentOn("Core-Functions")
   .IsDependentOn("Admin")
   .IsDependentOn("DryRun")
   .IsDependentOn("Baseline")
   .IsDependentOn("UserTokens");

RunTarget(target);
