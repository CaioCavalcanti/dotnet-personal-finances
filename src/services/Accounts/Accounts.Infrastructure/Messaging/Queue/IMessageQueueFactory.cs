using System.Threading.Tasks;

namespace Accounts.Infrastructure.Messaging.Queue
{
    public interface IMessageQueueFactory
    {
        IMessageQueue Create(string queueName);
    }
}