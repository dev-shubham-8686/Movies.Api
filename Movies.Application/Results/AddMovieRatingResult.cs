using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Results
{
    public class AddMovieRatingResult
    {
        public Guid? UserId { get; set; }

        public int? Rating { get; set; }

        public Guid? MovieId { get; set; }
    }
}
