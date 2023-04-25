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
                fonts.AddFont("OpenSans-Regular.ttf", "FontAwesome5Solid");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FontAwesome5Solid.otf", "FontAwesome5Solid");
                fonts.AddFont("FontAwesome5Pro.otf", "FontAwesome5Pro");
                fonts.AddFont("FontAwesome5Regular.otf", "FontAwesome5Regular");
                fonts.AddFont("FontAwesome5Brands.otf", "FontAwesome5Brands");
            });

        return builder.Build();
    }
}
