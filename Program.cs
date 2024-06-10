using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.WebHost.UseUrls("http://localhost:5000");


// Register your DbContext with dependency injection
builder.Services.AddDbContext<LivreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuteurRepository, AuteurRepository>();
builder.Services.AddScoped<ILivreRepository, LivreRepository>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<LivreContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
  // Default Password settings.
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireUppercase = false;
});
var app = builder.Build();

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

app.Run();
