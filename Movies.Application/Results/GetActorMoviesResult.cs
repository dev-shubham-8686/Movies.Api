using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Results
{
    public class GetActorMoviesResult
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public int? YearOfRelease { get; set; }
    }
}
