#tool "nuget:?package=Cake.FileHelpers"

#load "./build/parameters.cake"

var Parameters = BuildParameters.Load(Context, BuildSystem);

Setup(context => {
    Information($"Running build: {Parameters.Target} {Parameters.Configuration}");
});

Task("Restore-Nuget-Packages")
    .Does(() => {
        NuGetRestore(Parameters.SolutionFilePath);
    });

Task("Build-Solution")
    .IsDependentOn("Restore-Nuget-Packages")
    .Does(() => {
        MSBuild(Parameters.SolutionFilePath, new MSBuildSettings {
            Configuration = Parameters.Configuration
        }
        .WithProperty("DeployOnBuild", "true")
        .WithProperty("PublishProfile", "Staging"));
    });

Task("Build")
    .IsDependentOn("Build-Solution")
    .Does(() => {});

Task("Prep-Output-Directory")
    .Does(() => {
        EnsureDirectoryExists(Parameters.OutputPath);
        CleanDirectory(Parameters.OutputPath);
    });

Task("Create-Settings-Archive")
    .IsDependentOn("Prep-Output-Directory")
    .Does(() => {
        var uiArchivePath = $"{Parameters.ArchivePath}/interfaces/root";
        var wcfArchivePath = $"{Parameters.ArchivePath}/services/{Parameters.WcfName}";
        var persistenceScriptPath = $"{Parameters.ArchivePath}/persistence/scripts";
        var settingsManifest = $"{Parameters.ArchivePath}/DeploymentManifest.xml";

        EnsureDirectoryExists(Parameters.ArchivePath);
        CleanDirectory(Parameters.ArchivePath);

        EnsureDirectoryExists(uiArchivePath);
        EnsureDirectoryExists(wcfArchivePath);
        EnsureDirectoryExists(persistenceScriptPath);
        CopyDirectory(Parameters.UiPublishPath, uiArchivePath);
        CopyDirectory(Parameters.WcfBinariesPath, wcfArchivePath);
        DeleteFiles($"{wcfArchivePath}/*.pdb");
        CopyFile(Parameters.DataTierProvisioningScript, $"{persistenceScriptPath}/ApplicationProvisioning_Script.sql");
        CopyFile(Parameters.ApplicationManifest, settingsManifest);
        Zip(Parameters.ArchivePath, Parameters.ApprendaArchive);
    });

Task("Package")
    .IsDependentOn("Create-Settings-Archive")
    .Does(() => {});

Task("Prepare-Artifacts")
    .IsDependentOn("Build")
    .IsDependentOn("Package")
    .Does(()=> {});

Task("Default")
    .Does(() => {
        RunTarget("Prepare-Artifacts");
    });

RunTarget(Parameters.Target);
