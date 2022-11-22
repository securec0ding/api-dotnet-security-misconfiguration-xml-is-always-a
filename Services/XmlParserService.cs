using Backend.Model;
using System.Collections.Generic;
using System.Xml;

namespace Backend.Services
{
    public class XmlParserService : IXmlParserService
    {
        public EmailModel[] ParseEmails(string xml)
        {
            var result = new List<EmailModel>();
            var xmlDocument = new XmlDocument();
            xmlDocument.XmlResolver = new XmlUrlResolver();
            xmlDocument.LoadXml(xml);
            var emailNodes = xmlDocument.GetElementsByTagName("email");
            foreach (XmlElement n in emailNodes)
            {
                var email = new EmailModel
                {
                    Subject = n.GetElementsByTagName("subject")[0].InnerText,
                    Sender = n.GetElementsByTagName("sender")[0].InnerText,
                    Timestamp = n.GetElementsByTagName("timestamp")[0].InnerText,
                    Body = n.GetElementsByTagName("body")[0].InnerText,
                };
                result.Add(email);
            }
            return result.ToArray();
        }
    }
}