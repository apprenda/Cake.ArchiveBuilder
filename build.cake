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

Task("Prep-Output-Directories")
    .Does(() => {
      EnsureDirectoryExists(Parameters.StagingPath);
      CleanDirectory(Parameters.StagingPath);

      EnsureDirectoryExists(Parameters.OutputPath);
      CleanDirectory(Parameters.OutputPath);
    });

Task("Create-Apprenda-Archive")
    .IsDependentOn("Prep-Output-Directories")
    .Does(() => {
        var uiArchivePath = $"{Parameters.StagingPath}/interfaces/root";
        var wcfArchivePath = $"{Parameters.StagingPath}/services/{Parameters.WcfName}";
        var persistenceScriptPath = $"{Parameters.StagingPath}/persistence/scripts";
        var manifest = $"{Parameters.StagingPath}/DeploymentManifest.xml";

        EnsureDirectoryExists(uiArchivePath);
        EnsureDirectoryExists(wcfArchivePath);
        EnsureDirectoryExists(persistenceScriptPath);

        CopyDirectory(Parameters.UiPublishPath, uiArchivePath);
        CopyDirectory(Parameters.WcfBinariesPath, wcfArchivePath);

        DeleteFiles($"{wcfArchivePath}/*.pdb");
        DeleteFiles($"{uiArchivePath}/**/*.pdb");
        DeleteFiles($"{uiArchivePath}/DeploymentManifest.xml");

        CopyFile(Parameters.DataTierProvisioningScript, $"{persistenceScriptPath}/ApplicationProvisioning_Script.sql");
        CopyFile(Parameters.ApplicationManifest, manifest);

        Zip(Parameters.StagingPath, Parameters.ApprendaArchive);
    });

Task("Package")
    .IsDependentOn("Create-Apprenda-Archive")
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
