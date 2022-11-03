namespace PasswordManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("RegistrationPage", typeof(Views.RegisterPage));
		Routing.RegisterRoute("PasswordsPage", typeof(Views.AllPasswordsPage));
		Routing.RegisterRoute("PasswordPage", typeof(Views.PasswordPage));
	}
}
