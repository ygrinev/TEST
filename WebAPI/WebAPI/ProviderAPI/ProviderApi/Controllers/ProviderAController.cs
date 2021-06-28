using Microsoft.AspNetCore.Mvc;
using ProviderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProviderApi.Controllers
{
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

        // POST api/<ProviderAController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProviderAController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProviderAController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
