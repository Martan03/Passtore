namespace PasswordManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("register", typeof(Views.RegisterPage));
		Routing.RegisterRoute("passwords", typeof(Views.AllPasswords));
	}
}
