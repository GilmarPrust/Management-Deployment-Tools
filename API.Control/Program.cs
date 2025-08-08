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
    cfg.AddProfile(new ProfileTaskProfile());
    cfg.AddProfile(new ManufacturerProfile());
    cfg.AddProfile(new OperatingSystemProfile());
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
builder.Services.AddScoped<IProfileTaskService, ProfileTaskService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IOperatingSystemService, OperatingSystemService>();


// Adicione os serviços de validação
builder.Services.AddValidatorsFromAssemblyContaining<Device_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeviceModel_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<MacAddress_Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<DriverPackCreateDTOValidator>();


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
app.MapGroup("/api/applications").WithTags("Application").MapApplicationEndpoints();
app.MapGroup("/api/appxpackages").WithTags("AppxPackage").MapAppxPackageEndPoints();
app.MapGroup("/api/deployprofiles").WithTags("DeployProfile").MapDeployProfileEndpoints();
app.MapGroup("/api/devices").WithTags("Device").MapDeviceEndpoints();
app.MapGroup("/api/devicemodels").WithTags("DeviceModel").MapDeviceModelsEndpoints();
app.MapGroup("/api/driverpacks").WithTags("DriverPack").MapDriverPackEndpoints();
app.MapGroup("/api/firmwares").WithTags("Firmware").MapFirmwareEndpoints();
app.MapGroup("/api/images").WithTags("Image").MapImageEndpoints();
app.MapGroup("/api/inventories").WithTags("Inventory").MapInventoryEndpoints();
app.MapGroup("/api/profiletasks").WithTags("ProfileTask").MapProfileTaskEndpoints();
app.MapGroup("/api/manufacturers").WithTags("Manufacturer").MapManufacturerEndpoints();
app.MapGroup("/api/operatingsystems").WithTags("OperatingSystem").MapOperatingSystemEndpoints();

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


// Migração automática ou criação de banco
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // ou db.Database.Migrate();
    db.SeedDefaultData(); // chama o método de seed para dados padrão.
}

app.Run();



var config = new UnattendConfig
{
    ComputerName = "PC-FILIAL01",
    DomainName = "empresa.local",
    DomainUser = "instalador",
    DomainPassword = "senha@123",
    OUPath = "OU=TI,DC=empresa,DC=local",
    TimeZone = "E. South America Standard Time",
    LocalAdminPassword = "SenhaSegura@2025"
};

var generator = new UnattendXmlGenerator();
var xml = generator.GenerateXml(config);

File.WriteAllText("unattend.xml", xml);

Console.WriteLine("Arquivo unattend.xml gerado com sucesso!");
