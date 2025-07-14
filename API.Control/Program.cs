using API.Control.Endpoints;
using API.Control.Mappings;
using API.Control.Models;
using API.Control.Services.Implementations;
using API.Control.Services.Interfaces;
using API.Control.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Minha API Incrível",
        Version = "v1",
        Description = "Documentação da API para gerencimento de EndPoints.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Gilmar",
            Email = "seu.email@dominio.com",
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adicione serviços ao contêiner.
//builder.Services.AddValidatorsFromAssemblyContaining<DeviceModel_Create_Validator>(); // Certifique-se de que o pacote FluentValidation.Extensions.DependencyInjection está instalado.

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new ApplicationProfile());
    cfg.AddProfile(new AppxPackageProfile());
    cfg.AddProfile(new DeviceModelProfile());
    cfg.AddProfile(new DeviceProfile());
    cfg.AddProfile(new DriverPackProfile());
    cfg.AddProfile(new FirmwareProfile());
    cfg.AddProfile(new ImageProfile());
    cfg.AddProfile(new InventoryProfile());
    cfg.AddProfile(new ProfileDeployProfile());
});

builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDeviceModelService, DeviceModelService>();
builder.Services.AddScoped<IFirmwareService, FirmwareService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IAppxPackageService, AppxPackageService>();

// Adicione os serviços de validação
builder.Services.AddValidatorsFromAssemblyContaining<Device_Create_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeviceModel_Create_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<MacAddress_Validator>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Control V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.MapGroup("/api/devices").WithTags("Devices").MapDeviceEndpoints();
app.MapGroup("/api/devicemodels").WithTags("DeviceModels").MapDeviceModelsEndpoints();


// Demais middlewares
app.UseHttpsRedirection();



app.Run();
