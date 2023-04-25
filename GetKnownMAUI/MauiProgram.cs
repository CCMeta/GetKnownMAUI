namespace GetKnownMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("OpenSansRegular.otf", "Font Awesome 5 Free Solid");
                fonts.AddFont("FontAwesome5Pro.otf", "Font Awesome 5 Free Solid");
                fonts.AddFont("FontAwesome5Regular.otf", "Font Awesome 5 Free Solid");
                fonts.AddFont("FontAwesome5Brands.otf", "Font Awesome 5 Free Solid");

            });

		return builder.Build();
	}
}
