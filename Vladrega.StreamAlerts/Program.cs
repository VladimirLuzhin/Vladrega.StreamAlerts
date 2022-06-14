using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Vladrega.StreamAlerts.Authentication;
using Vladrega.StreamAlerts.Connectors.SignalR;
using Vladrega.StreamAlerts.Core;
using Vladrega.StreamAlerts.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(config =>
{
    var isPortExist = int.TryParse(Environment.GetEnvironmentVariable("APPLICATION_PORT"), out var port);
    config.ListenAnyIP(isPortExist ? port : 5006);
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtOptions.Issuer,
            IssuerSigningKey = JwtOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateLifetime = true
        };
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/updates")))
                {
                    context.Token = accessToken;
                }
                
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSignalR();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddSingleton<AlertsBus>();
builder.Services.AddSingleton<SignalRAlertsHub>();
builder.Services.AddSingleton<IAlertsHub>(provider => provider.GetService<SignalRAlertsHub>());
builder.Services.AddHostedService<AlertsSender>();

var app = builder.Build();

app.UseCors(configuration =>
{
    configuration.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalRAlertsHub>("/updates");
app.UseHealthChecks("/health");

app.Run();
