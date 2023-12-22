namespace VitaLink
{
    public class Senior
    {
        int i = -1;
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

       
        public string GetHeartRate()
        {
            int[] heartrate = { 66, 67, 70, 71, 73, 75, 77, 80 };
            
            // TODO
            if (i < heartrate.Length)
            {
                i++;
                return Convert.ToString(heartrate[i]) + " BPM";
            }
            else
            {
                return "0 BPM";
            }
        }

        public string GetTemperature()
        {
            // TODO
            double[] temperature = { 36.3, 36.5, 36.7, 37.0, 37.3, 37.5, 37.2, 37.0 };
            if (i < temperature.Length)
            {
                return Convert.ToString(temperature[i]) + " °C";
            }
            else
            {
                return "0 °C";
            }
        }

        public string GetLocation()
        {
            // TODO
            return "Professor Goosenslaan 1, Tilburg";
        }
    }


}