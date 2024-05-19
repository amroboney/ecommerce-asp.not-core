using System;
namespace EcommerceAPI.Services
{
	public static class ImageValidator
	{
        public static bool IsValidImage(IFormFile file)
        {
			var allowedExtensions = new string[] { ".jpg", "jpeg", "png" };

			if (!allowedExtensions.Contains(Path.GetExtension(file.FileName)))
			{
				return false;
			}
			if(file.Length > 10485760)
			{
				return false;
			}

			return true;
		}
	}
}

