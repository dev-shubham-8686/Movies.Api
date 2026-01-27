using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class GetUserRatingsResponse
    {
        public Guid? UserId { get; set; }
        public IEnumerable<GetUserRatingsItem> Ratings { get; set; } = Enumerable.Empty<GetUserRatingsItem>();
    }

    public class GetUserRatingsItem
    {
        public Guid? MovieId { get; set; }

        public int? Rating { get; set; }
    }
}
