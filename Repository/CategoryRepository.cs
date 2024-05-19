using System.Linq;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;

namespace EcommerceAPI.Repository
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        
        public CategoryRepository(DataContext context) : base (context)
        {
        }
        
        // get Active Categories
        public ICollection<Category> getActive()
        {
            return _context.Categories.Where(c => c.Status == true).ToList();
        }
    }
}