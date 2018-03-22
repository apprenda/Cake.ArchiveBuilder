#tool "nuget:?package=Cake.FileHelpers"

#load "./build/parameters.cake"

var Parameters = BuildParameters.Load(Context);

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
    .IsDependentOn("Build-Solution");

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
        var uiArchivePath = Parameters.StagingPath.Combine("interfaces/root");
        var wcfArchivePath = Parameters.StagingPath.Combine($"services/{Parameters.WcfName}");
        var persistenceScriptPath = Parameters.StagingPath.Combine("persistence/scripts");
        var manifestFilePath = Parameters.StagingPath.CombineWithFilePath("DeploymentManifest.xml");
        var persistenceScriptFilePath = persistenceScriptPath.CombineWithFilePath("ApplicationProvisioning_Script.sql");

        EnsureDirectoryExists(uiArchivePath);
        EnsureDirectoryExists(wcfArchivePath);
        EnsureDirectoryExists(persistenceScriptPath);

        CopyDirectory(Parameters.UiPublishPath, uiArchivePath);
        CopyDirectory(Parameters.WcfBinariesPath, wcfArchivePath);

        DeleteFiles($"{wcfArchivePath}/*.pdb");
        DeleteFiles($"{uiArchivePath}/**/*.pdb");
        DeleteFiles($"{uiArchivePath}/DeploymentManifest.xml");

        CopyFile(Parameters.DataTierProvisioningScriptFilePath, persistenceScriptFilePath);
        CopyFile(Parameters.ApplicationManifestFilePath, manifestFilePath);

        Zip(Parameters.StagingPath, Parameters.ApprendaArchiveFilePath);
    });

Task("Package")
    .IsDependentOn("Create-Apprenda-Archive");

Task("Prepare-Artifacts")
    .IsDependentOn("Build")
    .IsDependentOn("Package");

Task("Default")
    .IsDependentOn("Prepare-Artifacts");

RunTarget(Parameters.Target);
