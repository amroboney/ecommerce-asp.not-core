using System;
namespace EcommerceAPI.Models
{
	public class BaseEntity
	{
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

