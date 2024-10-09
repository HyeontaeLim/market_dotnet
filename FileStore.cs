using market.Domain;

namespace market;

public class FileStore
{
    private readonly string fileDir = "/Users/hyeontaelim/RiderProjects/market/market/wwwroot/images";
    
    public string getFullPath(string fileName)
    {
        return Path.Combine(fileDir, fileName);
    }
    
    public string GetServedPath(string fileName)
    {
        return Path.Combine("/images", fileName);
    }
    
    public UploadFile StoreFile(IFormFile? formFile)
    {
        if (formFile == null)
        {
            return new UploadFile("no_image.jpg", "no_image.jpg");
        }

        string originalFileName = formFile.FileName;
        string storeFileName = CreateStoreFileName(originalFileName);
        formFile.CopyTo(new FileStream(getFullPath(storeFileName), FileMode.Create));
        return new UploadFile(originalFileName, storeFileName);
    }

    private string CreateStoreFileName(string originalFileName)
    {
        Guid uuidGuid = Guid.NewGuid();
        string uuid = uuidGuid.ToString();
        string ext = ExtractExt(originalFileName);
        return uuid + "." + ext;
    }

    private string ExtractExt(string originalFileName)
    {
        int pos = originalFileName.LastIndexOf(".");
        string ext = originalFileName.Substring(pos + 1);
        return ext;
    }
}