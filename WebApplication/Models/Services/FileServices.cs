﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication.Models.Services
{
    public class FileServices
    {
        public List<string> ReturnFilePaths(string directoryPath)
        {
            List<string> urls = new();

            DirectoryInfo main = new(directoryPath);
            if (main.Exists)
            {
                // add path children
                foreach (FileInfo fileInfo in main.EnumerateFiles())
                {
                    string url = $"{directoryPath}/{fileInfo.Name}";
                    urls.Add(url.Replace("wwwroot/", string.Empty));
                }

                // search child directories
                foreach (DirectoryInfo directoryInfo in main.EnumerateDirectories())
                    urls.AddRange(ReturnFilePaths($"{directoryPath}/{directoryInfo.Name}"));
            }

            return urls;
        }

        public async Task SaveFileToDirectoryAsync(IFormFile file, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string path = Path.Combine(directoryPath, file.FileName);
            using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
