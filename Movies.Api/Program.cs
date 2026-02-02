
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movies.Api.Mapping;
using Movies.Application;
using Movies.Application.Database;
using System.Text;
using System.Text.RegularExpressions;

namespace Movies.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy(AuthConstants.AdminUserPolicyName,
                    p => p.RequireClaim(AuthConstants.AdminUserClaimName, "true"));

                x.AddPolicy(AuthConstants.TrustedMemberPolicyName,
                    p => p.RequireAssertion(c =>
                        c.User.HasClaim(m => m is { Type: AuthConstants.AdminUserClaimName, Value: "true" }) ||
                        c.User.HasClaim(m => m is { Type: AuthConstants.TrustedMemberClaimName, Value: "true" })));
            });

            builder.Services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1.0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");

            }).AddMvc();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplication();
            builder.Services.AddDatabase(config["Database:ConnectionString"]!);

            var app = builder.Build();

            // --- Database initialization: run migrations and optional SQL script ---
            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var logger = services.GetRequiredService<ILogger<Program>>();
            //    try
            //    {
            //        var context = services.GetRequiredService<Movies.Application.Database.MovieDbContext>();

            //        // Apply EF Core migrations (recommended). This is idempotent.
            //        //context.Database.Migrate();
            //        context.Database.EnsureCreated();

            //        // Execute script if present in content root /Scripts/init.sql
            //        var scriptPath = Path.Combine(app.Environment.ContentRootPath, "Scripts", "init.sql");
            //        if (File.Exists(scriptPath))
            //        {
            //            ExecuteSqlScript(context, scriptPath, logger);
            //            logger.LogInformation("Executed DB init script: {path}", scriptPath);
            //        }
            //        else
            //        {
            //            logger.LogInformation("DB init script not found at {path}", scriptPath);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError(ex, "An error occurred while initializing the database.");
            //        // For safety fail fast in early dev; in production you might choose to continue or signal health checks.
            //        throw;
            //    }
            //}
            // ------------------------------------------------------------------


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseMiddleware<ValidationMappingMiddleware>();

            app.MapControllers();

            var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
            await dbInitializer.InitializeAsync();

            app.Run();
        }

        // Local helper: split on GO (case-insensitive) and execute batches
        //static void ExecuteSqlScript(Movies.Application.Database.MovieDbContext db, string path, ILogger logger)
        //{
        //    var sql = File.ReadAllText(path);
        //    // Split by lines that contain only GO, case-insensitive
        //    var batches = Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //    foreach (var batch in batches)
        //    {
        //        var trimmed = batch.Trim();
        //        if (string.IsNullOrWhiteSpace(trimmed)) continue;
        //        try
        //        {
        //            db.Database.ExecuteSqlRaw(trimmed);
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.LogError(ex, "Failed to execute SQL batch from {path}. Batch preview: {preview}", path, trimmed.Length > 200 ? trimmed[..200] : trimmed);
        //            throw;
        //        }
        //    }
        //}
    }
}
