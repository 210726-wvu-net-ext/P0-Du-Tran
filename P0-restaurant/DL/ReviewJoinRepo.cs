using Models;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DL
{
    public class ReviewJoinRepo : IReviewJoinRepo
    {
        private P0_RestaurantRContext _context;
        public ReviewJoinRepo(P0_RestaurantRContext context)
        {
            _context = context;
        }

        public List<Models.ReviewJoin> GetReviewJoins()
        {
            return _context.ReviewJoins.Select(
                reviewjoin => new Models.ReviewJoin(reviewjoin.RestaurantId, reviewjoin.CustomerId, reviewjoin.ReviewId)
            ).ToList();
        }

        public Models.ReviewJoin AddAReviewJoin(Models.ReviewJoin reviewjoin)
        {
            _context.ReviewJoins.Add(
                new Entities.ReviewJoin {
                    RestaurantId = reviewjoin.RestaurantId,
                    CustomerId = reviewjoin.CustomerId,
                    ReviewId = reviewjoin.ReviewId
                }
            );
            _context.SaveChanges();
            return reviewjoin;
        }
    }
}