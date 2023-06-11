using Microsoft.AspNetCore.Http;

namespace Marketplace.Common.Interfaces;

public interface IFileManager
{
    Task<string> SaveFileToWwwrootAsync(IFormFile logoFile, string folderName);
}