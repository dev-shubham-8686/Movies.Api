using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Results
{
    public class GetMovieRatingsResult
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public IEnumerable<GetMovieRatingsCollectionItem> Ratings { get; set; } = Enumerable.Empty<GetMovieRatingsCollectionItem>();
    }

    public class GetMovieRatingsCollectionItem
    {
        public Guid? UserId { get; set; }

        public int? Rating { get; set; }
    }
}
