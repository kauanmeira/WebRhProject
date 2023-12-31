﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebRhProject.Data;
using WebRhProject.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Contexto") ?? throw new InvalidOperationException("Connection string 'Contexto' not found.")));

services.AddControllersWithViews();
services.AddScoped<CargoService>();
services.AddScoped<ColaboradorService>();
services.AddScoped<EmpresaService>();
services.AddScoped<UsuarioService>();
services.AddScoped<HoleriteService>();
services.AddScoped<BeneficioService>();

// Configura a serialização JSON para preservar referências circulares
services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "demissao",
    pattern: "colaboradores/demissao/{id}",
    defaults: new { controller = "Colaboradores", action = "Demissao" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

app.Run();


