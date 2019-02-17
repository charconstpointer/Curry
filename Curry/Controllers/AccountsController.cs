using Curry.Auth;
using Curry.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Curry.Mappings;
using Curry.Models.User;

namespace Curry.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenFactory<JwtSecurityToken> _tokenFactory;

        private readonly IMapper _mapper;
        //IConfiguration config;
        public AccountsController(IUserService service, ITokenFactory<JwtSecurityToken> tokenFactory, IMapper mapper)
        {
            _userService = service;
            _tokenFactory = tokenFactory;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]UserBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(user);
            }

            var usa = _mapper.Map<UserBindingModel, User>(user);
            await _userService.AddUserAsync(usa);
            return Ok();
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userService.FindUserByName(name);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.FindUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var usa = _mapper.Map<User, UserResourceModel>(user);
            return Ok(usa);
        }
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var verified = await _userService.Authenticate(user);
            if (verified == null) return Unauthorized();
            var token = _tokenFactory.GenerateToken(verified);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
