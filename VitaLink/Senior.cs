namespace VitaLink
{
    public class Senior
    {
        private int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }

        public Senior(int id, string name, string imageUrl, int age)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Age = age;
        }

        public int GetHeartRate()
        {
            // TODO
            return 0;
        }

        public int GetTemperature()
        {
            // TODO
            return 0;
        }

        public string GetLocation()
        {
            // TODO
            return "";
        }
    }


}