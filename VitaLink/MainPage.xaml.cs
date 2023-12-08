namespace VitaLink
{
    public partial class MainPage : ContentPage
    {
        public static int peopleButtonSize = 50;

        public MainPage() {
            InitializeComponent();
            // Set the theme to light
            Application.Current.UserAppTheme = AppTheme.Light;

            // Add the buttons to the stacklayout
            peopleButtons.Children.Add(CreateFollowerButton("kees.jpg"));
            peopleButtons.Children.Add(CreateFollowerButton("kees.jpg"));
        }
        private Frame CreateFollowerButton(string imageName)
        {
            // Create an image with the specified source
            var imageButton = new ImageButton
            {
                Source = imageName,
                Aspect = Aspect.AspectFit,
                WidthRequest = peopleButtonSize,
                HeightRequest = peopleButtonSize
            };
            imageButton.Clicked += (sender, args) =>
            {
                // TODO Show stats of the person
            };

            // Create a frame to wrap the image
            var frame = new Frame
            {
                Content = imageButton,
                HasShadow = true,
                CornerRadius = 25, // Round the corners
                WidthRequest = peopleButtonSize,
                HeightRequest = peopleButtonSize,
                Margin = 5
            };

            return frame;
        }

    }
}