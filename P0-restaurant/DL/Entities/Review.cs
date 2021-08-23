using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Review
    {
        public Review()
        {
            ReviewJoins = new HashSet<ReviewJoin>();
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Time { get; set; }
        public string Rating { get; set; }

        public virtual ICollection<ReviewJoin> ReviewJoins { get; set; }
    }
}
