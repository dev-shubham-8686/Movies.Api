using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpPost(ApiEndpoints.Movies.Create)]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = request.MapToMovie();

        await _movieService.CreateAsync(movie);

        var movieResponse = movie.MapToResponse();

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movieResponse);

        //return Ok(movieResponse);
    }

    [HttpGet(ApiEndpoints.Movies.Get)]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie is null)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Movies.GetAll)]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieService.GetAllAsync();

        var moviesResponse = movies.MapToResponse();

        return Ok(moviesResponse);
    }

    [HttpPut(ApiEndpoints.Movies.Update)]
    [ProducesResponseType(typeof(MovieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateMovieRequest request)
    {
        var movie = request.MapToMovie(id);
        var updated = await _movieService.UpdateAsync(movie);
        if (!updated)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Movies.Delete)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _movieService.DeleteByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet(ApiEndpoints.Movies.GetRatings)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRatings([FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpPost(ApiEndpoints.Movies.AddRating)]
    [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddRating([FromRoute] Guid id)
    {
        var userId = HttpContext.GetUserId();

        return Ok();
    }

    [HttpDelete(ApiEndpoints.Movies.DeleteRating)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRating([FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpGet(ApiEndpoints.Ratings.GetUserRatings)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserRatings([FromRoute] Guid id)
    {
        var userId = HttpContext.GetUserId();

        return Ok();
    }
}
