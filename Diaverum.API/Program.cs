using Asp.Versioning;
using AutoMapper;
using Diaverum.API.ExceptionHandling;
using Diaverum.API.SwaggerConfig;
using Diaverum.Data;
using Diaverum.Mapping;
using Diaverum.Service;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
    config.AddConfiguration(builder.Configuration.GetSection("Logging"));
});

builder.Services.AddControllers();
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<DiaverumDbContext>(options =>
{
    options.UseSqlServer(configuration.GetValue<string>("Database:ConnectionString"));
});

var mappingConfig = new MapperConfiguration(config =>
{
    config.AddProfiles([
        new DiaverumProfile()
    ]);
}, logger);
var mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IDiaverumItemService, DiaverumItemService>();

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapOpenApi();

app.UseSwagger();

app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();


app.MapControllers();

app.Run();

public partial class Program { }