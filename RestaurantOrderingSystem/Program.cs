using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Services Configuration
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Entity Framework Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Custom Services (BURAYA TA�INDI - app.Build() �ncesi)
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IServerService, ServerService>();

// Session Configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// SignalR
builder.Services.AddSignalR();

// Razor Runtime Compilation (Development iin)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

var app = builder.Build();

// Middleware Pipeline Configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Database Initialization
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        context.Database.EnsureCreated();
        DbInitializer.Initialize(context);
        logger.LogInformation("Veritaban� ba�ar�yla olu�turuldu ve ba�lat�ld�.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Veritaban� ba�lat�l�rken bir hata olu�tu.");
    }
}

app.Run();