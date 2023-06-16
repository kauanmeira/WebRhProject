using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebRhProject.Data;
using WebRhProject.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Contexto") ?? throw new InvalidOperationException("Connection string 'Contexto' not found.")));

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();
services.AddScoped<CargoService>();
services.AddScoped<ColaboradorService>();
services.AddScoped<EmpresaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Run();
