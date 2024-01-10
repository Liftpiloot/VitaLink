using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace VitaLink
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Set the theme to light
            Application.Current.UserAppTheme = AppTheme.Light;

            // Go to homeScreen.xaml.cs when loginbutton is clicked
            LoginButton.Clicked += (sender, args) =>
            {
                string email = EmailEntry.Text;
                string password = PasswordEntry.Text;
                // alert user if email or password is empty
                if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
                {
                    DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }
                
                loginAsync(email, password);
            };
            // Go to register.xaml.cs when registerbutton is clicked
            RegisterButton.Clicked += (sender, args) =>
            {
                // TODO Handle register
                Navigation.PushAsync(new RegisterPage(this));
            };

        }
        public async Task loginAsync(string email, string password)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://vita-link.nl/api/v1/login"),
                Headers =
                {
                    { "Accept", "application/json" },
                },
                Content = new StringContent("{\n  \"email\": \"" + email + "\",\n  \"password\": \"" + password + "\"\n}")

                {
                    Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
                }
            };
            using (var response = await client.SendAsync(request))
            {
                // show error if login failed
                if (!response.IsSuccessStatusCode)
                { 
                    await DisplayAlert("Error", "Login failed", "OK");
                    return;
                }
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                if (response.IsSuccessStatusCode) 
                {
                    // Deserialize JSON into your object
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(body);
                    // Set user data
                    User.GetInstance().Id = loginResponse.id.ToString();
                    User.GetInstance().Username = loginResponse.name;
                    User.GetInstance().UserType = loginResponse.type == "senior" ? UserType.Senior : UserType.Carer;
                    User.GetInstance().Email = loginResponse.email;
                    Navigation.PushAsync(new HomeScreen());
                }   
            }
            
        }
    }
    public class LoginResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set;}
        public string email_verified_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; } 
        public string type { get; set; }
    }
}