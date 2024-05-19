using System;
using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces
{
	public interface IFileRepository
	{
		public Tuple<int, string> SaveImage(IFormFile imageFile, string folderName);

		public bool DeleteImage(string imageFileName, string folderName);

		public string GetImage(string imageName, string folderName);

    }
}

