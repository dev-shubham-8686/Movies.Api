using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class GetMovieRatingsResponse
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public IEnumerable<GetMovieRatingsItem> Ratings { get; set; } = Enumerable.Empty<GetMovieRatingsItem>();
    }

    public class GetMovieRatingsItem
    {

        public Guid? UserId { get; set; }

        public int? Rating { get; set; }
    }
}
