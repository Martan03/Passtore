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

		if (string.Compare(PasswordEntry.Text, PasswordAgainEntry.Text) != 0)
		{
			RegisterStatus.IsVisible = true;
			RegisterStatus.Text = "Passwords do not match";
		}
		else if (string.IsNullOrEmpty(PasswordEntry.Text) || string.IsNullOrEmpty(PasswordAgainEntry.Text))
        {
            RegisterStatus.IsVisible = true;
            RegisterStatus.Text = "Password must be filled in";
        }

		user.CreateUser(PasswordEntry.Text);

		await Shell.Current.GoToAsync($"{nameof(AllPasswords)}");
	}
}