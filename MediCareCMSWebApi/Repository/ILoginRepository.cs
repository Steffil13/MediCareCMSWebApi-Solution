using MediCareCMSWebApi.Models;

namespace MediCareCMSWebApi.Repository
{
    public interface ILoginRepository
    {

        //select user details by using username and password

        public User ValidateUser(string username, string password);
    }
}
