using HRJobTracker.Data;
using HRJobTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(builder =>
{
    builder.AddPolicy(name: "AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr1"));
});

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//var app = builder.Build();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
