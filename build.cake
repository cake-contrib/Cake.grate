///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var packageversion = Argument("packageversion", "0.1");

///////////////////////////////////////////////////////////////////////////////
// Variables and Constants
///////////////////////////////////////////////////////////////////////////////

const string BuildArtifacts = "./BuildArtifacts";

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
   packageversion = packageversion
      .Replace("refs/tags/", "")
      .TrimStart('v');
   Information($"Using version: {packageversion}");
});

Task("Build")
.Does(() => 
{
   DotNetClean("./src/Cake.grate/Cake.grate.csproj");

   var settings = new DotNetBuildSettings
   {
      Configuration = "Release",
      MSBuildSettings = new DotNetMSBuildSettings
      {
         Version = packageversion
      }
   };

   DotNetBuild("./src/Cake.grate/Cake.grate.csproj", settings);
});

Task("Test")
.Does(() => 
{
   DotNetTest("./src/Cake.grate.Tests/Cake.grate.Tests.csproj");
});

Task("Pack")
.Does(() => 
{
   CleanDirectory(BuildArtifacts);

   var settings = new DotNetPackSettings
   {
      Configuration = "Release",
      OutputDirectory = BuildArtifacts,
      NoBuild = true, //already built
      IncludeSymbols = true,
      MSBuildSettings = new DotNetMSBuildSettings
      {
         Version = packageversion
      }
   };
   DotNetPack("./src/Cake.grate/Cake.grate.csproj", settings);
});

Task("Push")
.Does(() => 
{
   var settings = new DotNetNuGetPushSettings
   {
      Source = "https://api.nuget.org/v3/index.json",
      ApiKey = EnvironmentVariable<string>("NugetKey", "")
   };
   var packageFilePath = GetFiles($"{BuildArtifacts}/Cake.grate*.nupkg").Single();
   DotNetNuGetPush(packageFilePath, settings);
});

Task("Build-And-Test")
   .IsDependentOn("Build")
   .IsDependentOn("Test");

Task("Deploy")
   .IsDependentOn("Build-And-Test")
   .IsDependentOn("Pack")
   .IsDependentOn("Push");


Task("Default")
.Does(() => 
{
   Information(@"The following tasks are available:

Build-And-Test:   Builds the Addin and runs the Unit Tests
Deploy:           Runs Build-And-Test and then packs and deploys the Nuget package");
});
   

RunTarget(target);
