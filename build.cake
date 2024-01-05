///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////


//Todo: Add test for DisableTokenReplacement

Task("Build")
.Does(() => 
{
    DotNetClean("./src/Cake.grate/Cake.grate.csproj");

    var settings = new DotNetBuildSettings
    {
        Configuration = configuration
    };

    DotNetBuild("./src/Cake.grate/Cake.grate.csproj", settings);
});

Task("Test")
.Does(() => 
{
   DotNetTest("./src/Cake.grate.Tests/Cake.grate.Tests.csproj");
});

Task("Build-And-Test")
   .IsDependentOn("Build")
   .IsDependentOn("Test");


Task("Default")
.Does(() => 
{
   Information(@"The following tasks are available:

Build-And-Test: Builds the Addin and runs the Unit Tests");
});
   

RunTarget(target);
