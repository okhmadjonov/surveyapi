using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using surveyapi.Configuration;
using surveyapi.Data;
using surveyapi.Repositories;
using surveyapi.Services;
using surveyapi.SqlQueryService;
using surveyapi.TokenGenerator;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<SurveyDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IAuthRepository, AuthService>();
builder.Services.AddScoped<ITokenRepository, TokenGenerator>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddScoped<IChoiceRepository, ChoiceService>();
builder.Services.AddScoped<IQuestionRepository, QuestionService>();
builder.Services.AddScoped<ISurveyRepository, SurveyService>();
builder.Services.AddScoped<IRoleRepository, RoleService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleService>();
builder.Services.AddScoped<IPersonRepository, PersonService>();

builder.Services.AddServiceFunctionsConfiguration()
            .AddErrorFilter()
            .AddSwaggerService(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<SurveyDbContext>();

    var migrationScriptsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MigrationScripts");
    var sqlFiles = Directory.GetFiles(migrationScriptsDirectory, "*.sql");
    var tableCreationService = new TableCreationService(connectionString, migrationScriptsDirectory);
    tableCreationService.CreateTablesFromScripts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseStaticFiles();
//app.UseMiddleware<SurveyMiddleware>();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
