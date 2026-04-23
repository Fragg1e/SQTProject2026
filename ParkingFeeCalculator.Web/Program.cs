using Microsoft.AspNetCore.DataProtection;
using ParkingFeeCalculator;
//startup file and launch file
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddRazorPages(); //makes .cshtml files visible to the app
builder.Services
    .AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"))); //config for encryption keys and stores
//dependency injection
builder.Services.AddSingleton<IDiscountService, DiscountService>();
builder.Services.AddScoped<IParkingService, ParkingService>();

var app = builder.Build();
//if a request fails users see a friendly error page instead of the raw error
if (!app.Environment.IsDevelopment()) 
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
//request pipeline (ORDER MATTERS THE ORDER REALLY MATTERS)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
