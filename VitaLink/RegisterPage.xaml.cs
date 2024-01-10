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
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string username = UsernameEntry.Text;
            
            // Check if UserTypePicker.SelectedItem is null
            if (UserTypePicker.SelectedItem == null)
            {
                DisplayAlert("Error", "Please select a user type", "OK");
                return;
            }
            string type = UserTypePicker.SelectedItem.ToString();

            // alert user if not all fields are filled in
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(type))
            {
                DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }
            RegisterAsync(email, password, username, type);
        };
	}

    private async Task RegisterAsync(string email, string password, string username, string type)
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
            Content = new StringContent("{\n  \"name\": \"" + username + "\",\n  \"email\": \"" + email + "\",\n  \"password\": \"" + password + "\",\n  \"type\": \"" + type + "\"\n}")
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