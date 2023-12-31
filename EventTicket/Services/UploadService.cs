﻿using System.Net.Http.Headers;

namespace EventTicket.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _userContent;
        private const string USER_CONTENT_FOLDER = "user-content";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userContent = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER);
            _httpContextAccessor = httpContextAccessor;
            if (!Directory.Exists(_userContent))
            {
                Directory.CreateDirectory(_userContent);
            }
        }

        public string GetFullPath(string filename)
        {
            var req = _httpContextAccessor?.HttpContext?.Request;
            var path = $"{req?.Scheme}://{req?.Host}/{USER_CONTENT_FOLDER}/{filename}";
            return path;
        }

        public async Task DeleteFile(string filename)
        {
            string path = Path.Combine(_userContent, Path.GetFileName(filename));
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }
        }

        private async Task<string> ConfirmSave(Stream stream, string fileName)
        {
            string filePath = Path.Combine(_userContent, fileName);
            using (var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                await stream.CopyToAsync(fs);
            }
            return fileName;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

            return await ConfirmSave(file.OpenReadStream(), fileName);
        }
    }
}