using System.Collections.Generic;
using practice_deploy.Models;
using System.Collections;
using practice_deploy.Repository;
using System.Threading.Tasks;

namespace practice_deploy.Services
{
    public interface IUserServices
    {
        public Task<User> Login(User u);
        public void RegisterUser(User u);
        public Task<IList<User>> GetAllUsers();
        public Task<User> GetUserByID(int UserID);
        public void DeleteUser(int UserID);

    }

}