using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Data.Dto
{
    public class ProductRequestDto
    {
        
        public string Name { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid UnitId { get; set; }
    }
}