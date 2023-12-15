namespace VitaLink;

public partial class HomeScreen : ContentPage
{
    private const int PeopleButtonSize = 50;
    private Senior _selectedSenior;

    public HomeScreen()
    {
        InitializeComponent();
        // Set the theme to light
        Application.Current.UserAppTheme = AppTheme.Light;
        
        // Example for creating a user, should be handled with the database
        User user = User.GetInstance();
        user.Token = "123";
        user.Username = "Abel";
        
        // Add the buttons to the stack layout
        for (int i = 0; i < user.FollowingList.Count; i++)
        {
            peopleButtons.Children.Add(CreateFollowerButton(user.FollowingList[i]));
        }

        accountbutton.Clicked += (sender, args) =>
        {
            // TODO Handle account
            Navigation.PushAsync(new SettingsPage());
        };
    }
    private Frame CreateFollowerButton(Senior senior)
    {
        // Create an image with the specified source
        var imageButton = new ImageButton
        {
            Source = senior.ImageUrl,
            Aspect = Aspect.AspectFit,
            WidthRequest = PeopleButtonSize,
            HeightRequest = PeopleButtonSize
        };
        imageButton.Clicked += (sender, args) =>
        {
            ShowStats(senior);
            // TODO Show stats of the person
        };

        // Create a frame to wrap the image
        var frame = new Frame
        {
            Content = imageButton,
            HasShadow = false,
            CornerRadius = 25, // Round the corners
            WidthRequest = PeopleButtonSize,
            HeightRequest = PeopleButtonSize,
            Margin = 5
        };

        return frame;
    }

    private void ShowStats(Senior senior)
    {
        _selectedSenior = senior;
        locationText.Text = senior.GetLocation();
        heartRateText.Text = senior.GetHeartRate().ToString();
        temperatureText.Text = senior.GetTemperature().ToString();
    }
}