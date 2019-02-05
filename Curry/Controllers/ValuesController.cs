using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curry.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curry.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly int _currentUser;
        public ValuesController(IHttpContextAccessor httpContextAccessor)
        {
            _currentUser = httpContextAccessor.CurrentUser();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", "value2", _currentUser.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
