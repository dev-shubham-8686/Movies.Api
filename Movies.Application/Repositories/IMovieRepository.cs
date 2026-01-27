using Movies.Application.Models;
using Movies.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IMovieRepository
    {
        Task<bool> CreateAsync(Movie movie, CancellationToken token = default);

        Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default);

        Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default);

        Task<bool> UpdateAsync(Movie movie, CancellationToken token = default);

        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

        Task<GetMovieRatingsResult> GetRatingsAsync(Guid id, CancellationToken token = default);

        Task<AddMovieRatingResult> AddRatingAsync(Guid id, Guid userId, int? rating, CancellationToken token = default);

        Task DeleteRatingAsync(Guid id, Guid userId, CancellationToken token = default);

    }
}
