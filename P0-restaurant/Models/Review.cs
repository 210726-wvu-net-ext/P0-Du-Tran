namespace Models
{
    public class Review
    {
        public Review(){}
        public Review(int id, string comment, string rating)
        {
            this.Id = id;
            this.Comment = comment;
            this.Rating = rating;
        }
        public Review(int id, string comment, DateTime time, string rating):this(id, comment, rating)
        {
            this.Time = time;
        }
        public int Id {get; set;}
        public string Comment {get; set;}
        public DateTime Time {get; set;}
        public string Rating {get; set;}
    }
}