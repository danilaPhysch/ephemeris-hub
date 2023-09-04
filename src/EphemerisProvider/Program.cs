using EphemerisProvider.Adapters;
using EphemerisProvider.Api.Parameters;
using EphemerisProvider.Api.Validation;
using EphemerisProvider.Application;
using EphemerisProvider.Infrastructure.Configuration;
using EphemerisProvider.Infrastructure.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureConfig(builder.Configuration);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(AppConfiguration.ConnectionString));

builder.Services.AddHostedService<EphemerisLoaderHostedService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEphemerisProcessor, FakeEphemerisProcessor>();

builder.Services.AddScoped<IValidator<GetGlonassEphemerisParameters>, GetGlonassEphemerisParametersValidator>();
builder.Services.AddScoped<IGlonassEphemerisRepository, FakeGlonassEphemerisRepository>();

// builder.Host.UseSerilog();

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

await app.SeedAppDbContext();

await app.RunAsync();