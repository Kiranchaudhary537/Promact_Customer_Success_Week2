using Auth0.AspNetCore.Authentication;
using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Promact.CustomerSuccess.Platform.Data;
using Serilog;
using Serilog.Events;
using System.Security.Claims;
using Volo.Abp.Data;

namespace Promact.CustomerSuccess.Platform;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var loggerConfiguration = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console());

        if (IsMigrateDatabase(args))
        {
            loggerConfiguration.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning);
            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
        }

        Log.Logger = loggerConfiguration.CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            if (IsMigrateDatabase(args))
            {
                builder.Services.AddDataMigrationEnvironment();
            }
            await builder.AddApplicationAsync<PlatformModule>();
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                          .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                          {
                              options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
                              options.Audience = builder.Configuration["Auth0:Audience"];
                              options.TokenValidationParameters = new TokenValidationParameters
                              {
                                  NameClaimType = ClaimTypes.NameIdentifier,
                                  ValidateIssuer = true,
                                  ValidateAudience = true,
                                  ValidateLifetime = true,
                                  ValidateIssuerSigningKey = false,
                                  ValidIssuer = "Auth0",
                                  ValidAudience = "http://localhost:4200/"
                              };
                          })
                          .AddOpenIdConnect("  ", options =>
                          {
                              options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
                              options.ClientId = builder.Configuration["Auth0:ClientId"];
                              options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
                              options.ResponseType = OpenIdConnectResponseType.Code;
                              options.Scope.Clear();
                              options.Scope.Add("openid");
                              options.Scope.Add("profile");
                              options.SaveTokens = true;
                              options.CallbackPath = new PathString("/callback");
                              options.ClaimsIssuer = "Auth0";
                          });

            var app = builder.Build();
            await app.InitializeApplicationAsync();

            if (IsMigrateDatabase(args))
            {
                await app.Services.GetRequiredService<PlatformDbMigrationService>().MigrateAsync();
                return 0;
            }

            Log.Information("Starting Promact.CustomerSuccess.Platform.");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Promact.CustomerSuccess.Platform terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static bool IsMigrateDatabase(string[] args)
    {
        return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
    }
}
