using Microsoft.EntityFrameworkCore;
using Movies.Application.Database;
using Movies.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class RatingRepository: IRatingRepository
    {
        private readonly MovieDbContext _movieDbContext;

        public RatingRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<GetUserRatingsResult> GetUserRatingsAsync(Guid userId, CancellationToken token = default)
        {
            var result = new GetUserRatingsResult
            {
                UserId = userId,
                Ratings = new List<GetUserRatingsCollectionItem>()
            };

            var ratings = await _movieDbContext.MovieRatings
                .Where(r => r.UserId == userId)
                .Select(r => new GetUserRatingsCollectionItem
                {
                    MovieId = r.MovieId,
                    Rating = r.Rating
                }).ToListAsync(token);

            result.Ratings = ratings;

            return result;


        }
    }
}
