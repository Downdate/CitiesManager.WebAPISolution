using Asp.Versioning;
using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));


});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Swagger configuration
//builder.Services.AddEndpointsApiExplorer(); // generates description for all endpoints
//builder.Services.AddSwaggerGen(options => { 
//    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
    

//}); // generates OpenAPI specification

//builder.Services.AddApiVersioning(config => {
//    config.ApiVersionReader = new UrlSegmentApiVersionReader();
//    //config.ApiVersionReader = new QueryStringApiVersionReader();// Reads version number from request query string called "api-version"
//    //config.ApiVersionReader = new HeaderApiVersionReader("api-version"); // Reads version number from request header called "api-version"

//    //default api version declaration
//    config.DefaultApiVersion = new ApiVersion(1, 0);
//    config.AssumeDefaultVersionWhenUnspecified = true;
//});



builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Cities Manager API";
        document.Info.Version = "v1";
        document.Info.Description = "Cities Manager REST API";

        return Task.CompletedTask;
    });
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Cities Manager API";
        document.Info.Version = "v2";
        document.Info.Description = "Cities Manager REST API";

        return Task.CompletedTask;
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();

    // Or:
    // options.ApiVersionReader = new QueryStringApiVersionReader();
    // options.ApiVersionReader = new HeaderApiVersionReader("api-version");

    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Configure the HTTP request pipeline.

app.UseHsts();

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();
