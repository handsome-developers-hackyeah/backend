using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Comptee.Jwt;
using Comptee.Middlewears;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5011, listenOptions =>
            {
                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                listenOptions.KestrelServerOptions.Limits.MaxRequestBodySize = long.MaxValue;
            });
        });
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
        }).AddXmlDataContractSerializerFormatters();

        builder.Services.AddMvc()
            .AddJsonOptions(c =>
            {
                c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                c.JsonSerializerOptions.MaxDepth = 32;
                c.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssemblies(new[] { typeof(Program).Assembly });
            });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "handsomedevelopers API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Jwt: Bearer {jwt token}"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
        });
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(JwtPolicies.Admin, JwtPolicies.AdminPolicy());
    config.AddPolicy(JwtPolicies.User, JwtPolicies.UserPolicy());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtAuth, JwtAuth>();
builder.Services.Configure<string>(builder.Configuration);

builder.Services.AddMediatR(typeof(Program));


var app = builder.Build();

app.UseSwaggerUI();
app.UseSwagger();
        
app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ExceptionHandler>();

app.MapGet("/", () => "Hello World!");

        
app.Run();