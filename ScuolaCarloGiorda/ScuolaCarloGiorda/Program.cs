using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.HttpOverrides;
using ScuolaCarloGiorda.Data;
using ScuolaCarloGiorda.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 443; });


builder.Services.AddSingleton<SchoolDbContext>();

builder.Services.AddSingleton<CorsiService>();


 

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});


var app = builder.Build();
SchoolDbContext? context=(SchoolDbContext)app.Services.GetService(typeof(SchoolDbContext))!;
context.Initialize();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseForwardedHeaders();
if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DYNO")))
{
    Console.WriteLine("Use https redirection");
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();
app.UseCors("CorsPolicy");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();