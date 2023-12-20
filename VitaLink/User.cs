namespace VitaLink
{
    public class User
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public List<Senior> FollowingList { get; set; }
        public UserType UserType { get; set; }
        private static User _instance;

        private User()
        {
            // Fake data for testing purposes
            Senior kees = new Senior(1, "Kees", "kees.jpg", 80);
            Senior peter = new Senior(2, "Peter", "peter.jpg", 75);
            Senior ron = new Senior(3, "Ron", "ron.jpg", 85);
            List<Senior> people = new List<Senior>
            {
                kees,
                peter,
                ron
            };
            FollowingList = people;
        }
        public static User GetInstance()
        {
            if (_instance == null)
            {
                _instance = new User();
            }
            return _instance;
        }
        
    }
}
