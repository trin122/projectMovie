namespace WebApi.Model
{
    public class Genre
    {
        public int Id { get; set; }          // Khóa chính của bảng genres
        public string Genre_Name { get; set; } // Tên thể loại phim

        // Danh sách các bộ phim thuộc thể loại này (nếu có)
        public List<Movie> Movies { get; set; }
    }
}
