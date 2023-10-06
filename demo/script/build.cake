
#tool "dotnet:?package=grate&version=1.5.3"
#r "..\..\src\Cake.grate\bin\Debug\net6.0\Cake.grate.dll"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////


//Todo: Add test for DisableTokenReplacement

Task("Core-Functions")
.Does(() => 
{
   const string outputDirectory = "./output";
   CreateDirectory(outputDirectory);

   GrateMigrate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-core-functions;Trusted_Connection=True;TrustServerCertificate=true;",
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

   GrateMigrate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      Silent = true
   });


   var backupFile = new FilePath("./grate-admin-functions.bak")
      .MakeAbsolute(Context.Environment);

   GrateMigrate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      ConnectionStringAdmin = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      CommandTimeoutAdmin = 60,
      Restore = backupFile.ToString(),
      Silent = true
   });
});

Task("DryRun")
.Does(() => 
{
   GrateMigrate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-dry-run;Trusted_Connection=True;TrustServerCertificate=true;",
      DryRun = true,
      Silent = true
   });
});

Task("Baseline")
.Does(() => 
{
   GrateMigrate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-baseline;Trusted_Connection=True;TrustServerCertificate=true;",
      Baseline = true,
      Silent = true
   });
});


Task("Default")
   .IsDependentOn("Core-Functions")
   .IsDependentOn("Admin")
   .IsDependentOn("DryRun")
   .IsDependentOn("Baseline");

RunTarget(target);
