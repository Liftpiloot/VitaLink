namespace VitaLink
{
    public class Person
    {
        private int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }

        public Person(int id, string name, string image, int age)
        {
            Id = id;
            Name = name;
            Image = image;
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