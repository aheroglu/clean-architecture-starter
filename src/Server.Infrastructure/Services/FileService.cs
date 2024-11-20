using Microsoft.AspNetCore.Http;
using Server.Application.Services;

namespace Server.Infrastructure.Services;

public sealed class FileService : IFileService
{
    public void FileDeleteFromServer(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string FileSaveToServer(IFormFile file, string filePath)
    {
        string text = Guid.NewGuid().ToString() + "-" + file.FileName;
        string path = filePath + text;

        using (FileStream target = File.Create(path))
        {
            file.CopyTo(target);
        }

        return text;
    }
}
