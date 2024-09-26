using Sakkinny.Enums;
using Sakkinny.Models;
using Sakkinny.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Register services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ApartmentService>();

// Add identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Configure Cookie Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/api/Auth/Login"; // Redirect to this path if not logged in
    options.AccessDeniedPath = "/api/Auth/AccessDenied"; // Redirect if access is denied
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Cookie expiration time
    options.SlidingExpiration = true; // Renew cookie if user is active
    options.Cookie.HttpOnly = true; // Make sure cookie is not accessible via JavaScript
});

// CORS policy for React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles(roleManager);
}

async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    foreach (var role in Enum.GetValues(typeof(UserRole)).Cast<UserRole>())
    {
        var roleExists = await roleManager.RoleExistsAsync(role.ToString());
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(role.ToString()));
        }
    }
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run();
