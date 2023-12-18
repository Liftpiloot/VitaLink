namespace VitaLink;
using Microsoft.Maui.Controls;

public partial class HomeScreen
{
    private const int PeopleButtonSize = 60;
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
        
        // Set the first senior as the selected senior
        _selectedSenior = user.FollowingList[0];
        VisualiseSelectedButton((ImageButton)((Frame)PeopleButtons.Children[0]).Content);

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
    
    /*
     * Creates a button for a senior
     * @param senior The senior to create a button for
     * @return The button
     */
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
            VisualiseSelectedButton(imageButton);
            _selectedSenior = senior;
            ShowStats(senior);
        };

        // Create a frame to wrap the image
        var frame = new Frame
        {
            Content = imageButton,
            HasShadow = false,
            CornerRadius = 50, // Round the corners
            WidthRequest = PeopleButtonSize,
            HeightRequest = PeopleButtonSize,
            Margin = 10,
        };
        return frame;
    }
    
    /*
     * Visualises the selected button, by making it bigger than the other buttons
     * @param imageButton The button to visualise
     */
    private async Task VisualiseSelectedButton(ImageButton imageButton)
    {
        foreach (var view in PeopleButtons.Children)
        {
            var button = (Frame)view;
            if (button.Content == imageButton)
            { 
                button.ScaleTo(1.2, 100); 
                button.Content.ScaleTo(1, 100);
            }
            else{
                button.ScaleTo(1, 100);
                button.Content.ScaleTo(1, 100);
            }
        }
        await imageButton.ScaleTo(1, 100);
    }

    private void ShowStats(Senior senior)
    {
        LocationText.Text = senior.GetLocation();
        HeartRateText.Text = senior.GetHeartRate().ToString();
        TemperatureText.Text = senior.GetTemperature().ToString();
    }
}