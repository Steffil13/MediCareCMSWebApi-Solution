using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Repository
{
    public interface IUserRepository
    {
        List<UserInputModel> GetAllUsers();
        (string Username, string Password) AddUser(UserInputModel user);
        bool UpdateUser(int id, UserInputModel user);
        bool DeactivateUser(int id);
        UserInputModel? GetUserById(int id);

    }

}
