using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class ActorsResponse
    {
        public IEnumerable<ActorResponse> Items { get; set; } = Enumerable.Empty<ActorResponse>();
    }
}
