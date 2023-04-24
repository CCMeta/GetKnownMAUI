using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetKnownAPI.Models;
using GetKnownAPI.Services;

namespace GetKnownAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : DefaultController
    {
        private readonly UsersRepository _usersRepository;
        private readonly SessionService _sessionService;

        public TokenController(UsersRepository usersRepository, SessionService sessionService, IHttpContextAccessor context) : base(context)
        {
            _usersRepository = usersRepository;
            _sessionService = sessionService;
        }
        // GET: api/<TokenController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var users = await _usersRepository.ListAsync();
            return Ok(users);
        }

        // GET api/<TokenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TokenController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Users user)
        {
            var result = await _usersRepository.Get(user);
            if (result.Count() == 1)
            {
                var _user = result.First();
                if (_user.username is not null)
                {
                    var token = _sessionService.CreatToken(_user.username);
                    _sessionService.Sessions.Add(token, _user.id);
                    return Ok(new { token, _user.username, _user.avatar, _user.id, _user.nickname, _user.intro });
                }
            }
            return NotFound(user);
        }

        // PUT api/<TokenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TokenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
