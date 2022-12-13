
//CreateBuilder(args) will set up Kestrel web server
//Kestrel will handle the requests
//Kestrel is placed behide IIS web server and that configuration is done by CreateBuilder(args)
//Also is configured where to find executable code (specify content root)
//Configuration information is read from appsettings.json
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Here we can our services
// .AddControllersWithViews() that's a Framework Service and extension methods exists
//    that service make our application MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//From Here
// Middleware is set up
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//To Here

//Our application start listening for incomming requests
app.Run();
