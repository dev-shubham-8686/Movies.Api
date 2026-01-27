using Movies.Application.Models;
using Movies.Application.Results;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Mapping;

public static class ContractMapping
{

    #region Movie
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList()
        };
    }

    public static Movie MapToMovie(this UpdateMovieRequest request, Guid id)
    {
        return new Movie
        {
            Id = id,
            Title = request.Title,
            YearOfRelease = request.YearOfRelease,
            Genres = request.Genres.ToList()
        };
    }

    public static MovieResponse MapToResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            YearOfRelease = movie.YearOfRelease,
            Genres = movie.Genres
        };
    }

    public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
    {
        return new MoviesResponse
        {
            Items = movies.Select(MapToResponse)
        };
    }

    public static GetMovieRatingsResponse MapToResponse(this GetMovieRatingsResult result)
    {
        return new GetMovieRatingsResponse
        {
            Id = result.Id,
            Title = result.Title,
            Ratings = result.Ratings.Select(c => new GetMovieRatingsItem
            {
                UserId = c.UserId,
                Rating = c.Rating
            })
        };
    }

    public static AddMovieRatingResponse MapToResponse(this AddMovieRatingResult result)
    {
       return new AddMovieRatingResponse
       {
           UserId = result.UserId,
           Rating = result.Rating,
           MovieId = result.MovieId
       };
    }

    public static GetUserRatingsResponse MapToResponse(this GetUserRatingsResult result)
    {
        return new GetUserRatingsResponse
        {
            UserId = result.UserId,
            Ratings = result.Ratings.Select(c => new GetUserRatingsItem
            {
                MovieId = c.MovieId,
                Rating = c.Rating
            })
        };
    }

    #endregion Movie

    #region Actor

    public static ActorsResponse MapToResponse(this IEnumerable<Actor> actors) {

        return new ActorsResponse
        {
            Items = actors.Select(c => new ActorResponse
            {
                Id = c.Id,
                Name = c.Name,
                LastName = c.LastName,
                Age = c.Age,
            })
        };
    
    }

    public static ActorResponse MapToResponse(this Actor request)
    {
        return new ActorResponse
        {
            Id = request.Id,
            Name = request.Name,
            LastName = request.LastName,
            Age = request.Age
        };
    }

    public static Actor MapToRequest(this CreateActorRequest request)
    {
        return new Actor
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            LastName = request.LastName,
            Age = request.Age,
        };
    }

    public static Actor MapToRequest(this UpdateActorRequest request, Guid id)
    {
        return new Actor
        {
            Id = id,
            Name = request.Name,
            LastName = request.LastName,
            Age = request.Age
        };
    }


    public static GetActorMoviesResponse MapToResponse(this IEnumerable<GetActorMoviesResult> response)
    {
        return new GetActorMoviesResponse
        {
            Items = response.Select(c => new GetActorMovieResponse
            { 
              Id = c.Id,
              Title = c.Title,
              YearOfRelease = c.YearOfRelease
            })
        };
    }

    public static AddActorMovieResponse MapToResponse(this AddActorMovieResult response)
    {
        return new AddActorMovieResponse
        {
            Id= response.Id,
            Name= response.Name,
            LastName = response.LastName,
            Movies = response.Movies.Select(c => new Contracts.Responses.AddActorMovieItem
            {
                Id = c.Id,
                Title = c.Title,
            }),

        };
    }


    #endregion

}
