using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
namespace VitaLink
{
    public class Senior
    {
        
        private int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }

        public List<HealthDataItem> Data { get; set; }
        
        
        public Senior(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
        // Constructor if an image is available
        public Senior(int id, string name, string imageUrl, int age)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Age = age;
        }

        public async Task getHealthData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://vita-link.nl/api/v1/getHealthData?user_id=" + Id),
                Headers =
            {
                { "Accept", "application/json" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                // Deserialize JSON into your object
                var healthDataResponse = JsonConvert.DeserializeObject<HealthDataResponse>(body);
                List<HealthDataItem> healthData = new List<HealthDataItem>();

                // Now you can access specific properties of healthDataResponse
                foreach (var HealthDataItem in healthDataResponse.Data)
                {
                    // Access specific data from each item
                    /*int id = healthDataItem.Id;
                    int userId = healthDataItem.UserId;*/
                    double data = HealthDataItem.Data;
                    string type = HealthDataItem.Type;

                    // Use the data in your logic or call another method
                    healthData.Add(new HealthDataItem
                    {
                        Data = data,
                        Type = type
                    });
                }

                Data = healthData;
            }
        }

        public string GetHeartrate()
        { 
            // return heartbeat if it exists
            foreach (var healthDataItem in Data)
            {
                if (healthDataItem.Type == "heartbeat")
                {
                    return healthDataItem.Data.ToString() + " BPM";
                }
            }
            return "error";
        }

        public string GetTemperature()
        {
            // return temperature if it exists
            foreach (var healthDataItem in Data)
            {
                if (healthDataItem.Type == "temperature")
                {
                    return healthDataItem.Data.ToString() + " °C";
                }
            }
            return "error";
        }

        public string GetLocation()
        {
            // TODO
            return "Professor Goosenslaan 1, Tilburg";
        }
    }


}