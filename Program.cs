using LifeHub_APIs.Data;
using LifeHub_APIs.services;
using LifeHub_APIs.services.CalendarEventsServices;
using LifeHub_APIs.services.DailyTasksServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
        // Converting every enums values to be string.
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LifeHubDatabase"))
);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDailyTasksService, DailyTasksService>();
builder.Services.AddScoped<ICalendarEventsService, CalendarEventsService>();
builder.Services.AddScoped<ITestRelations, TestRelationsService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecretKey"]!)
                ),
            ValidateIssuerSigningKey = true,
        };
    }
    )
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
