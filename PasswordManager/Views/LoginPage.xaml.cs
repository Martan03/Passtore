using System.Security.Cryptography;

namespace PasswordManager.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void LoginButton_Clicked(object sender, EventArgs e)
	{
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
    }
}