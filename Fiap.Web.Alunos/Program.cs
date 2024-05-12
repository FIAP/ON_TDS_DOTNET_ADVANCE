#region IMPORTAÇÃO REFERENTE AO BANCO DE DADOS
using AutoMapper;
using Fiap.Web.Alunos.Data.Contexts;
using Fiap.Web.Alunos.Logging;
using Fiap.Web.Alunos.Models;
using Fiap.Web.Alunos.ViewModels;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
#endregion

var builder = WebApplication.CreateBuilder(args);

#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion


#region Registro IServiceCollection
builder.Services.AddSingleton<ICustomLogger, MockLogger>();
#endregion


#region AutoMapper

// Configuração do AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    // Permite que coleções nulas sejam mapeadas
    c.AllowNullCollections = true;
    // Permite que valores de destino nulos sejam mapeados
    c.AllowNullDestinationValues = true;

    // Define o mapeamento de ClienteModel para ClienteCreateViewModel
    c.CreateMap<ClienteModel, ClienteCreateViewModel>();
    // Define o mapeamento reverso de ClienteCreateViewModel para ClienteModel
    c.CreateMap<ClienteCreateViewModel, ClienteModel>();
});

// Cria o mapper com base na configuração definida
IMapper mapper = mapperConfig.CreateMapper();

// Registra o IMapper como um serviço singleton no container de DI do ASP.NET Core
builder.Services.AddSingleton(mapper);

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

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


