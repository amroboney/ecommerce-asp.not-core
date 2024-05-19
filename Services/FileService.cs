using System;
using Azure.Core;
using EcommerceAPI.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace EcommerceAPI.Services
{
	public class FileService: IFileRepository
    {
        private IWebHostEnvironment environment;
        private IConfiguration configuration;
		public FileService(IWebHostEnvironment env, IConfiguration configuration)
		{
            this.environment = env;
            this.configuration = configuration;
		}

        public bool DeleteImage(string imageFileName, string folderName)
        {
            try
            {
                var path = environment.WebRootPath;
                var fullPath = Path.Combine(path, "Uploads", imageFileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile, string folderName)
        {
            try
            {
                var contentPath = environment.ContentRootPath;

                var path = Path.Combine(contentPath, $"Uploads/{folderName}");
                 
                // Create folder if not exists
                if (!Directory.Exists(path))                
                    Directory.CreateDirectory(path);

                var ext = Path.GetExtension(imageFile.FileName);
                var allowsExtensioin = new string[] { ".jpg", ".jpeg", ".png" };
                if (!allowsExtensioin.Contains(ext))
                {
                    string message = string.Format("Only {0} extensions are allowed");
                    return new Tuple<int, string>(0, message);
                }

                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;

                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch(Exception e)
            {
                return new Tuple<int, string>(0, "ٍError Happend");
            }
        }

        // this function to get full image path 
        public string GetImage(string imageName, string folderName)
        {
            string filePath = Path.Combine(configuration["BaseUrl"],
            $"{configuration["RequestFilePathCallInFunction"]}/{folderName}",
            imageName);

            //return $"{filePath}";
            if ( System.IO.File.Exists(Path.Combine(environment.ContentRootPath,$"Uploads/{folderName}",imageName)))
                return filePath;

            return null;
        }
    }
}

