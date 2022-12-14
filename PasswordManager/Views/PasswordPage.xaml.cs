namespace PasswordManager.Views;

// adds $_GET paramater (basically) - "?password=something"
[QueryProperty(nameof(password), nameof(password))]
[QueryProperty(nameof(id), nameof(id))]
public partial class PasswordPage : ContentPage
{
	public string password { get; set; }
	public int id { get; set; } = -1;
	private Models.Password pswd { get; set; } = new();

	public PasswordPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
    {
        Models.AllPasswords allPasswords = new(password);
        allPasswords.LoadPasswords();

		pswd.Id = -1;

		// Get password to edit when id is not -1
		if (id != -1)
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

		// Calls edit function even when adding, it will work it out :)
		allPasswords.EditPassword(pswd);

		await Shell.Current.GoToAsync("..");
	}

	private void ShowPassword_Clicked(object sender, EventArgs e)
	{
		PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
	}

	private async void CopyPassword_Clicked(object sender, EventArgs e)
	{
		await Clipboard.Default.SetTextAsync(pswd.Pswd);
		CopyButton.Text = "Copied";
	}

	private async void RemovePassword_Clicked(object sender, EventArgs e)
	{
        Models.AllPasswords allPasswords = new(password);
        allPasswords.LoadPasswords();

		allPasswords.RemovePassword(pswd);

		await Shell.Current.GoToAsync("..");
    }
}