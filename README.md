---
page_type: sample
languages:
- csharp
products:
- azure
- azure-storage
- dotnet-core
description: "A simple sample project to help you get started using Azure Storage with .NET Core and C# as the development language."
---

## Azure SDK Versions
To use the latest Azure SDK version [storage-blobs-dotnet-quickstart-v3](./storage-blobs-dotnet-quickstart-V3), please add the following dependency:
- [Microsoft.Azure.Storage.Blob](https://www.nuget.org/packages/Microsoft.Azure.Storage.Blob/) 

For the previous Azure SDK version [storage-blobs-dotnet-quickstart-v12](./storage-blobs-dotnet-quickstart-V12), please add the following dependency: 
- [Azure.Storage.Blobs](https://www.nuget.org/packages/Azure.Storage.Blobs/)


## Prerequisites

To complete this tutorial:

* Install .NET core 2.1 for [Linux](https://dotnet.microsoft.com/download/dotnet-core/2.1) or [Windows](https://dotnet.microsoft.com/download/dotnet-core/2.1)

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Create a Storage Account using the Azure Portal

Step 1 : Create a new general-purpose Storage Account to use for this tutorial. 
 
*  Go to the [Azure Portal](https://portal.azure.com) and log in using your Azure account. 
*  Select **New** > **Storage** > **Storage account**. 
*  Select your Subscription. 
*  For `Resource group`, create a new one and give it a unique name. 
*  Enter a name for your storage Account.
*  Select the `Location` to use for your Storage Account.
*  Set `Account kind` to **StorageV2(general purpose v2)**.
*  Set `Performance` to **Standard**. 
*  Set `Replication` to **Locally-redundant storage (LRS)**.
*  Set `Secure transfer required` to **Disabled**.
*  Check **Review + create** and click **Create** to create your Storage Account. 
 
Step 2 : Copy and save Connection string.

After your Storage Account is created. Click on it to open it. 
Select **Settings** > **Access keys** > **Key1/key**, copy the associated **Connection string** to the clipboard, then paste it into a text editor for later use.

## Put the connection string in an environment variable

This solution requires a connection string be stored in an environment variable securely on the machine running the sample. Follow one of the examples below depending on your operating system to create the environment variable. If using Windows close your open IDE or shell and restart it to be able to read the environment variable.

### Linux

```bash
export AZURE_STORAGE_CONNECTIONSTRING="<YourConnectionString>"
```

### Windows

```cmd
setx AZURE_STORAGE_CONNECTIONSTRING "<YourConnectionString>"
```

At this point, you can run this application. It creates its own file to upload and download, and then cleans up after itself by deleting everything at the end.

## Run the application
First, clone the repository on your machine:

```bash
git clone https://github.com/Azure-Samples/storage-blobs-dotnet-quickstart.git
```

Then, switch to the appropriate folder:

Navigate to your directory where the project file (.csproj) resides and run the application with the `dotnet run` command.

```console
dotnet run
```

## This Quickstart shows how to do following operations of Storage Blobs.
- Create a Storage Account using the Azure Portal.
- Create a container.
- Upload a file to block blob.
- List blobs.
- Download a blob to file.
- Delete a blob.
- Delete the container.

## More information

The [Azure Storage documentation](https://docs.microsoft.com/azure/storage/) includes a rich set of tutorials and conceptual articles, which serve as a good complement to the samples.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
