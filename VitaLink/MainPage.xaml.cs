using System.Net.Http.Headers;
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

            // Go to homeScreen.xaml.cs when login button is clicked
            LoginButton.Clicked += (_, _) =>
            {
                string email = EmailEntry.Text;
                string password = PasswordEntry.Text;
                // alert user if email or password is empty
                if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
                {
                    DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }
                
                LoginAsync(email, password);
            };
            // Go to register.xaml.cs when register button is clicked
            RegisterButton.Clicked += (_, _) =>
            {
                // TODO Handle register
                Navigation.PushAsync(new RegisterPage(this));
            };

        }

        private async Task LoginAsync(string email, string password)
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
            using var response = await client.SendAsync(request);
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
                User.GetInstance().Id = loginResponse.Id.ToString();
                User.GetInstance().Username = loginResponse.Name;
                User.GetInstance().UserType = loginResponse.Type == "senior" ? UserType.Senior : UserType.Carer;
                User.GetInstance().Email = loginResponse.Email;
                await Navigation.PushAsync(new HomeScreen());
            }
        }
    }
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set;}
        public string EmailVerifiedAt { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; } 
        public string Type { get; set; }
    }
}