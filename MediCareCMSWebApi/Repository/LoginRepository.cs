using MediCareCMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MediCareDbContext _context;

        public LoginRepository(MediCareDbContext context)
        {
            _context = context;
        }
        public User ValidateUser(string username, string password)
        {
            if (_context != null)
            {
                User? dbUser = _context.Users.FirstOrDefault(
                    u => u.Username == username && u.Password == password);
               
                if (dbUser != null)
                {
                    return dbUser;
                }
            }
            return null;
        }
    }
}
