﻿//------------------------------------------------------------------------------
//MIT License

//Copyright(c) 2017 Microsoft Corporation. All rights reserved.

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Storage.Blob.Dotnet.Quickstart.V12
{
    /// <summary>
    /// Azure Storage QuickStart Sample - Demonstrate how to upload, list, download, and delete blobs. 
    ///
    /// Note: This sample uses the .NET asynchronous programming model to demonstrate how to call Blob storage using the 
    /// azure storage client library's asynchronous API's. When used in production applications, this approach enables you to improve the 
    /// responsiveness of your application.  
    /// 
    /// </summary>

    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Azure Blob Storage - .NET QuickStart sample");
            Console.WriteLine();
            await OperateBlobAsync();

            Console.WriteLine("Press any key to exit the sample application.");
            Console.ReadLine();
        }

        private static async Task OperateBlobAsync()
        {
            string tempDirectory = null;
            string destinationPath = null;
            string sourcePath = null;
            BlobContainerClient blobContainerClient = null;

            // Retrieve the connection string for use with the application. The storage connection string is stored
            // in an environment variable on the machine running the application called AZURE_STORAGE_CONNECTIONSTRING.
            // If the environment variable is created after the application is launched in a console or with Visual
            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.
            string storageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTIONSTRING");

            if (storageConnectionString == null)
            {
                Console.WriteLine("A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'AZURE_STORAGE_CONNECTIONSTRING' with your storage " +
                    "connection string as a value.");

                return;
            }

            try
            {
                // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
                string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
                blobContainerClient = new BlobContainerClient(storageConnectionString, containerName);
                await blobContainerClient.CreateAsync();
                Console.WriteLine("Created container '{0}'.", blobContainerClient.Uri);
                Console.WriteLine();

                // Set the permissions so the blobs are public. 
                await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);
                Console.WriteLine("Setting the Blob access policy to public.");
                Console.WriteLine();

                // Create a file in a temp directory folder to upload to a blob.
                tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempDirectory);
                string blobFileName = "QuickStart" + Path.GetRandomFileName() + ".txt";
                sourcePath = Path.Combine(tempDirectory, blobFileName);

                // Write text to this file.
                File.WriteAllText(sourcePath, "Storage Blob Quickstart.");
                Console.WriteLine("Created Temp file = {0}.", sourcePath);
                Console.WriteLine();

                // Get a reference to the blob named "sample-blob", then upload the file to the blob.
                Console.WriteLine("Uploading file to Blob storage as blob '{0}'.", blobFileName);
                string blobName = "sample-blob";
                BlobClient blob = blobContainerClient.GetBlobClient(blobName);
             
                // Open this file and upload it to blob
                using (FileStream fileStream = File.OpenRead(sourcePath))
                {
                    await blob.UploadAsync(fileStream);
                }

                Console.WriteLine("Uploaded successfully.");
                Console.WriteLine();

                // List the blobs in the container.
                Console.WriteLine("Listing blobs in container.");

                await foreach (BlobItem item in blobContainerClient.GetBlobsAsync())
                {
                    Console.WriteLine("The blob name is '{0}'.",item.Name);
                }

                Console.WriteLine("Listed successfully.");
                Console.WriteLine();

                // Append the string "_DOWNLOADED" before the .txt extension so that you can see both files in the temp directory.
                destinationPath = sourcePath.Replace(".txt", "_DOWNLOADED.txt");

                // Download the blob to a file in same directory, using the reference created earlier. 
                Console.WriteLine("Downloading blob to file in the temp directory {0}.", destinationPath);
                BlobDownloadInfo blobDownload = await blob.DownloadAsync();

                using (FileStream fileStream = File.OpenWrite(destinationPath))
                {
                    await blobDownload.Content.CopyToAsync(fileStream);
                }

                Console.WriteLine("Downloaded successfully.");
                Console.WriteLine();
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine("Error returned from the service: {0}.", ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to delete the sample files and example container.");
                Console.ReadLine();

                // Clean up resources. This includes the container and the two temp files.
                Console.WriteLine("Deleting the container and any blobs it contains.");
                if (blobContainerClient != null)
                {
                    await blobContainerClient.DeleteAsync();
                }

                Console.WriteLine("Deleting the local source file and local downloaded files.");
                Console.WriteLine();
                File.Delete(sourcePath);
                File.Delete(destinationPath);
                Directory.Delete(tempDirectory);
            }
        }
    }
}

