
//CreateBuilder(args) will set up Kestrel web server
//Kestrel will handle the requests
//Kestrel is placed behide IIS web server and that configuration is done by CreateBuilder(args)
//Also is configured where to find executable code (specify content root)
//Configuration information is read from appsettings.json
using EfisPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
builder.Services.AddScoped<IPieRepository, MockPieRepository>();

// Add services to the container.
// Here we can our services
// .AddControllersWithViews() that's a Framework Service and extension methods exists
//    that service make our application MVC
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EfisPieShopDbContext>( options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:EfisPieShopDbContextConnection"]);
});

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

// UseStaticFiles() is preconfigured to look for incoming requests for static files (.jpg, .jpeg, css)
// and it will look in that default configured folder (wwwroot) for that static file and return it
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    // Enables us to see the errors of the application
    // It's a dignostic middleware component that will not always show the exception page
    app.UseDeveloperExceptionPage();
}


app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//To Here

//Our application start listening for incomming requests
app.Run();
