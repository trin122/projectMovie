namespace WebApi.Model
{
    public class WatchHistory
    {
        public int Id { get; set; }        // Khóa chính của bảng watch_history
        public int User_Id { get; set; }    // Khóa ngoại liên kết đến bảng users
        public int Movie_Id { get; set; }   // Khóa ngoại liên kết đến bảng movies

        // Liên kết với bảng users (User)
        public User User { get; set; }

        // Liên kết với bảng movies (Movie)
        public Movie Movie { get; set; }
    }
}
