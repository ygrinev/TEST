using Microsoft.AspNetCore.Mvc;
using ProviderApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProviderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderCController : ControllerBase
    {
        // GET: api/<ProviderCController>
        [HttpGet]
        [Route("quote")]
        [Produces("application/xml")]
        public ContentResult Get()
        {
            return SerializeWithoutNamespaces(new QuoteC { quote = "33" });
        }
        private ContentResult SerializeWithoutNamespaces(QuoteC instanseMyClass)
        {
            var sw = new StringWriter();
            var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true });

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var serializer = new XmlSerializer(instanseMyClass.GetType());
            serializer.Serialize(xmlWriter, instanseMyClass, ns);

            return Content(sw.ToString());
        }

        [HttpGet("{id}")]
        [Produces("application/xml")]
        public string Get(string id)
        {
            string idLowCase = (id ?? "").ToLower();
            return idLowCase == "test" ? "ProviderC working..." : idLowCase == "0" ? "ProviderAPI working!..." : "ProviderC working...\n(unknown parameter'" + idLowCase + "')";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
