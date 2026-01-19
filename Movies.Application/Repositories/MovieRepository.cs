using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Database;
using Movies.Application.Models;

namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieDbContext _movieDbContext;

        public MovieRepository(MovieDbContext movieDbContext) {

            _movieDbContext = movieDbContext;

        }

        //private readonly List<Movie> _movies = new();
        public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
        {
           await _movieDbContext.Movies.AddAsync(movie, token);

           var result = await _movieDbContext.SaveChangesAsync(token);

           return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {

            var getMovie = await _movieDbContext.Movies.FindAsync(id, token);

            if (getMovie is null) { 
            
                return false;

            }

            _movieDbContext.Remove(getMovie);

            var result = await _movieDbContext.SaveChangesAsync(token);

            return result > 0;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
        {
            var movies = await _movieDbContext.Movies.ToListAsync(token);

            return movies.AsEnumerable();
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var movie = await _movieDbContext.Movies.SingleOrDefaultAsync(c => c.Id == id, token);

            return movie ?? null;
        }

        public async Task<bool> UpdateAsync(Movie movie, CancellationToken token = default)
        {
            
            var getMovie = await _movieDbContext.Movies.FindAsync(movie.Id, token);

            if (getMovie is null) { 

                return false;
            
            }

            getMovie.Title = movie.Title;
            getMovie.YearOfRelease = movie.YearOfRelease;

            await _movieDbContext.SaveChangesAsync(token);

            return true;   
        }
    }
}
