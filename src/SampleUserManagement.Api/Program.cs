using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleUserManagement.Api.Configurations;
using SampleUserManagement.Api.Endpoints;
using SampleUserManagement.Application.Configurations;
using SampleUserManagement.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerSetup();

// Persistence
builder.Services.AddDatabaseSetup(builder.Configuration);

// Applicaton
builder.Services.AddMediatRSetup();
builder.Services.AddAMapperSetup();
builder.Services.AddValidationSetup();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapRoleEndpoints();
app.MapProductEndpoints();

app.Run();
