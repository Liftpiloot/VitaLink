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
            userTypePicker.Items.Add(userType);
        }

		// Go to homeScreen.xaml.cs when registerbutton is clicked
		registerbutton.Clicked += (sender, args) =>
		{
            // TODO Handle register
            Navigation.PushAsync(new HomeScreen());
        };
	}
}