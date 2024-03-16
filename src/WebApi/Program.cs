using TechTrain.ReusableModules.WebApi;
using TechTrain.ReusableModules.WebApi.Common;
using Webapi.Extensions;
using static TechTrain.ReusableModules.WebApi.Common.ApiValidatorServiceExtensions;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// get IConfiguration
var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);
builder.Services.AddApiValidator(configuration)
    .AddApiValidator<ApiPathValidator>()
    .AddApiValidator<NoopValidator>()
    .AddApiValidator<ControllerValidator>()
    .AddApiValidator<GroupCollectionValidator>();
     
//  .AddApiValidator<PathParameterValidator>();

// Add ValidationEventLogger

builder.Services.AddSingleton<IValidationEventLogger, ValidationEventLogger>();

var app = builder.Build();
app.UseMiddleware<ApiValidatorMiddleware>();

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
