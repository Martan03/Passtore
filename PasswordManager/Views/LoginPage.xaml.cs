using System.Security.Cryptography;

namespace PasswordManager.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		Models.User user = new();
		user = user.LoadFromJson();

		if (user is null)


		if (user.CheckPassword(PasswordEntry.Text))
			await Shell.Current.GoToAsync($"{nameof(AllPasswords)}");
    }
}