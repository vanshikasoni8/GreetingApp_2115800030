using BussinessLayer.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using BussinessLayer.Service;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using Microsoft.EntityFrameworkCore;
using BussinessLayer.Interface;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
try
{
    logger.Info("Application Starting...");



    var builder = WebApplication.CreateBuilder(args);

    // NLog ko use karne ke liye configure 

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole(); //  console logging is enabled

    builder.Services.AddControllers();

    //dependancy Injection 

    builder.Services.AddDbContext<GreetingAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Register repository layer
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    // Register business layer
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();

    var app = builder.Build();

    // Swagger Enable

    app.UseSwagger();
    app.UseSwaggerUI();


    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Application failed to start!");
    throw;
}
finally
{
    LogManager.Shutdown();
}