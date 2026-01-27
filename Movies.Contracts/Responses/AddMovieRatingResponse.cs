using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class AddMovieRatingResponse
    {
        public Guid? UserId { get; set; }

        public int? Rating { get; set; }

        public Guid? MovieId { get; set; }
    }
}
