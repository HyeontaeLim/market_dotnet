namespace market.Domain;

public class UploadFile
{
    public string UploadFileName { get; set; }
    public string StoreFileName { get; set; }

    public UploadFile(string uploadFileName, string storeFileName)
    {
        UploadFileName = uploadFileName;
        StoreFileName = storeFileName;
    }
}