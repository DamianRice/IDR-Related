using PrismCoreTemplate.Services.Interfaces;

namespace PrismCoreTemplate.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
