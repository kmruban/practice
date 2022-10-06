using System.Collections.Generic;
using practice_deploy.Models;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using practice_deploy.Common;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Http;
using System.Web;
using System.Linq;

namespace practice_deploy.Repository
{
    public class UserRepository : IUserRepository
    {
        private MySqlConnection _connection;
        public UserRepository()
        {
            string connectionString = "server=database-1.chb9uo57twck.us-east-1.rds.amazonaws.com;userid=admin;password=kyleruban;database=mlbstats_db";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();

        }
        ~UserRepository()
        {
            _connection.Close();
        }
        public async Task<User> Login(User u)
        {
            var statement = "SELECT * FROM users WHERE username=@uname && password=@pwd";
            var command = new MySqlCommand(statement, _connection);

            command.Parameters.AddWithValue("@uname", u.Username);
            command.Parameters.AddWithValue("@pwd", Global.ConvertToEncrypt(u.Password));
            var results = await command.ExecuteReaderAsync();

            User m = null;
            if (results.Read())
            {
                m = new User
                {
                    UserID = (int)results[0],
                    Firstname = (string)results[1],
                    Lastname = (string)results[2],
                    Username = (string)results[3],
                    Email = (string)results[4],
                    Password = (string)results[5]
                };
            }

            results.Close();
            return m;

        }

        public void RegisterUser(User u)
        {
            var statement = "Insert into users (firstname, lastname, username, email, password) Values (@fname,@lname,@uname,@email,@pwd)";
            var command = new MySqlCommand(statement, _connection);

            command.Parameters.AddWithValue("@fname", u.Firstname);
            command.Parameters.AddWithValue("@lname", u.Lastname);
            command.Parameters.AddWithValue("@uname", u.Username);
            command.Parameters.AddWithValue("@email", u.Email);
            command.Parameters.AddWithValue("@pwd", Global.ConvertToEncrypt(u.Password));

            int result = command.ExecuteNonQuery();
        }

        public async Task<IList<User>> GetAllUsers()
        {
            var statement = "Select * from users";
            var command = new MySqlCommand(statement, _connection);
            var results = await command.ExecuteReaderAsync();
            Console.WriteLine(results);
            IList<User> newList = new List<User>();
            while (results.Read())
            {
                User m = new User
                {
                    UserID = (int)results[0],
                    Firstname = (string)results[1],
                    Lastname = (string)results[2],
                    Username = (string)results[3],
                    Email = (string)results[4],
                    Password = (string)results[5]
                };
                newList.Add(m);

            }
            results.Close();
            return newList;
        }

        public async Task<User> GetUserByID(int UserID)
        {
            var statement = "Select * from users WHERE userID = @id";
            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@id", UserID);
            var results = await command.ExecuteReaderAsync();
            User m = null;
            if (results.Read())
            {
                m = new User
                {
                    UserID = (int)results[0],
                    Firstname = (string)results[1],
                    Lastname = (string)results[2],
                    Username = (string)results[3],
                    Email = (string)results[4],
                    Password = (string)results[5]
                };
            }
            results.Close();
            return m;
        }

        public void DeleteUser(int UserID)
        {
            var statement = "DELETE FROM users Where UserID=@deleteID";

            var command = new MySqlCommand(statement, _connection);
            command.Parameters.AddWithValue("@deleteID", UserID);

            int result = command.ExecuteNonQuery();

        }


    }

}