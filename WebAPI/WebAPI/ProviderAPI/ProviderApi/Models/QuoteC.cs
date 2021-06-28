using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProviderApi.Models
{
    [XmlRoot("xml")]
    public class QuoteC
    {
        [XmlElement("quote")]
        public string quote { get; set; }
        public QuoteC() { }
    }
}
