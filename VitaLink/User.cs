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
        public List<Senior> FollowingList { get; set; }
        public UserType UserType { get; set; }
        private static User instance;

        private User()
        {
            // Fake data for testing purposes
            Senior kees = new Senior(1, "Kees", "kees.jpg", 80);
            Senior peter = new Senior(2, "Peter", "peter.jpg", 75);
            List<Senior> people = new List<Senior>();
            people.Add(kees);
            people.Add(peter);
            FollowingList = people;
        }
        public static User GetInstance()
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }
        
    }
}
