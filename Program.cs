using Microsoft.EntityFrameworkCore;
using ProjectFor7COMm.Data;
using ProjectFor7COMm.Models;
using ProjectFor7COMm.Repositories;
using ProjectFor7COMm.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

builder.Services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
builder.Services.AddScoped<IUserService, UserService>();

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
