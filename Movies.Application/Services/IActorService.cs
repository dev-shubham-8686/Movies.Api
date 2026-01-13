using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public interface IActorService
    {
        Task<bool> CreateAsync(Actor actor, CancellationToken token = default);

        Task<Actor?> GetByIdAsync(Guid id, CancellationToken token = default);

        Task<IEnumerable<Actor>> GetAllAsync(CancellationToken token = default);

        Task<Actor?> UpdateAsync(Actor actor, CancellationToken token = default);

        Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
    }
}
