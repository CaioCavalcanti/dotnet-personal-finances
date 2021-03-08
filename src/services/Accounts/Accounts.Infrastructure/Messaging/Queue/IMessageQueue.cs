using System.Threading.Tasks;

namespace Accounts.Infrastructure.Messaging
{
    public interface IMessageQueue
    {
        Task Send(string message);
    }
}