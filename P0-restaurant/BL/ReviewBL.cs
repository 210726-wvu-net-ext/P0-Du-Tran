using Models;
using DL;

namespace BL
{
    public class ReviewBL : IReviewBL
    {
        private IReviewRepo _repo;

        public ReviewBL(IReviewRepo repo)
        {
            _repo = repo;
        }

        public Review AddReview(Review review)
        {
            return _repo.AddReview(review);
        }

        public List<Review> ViewReview()
        {
            return _repo.GetReview();
        }

        public Review SearchReviewByReviewId(int id)
        {
            return _repo.SearchReviewByReviewId(id);
        }

        
    }
}