using Backend.Model;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Backend.Controllers
{

    [ApiController]
    [Route("/api")]
    public class XmlController : ControllerBase
    {
        private readonly IXmlParserService xmlParserService;

        public XmlController(IXmlParserService xmlParserService)
        {
            this.xmlParserService = xmlParserService;
        }

        [HttpPost]
        [Route("xml")]
        public async Task<IActionResult> ProcessXml()
        {
            try
            {
                using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
                var xml = await reader.ReadToEndAsync();
                var emails = this.xmlParserService.ParseEmails(xml);
                return Ok(emails);
            }
            catch (XmlException ex)
            {
                var responseModel = new XmlErrorModel { Message = ex.Message };
                return BadRequest(responseModel);
            }
        }
    }
}