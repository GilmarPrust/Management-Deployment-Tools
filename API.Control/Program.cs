using API.Control.Data;
using API.Control.Endpoints;
using API.Control.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=dcm.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API Minimal",
        Version = "v1",
        Description = "Documentação da API com Swagger"
    });
});

builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<DeviceModelService>();


var app = builder.Build();

// Ativa o Swagger em ambiente de desenvolvimento (ou sempre, se preferir)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Mapear endpoints
app.MapDeviceEndpoints();


app.Run();