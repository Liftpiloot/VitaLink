using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitaLink
{
    public class User
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public List<Person> FollowingList { get; set; }
        public UserType UserType { get; set; }
        private static User instance;

        private User()
        {
        }
        public static User getInstance()
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }
    }
}
