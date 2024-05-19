using System;
using EcommerceAPI.Enums;

namespace EcommerceAPI.Models
{
	public class Order
	{
		public Guid Id { get; set; }
		public string Serial { get; set; }
		public float Total { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime Date { get; set; }

		public Guid AddressId { get; set; }

		// Navigation
		public Address Address { get; set; }

        public ICollection<OrderItem> OrderItams { get; set; }
    }
}

