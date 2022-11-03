namespace PasswordManager.Views;

[QueryProperty(nameof(password), nameof(password))]
public partial class AllPasswordsPage : ContentPage
{
	public string password { get; set; }

	public AllPasswordsPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		BindingContext = new Models.AllPasswords(password);
		((Models.AllPasswords)BindingContext).LoadPasswords();
	}

	private async void Add_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"PasswordPage?{nameof(PasswordPage.password)}={password}");
	}

	private async void PasswordCard_Tapped(object sender, EventArgs e)
	{
		var pswd = ((VisualElement)sender).BindingContext as Models.Password;
		if (pswd is null)
			return;

        await Shell.Current.GoToAsync($"PasswordPage?{nameof(PasswordPage.id)}={pswd.Name}&{nameof(PasswordPage.password)}={password}");
    }
}