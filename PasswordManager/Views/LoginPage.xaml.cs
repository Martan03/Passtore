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
		// If user doesn't exist, go to registration page
		if (user is null)
			await Shell.Current.GoToAsync("RegistrationPage");
	}

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		// Checks if passwords is correct
		if (string.IsNullOrEmpty(PasswordEntry.Text) || !user.CheckPassword(PasswordEntry.Text))
		{
			LoginStatus.IsVisible = true;
			LoginStatus.Text = "Password is incorrect";
			return;
		}
			
		await Shell.Current.GoToAsync($"PasswordsPage?{nameof(AllPasswordsPage.password)}={PasswordEntry.Text}");
    }
}