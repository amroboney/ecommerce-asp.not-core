using System;
namespace EcommerceAPI.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public string Image { get; set; }
		public bool Status { get; set; }
		public bool Special { get; set; }

		public IFormFile ImageFile { get; set; }
		public Guid CategoryId { get; set; }
		public Guid BrandId { get; set; }
		public Guid UnitId { get; set; }

		public Category Category { get; set; }
		public Brand Brand { get; set; }
		public Unit Unit { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }
	}
}

