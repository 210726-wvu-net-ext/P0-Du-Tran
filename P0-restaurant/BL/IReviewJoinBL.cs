using Models;
using System.Collections.Generic;

namespace BL
{
    public interface IReviewJoinBL
    {
         List<ReviewJoin> ViewReviewJoins();
         ReviewJoin AddAReviewJoin(ReviewJoin reviewjoin);
    }
}