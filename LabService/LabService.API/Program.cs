using LabService.API.Mapper;
using LabService.API.Services;
using LabServiceLibrary.Data;
using LabServiceLibrary.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LabServiceDBContext>(
    options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b=>b.MigrationsAssembly("LabService.API")));
builder.Services.AddScoped<ILabTestRepository,LabTestRepository>();
builder.Services.AddScoped<ILabReportRepository, LabReportRepository>();
builder.Services.AddScoped<ILabService,LabService.API.Services.LabService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();