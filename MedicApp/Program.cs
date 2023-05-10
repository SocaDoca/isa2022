using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Middlewares;
using MedicApp.Models;
using MedicApp.Utils;
using MedicApp.Utils.AppSettings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;
using Quartz;
using MedicApp.Scheduler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    var jobKey = new JobKey("CronJobWorker");
    q.AddJob<CronJobWorker>(x => x.WithIdentity(jobKey));

    q.AddTrigger(x =>
    {
        x.ForJob(jobKey);
        x.WithCronSchedule("0 15 06 01 * ?");
    });
});

builder.Services.AddQuartzHostedService(x => x.WaitForJobsToComplete = true);
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                                            options.UseMySQL(connectionString));

builder.Services.Configure<SecretSettings>(builder.Configuration.GetSection("SecretSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<IEmailUtils, EmailUtils>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUserIntegration, UserIntegration>();
builder.Services.AddTransient<IClinicIntegration, ClinicIntegration>();
builder.Services.AddTransient<IRolesIntegration, RolesIntegration>();
builder.Services.AddTransient<IAppointmentIntegration, AppointmentIntegration>();
builder.Services.AddScoped<IUrlHelper>(x => {
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretSettings:Secret"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<TransactionMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();


    // custom jwt auth middleware
    //app.UseMiddleware<JwtMiddleware>();
}

app.UseRouting();
app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
