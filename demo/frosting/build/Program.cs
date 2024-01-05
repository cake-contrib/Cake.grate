using System;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Core;
using Cake.Grate;
using Cake.Frosting;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .InstallTool(new Uri("dotnet:?package=grate&version=1.5.4"))
            .Run(args);
    }
}


[TaskName("Core-Functions")]
public sealed class CoreFunctions : FrostingTask<FrostingContext>
{
    public override void Run(FrostingContext context)
    {
        const string outputDirectory = "./output";
        context.CreateDirectory(outputDirectory);

        context.Grate(new GrateSettings()
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
            SqlFilesDirectory = "../sqlfiles",
            Folders= "up=renamed_up;beforemigration=preparefordeploy",
            WarnOnOneTimeScriptChanges = true,
            WarnAndIgnoreOnOneTimeScriptChanges = true,
            RunAllAnyTimeScripts = true
        });
    }
}

[TaskName("Admin")]
public sealed class Admin : FrostingTask<FrostingContext>
{
    public override void Run(FrostingContext context)
    {
        // Using ConnectionStringAdmin requires the database to already exist

        context.Grate(new GrateSettings()
        {
            ConnectionString = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
            Silent = true
        });

        // Ensure file permissions allow access to the backup file from SQL Server
        var backupFile = new FilePath("../grate-admin-functions.bak")
            .MakeAbsolute(context.Environment);

        context.Grate(new GrateSettings()
        {
            ConnectionString = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
            ConnectionStringAdmin = "Server=(local)\\sql2022;Database=grate-admin-functions;Trusted_Connection=True;TrustServerCertificate=true;",
            CommandTimeoutAdmin = 60,
            Restore = backupFile.ToString(),
            Silent = true
        });
    }
}

[TaskName("DryRun")]
public sealed class DryRun : FrostingTask<FrostingContext>
{
    public override void Run(FrostingContext context)
    {
        context.Grate(new GrateSettings()
        {
            ConnectionString = "Server=(local)\\sql2022;Database=grate-dry-run;Trusted_Connection=True;TrustServerCertificate=true;",
            DryRun = true,
            Silent = true
        });
    }
}

[TaskName("Baseline")]
public sealed class Baseline : FrostingTask<FrostingContext>
{
    public override void Run(FrostingContext context)
    {
        context.Grate(new GrateSettings()
        {
            ConnectionString = "Server=(local)\\sql2022;Database=grate-baseline;Trusted_Connection=True;TrustServerCertificate=true;",
            Baseline = true,
            Silent = true
        });
    }
}


[TaskName("Default")]
[IsDependentOn(typeof(CoreFunctions))]
[IsDependentOn(typeof(Admin))]
[IsDependentOn(typeof(DryRun))]
[IsDependentOn(typeof(Baseline))]
public class DefaultTask : FrostingTask
{
}