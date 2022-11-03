namespace PasswordManager.Views;

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

		allPasswords.EditPassword(pswd);

		await Shell.Current.GoToAsync("..");
	}

	private void ShowPassword_Clicked(object sender, EventArgs e)
	{
		PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
	}
}