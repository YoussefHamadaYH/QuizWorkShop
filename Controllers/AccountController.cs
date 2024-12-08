using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizWorkShop.DTOs;
using QuizWorkShop.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(RegisterUserDTO user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = user.userName,
                    Email = user.email
                };
                IdentityResult result = await _userManager.CreateAsync(applicationUser, user.password);
                if (result.Succeeded)
                {
                    return Ok("Succeeded Regestration");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser? User = await _userManager.FindByNameAsync(login.userName);
                if (User != null) 
                { 
                    if(await _userManager.CheckPasswordAsync(User , login.Password))
                    {
                        #region Clamis
                        List<Claim> userData = new List<Claim>();
                        userData.Add(new Claim(ClaimTypes.Name, User.UserName));      // User Name
                        userData.Add(new Claim(ClaimTypes.Email, User.Email));       // Email
                        userData.Add(new Claim(ClaimTypes.NameIdentifier, User.Id)); //id
                        userData.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //jwtid
                        userData.Add(new Claim(ClaimTypes.Role, "Admin"));
                        userData.Add(new Claim(ClaimTypes.Role, "User"));
                        #endregion

                        #region secritKey
                        string key = "Welcom to my secrit key in Qiz0123456789abcde";
                        var secritKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                        var signingCredentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256);
                        #endregion

                        #region Generte Token
                        var token = new JwtSecurityToken(
                            //PayLoad
                            claims: userData, expires: DateTime.Now.AddDays(1),
                            //Signature => SecurityKey + HahAlgorithm
                            signingCredentials: signingCredentials
                            );
                        //TokenObj Encoding to String
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(tokenString);
                        #endregion
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name is Invalid");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
