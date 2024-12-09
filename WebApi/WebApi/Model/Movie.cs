namespace WebApi.Model
{
    public class Movie
    {
        public int Id { get; set; }               // ID của bộ phim (Primary Key)
        public string Title { get; set; }         // Tên bộ phim
        public string Description { get; set; }   // Mô tả bộ phim
        public int ReleaseYear { get; set; }      // Năm phát hành bộ phim
        public string VideoUrl { get; set; }      // URL video của bộ phim
        public string Genres { get; set; }        // Danh sách thể loại (comma-separated list)
        public string Filters { get; set; }       // Danh sách bộ lọc (comma-separated list)
        public string Countries { get; set; }     // Danh sách quốc gia (comma-separated list)
        public string HinhAnh { get; set; }        // Hình ảnh minh họa (Lưu dưới dạng byte array)
    }
}
