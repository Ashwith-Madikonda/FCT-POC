using FCT_POC_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCT_POC_API.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            bool isValidUser = false;

            if (user != null)
            {
                if (user.Email != null && user.Email == "admin" && user.Password != null && user.Password == "admin")
                {
                    isValidUser = true;
                }
            }



            if (isValidUser)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this_is_a_dummy_secret_key_for_the_poc");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, user.Email)
                        // Add more claims as needed
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = "https://localhost:44347",
                    Audience = "https://localhost:44347"
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                Response.Headers.Append("Authorization", "Bearer " + tokenString);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
