// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Movies.Api.Sdk;
using Movies.Api.Sdk.Consumer;
using Refit;
using System.Text.Json;


//var moviesApi = RestService.For<IMoviesApi>("http://localhost:5062");

var services = new ServiceCollection();

services.AddHttpClient()
    .AddSingleton<AuthTokenProvider>()
    .AddRefitClient<IMoviesApi>(s => new RefitSettings
    {
        AuthorizationHeaderValueGetter = async (request, cancellationToken) => await s.GetRequiredService<AuthTokenProvider>().GetTokenAsync()
    })
 .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5062"));

var provider = services.BuildServiceProvider();

var moviesApi = provider.GetRequiredService<IMoviesApi>();


var movies = await moviesApi.GetMoviesAsync();

Console.WriteLine(JsonSerializer.Serialize(movies));

var movie = await moviesApi.GetMovieAsync("F84E5615-7696-46F7-9510-796C30E3D2D9");

Console.WriteLine(JsonSerializer.Serialize(movie)); 

Console.ReadLine();