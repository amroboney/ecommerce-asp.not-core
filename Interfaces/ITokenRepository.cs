using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Interfaces
{
    public interface ITokenRepository
    {
        string createJwtToken(IdentityUser user, List<string> roles);
    }   
}