using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//-----------------------------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("Connection")));
//-----------------------------------------------------------------------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepositoryBase<User>, EFRepository<User>>();
builder.Services.AddScoped<UserMapping>();
//--------------------------------------------------------------------------------------




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
