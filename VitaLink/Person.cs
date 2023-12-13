namespace VitaLink
{
    public class Person
    {
        private int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }

        public Person(int id, string name, string imageUrl, int age)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Age = age;
        }

        public int getHeartRate()
        {
            // TODO
            return 0;
        }

        public int getTemperature()
        {
            // TODO
            return 0;
        }

        public string getLocation()
        {
            // TODO
            return "";
        }
    }


}