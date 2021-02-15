using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorizationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get authorize token
        /// </summary>
        /// <param name="userRoles">list user role from angular in adweb</param>
        /// <returns></returns>
        [HttpPost("token")]
        public IActionResult GetToken([FromBody]List<string> userRoles)
        {
            // Set role
            //List<string> userRoles = new List<string> { "Hanoi_NBB_COO_ADMIN" };
            try
            {
                // Current user
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "User"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                // Add list role -> current user
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Generate token
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
    public static class UserRoles
    {
        public const string Admin = "Hanoi_NBB_COO_ADMIN";
        public const string User = "Hanoi_NBB_Pizza_USER";
    }
}