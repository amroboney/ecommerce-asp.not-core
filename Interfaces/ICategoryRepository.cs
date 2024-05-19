using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        

        ICollection<Category> getActive();

    }
}