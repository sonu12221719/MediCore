using AppointmentLibrary.Data;
using AppointmentLibrary.Repository;
using AppointmentService.API.Mapper;
using AppointmentService.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IAppointmentService,AppointmentService.API.Services.AppointmentService>();
builder.Services.AddScoped<IScheduleService, Scheduleservice>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppointmentDBContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b=>b.MigrationsAssembly("AppointmentService.API")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();