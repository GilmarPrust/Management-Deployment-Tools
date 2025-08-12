var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.Run();


