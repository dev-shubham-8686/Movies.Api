using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public int? YearOfRelese { get; set; }

        public List<string> Genres { get; set; } = new();
    }
}
