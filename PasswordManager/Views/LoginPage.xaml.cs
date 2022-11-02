using System.Security.Cryptography;

namespace PasswordManager.Views;

public partial class LoginPage : ContentPage
{
	private Models.User user { get; set; } = new();

	public LoginPage()
	{
		InitializeComponent();

		user = user.LoadFromJson();
		RegisterUserIfNull();
	}

	private async void RegisterUserIfNull()
	{
		if (user is null)
			await Shell.Current.GoToAsync("/register");
	}

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		if (!user.CheckPassword(PasswordEntry.Text))
		{
			LoginStatus.IsVisible = true;
			LoginStatus.Text = "Password is incorrect";
			return;
		}
			
		await Shell.Current.GoToAsync($"passwords?{nameof(AllPasswords.password)}={PasswordEntry.Text}");
    }
}