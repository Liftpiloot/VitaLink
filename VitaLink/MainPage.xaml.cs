using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

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
                loginAsync(email, password);
                // TODO Handle login

                

               
            };
            // Go to register.xaml.cs when registerbutton is clicked
            RegisterButton.Clicked += (sender, args) =>
            {
                // TODO Handle register
                Navigation.PushAsync(new RegisterPage());
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
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            Navigation.PushAsync(new HomeScreen());
        }
    }
}