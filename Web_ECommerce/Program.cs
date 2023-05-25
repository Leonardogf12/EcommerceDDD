using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Configuration;
using Domain.Interfaces.Generics;
using Infrastructure.Repository.Generics;
using Domain.Interfaces.InterfaceProduct;
using Infrastructure.Repository.Repositories;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;

var builder = WebApplication.CreateBuilder(args);

#region STRING CONEXAO MYSQL

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.29")));

#endregion

#region INJECAO DE DEPENDENCIA

//*É MINHA LIGACAO DA INTERFACE COM REPOSITORIO.
builder.Services.AddSingleton(typeof(IGenerics<>), typeof(RepositoryGenerics<>));

//*INTERFACE DE PRODUTO.
builder.Services.AddSingleton<IProduct, RepositoryProduct>();

//*INTERFACE DA APLICAÇAO.
builder.Services.AddSingleton<InterfaceProductApp, AppProduct>();

//*SERVICO DO DOMINIO.
builder.Services.AddSingleton<IServiceProduct, ServiceProduct>();


#endregion

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
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
