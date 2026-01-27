using Movies.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IRatingRepository
    {
        Task<GetUserRatingsResult> GetUserRatingsAsync(Guid userId , CancellationToken token = default);
    }
}
