using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using SwapiClient.Application;
using SwapiClient.Application.RequestHandler;
using SwapiClient.Infrastructure;
using SwapiClient.Middleware;
using SwapiClient.Swagger.OperationFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("Swapi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://swapi.dev/api/");

});
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<GetPeopleRequestHandler>();
    //config.RegisterServicesFromAssemblyContaining(typeof(GetPeopleRequestHandler));
});

builder.Services.AddScoped<ISwapiHttpClientHelper, SwapiHttpClientHelper>();
//builder.Services.AddScoped<IRepository<Person>, Repository<Person>>();
builder.Services.AddScoped<ISwapiWrapper, SwapiWrapper>();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<PublicKeyValidationMiddleware>();
app.UseMiddleware<ProtectedKeyValidationMiddleware>();
//Handle exceptions globally with this middleware.
app.UseMiddleware<ExceptionMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
