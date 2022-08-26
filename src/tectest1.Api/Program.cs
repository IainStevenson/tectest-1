using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using tectest1.Api.Domain;

using tectest1.Api.Database;
using tectest1.Api.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("tectest1");


// Add services to the container.
builder.Services
    .AddSingleton<ICSVConvertor<RawMeterReading>, RawMeterCSVConverter>()
    .AddSingleton<IValidator<RawMeterReading>, RawMeterReadingValidator>()
    .AddSingleton<IRawContentValidator, CustomCSVContentValidator>()
    .AddDbContext<DatabaseContext>(opts =>
    {
        opts.UseSqlServer(connectionString);
     
    })
    .AddScoped<ICreateRepository<MeterReadingUpload>, MeterReadingUploadRepository>();


builder.Services.AddControllers(options =>
{
    var jsonInputFormatter = options.InputFormatters
        .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
        .Single();
    //jsonInputFormatter.SupportedMediaTypes.Add("text/plain");
    jsonInputFormatter.SupportedMediaTypes.Add("application/json");
    jsonInputFormatter.SupportedMediaTypes.Add("application/octet-stream");

}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(RawMeterReadingFilePostHandler));

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
