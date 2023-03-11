using OpenTelemetryCSharp;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var serviceName = "StreetService";
builder.Services.AddDbContext<ContactsAPIDbContext>(options =>options.UseInMemoryDatabase("ContactsDb"));
builder.Services.AddOpenTelemetry()
    .WithTracing(builder => builder
        .AddAspNetCoreInstrumentation()
        .AddJaegerExporter()
        .AddConsoleExporter()
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: "1.0.0"))
        );

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ActivityMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
