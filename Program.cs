using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudentCourse.Data;
using StudentCourse.Filters;
using StudentCourse.Mappings;
using StudentCourse.Repositories;
using StudentCourse.Repositories.Interfaces;
using StudentCourse.Services;
using StudentCourse.Services.Interfaces;
using StudentCourse.Validators;
using Microsoft.AspNetCore.Mvc;
using StudentCourse.Models;

var builder = WebApplication.CreateBuilder(args);

//add serilog 

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
Log.Information("Serilog is working!");
builder.Host.UseSerilog();

// Add services to the container.
//validation filter
builder.Services.AddControllers(options =>
{
    options.Filters
        .Add<ValidationFilter>();
});

//automapper registering
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(
    typeof(StudentMapper),
    typeof(CourseMapper),
    typeof(StudentCourseMapper));

//register Fluent Validations
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//appDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//repositories registration
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentCourseRepository,StudentCourseRepository>();
//services registration
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentCourseService,StudentCourseService>();

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
