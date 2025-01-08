namespace WebApi.Model
{
    public class Changepassword
    {
        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }  // Thêm trường xác nhận mật khẩu mới
    }
}
