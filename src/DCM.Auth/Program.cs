var builder = WebApplication.CreateBuilder(args);

// Configura��o dos servi�os
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();


app.Run();


