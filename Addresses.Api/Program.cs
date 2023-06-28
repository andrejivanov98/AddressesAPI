using Addresses.DataContext.EntityFramework;
using Addresses.Services.Abstractions.Repository;
using Addresses.Services.Abstractions.Service;
using Addresses.Services.Repositories;
using Addresses.Services.Services.Command;
using Addresses.Services.Services.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<AddressesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AddressesDbContext>();

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

// Register services
builder.Services.AddScoped<IAddressesQueryService, AddressesQueryService>();
builder.Services.AddScoped<IAddressesCommandService, AddressesCommandService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address API", Version = "v1", Description = "A simple API for address management." });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
