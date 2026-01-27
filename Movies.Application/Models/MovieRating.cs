        using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    [Table("MovieRating")]
    public class MovieRating
    {
        [Key]
        public Guid MovieId { get; set; }

        public Guid UserId { get; set; }

        public int? Rating { get; set; }
    }
}
