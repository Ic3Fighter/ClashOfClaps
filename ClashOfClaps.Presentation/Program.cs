using ClashOfClaps.Business;
using ClashOfClaps.Data;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Starting application with Environment: {builder.Environment.EnvironmentName}");

// Add services to the container.
builder.Services.AddControllersWithViews();

// project layers
builder.Services.AddData(builder.Configuration);
builder.Services.AddBusiness();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection(); // not needed for local networks without dns
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
