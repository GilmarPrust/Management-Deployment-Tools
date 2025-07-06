using Microsoft.EntityFrameworkCore;
using ProjetoPedidos.Data;
using Microsoft.OpenApi.Models; // Adicionada para corrigir o erro CS1061  
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder; // Adicionada para corrigir o erro CS1061  

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=pedidos.db"));
builder.Services.AddScoped<ClienteService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjetoPedidos API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.MapControllers();
app.Run();

