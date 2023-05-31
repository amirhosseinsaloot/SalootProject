﻿using Api.Extensions;
using FluentValidation.AspNetCore;
using Infrastructure.Setting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var services = builder.Services;

var configuration = builder.Configuration;

var applicationSettings = configuration
                          .GetSection(nameof(ApplicationSettings))
                          .Get<ApplicationSettings>();

const string CORS_POLICY = "CorsPolicy";
SerilogExtensions.Register(applicationSettings);

// Add services to the container.
ConfigureServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigurePipeline(app);

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddConfiguration(configuration);

    // Database services
    services.AddDbContext(applicationSettings.DatabaseSetting);

    services.AddCustomIdentity(applicationSettings.IdentitySetting);

    services.AutoMapperRegistration();

    services.AddJwtAuthentication(applicationSettings.JwtSetting);

    services.AddApplicationDependencyRegistration(applicationSettings);

    services.AddSwagger();

    services.AddHttpContextAccessor();

    // Add service and create Policy with options
    services.AddCors(options =>
    {
        options.AddPolicy(name: CORS_POLICY,
            builder => builder
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .SetIsOriginAllowed(origin => true)  // Allow any origin
                      .AllowCredentials());                // Allow credentials
    });

    services.AddMvc();
    services.AddControllers();
    services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<Program>();
}

void ConfigurePipeline(IApplicationBuilder app)
{
    app.IntializeDatabase();

    app.UseGlobalExceptionHandler();

    app.UseRouting();

    // Enable Cors
    app.UseCors(CORS_POLICY);

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.RegisterSwaggerMidlleware();
}


// Make the implicit Program class public so test projects can access it
public partial class Program { }