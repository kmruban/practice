using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using practice_deploy.Models;
using System.Text;

namespace practice_deploy.Common
{
    public static class Global
    {
        public static string Key = "adef@@kfxcbv@";
        public static string ConvertToEncrypt(string password){
            if(string.IsNullOrEmpty(password)){
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }


    }
}