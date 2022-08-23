using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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

//Computing TF for all words in all texts
Dictionary<string, double[]> TF = preSearch.TF();
// Passing TF result to iDF method to calculate iDF of all words in TF dict
Dictionary<string, double> iDF = preSearch.iDF(TF);
// Calculating TFiDF of all words in DataBase
Dictionary<string, (double, double)[]> TFiDF = preSearch.TFiDF(TF, iDF);

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();