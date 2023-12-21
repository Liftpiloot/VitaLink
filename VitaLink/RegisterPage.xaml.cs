using System.Net.Http.Headers;

namespace VitaLink;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		// Set the theme to light
		Application.Current.UserAppTheme = AppTheme.Light;

		// add enum name to userTypePicker for every enum value in UserType
		foreach (string userType in Enum.GetNames(typeof(UserType)))
		{
            UserTypePicker.Items.Add(userType);
        }

		// Go to homeScreen.xaml.cs when registerbutton is clicked
		RegisterButton.Clicked += (sender, args) =>
		{
            // TODO Handle register
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string username = UsernameEntry.Text;
            registerAsync(email, password, username);
        };
	}

	public async Task registerAsync(string email, string password, string username)
	{
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://vita-link.nl/api/v1/users"),
            Headers =
    {
        { "Accept", "application/json" },
    },
            Content = new StringContent("{\n  \"name\": \"" + username + "\",\n  \"email\": \"" + email + "\",\n  \"password\": \"" + password + "\"\n}")
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
            if (response.IsSuccessStatusCode)
            {
                Navigation.PushAsync(new HomeScreen());
            }
        }
    }
}