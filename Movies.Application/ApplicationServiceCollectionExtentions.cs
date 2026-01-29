using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Database;
using Movies.Application.Repositories;
using Movies.Application.Services;

namespace Movies.Application
{
    public static class ApplicationServiceCollectionExtentions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            
            services.AddScoped<IMovieService, MovieService>();

            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IActorService, ActorService>();

            services.AddScoped<IActorRepository, ActorRepository>();

            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Scoped);

            return services;
        }


        public static IServiceCollection AddDatabase(this IServiceCollection services, string? connectionString = null)
        {
            // Factory as singleton (string-only config)
            services.AddSingleton<IDbConnectionFactory, MssqlConnectionFactory>();


            // If connectionString is null, EF will read from IConfiguration inside Program.cs
            services.AddDbContext<MovieDbContext>((sp, options) =>
            {
              
                var cfg = sp.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
                var cs = connectionString ?? cfg["Database:ConnectionString"]
                    ?? throw new InvalidOperationException("Database:ConnectionString not found.");
                options.UseSqlServer(cs);
            });

            return services;    

        }

    }


}
