using Emr.API.Mapper;
using Emr.API.Services;
using EmrLibrary.Data;
using EmrLibrary.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IEMRService, EMRService>();
builder.Services.AddScoped<IEMRRepository,EMRRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<ITreatmentLogRepository, TreatmentLogRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<EmrDBContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b=>b.MigrationsAssembly("Emr.API")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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