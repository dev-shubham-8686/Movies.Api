using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class GetActorMoviesResponse
    {
        public IEnumerable<GetActorMovieResponse> Items = Enumerable.Empty<GetActorMovieResponse>();    
    }


    public class GetActorMovieResponse
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public int? YearOfRelease { get; set; }
    }
}
