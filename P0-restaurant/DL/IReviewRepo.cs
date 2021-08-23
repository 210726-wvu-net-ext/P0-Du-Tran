using Models;
using System.Collections.Generic;

namespace DL
{
    public interface IReviewRepo
    {
        Review AddReview(Review review);
        List<Review> GetReview();
        Review SearchReviewByReviewId(int id);
        
    }
}