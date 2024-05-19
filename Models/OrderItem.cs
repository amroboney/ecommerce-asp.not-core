using System;
namespace EcommerceAPI.Models
{
	public class OrderItem
	{
		public Guid Id { get; set; }
		public int Count { get; set; }
		public float Price { get; set; }
		public Guid Orderid { get; set; }
		public Guid Productid { get; set; }

		public Order Order { get; set; }
		public Product product { get; set; }
	}
}

