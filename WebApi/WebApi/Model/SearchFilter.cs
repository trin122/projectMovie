namespace WebApi.Model
{
    public class SearchFilter
    {
        public int Id { get; set; }             // Khóa chính (Primary Key) của bảng search_filters
        public int GenreId { get; set; }        // ID thể loại (Foreign Key đến bảng genres)
        public string Country { get; set; }     // Quốc gia của bộ phim
        public int ReleaseYear { get; set; }    // Năm phát hành bộ phim

        // Điều này sẽ giúp kết nối giữa bảng search_filters và bảng genres
        public Genre Genre { get; set; }        // Liên kết đến bảng genres
    }
}
