using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Results;
using Movies.Application.Validators;
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
        private readonly IValidator<Actor> _actorValidator;

        public ActorService(IActorRepository actorRepository, IValidator<Actor> actorValidator) {

            _actorRepository = actorRepository;
            _actorValidator = actorValidator;
        }

        public async Task<AddActorMovieResult?> AddMovieAsync(Guid id, Guid movieId, CancellationToken token = default)
        {
            return await _actorRepository.AddMovieAsync(id, movieId, token);
        }

        public async Task<bool> CreateAsync(Actor actor, CancellationToken token = default)
        {
            await _actorValidator.ValidateAndThrowAsync(actor, cancellationToken: token);
            return await _actorRepository.CreateAsync(actor, token);
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _actorRepository.DeleteByIdAsync(id, token);
        }

        public async Task<IEnumerable<Actor>> GetAllAsync(CancellationToken token = default)
        {
            return await _actorRepository.GetAllAsync(token);
        }

        public async Task<Actor?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _actorRepository.GetByIdAsync(id, token);
        }

        public async Task<IEnumerable<GetActorMoviesResult>> GetMoviesAsync(Guid id, CancellationToken token = default)
        {
            return await _actorRepository.GetMoviesAsync(id, token);
        }

        public async Task<Movie?> RemoveMovieAsync(Guid id, Guid movieId, CancellationToken token = default)
        {
            return await _actorRepository.RemoveMovieAsync(id, movieId, token);
        }

        public async Task<Actor?> UpdateAsync(Actor actor, CancellationToken token = default)
        {
            return await _actorRepository.UpdateAsync(actor, token);
        }
    }
}
