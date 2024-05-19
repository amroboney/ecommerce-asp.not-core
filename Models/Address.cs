using System;
namespace EcommerceAPI.Models
{
	public class Address
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }


		public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
		public Address()
		{
			CreatedOn = DateTime.UtcNow;
			UpdatedOn = DateTime.UtcNow;
		}
    }
}

