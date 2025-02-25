using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
namespace MoogleServer;

class Program
{
    //Computing TF for all words in all texts
    public static Dictionary<string, double[]> TF = preSearch.TF();
    // Passing TF result to iDF method to calculate iDF of all words in TF dict
    public static Dictionary<string, double> iDF = preSearch.iDF(TF);

    private static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
