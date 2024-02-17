using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleUserManagement.Api.Configurations;
using SampleUserManagement.Api.Endpoints;
using SampleUserManagement.Application.Configurations;
using SampleUserManagement.Infrastructure.Configurations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Swagger
builder.Services.AddSwaggerSetup();

// Persistence
builder.Services.AddDatabaseSetup(builder.Configuration);

// Applicaton
builder.Services.AddMediatRSetup();
builder.Services.AddMapperSetup();
builder.Services.AddValidationSetup();

// Register HttpContextAcessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.UseHttpsRedirection();

// Register Endpoints
app.MapUserEndpoints();
app.MapRoleEndpoints();

app.Run();
