using Microsoft.EntityFrameworkCore;
using Movies.Application.Database;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieDbContext _movieDbContext;
        public ActorRepository(MovieDbContext movieDbContext) {

            _movieDbContext = movieDbContext;

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
