using Models;
using DL.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DL
{
    public class ReviewRepo : IReviewRepo
    {
        private P0_RestaurantRContext _context;
        public ReviewRepo(P0_RestaurantRContext context)
        {
            _context = context;
        }
        public Models.Review  AddReview(Models.Review review)
        {
            _context.Reviews.Add(
                new Entities.Review{
                    Comment = review.Comment,
                    Rating = (review.Rating),
                    Time = DateTime.Now
                }
            );
            _context.SaveChanges();

            return review;
        }

        public List<Models.Review> GetReview()
        {
            return _context.Reviews.Select(
                review => new Models.Review(review.Id, review.Comment, review.Rating)
            ).ToList();
        }

        public Models.Review SearchReviewByReviewId(int id)
        {
            Entities.Review foundReview = _context.Reviews
                .FirstOrDefault(review => review.Id == id);
            if(foundReview != null)
            {
                return new Models.Review(foundReview.Id, foundReview.Comment, foundReview.Rating);
            }
            return new Models.Review();
        }
        
    }
}