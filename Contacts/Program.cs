using Contacts.Database;
using Contacts.Models;
using Contacts.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        builder.Services.AddAuthentication().AddJwtBearer(options =>
        {
            
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("Future Password is Incoming")),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
            
        });
    });


ConfigureDB(builder.Services);
var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthorization();
app.Run();




static void ConfigureDB(IServiceCollection services)
{
    var config = services.BuildServiceProvider().GetService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));
}

