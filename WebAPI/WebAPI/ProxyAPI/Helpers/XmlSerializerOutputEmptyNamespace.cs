using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProxyAPI.Helpers
{
    public class XmlSerializerOutputEmptyNamespace : XmlSerializerOutputFormatter
    {
        protected override void Serialize(XmlSerializer xmlSerializer, XmlWriter xmlWriter, object value)
        {
            //applying "empty" namespace will produce no namespaces
            var emptyNamespaces = new XmlSerializerNamespaces();
            emptyNamespaces.Add("", "");
            xmlSerializer.Serialize(xmlWriter, value, emptyNamespaces);
        }
    }
}
