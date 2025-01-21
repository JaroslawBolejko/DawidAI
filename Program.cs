using BlazorChatAI.Components;
using BlazorChatAI.Models;

namespace BlazorChatAI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            var openAiApiKey = builder.Configuration["OpenAI:ApiKey"] ?? throw new Exception("Lack of key in appsettings.json");
            builder.Services.AddHttpClient("OpenAI");
            builder.Services.AddScoped<OpenAiService>(x =>
            {
                var httpClientFactory = x.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                return new OpenAiService(httpClient, openAiApiKey);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
