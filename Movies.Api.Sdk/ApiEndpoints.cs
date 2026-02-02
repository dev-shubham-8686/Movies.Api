namespace Movies.Api.Sdk;

public static class ApiEndpoints
{
    private const string ApiBase = "/api";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string GetActors = $"{Base}/{{id}}/actors";
        public const string AddActor = $"{Base}/{{id}}/actors";
        public const string RemoveActor = $"{Base}/{{id}}/actors/{{actorId}}";
        public const string GetRatings = $"{Base}/{{id}}/ratings";
        public const string AddRating = $"{Base}/{{id}}/ratings";
        public const string DeleteRating = $"{Base}/{{id}}/ratings";
    }


    public static class Actors
    {
        private const string Base = $"{ApiBase}/actors";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string GetMovies = $"{Base}/{{id}}/movies";
        public const string AddMovie = $"{Base}/{{id}}/movies";
        public const string RemoveMovie = $"{Base}/{{id}}/movies/{{movieId}}";
    }

    public static class Ratings
    {
        private const string Base = $"{ApiBase}/ratings";

        public const string GetUserRatings = $"{Base}/me";
    }
}
