using FCG.Api.IoC;
using FCG.Api.Middlewares;
using FCG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("PaymentsService");
});

builder.Services.AddDbContext<FCGContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("Conn"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        )
    );
    opt.UseLazyLoadingProxies();
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 400;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Payments API", Version = "v1" , Description = "Generic Payment Processing Microservice" });
});

builder.Services.StartRegisterServices();

var app = builder.Build();

app.UseMiddleware<LogMiddleware>();
app.UseMiddleware<ErroMiddleware>();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(c =>
    {
        c.RoutePrefix = "docs";
        c.SpecUrl = "/swagger/v1/swagger.json";
        c.DocumentTitle = "Payments API Documentation";
    });
}

app.MapControllers();
app.Run();