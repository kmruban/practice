using System.Collections.Generic;
using practice_deploy.Models;
using System.Threading.Tasks;

namespace practice_deploy.Repository
{
    public interface IUserRepository
    {
        public Task<User> Login(User u);
        public void RegisterUser(User u);
        public Task<IList<User>> GetAllUsers();
        public Task<User> GetUserByID(int UserID);
        public void DeleteUser(int UserID);

    }

}