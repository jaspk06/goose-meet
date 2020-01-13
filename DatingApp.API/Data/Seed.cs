using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        //DataConext context is being injected so that the database can be updated
        public static void SeedUsers(DataContext context){
            //check to see if any of the imported users already exist
            //if they do, do not seed users
            if (!context.Users.Any()){
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);        //converts JSON data and puts in User object
                
                //generates a new password hash and password salt for every user in the seeded file
                foreach (var user in users){
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }
                //updating database
                context.SaveChanges();
            }
        }
    
    //method copied from AuthRepository since it was previously private in AuthRepository.cs
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}