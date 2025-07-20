using API.Control.Endpoints;
using API.Control.Mappings;
using API.Control.Models;
using API.Control.Services.Implementations;
using API.Control.Services.Interfaces;
using API.Control.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
// Configuração do OpenAPI
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqliteOptions => sqliteOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Configuração do Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API.Control",
        Version = "v1",
        Description = "Documentação da API para gerencimento de EndPoints.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Gilmar Prust",
            Email = "gilmar.prust@outlook.com",
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


// Configuração do AutoMapper, mapeamento de DTOs para entidades e vice-versa.
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new ApplicationProfile());
    cfg.AddProfile(new AppxPackageProfile());
    cfg.AddProfile(new DeployProfileProfile());
    cfg.AddProfile(new DeviceModelProfile());
    cfg.AddProfile(new DeviceProfile());
    cfg.AddProfile(new DriverPackProfile());
    cfg.AddProfile(new FirmwareProfile());
    cfg.AddProfile(new ImageProfile());
    cfg.AddProfile(new InventoryProfile());
});


// Configuração dos serviços de injeção de dependência, registrando as implementações dos serviços.
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IAppxPackageService, AppxPackageService>();
builder.Services.AddScoped<IDeployProfileService, DeployProfileService>();
builder.Services.AddScoped<IDeviceModelService, DeviceModelService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IDriverPackService, DriverPackService>();
builder.Services.AddScoped<IFirmwareService, FirmwareService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();


// Adicione os serviços de validação
builder.Services.AddValidatorsFromAssemblyContaining<Device_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeviceModel_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<MacAddress_Validator>();


// Configuração do FluentValidation, validação automática para endpoints minimalistas:
builder.Services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
builder.Services.AddFluentValidationClientsideAdapters();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Control V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}



// Configuração do CORS, permitindo qualquer origem, cabeçalho e método.
app.MapGroup("/api/applications").WithTags("Applications").MapApplicationEndpoints();
app.MapGroup("/api/appxpackages").WithTags("AppxPackages").MapAppxPackageEndPoints();
app.MapGroup("/api/deployprofiles").WithTags("DeployProfiles").MapDeployProfileEndpoints();
app.MapGroup("/api/devices").WithTags("Devices").MapDeviceEndpoints();
app.MapGroup("/api/devicemodels").WithTags("DeviceModels").MapDeviceModelsEndpoints();
app.MapGroup("/api/driverpacks").WithTags("DriverPacks").MapDriverPackEndpoints();
app.MapGroup("/api/firmwares").WithTags("Firmwares").MapFirmwareEndpoints();
app.MapGroup("/api/image").WithTags("Images").MapImageEndpoints();
app.MapGroup("/api/inventory").WithTags("Inventory").MapInventoryEndpoints();

// Middleware para uso de arquivos estáticos, como imagens, CSS e JavaScript.
app.UseStaticFiles();

// Middleware para uso de CORS.
app.UseHttpsRedirection();
app.UseCors();

// Middleware para tratamento de erros, Melhora o retorno de erros para o cliente.
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext httpContext) =>
{
    return Results.Problem("Ocorreu um erro inesperado.");
});

app.Run();
