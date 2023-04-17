using HrApi.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var hrconnectionstring = builder.Configuration.GetConnectionString("hr-data");

if(hrconnectionstring is null)
{
    throw new Exception("No Connection String for the HR database");
}

builder.Services.AddDbContext<HrDataContext>(options =>
{
    options.UseSqlServer(hrconnectionstring);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
