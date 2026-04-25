using Microsoft.EntityFrameworkCore;
using Pharmacy.API.Mapper;
using Pharmacy.API.Middleware;
using Pharmacy.API.Services;
using PharmacyLibrary.Data;
using PharmacyLibrary.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PharmacyDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("Pharmacy.API")));

builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IDispenseRepository, DispenseRepository>();
builder.Services.AddScoped<IPharmacyService, PharmacyService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();  
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