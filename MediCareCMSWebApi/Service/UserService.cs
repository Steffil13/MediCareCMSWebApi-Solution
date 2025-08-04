using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.ViewModel;

namespace MediCareCMSWebApi.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserInputModel> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public (string Username, string Password) AddUser(UserInputModel user)
        {
            return _userRepository.AddUser(user);
        }

        public bool UpdateUser(int id, UserInputModel user)
        {
            return _userRepository.UpdateUser(id, user);
        }

        public bool DeactivateUser(int id)
        {
            return _userRepository.DeactivateUser(id);
        }
        public UserInputModel? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

    }

}
