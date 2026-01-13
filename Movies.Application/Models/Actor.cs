using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    [Table("Actor")]
    public class Actor
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }
    }
}
