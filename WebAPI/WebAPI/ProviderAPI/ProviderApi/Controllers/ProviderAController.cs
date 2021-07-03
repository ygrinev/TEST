using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProviderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProviderApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderAController : ControllerBase
    {
        // GET: api/<ProviderAController>
        [HttpGet]
        [Route("quote")]
        public QuoteA Get()
        {
            return new QuoteA { total = 55 };
        }

        // GET api/<ProviderAController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return $"ProviderA working...{((id??"").ToLower() == "test" ? "" : "\n(unknown parameter'" + (id??"") + "')")}";
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var userInfo = await _userService.Authenticate(user.Username, user.Password);

            if (userInfo == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(userInfo);
        }


        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
