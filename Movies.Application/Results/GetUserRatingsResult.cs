using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Results
{
    public class GetUserRatingsResult
    {
        public Guid? UserId { get; set; }
        public IEnumerable<GetUserRatingsCollectionItem> Ratings { get; set; } = Enumerable.Empty<GetUserRatingsCollectionItem>();
    }

    public class GetUserRatingsCollectionItem
    {
        public Guid? MovieId { get; set; }

        public int? Rating { get; set; }
    }
}
