namespace Movies.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/movies";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string GetActors = $"{Base}/{{id:guid}}/actors";
        public const string AddActor = $"{Base}/{{id:guid}}/actors";
        public const string RemoveActor = $"{Base}/{{id:guid}}/actors/{{actorId:guid}}";
    }


    public static class Actors
    {
        private const string Base = $"{ApiBase}/actors";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string GetMovies = $"{Base}/{{id:guid}}/movies";
        public const string AddMovie = $"{Base}/{{id:guid}}/movies";
        public const string RemoveMovie = $"{Base}/{{id:guid}}/movies/{{movieId:guid}}";
    }
}
