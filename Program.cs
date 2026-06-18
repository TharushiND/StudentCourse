using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
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
    options.Filters.Add<ValidationFilter>();
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


//automapper registering
//builder.Services.AddAutoMapper(typeof(Program)); this duplicates the same automapper registering.
builder.Services.AddAutoMapper(
    typeof(StudentMapper),
    typeof(CourseMapper),
    typeof(StudentCourseMapper));


//register Fluent Validations
builder.Services.AddFluentValidationAutoValidation(); /*This turns on the automatic checking system in ASP.NET Core.
automatically check if there is a validation rule for it before running the controller code.*/
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>(); /*This searches your project and finds your actual validation rule classes,
and registers them into the application's memory.*/



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//appDbContext,connection string
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
app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.Run();
