using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Database;
using Movies.Application.Models;
using Movies.Application.Results;

namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly MovieDbContext _movieDbContext;

        

        public MovieRepository(MovieDbContext movieDbContext) {

            _movieDbContext = movieDbContext;

        }

        public async Task<AddMovieRatingResult> AddRatingAsync(Guid id, Guid userId, int? rating, CancellationToken token = default)
        {
            await _movieDbContext.MovieRatings.AddAsync(new MovieRating
            {
                MovieId = id,
                UserId = userId,
                Rating = rating
            }, token);

            await _movieDbContext.SaveChangesAsync(token);

            return new AddMovieRatingResult
            {
                MovieId = id,
                UserId = userId,
                Rating = rating
            };
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

        public async Task DeleteRatingAsync(Guid id, Guid userId, CancellationToken token = default)
        {
            await _movieDbContext.MovieRatings
                .Where(c => c.MovieId == id && c.UserId == userId)
                .ExecuteDeleteAsync(token);

            await _movieDbContext.SaveChangesAsync(token);

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

        public async Task<GetMovieRatingsResult> GetRatingsAsync(Guid id, CancellationToken token = default)
        {

            var result = new GetMovieRatingsResult();

            var movie = await GetByIdAsync(id, token);

            if(movie is null) {
                return result;
            }

            var ratings = await _movieDbContext.MovieRatings
                .Where(c => c.MovieId == id)
                .Select(c => new GetMovieRatingsCollectionItem
                {
                    UserId = c.UserId,
                    Rating = c.Rating
                })
                .ToListAsync(token);

            result.Id = movie.Id;
            result.Title = movie.Title;
            result.Ratings = ratings;

            return result;
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
