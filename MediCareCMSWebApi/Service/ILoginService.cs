using MediCareCMSWebApi.Models;

namespace MediCareCMSWebApi.Service
{
    public interface ILoginService
    {
        public User AuthenticateUser(string username, string password);
    }
}
