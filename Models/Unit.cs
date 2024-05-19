using System;
namespace EcommerceAPI.Models
{
	public class Unit
	{
		public Guid Id { get; set; }
		public String Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

