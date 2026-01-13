using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Controllers
{
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorsController(IActorService actorService) {

            _actorService = actorService;
        }


        [HttpGet(ApiEndpoints.Actors.GetAll)]
        [ProducesResponseType(typeof(ActorsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var result = await _actorService.GetAllAsync(token);

            return Ok(result.MapToResponse());  
        }

        [HttpPost(ApiEndpoints.Actors.Create)]
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateActorRequest request, CancellationToken token) {

            var actor = request.MapToRequest();

            var create = await _actorService.CreateAsync(actor, token);

            var getActor = await _actorService.GetByIdAsync(actor.Id);

            if (getActor == null) {

                return NotFound();
            }

            var response = getActor.MapToResponse();

            return CreatedAtAction(nameof(Get), new { id = actor.Id }, response);

        }

        [HttpPut(ApiEndpoints.Actors.Update)]
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateActorRequest request, CancellationToken token)
        {

            var actor = request.MapToRequest(id);

            var update = await _actorService.UpdateAsync(actor, token);

            if(update is null)
            {
                return NotFound();
            }

            var result = update.MapToResponse();

            return Ok(result);
        }

        [HttpDelete(ApiEndpoints.Actors.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var result = await _actorService.DeleteByIdAsync(id, token);

            if(!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet(ApiEndpoints.Actors.Get)]
        [ProducesResponseType(typeof(ActorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {
            var result = await _actorService.GetByIdAsync(id, token);

            if(result is null)
            {
                return NotFound();  
            }

            return Ok(result.MapToResponse());
        }    
    }
}
