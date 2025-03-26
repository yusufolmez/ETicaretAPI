using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Servıces;
using ETicaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string extension = Path.GetExtension(fileName);
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            string baseFileName = first
                ? NameOperation.CharacterRegulatory(nameWithoutExtension)
                : nameWithoutExtension;

            string newFileName = $"{baseFileName}{extension}";
            string fullPath = Path.Combine(path, newFileName);

            if (!File.Exists(fullPath))
                return newFileName;

            string searchPattern = $"{baseFileName}*{extension}";
            var files = Directory.GetFiles(path, searchPattern);

            int maxNumber = 1;
            foreach (var file in files)
            {
                string currentFileName = Path.GetFileNameWithoutExtension(file);

                if (currentFileName.Equals(baseFileName, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (currentFileName.StartsWith($"{baseFileName}-", StringComparison.OrdinalIgnoreCase))
                {
                    string suffix = currentFileName.Substring(baseFileName.Length + 1);
                    if (int.TryParse(suffix, out int number))
                    {
                        if (number > maxNumber)
                            maxNumber = number;
                    }
                }
            }

            // Yeni dosya ismi, bulunan en yüksek numaradan 1 fazlası olacak
            int counter = maxNumber + 1;
            newFileName = $"{baseFileName}-{counter}{extension}";
            while (File.Exists(Path.Combine(path, newFileName)))
            {
                counter++;
                newFileName = $"{baseFileName}-{counter}{extension}";
            }

            return newFileName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
            {
                return datas;
            }
            return null;
        }
    }
}
