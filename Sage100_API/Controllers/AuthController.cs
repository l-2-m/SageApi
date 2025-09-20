using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sage100_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sage100_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiOptions _apioptions;

        public AuthController(IOptions<ApiOptions> options)
        {
            _apioptions = options.Value;
        }

        [HttpPost("login")]
        public IActionResult GetToken(string apiKey)
        {
            if (apiKey.IsNullOrEmpty())
            {
                return BadRequest("Invalid client request");
            }
            if (apiKey == _apioptions.APIKey)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apioptions.Secret));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else{ return Unauthorized(); }
        }
    }
}
