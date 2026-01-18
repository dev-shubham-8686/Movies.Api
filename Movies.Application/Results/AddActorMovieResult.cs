using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Results
{
    public class AddActorMovieResult
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public IEnumerable<AddActorMovieItem> Movies { get; set; } = Enumerable.Empty<AddActorMovieItem>();
    }

    public class AddActorMovieItem
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }
    }
}
