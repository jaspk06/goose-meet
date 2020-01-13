using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    //I means interface for the AuthRepository class
    //consists of abstract functions that must be included in class
    public interface IAuthRepository
    {
         Task<User> Login(string username, string password);
         Task<User> Register (User user, string password);
         Task<bool> UserExists(string username);
    }
}