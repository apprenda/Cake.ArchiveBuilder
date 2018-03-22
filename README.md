# Cake.ArchiveBuilder
Extends Cake to create Apprenda Application Packages (also known as Apprenda Archives) from a Visual Studio solution for use with the Apprenda Cloud Platform. For those not familiar with Cake, Cake (C# Make) is a cross-platform build automation system with a C# DSL for tasks such as compiling code, copying files and folders, running unit tests, compressing files and building NuGet packages. You can learn more at https://cakebuild.net/

This is an alternative method for creating Apprenda Archives without needing to use ACS.exe (Apprenda Cloud Shell) or the Visual Studio Extension for Apprenda.

# Folder and File Structure
This project consists of the following key items
- Build: Contains the parameters for Cake
- src: Contains the source code for the Apprenda Application. TimeCard from http://docs.apprenda.com/downloads is used as an example
- build.cake: Contains the logic for building the solution, copying the files, and packaging everything into the Apprenda archive
- build.ps1: The powershell wrapper script to invoke Cake

# Apprenda Application Structure
This is a 3 tier application, with an ASP.NET user interface, a WCF service, and a Data Tier component that will be deployed on SQL Server
- root contains the ASP.NET user interface
- TimeCard.Service is the WCF service (depends on TimeCard.Data)
- ApplicationProvisioning_Script.sql contains the SQL Data Tier definition (tables, relationships, initial data, etc)
- DeploymentManifest.xml contains the Apprenda-specific configuration settings for this application

# What to modify
This example was meant as a template so that you can reuse this method with any application you want to upload in Apprenda.
- Replace this TimeCard application with the source code for the application of your choice. The source code needs to go into the \src\ directory
- Make sure that any ASP.NET user interface components define the config file Properties\PublishProfiles\Staging.pubxml
- Make the necessary changes to parameters.cake for the application you are building. For example look for the "// Start - Configurable data" section
- Make any process changes for building, copying files, and packaging the archive in the build.cake file

# How to Build and Create Apprenda Archive
- Clone this repository
- Make any necessary modification to the Cake files
- Make any necessary modifications to the source code for the application you intend to package
- Simply execute .\build.ps1 from a PowerShell window and watch Cake do its magic
- The binaries for your application will go to folder {applicationName}_Binaries
- You will find Apprenda application package (.zip file) in the folder {applicationName}__ApprendaArchive. You can deploy this zip file to the Apprenda Cloud Platform directly using the Developer Portal, or can use this file as part of a larger CI/CD system that automatically deploys applications to Apprenda

# Debugging
If you would like to debug your Cake process, you can follow the steps outlined in https://cakebuild.net/blog/2016/05/debug-cake-file. The build.ps1 PowerShell file supports a new parameter called DebugCake that will trigger the debugging process. Invoke as `.\build.ps1 -DebugCake`
