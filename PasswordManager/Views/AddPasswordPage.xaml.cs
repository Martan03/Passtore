namespace PasswordManager.Views;

[QueryProperty(nameof(password), nameof(password))]
public partial class AddPasswordPage : ContentPage
{
	public string password { get; set; }

	public AddPasswordPage()
	{
		InitializeComponent();
	}

	private async void AddPassword_Clicked(object sender, EventArgs e)
	{
		Models.AllPasswords allPasswords = new(password);

		Models.Password pswd = new();
		pswd.Name = NameEntry.Text;
		pswd.Url = WebsiteEntry.Text;
		pswd.Username = UsernameEntry.Text;
		pswd.Pswd = PasswordEntry.Text;

		allPasswords.AddPassword(pswd);

		await Shell.Current.GoToAsync("..");
	}
}