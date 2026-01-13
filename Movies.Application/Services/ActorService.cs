using Movies.Application.Models;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository) {

            _actorRepository = actorRepository;
        }

        public Task<bool> CreateAsync(Actor actor, CancellationToken token = default)
        {
            return _actorRepository.CreateAsync(actor, token);
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return _actorRepository.DeleteByIdAsync(id, token);
        }

        public Task<IEnumerable<Actor>> GetAllAsync(CancellationToken token = default)
        {
            return _actorRepository.GetAllAsync(token);
        }

        public Task<Actor?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return _actorRepository.GetByIdAsync(id, token);
        }

        public Task<Actor?> UpdateAsync(Actor actor, CancellationToken token = default)
        {
            return _actorRepository.UpdateAsync(actor, token);
        }
    }
}
