using Blazored.LocalStorage;
using QuickOrderSystemAdminApp.Data;
using Blazored.Modal;
using QuickOrderSystemClassLibrary.Services.Api;
using QuickOrderSystemClassLibrary.Services;
using QuickOrderSystemClassLibrary.Services.UserService;

namespace QuickOrderSystemAdminApp
{
    public static class MauiProgram
    { public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredModal(); 
            builder.Services.AddBlazoredLocalStorage();
            #if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
			builder.Services.AddSingleton<PopUpService>();
			builder.Services.AddSingleton<OrderService>();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<QrCodeService>();
            builder.Services.AddSingleton<GenerateQRCode>();

            return builder.Build();
        }
    }
}