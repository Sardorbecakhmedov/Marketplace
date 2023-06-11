﻿using Marketplace.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Common.Helper;

public class FileManager : IFileManager
{
    private const string RootFolderName = "wwwroot";

    public async Task<string> SaveFileToWwwrootAsync(IFormFile logoFile, string folderName)
    {
        return await SaveFileAsync(logoFile, folderName);
    }

    private void CheckDirectory(string folderName)
    {
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(Path.Combine(RootFolderName, folderName));
        }
    }

    private async Task<string> SaveFileAsync(IFormFile logoFile, string folderName)
    {
        CheckDirectory(folderName);

        var newFileName = Guid.NewGuid() + Path.GetExtension(logoFile.FileName);

        using (var ms = new MemoryStream())
        {
            await logoFile.CopyToAsync(ms);
            await File.WriteAllBytesAsync(Path.Combine(RootFolderName, folderName, newFileName), ms.ToArray());
        }

        return $"/{folderName}/{newFileName}";
    }

}