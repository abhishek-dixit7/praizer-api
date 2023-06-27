namespace praizer_api.Utils;
using Azure.Storage.Blobs;

public class UploadToBlob
{
    public async Task<string?> UploadFile(IFormFile file, string fileName)
    {
        if (file == null || file.Length == 0)
            return null;
        
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        //Setting up the _configurations to get values from appsettings
        var _configurations = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        
        //setting up the blobserviceClient from Azure.Storage.Blobs
        var _blobServiceClient = new BlobServiceClient(_configurations.GetValue<string>("AzureBlobStorage:ConnectionString"));
        
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_configurations.GetValue<string>("AzureBlobStorage:ContainerName"));
        var blobClient = blobContainerClient.GetBlobClient(fileName);
        
        await blobClient.UploadAsync(memoryStream, true);

        var imageUrl = blobContainerClient.Uri + "/" + blobClient.Name;
        

        return imageUrl;
    }
}