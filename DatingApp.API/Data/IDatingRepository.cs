using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    //I means interface for the DatingRepository class
    //consists of abstract functions that must be included in class
    public interface IDatingRepository
    {
        //T mean generic operator
        //means entity of type T is generic and the type will be defined later
        //T : class means that T will be a class not a common variable type like int 
        //so if it is called as Add<User> T will be of type User during compilation
        //https://www.tutorialsteacher.com/csharp/csharp-generics
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int id);
    }
}