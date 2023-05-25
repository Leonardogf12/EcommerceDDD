using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

#region STRING CONEXAO MYSQL

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.29")));

#endregion

#region INJECAO DE DEPENDENCIA

//*� MINHA LIGACAO DA INTERFACE COM REPOSITORIO.
builder.Services.AddSingleton(typeof(IGenerics<>), typeof(RepositoryGenerics<>));

//*INTERFACE DE PRODUTO.
builder.Services.AddSingleton<IProduct, RepositoryProduct>();

//*INTERFACE DA APLICA�AO.
builder.Services.AddSingleton<InterfaceProductApp, AppProduct>();

//*SERVICO DO DOMINIO.
builder.Services.AddSingleton<IServiceProduct, ServiceProduct>();


#endregion

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
