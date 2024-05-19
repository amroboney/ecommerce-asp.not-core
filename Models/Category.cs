using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Image { get; set; }
		public bool Status { get; set; }

		
		public IFormFile ImageFile { get; set; }

		public ICollection<Product> Products { get; set; }

		
    }
}

