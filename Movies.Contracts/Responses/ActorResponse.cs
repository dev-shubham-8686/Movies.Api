using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class ActorResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public int? Age { get; set; }
    }
}
