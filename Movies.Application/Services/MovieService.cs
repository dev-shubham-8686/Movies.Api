using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;

        private readonly IRatingRepository _ratingRepository;

        public MovieService(IMovieRepository movieRepository, IRatingRepository ratingRepository)
        {

            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
        }

        public Task<AddMovieRatingResult> AddRatingAsync(Guid id, Guid userId, int? rating, CancellationToken token = default)
        {
            return _movieRepository.AddRatingAsync(id, userId, rating, token);
        }

        public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
        {
            return await _movieRepository.CreateAsync(movie, token);
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _movieRepository.DeleteByIdAsync(id, token);
        }

        public async Task DeleteRatingAsync(Guid id, Guid userId, CancellationToken token = default)
        {
           await _movieRepository.DeleteRatingAsync(id, userId, token);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
        {
            return await _movieRepository.GetAllAsync(token);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _movieRepository.GetByIdAsync(id, token);
        }

        public async Task<GetMovieRatingsResult> GetRatingsAsync(Guid id, CancellationToken token = default)
        {
            return await _movieRepository.GetRatingsAsync(id, token);
        }

        public async Task<GetUserRatingsResult> GetUserRatingsAsync(Guid userId, CancellationToken token = default)
        {
            return await _ratingRepository.GetUserRatingsAsync(userId, token);
        }

        public async Task<bool> UpdateAsync(Movie movie, CancellationToken token = default)
        {
            return await _movieRepository.UpdateAsync(movie, token);
        }
    }
}
