namespace PasswordManager.Views;

[QueryProperty(nameof(password), nameof(password))]
[QueryProperty(nameof(id), nameof(id))]
public partial class PasswordPage : ContentPage
{
	public string password { get; set; }
	public string id { get; set; }
	private Models.Password pswd { get; set; } = new();

	public PasswordPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
    {
        Models.AllPasswords allPasswords = new(password);
        allPasswords.LoadPasswords();

		if (!string.IsNullOrEmpty(id))
		{
			pswd = allPasswords.GetPassword(id);
			AddPasswordButton.Text = "Edit password";
		}

		BindingContext = pswd;
    }

	private async void AddPassword_Clicked(object sender, EventArgs e)
	{
		Models.AllPasswords allPasswords = new(password);
		allPasswords.LoadPasswords();

		Models.Password pswd = new();
		pswd.Name = NameEntry.Text;
		pswd.Url = WebsiteEntry.Text;
		pswd.Username = UsernameEntry.Text;
		pswd.Pswd = PasswordEntry.Text;

		allPasswords.AddPassword(pswd);

		await Shell.Current.GoToAsync("..");
	}

	private void ShowPassword_Clicked(object sender, EventArgs e)
	{
		PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
	}
}