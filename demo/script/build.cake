
#tool "dotnet:?package=grate&version=1.5.4"
#r "..\..\src\Cake.grate\bin\Debug\net8.0\Cake.grate.dll"

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

   Grate(new GrateSettings()
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
      RunAllAnyTimeScripts = true
   });
});

Task("Admin")
.Does(() => 
{
   // Using ConnectionStringAdmin requires the database to already exist

   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
      Silent = true
   });


   var backupFile = new FilePath("./grate-admin-functions.bak")
      .MakeAbsolute(Context.Environment);

   Grate(new GrateSettings()
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
   Grate(new GrateSettings()
   {
      ConnectionString = "Server=(local)\\sql2022;Database=grate-dry-run;Trusted_Connection=True;TrustServerCertificate=true;",
      DryRun = true,
      Silent = true
   });
});

Task("Baseline")
.Does(() => 
{
   Grate(new GrateSettings()
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
