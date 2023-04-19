using Auth.Microservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration { get; }

        public AuthController(IConfiguration iConfiguration)
        {
            _configuration = iConfiguration;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public string Login(LoginReqDTO request)
        {
            if (request is LoginReqDTO { UserName: "john.doe", Password: "123456" })
            {
                var token = BuildToken(_configuration.GetSection("Jwt:Key").Value ?? string.Empty,
                                                    _configuration.GetSection("Jwt:Issuer").Value ?? string.Empty,
                                                    new[] {
                                                        _configuration.GetSection("Jwt:Aud1").Value ?? string.Empty,
                                                        _configuration.GetSection("Jwt:Aud2").Value ?? string.Empty
                                                    },
                                                    request.UserName);
                return token;
            }
            return string.Empty;
        }

        private string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            };

            claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(30), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
