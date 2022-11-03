namespace PasswordManager.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	private async void RegisterButton_Clicked(object sender, EventArgs e)
	{
		Models.User user = new();

		// Either of password is empty case
		if (string.IsNullOrEmpty(PasswordEntry.Text) || string.IsNullOrEmpty(PasswordAgainEntry.Text))
        {
            RegisterStatus.IsVisible = true;
            RegisterStatus.Text = "Password must be filled in";
			return;
        }
		// Passwords do not match case
		else if (!string.Equals(PasswordEntry.Text, PasswordAgainEntry.Text))
		{
			RegisterStatus.IsVisible = true;
			RegisterStatus.Text = "Passwords do not match";
			return;
		}

		user.CreateUser(PasswordEntry.Text);

        await Shell.Current.GoToAsync($"PasswordsPage?{nameof(AllPasswordsPage.password)}={PasswordEntry.Text}");
    }
}