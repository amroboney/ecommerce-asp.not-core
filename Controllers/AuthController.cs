using EcommerceAPI.Data.Dto;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController: Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
            
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,
                PhoneNumber = registerRequestDto.PhoneNumber,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Created Successfly");
                    }
                }
            }
            return BadRequest("Somthing error");
        }

        
        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // get roles from the user 
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        // create token 
                        var jwtToken = _tokenRepository.createJwtToken(user, roles.ToList());
                        var responseData = new
                        {
                            responseCode = 100,
                            ResponseMessage = "Success",
                            Data = new
                            {
                                Name = user.UserName,
                                Email = user.Email,
                                Token = jwtToken
                            }
                        };
                        return Ok(responseData);
                    }
                    //Create Token
                    return Ok("Success");
                }
            }
            return BadRequest("username or password incorrect");
        }
    }
}

