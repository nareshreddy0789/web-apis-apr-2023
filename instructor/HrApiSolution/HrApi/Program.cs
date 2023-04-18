using AutoMapper;
using HrApi.Domain;
using HrApi.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// the default web application builder has about 190+ "Services" that do all the work in your API.



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var hrConnectionString = builder.Configuration.GetConnectionString("hr-data");

if(hrConnectionString is null)
{
    throw new Exception("No Connection String for HR Database");
}

builder.Services.AddDbContext<HrDataContext>(options =>
{
    options.UseSqlServer(hrConnectionString);
});

// "Slow" - so we are "eagerly" creating this at application startup.

var mapperConfiguration = new MapperConfiguration(options =>
{
    options.AddProfile<Departments>();
});

builder.Services.AddSingleton<IMapper>(mapperConfiguration.CreateMapper());
builder.Services.AddSingleton<MapperConfiguration>(mapperConfiguration);

// before the application is built is above here services.
var app = builder.Build();
// after the application is built, how do you want to handle HTTP requests and responses.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Open API - the documentation.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers(); // it is going to create a phone directory.
// route table:
    // if someone does a GET /deparments:
            // create an instance of the DepartmentsController
               // to create an instance of this, you have to give it a HrDataContext
            // Call the GetDepartments method.
    // if someone does a get /departments/(SOME INTEGER)
        // create the departmentcontroller and call getbyid with that integer.

app.Run(); // Starting the web server, and "blocking here"
