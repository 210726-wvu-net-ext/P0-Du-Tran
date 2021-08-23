using Models;
using DL;
using System.Collections.Generic;

namespace BL
{
    public class ReviewJoinBL : IReviewJoinBL
    {
        private IReviewJoinRepo _repo;
        public ReviewJoinBL(IReviewJoinRepo repo)
        {
            _repo = repo;
        }
        public List<ReviewJoin> ViewReviewJoins()
        {
            return _repo.GetReviewJoins();
        }

        public ReviewJoin AddAReviewJoin(ReviewJoin reviewjoin)
        {
            return _repo.AddAReviewJoin(reviewjoin);
        }
    }
}