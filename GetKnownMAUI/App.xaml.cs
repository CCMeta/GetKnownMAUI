using GetKnownMAUI.Views;

namespace GetKnownMAUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new LoginPage();
	}
}
