public class BuildParameters
{
    public string Target { get; set; }
    public string Configuration { get; set; }

    public string WcfName { get; set; }
    public DirectoryPath OutputPath { get; set; }
    public DirectoryPath StagingPath { get; set; }
    public DirectoryPath UiPublishPath { get; set; }
    public DirectoryPath WcfBinariesPath { get; set; }
    public FilePath SolutionFilePath { get; set; }
    public FilePath DataTierProvisioningScriptFilePath { get; set; }
    public FilePath ApplicationManifestFilePath { get; set; }
    public FilePath ApprendaArchiveFilePath { get; set; }

    public static BuildParameters Load(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var target = context.Argument("Target", "Default");
        var configuration = context.Argument("Configuration", "Release");
        //Alternative way: var configuration = context.Arguments.GetArgument("Configuration") ?? "Release";

        // Start - Configurable data
        var applicationName = "TimeCard";
        var uiName = "root";
        var wcfName = "TimeCard.Service";
        DirectoryPath sourcePath = "./src";
        FilePath manifestPath = "root/DeploymentManifest.xml";
        var provisioningScriptPath = "ApplicationProvisioning_Script.sql";
        DirectoryPath outputPath = $"./{applicationName}_ApprendaArchive";
        DirectoryPath stagingPath = $"./{applicationName}_Binaries";
        // End - Configurable data

        return new BuildParameters
        {
            Target = target,
            Configuration = configuration,
            OutputPath = outputPath,
            SolutionFilePath = sourcePath.CombineWithFilePath($"{applicationName}.sln"),
            StagingPath = stagingPath,
            WcfName = wcfName,
            UiPublishPath = sourcePath.Combine(uiName).Combine("ReleaseBuild"),
            WcfBinariesPath = sourcePath.Combine(wcfName).Combine("bin").Combine($"{configuration}"),
            DataTierProvisioningScriptFilePath = sourcePath.CombineWithFilePath($"{provisioningScriptPath}"),
            ApplicationManifestFilePath = sourcePath.CombineWithFilePath(manifestPath),
            ApprendaArchiveFilePath = outputPath.CombineWithFilePath($"{applicationName}.zip"),
        };
    }
}
