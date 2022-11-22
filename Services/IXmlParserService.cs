using Backend.Model;

namespace Backend.Services
{
    public interface IXmlParserService
    {
        EmailModel[] ParseEmails(string xml);
    }
}