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
	}

	private async void Add_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("AddPasswordPage");
	}
}