using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IReviewJoinRepo
    {
         List<ReviewJoin> GetReviewJoins();
         ReviewJoin AddAReviewJoin(ReviewJoin reviewjoin);
    }
}