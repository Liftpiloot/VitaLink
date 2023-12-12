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
            loginbutton.Clicked += (sender, args) =>
            {
                // TODO Handle login
                Navigation.PushAsync(new HomeScreen());
            };


        }
    }
}