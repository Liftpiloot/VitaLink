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

        List<HealthDataItem> Data { get; set; }

        public Senior(int id, string name, string imageUrl, int age)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Age = age;
        }

        async Task healthdataAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://vita-link.nl/api/v1/getHealthData?user_id=1"),
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

                // Now you can access specific properties of healthDataResponse
                foreach (var HealthDataItem in healthDataResponse.Data)
                {
                    // Access specific data from each item
                    /*int id = healthDataItem.Id;
                    int userId = healthDataItem.UserId;*/
                    double data = HealthDataItem.Data;
                    string type = HealthDataItem.Type;

                    // Use the data in your logic or call another method
                    Data.Add(data);
                    Data.Add(type);
                }
                

            }
        }

        public string GetHeartrate()
        { 
            if (Data.Type == "heartbeat")
            {
                return Data.ToString();
            }
            else
            {
                return "error";
            }
           
        }

        public string GetTemperature(double data, string type)
        {
            if (type == "temperature")
            {
                return data.ToString();
            }
            else
            {
                return "error";
            }
        }

        public string GetLocation()
        {
            // TODO
            return "Professor Goosenslaan 1, Tilburg";
        }
    }


}