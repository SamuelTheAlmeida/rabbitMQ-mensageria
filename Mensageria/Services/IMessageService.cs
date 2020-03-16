using System.Threading.Tasks;
namespace Mensageria.Services
{
    public interface IMessageService
    {
         Task SendMessage();
    }
}