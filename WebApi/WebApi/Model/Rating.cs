namespace WebApi.Model
{
    public class Rating
    {
        public int Id { get; set; }             // Khóa chính của bảng ratings
        public int UserId { get; set; }         // Khóa ngoại liên kết đến bảng users
        public int MovieId { get; set; }        // Khóa ngoại liên kết đến bảng movies
        public int RatingValue { get; set; }    // Giá trị đánh giá (từ 1 đến 5)

        // Liên kết với bảng users (User)
        public User User { get; set; }

        // Liên kết với bảng movies (Movie)
        public Movie Movie { get; set; }
    }
}
