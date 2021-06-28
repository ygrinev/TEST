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
    public class ProviderBController : ControllerBase
    {
        // GET: api/<ProviderBController>
        [HttpGet]
        [Route("quote")]
        public QuoteB Get()
        {
            return new QuoteB { amount = 44 };
        }

        // GET api/<ProviderBController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return $"ProviderB working...{((id ?? "").ToLower() == "test" ? "" : "\n(unknown parameter'" + (id ?? "") + "')")}";
        }

        // POST api/<ProviderBController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProviderBController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProviderBController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
