using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace LogicLayer
{
    public class UserManager
    {
        public static User? LoggedUser { get; set; }
        public static bool Register(string username, string password)
        {
            var user = DBAccess.AddNewUser(username, password);
            if (user != null)
            {
                LoggedUser = user;
                return true;
            }
            return false;
        }

        public static bool Login(string username, string password)
        {

            var user = DBAccess.GetUser(username, password);
            if (user != null)
            {
                LoggedUser = user;
                return true;
            }
            return false;
        }

        public static void Logout()
        {
            LoggedUser = null;
        }
    }
}
