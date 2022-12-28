using MedicApp.Database;
using MedicApp.Integrations;
using MedicApp.Middlewares;
using MedicApp.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                                            options.UseMySQL(connectionString));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<IUserIntegration, UserIntegration>();
//builder.Services.AddTransient<IAppSettings, AppSettings>();

//builder.Services.AddTransient<IClinicIntegration, ClinicIntegration>();




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
