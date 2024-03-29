
//CreateBuilder(args) will set up Kestrel web server
//Kestrel will handle the requests
//Kestrel is placed behide IIS web server and that configuration is done by CreateBuilder(args)
//Also is configured where to find executable code (specify content root)
//Configuration information is read from appsettings.json
using EfisPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EfisPieShopDbContextConnection") ?? 
        throw new InvalidOperationException("Connection string 'EfisPieShopDbContextConnection' not found.");

//For Identity
builder.Services.AddDbContext<EfisPieShopDbContext>(options =>
    options.UseSqlServer(connectionString));

//For Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<EfisPieShopDbContext>();


//OR
//builder.Services.AddDbContext<EfisPieShopDbContext>(options =>
//{
//    options.UseSqlServer(
//        builder.Configuration["ConnectionStrings:EfisPieShopDbContextConnection"]);
//});

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<EfisPieShopDbContext>();


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


//This will invoke the GetCart method, passing in the service provider
//AddScoped is going to create a ShoppingCart for the request, so all the places within the request that have access to ShoppingCart
// will use the same ShoppingCart that get instantiated in the GetCart method
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();                          //it is used in GetCard method
builder.Services.AddHttpContextAccessor();               //it is used in GetCard method

// Add services to the container.
// Here we can our services
// .AddControllersWithViews() that's a Framework Service and extension methods exists
//    that service make our application MVC
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>   // That's for API and ignore cycles between category and pie 
    {
        options.JsonSerializerOptions.ReferenceHandler= ReferenceHandler.IgnoreCycles; ;
    });

// To Add Razor Pages
builder.Services.AddRazorPages();


// To add Blazor to our app
builder.Services.AddServerSideBlazor();

// It's needed in order to have a RESTful API
//builder.Services.AddControllers();

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


//Needed middleware because of builder.Services.AddSession();     
app.UseSession();

//Authentication Middleware for Identity
app.UseAuthentication();


if (app.Environment.IsDevelopment())
{
    // Enables us to see the errors of the application
    // It's a dignostic middleware component that will not always show the exception page
    app.UseDeveloperExceptionPage();
}


app.UseRouting();

app.UseAuthorization();
app.UseAuthorization(); 

app.MapDefaultControllerRoute(); //"{controller=Home}/{action=Index}/{id?}"

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//To Here

// Middleware for Razor Pages
app.MapRazorPages();

// Enable Blazor in the pipeline
app.MapBlazorHub();
app.MapFallbackToPage("/app/{*catchall}", "/App/Index");

//Middleware for Routing for a RESTful API
//app.MapControllers();

DbInitializer.Seed(app);

//Our application start listening for incomming requests
app.Run();
