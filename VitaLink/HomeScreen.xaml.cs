namespace VitaLink;
using Microsoft.Maui.Controls;

public partial class HomeScreen
{
    private const int PeopleButtonSize = 50;
    private Senior _selectedSenior;

    public HomeScreen()
    {
        InitializeComponent();
        // Set the theme to light
        if (Application.Current != null) Application.Current.UserAppTheme = AppTheme.Light;

        // Example for creating a user, should be handled with the database
        User user = User.GetInstance();
        user.Username = "Abel";
        _selectedSenior = user.FollowingList[0];
        
        // Add the buttons to the stack layout
        foreach (var senior in user.FollowingList)
        {
            PeopleButtons.Children.Add(CreateFollowerButton(senior));
        }

        AccountButton.Clicked += (_, _) =>
        {
            // TODO Handle account
            Navigation.PushAsync(new SettingsPage());
        };
        
        
        
        // Run the ShowStats() method every 10 seconds
        Device.StartTimer(TimeSpan.FromSeconds(10), ()=>
        {
            ShowStats(_selectedSenior);
            return true;
        });
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
        imageButton.Clicked += (_, _) =>
        {
            foreach (var view in PeopleButtons.Children)
            {
                var button = (Frame)view;
                if (button.Content == imageButton)
                {
                    button.HeightRequest = PeopleButtonSize + 10;
                    button.Content.HeightRequest = PeopleButtonSize + 10;
                    button.WidthRequest = PeopleButtonSize + 10;
                    button.Content.WidthRequest = PeopleButtonSize + 10;
                }
                else{
                    button.HeightRequest = PeopleButtonSize;
                    button.Content.HeightRequest = PeopleButtonSize;
                    button.WidthRequest = PeopleButtonSize;
                    button.Content.WidthRequest = PeopleButtonSize;
                }
            }
            
            _selectedSenior = senior;
            ShowStats(senior);
        };

        // Create a frame to wrap the image
        var frame = new Frame
        {
            Content = imageButton,
            HasShadow = false,
            CornerRadius = 25, // Round the corners
            WidthRequest = PeopleButtonSize,
            HeightRequest = PeopleButtonSize,
            Margin = 5,
        };

        return frame;
    }

    private void ShowStats(Senior senior)
    {
        LocationText.Text = senior.GetLocation();
        HeartRateText.Text = senior.GetHeartRate().ToString();
        TemperatureText.Text = senior.GetTemperature().ToString();
    }
}