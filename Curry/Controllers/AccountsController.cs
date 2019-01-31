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
        private readonly UserService _userService;
        private readonly ITokenFactory<JwtSecurityToken> _tokenFactory;
        //IConfiguration config;
        public AccountsController(UserService service, ITokenFactory<JwtSecurityToken> tokenFactory)
        {
            _userService = service;
            _tokenFactory = tokenFactory;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _userService.AddUserAsync(user);
            return Ok();
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await _userService.FindUserByName(name);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var verified = await _userService.Authenticate(user);
            if (verified == null) return BadRequest();
            var token = _tokenFactory.GenerateToken(verified);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
