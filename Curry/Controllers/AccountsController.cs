using Curry.Auth;
using Curry.Models;
using Curry.Persistence.Repository;
using Curry.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Curry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserService userService;
        private readonly UserRepository userRepository;
        private readonly ITokenFactory<JwtSecurityToken> tokenFactory;
        //IConfiguration config;
        public AccountsController(UserRepository userRepository, UserService userService, ITokenFactory<JwtSecurityToken> tokenFactory)
        {
            this.tokenFactory = tokenFactory;
            this.userRepository = userRepository;
            this.userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await userRepository.AddUserAsync(user);
            return Ok();
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await userRepository.GetUserByName(name);
            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var verified = await userService.Authenticate(user);
            if (verified != null)
            {
                var token = tokenFactory.GenerateToken(verified);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }
    }
}
