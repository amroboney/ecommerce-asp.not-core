using System;
using System.Diagnostics.CodeAnalysis;

namespace EcommerceAPI.Data.Dto
{
	public class CreateCategoryDto
	{
        public string Name { get; set; }

        public string? Image { get; set; }

        public IFormFile ImageFile { get; set; }
        
    }
}

