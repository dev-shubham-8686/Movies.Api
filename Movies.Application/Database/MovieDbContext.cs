using Microsoft.EntityFrameworkCore;
using Movies.Application.Models;
using Movies.Application.Results;

namespace Movies.Application.Database
{
    public class MovieDbContext: DbContext
    {

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GetActorMoviesResult>(eb =>
            {
                eb.HasNoKey();                 // keyless query type
                eb.ToView(null);               // not mapped to a real view
            });
        }

        public async Task<IEnumerable<GetActorMoviesResult>> GetActorMoviesQueryAsync(
            Guid actorId,
            CancellationToken token = default)
        {
            var sql = @"
                         SELECT 
                             m.Id          AS Id,
                             m.Title       AS Title,
                             m.YearOfRelease AS YearOfRelease
                         FROM MovieActor AS ma
                         INNER JOIN Movie AS m ON m.Id = ma.MovieId
                         WHERE ma.ActorId = {0};";

            var result = await Set<GetActorMoviesResult>()
                .FromSqlRaw(sql, actorId)      // parameterized with placeholder {0}
                .AsNoTracking()
                .ToListAsync(token);

            return result;
        }




    }


}
