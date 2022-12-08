using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services.Helpers
{
    public static class FileService
    {
        private const string BasePath = "https://api.izmirbsbspor.org/";

        /// <summary>
        /// Tekil dosya yükleme işlemidir. Geriye dosya yolunu döner. wwwroot klasörü base klasördür tanımlamanıza gerek yoktur. 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<string> FileUpload(IFormFile form, string folder = "images/", CancellationToken cancellationToken = default)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(form.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{folder}", fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await form.CopyToAsync(stream, cancellationToken);
            return $"{BasePath}{folder}{fileName}";
        }

        /// <summary>
        /// Çoklu dosya yükleme işlemidir. Geriye dosyaların kayıt yollarını liste olarak döner. wwwroot klasörü base klasördür tanımlamanıza gerek yoktur.
        /// </summary>
        /// <param name="forms"></param>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> MultiFileUpload(IEnumerable<IFormFile> forms, string folder = "images/", CancellationToken cancellationToken = default)
        {
            List<string> paths = new();
            foreach (var form in forms)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(form.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{folder}", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await form.CopyToAsync(stream, cancellationToken);
                paths.Add($"{BasePath}{folder}{fileName}");
            }
            return paths;
        }

        /// <summary>
        /// Verilen path adresinde bulunan dosyayı siler.
        /// </summary>
        /// <param name="path"></param>
        public static void RemoveFile(string path)
        {
            var isExistedFile = Path.Combine(path);
            if (File.Exists(isExistedFile))
            {
                File.Delete(isExistedFile);
            }
        }
    }
}
