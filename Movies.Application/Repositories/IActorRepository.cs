using Movies.Application.Models;
using Movies.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IActorRepository
    {
        Task<bool> CreateAsync(Actor actor, CancellationToken token = default);

        Task<Actor?> GetByIdAsync(Guid id, CancellationToken token = default);

        Task<IEnumerable<Actor>> GetAllAsync(CancellationToken token = default);

        Task<Actor?> UpdateAsync(Actor actor, CancellationToken token = default);

        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

        Task<IEnumerable<GetActorMoviesResult>> GetMoviesAsync(Guid id, CancellationToken token = default);

        Task<AddActorMovieResult?> AddMovieAsync(Guid id, Guid movieId, CancellationToken token = default);

        Task<Movie?> RemoveMovieAsync(Guid id, Guid movieId, CancellationToken token = default);
    }
}
