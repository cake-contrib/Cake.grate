# ensure a most-recent debug-build, so we can reference that.
dotnet build ../../src/Cake.grate/Cake.grate.csproj 

dotnet run --project ./build/Build.csproj -- "$@"
