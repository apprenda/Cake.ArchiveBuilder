public class BuildParameters
{
    public string Target { get; set; }
    public string Configuration { get; set; }

    public string OutputPath { get; set; }

    public string SolutionFilePath { get; set; }

    public string ArchivePath { get; set; }
    public string WcfName { get; set; }
    public string UiPublishPath { get; set; }
    public string WcfBinariesPath { get; set; }
    public string DataTierProvisioningScript { get; set; }
    public string ApplicationManifest { get; set; }
    public string ApprendaArchive { get; set; }

    public static BuildParameters Load(ICakeContext context, BuildSystem buildSystem)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (buildSystem == null)
        {
            throw new ArgumentNullException(nameof(buildSystem));
        }

        var target = context.Arguments.GetArgument("Target") ?? "Default";
        var configuration = context.Arguments.GetArgument("Configuration") ?? "Release";

        // Start - Configurable data
        var applicationName = "TimeCard";
        var uiName = "root";
        var wcfName = "TimeCard.Service";
        var sourcePath = "./src";
        // End - Configurable data

        var outputPath = $"./{applicationName}_ApprendaArchive";
        var stagingPath = $"./{applicationName}_Binaries";

        return new BuildParameters
        {
            Target = target,
            Configuration = configuration,
            OutputPath = outputPath,
            SolutionFilePath = $"{sourcePath}/{applicationName}.sln",
            ArchivePath = $"{stagingPath}",
            WcfName = wcfName,
            UiPublishPath = $"{sourcePath}/{uiName}/ReleaseBuild",
            WcfBinariesPath = $"{sourcePath}/{wcfName}/bin/{configuration}",
            DataTierProvisioningScript = $"{sourcePath}/ApplicationProvisioning_Script.sql",
            ApplicationManifest = $"{sourcePath}/root/DeploymentManifest.xml",
            ApprendaArchive = $"{outputPath}/{applicationName}.zip",
        };
    }
}
