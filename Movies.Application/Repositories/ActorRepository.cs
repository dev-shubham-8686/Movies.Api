using Microsoft.EntityFrameworkCore;
using Movies.Application.Database;
using Movies.Application.Models;
using Movies.Application.Results;

namespace Movies.Application.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieDbContext _movieDbContext;

        private readonly IMovieRepository _movieRepository;
        public ActorRepository(MovieDbContext movieDbContext, IMovieRepository movieRepository)
        {

            _movieDbContext = movieDbContext;
            _movieRepository = movieRepository;
        }

        public async Task<AddActorMovieResult?> AddMovieAsync(Guid id, Guid movieId, CancellationToken token = default)
        {
           var getActor = await _movieDbContext.Actors.FindAsync(id, token);

            if(getActor != null)
            {
                var getMovie = await _movieRepository.GetByIdAsync(id, token);

                if(getMovie != null)
                {
                   await _movieDbContext.AddAsync(new MovieActor { Actorid = id.ToString(), MovieId = movieId.ToString() }, token);

                   var result = await _movieDbContext.SaveChangesAsync(token);

                   var movies = await _movieDbContext.GetActorMoviesQueryAsync(id, token);

                    var data = new AddActorMovieResult
                    {
                        Id = id,
                        Name = getActor.Name,
                        LastName = getActor.LastName,
                        Movies = movies.Select(x => new AddActorMovieItem { Id = x.Id, Title = x.Title })
                    };

                    return data;

                }
            }

            return null;
        }

        public async Task<bool> CreateAsync(Actor actor, CancellationToken token = default)
        {
            await _movieDbContext.AddAsync(actor, token);

            var result = await _movieDbContext.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            var getActor = await _movieDbContext.Actors.FindAsync(id, token);

            if (getActor != null) {

               _movieDbContext.Remove(getActor);

                var result = await _movieDbContext.SaveChangesAsync(token);

                return result > 0;
            }

            return false;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync(CancellationToken token = default)
        {
            return await _movieDbContext.Actors.ToListAsync(token);
        }

        public async Task<Actor?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var getActor = await _movieDbContext.Actors.FindAsync(id, token);

            if (getActor != null) {

                return getActor;
            
            }

            return null;

        }

        public async Task<IEnumerable<GetActorMoviesResult>> GetMoviesAsync(Guid id, CancellationToken token = default)
        {
           return await _movieDbContext.GetActorMoviesQueryAsync(id, token);
        }

        public async Task<Movie?> RemoveMovieAsync(Guid id, Guid movieId, CancellationToken token = default)
        {
            string strId = id.ToString(); 
            string strMovieId = movieId.ToString();
            var getActorMovie = await _movieDbContext.MovieActors.Where(c => c.Actorid == strId && c.MovieId == strMovieId).FirstOrDefaultAsync();

            if (getActorMovie != null) {

               _movieDbContext.Remove(getActorMovie);
               await _movieDbContext.SaveChangesAsync(token);
            
               var getMovie = await _movieDbContext.Movies.FindAsync(movieId, token);

                return getMovie ?? null;
            }

            return null;

        }

        public async Task<Actor?> UpdateAsync(Actor actor, CancellationToken token = default)
        {
            var getActor = await _movieDbContext.Actors.FindAsync(actor.Id, token);

            if (getActor != null) {

                getActor.Name = actor.Name;
                getActor.LastName = actor.LastName;
                getActor.Age = actor.Age;

                var result = await _movieDbContext.SaveChangesAsync(token);

                return getActor;
            }

            return null;

        }
    }
}
