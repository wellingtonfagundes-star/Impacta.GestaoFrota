using Impacta.GestaoFrota.Application.AutoMapper;
using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.Services;
using Impacta.GestaoFrota.Data.Context;
using Impacta.GestaoFrota.Data.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Repository;
using Impacta.GestaoFrota.Domain.Interfaces.Services;
using Impacta.GestaoFrota.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FrotaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Frota")
        ?? throw new InvalidOperationException("A connection string 'Frota' não foi configurada.")));
builder.Services.AddAutoMapper(typeof(CaracteristicaProfile).Assembly);
builder.Services.AddScoped<ICaracteristicaRepository, CaracteristicasRepository>();
builder.Services.AddScoped<ICaracteristicaService, CaracteristicaService>();
builder.Services.AddScoped<ICaracteristicaAppService, CaracteristicaAppService>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IVeiculoAppService, VeiculoAppService>();
builder.Services.AddScoped<ICaracteristicaVeiculoRepository, CaracteristicaVeiculoRepository>();
builder.Services.AddScoped<ICaracteristicaVeiculoService, CaracteristicaVeiculoService>();
builder.Services.AddScoped<ICaracteristicaVeiculoAppService, CaracteristicaVeiculoAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
