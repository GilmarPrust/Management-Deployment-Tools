
var builder = WebApplication.CreateBuilder(args);

// Configura��o dos servi�os
builder.Services.AddEndpointsApiExplorer()
    .AddOpenApi()
    .AddAppDbContext(builder.Configuration)
    .AddSwaggerConfiguration()
    .AddAutoMapperConfiguration()
    .AddServiceRegistrations();

// Registro autom�tico de todos os validators de todos os assemblies carregados
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssemblyContaining<Program>();



builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));

// Configura��o do CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Pipeline HTTP e middlewares
if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomMiddlewares();

// Rota de erro customizada
app.Map("/error", (HttpContext httpContext) =>
{
    return Results.Problem("Ocorreu um erro inesperado.");
});

// Endpoint de exemplo para garantir que o Swagger funcione
app.MapGet("/hello", () => "Hello, world!").WithName("HelloWorld").WithTags("Test");

// Migra��o autom�tica ou cria��o de banco
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Garante que as migra��es est�o aplicadas
    dbContext.SeedDefaultData();  // Chama o m�todo de seed
}

// Chama o registro autom�tico de endpoints
app.MapEndpoints();

app.Run();



// C�digo tempor�rio para gera��o do unattend.xml
// Considere mover para um arquivo separado se n�o fizer parte do fluxo principal da API
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
