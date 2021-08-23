using Models;
using System.Collections.Generic;

namespace BL
{
    public interface IReviewBL
    {
         Review AddReview(Review review);
         List<Review> ViewReview();
         Review SearchReviewByReviewId(int id);
    }
}