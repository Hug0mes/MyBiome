using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
	ProgressBar = true,
	Timeout = 5000
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>()  
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnection"]);
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = false,
    PositionClass = ToastPositions.BottomCenter
}
);

//builder.Services.AddIdentity<AppUser, IdentityRole>()
//	.AddDefaultUI()
//	.AddEntityFrameworkStores<DataContext>()
//	.AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    //    var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    await SeedData.SeedDatabase(context, userManager, roleManager);
}

app.Run();
