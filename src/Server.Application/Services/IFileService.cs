using Microsoft.AspNetCore.Http;

namespace Server.Application.Services;

public interface IFileService
{
    string FileSaveToServer(IFormFile file, string filePath);
    void FileDeleteFromServer(string path);
}