namespace VitaLink;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
		Username.Text = User.GetInstance().Username;
		Usertype.Text = User.GetInstance().UserType.ToString();
        Email.Text = User.GetInstance().Email;
	}
}