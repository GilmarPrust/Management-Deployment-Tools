using API.Control.Mappings;
using API.Control.Models;
using API.Control.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
// Adicione serviços ao contêiner.
builder.Services.AddValidatorsFromAssemblyContaining<DeviceModel_Create_Validator>(); // Certifique-se de que o pacote FluentValidation.Extensions.DependencyInjection está instalado.

builder.Services.AddAutoMapper(typeof(EntityProfiles)); // se seu Profile estiver em EntityProfiles

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register your services here, e.g.:
// builder.Services.AddScoped<IYourService, YourServiceImplementation>();
// Example: builder.Services.AddScoped<IDeviceService, DeviceService>();
// Example: builder.Services.AddScoped<IDeviceModelService, DeviceModelService>();
// Example: builder.Services.AddScoped<IInventoryService, InventoryService>();
// Example: builder.Services.AddScoped<IProfileDeployService, ProfileDeployService>();
// Example: builder.Services.AddScoped<IApplicationService, ApplicationService>();
// Example: builder.Services.AddScoped<IDriverPackService, DriverPackService>();
// Example: builder.Services.AddScoped<IFirmwareService, FirmwareService>();
// Example: builder.Services.AddScoped<IAppxPackageService, AppxPackageService>();
// Example: builder.Services.AddScoped<IDriverPackageService, DriverPackageService>();
// Example: builder.Services.AddScoped<IAppxPackageService, AppxPackageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();
/*app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Control V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});*/



/*DeviceModel dmodel = new DeviceModel()
{
    Manufacturer = "Manufacturer",
    Model = "Model",
    Type = "Type"
};*/

DeviceModel dmodel0 = new DeviceModel("Manufacturer", "Models", "Types");

app.Run();
