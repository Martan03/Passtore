namespace PasswordManager.Views;

[QueryProperty(nameof(password), nameof(password))]
public partial class AllPasswords : ContentPage
{
	public string password
	{
		set { LoadPasswords(value); }
	}

	public AllPasswords()
	{
		InitializeComponent();
	}

	public void LoadPasswords(string password)
	{
		testLabel.Text = password;
	}
}