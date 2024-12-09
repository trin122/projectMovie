using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Serialization;
namespace WebApi.Model
{
    public class User
    {
        public int Id { get; set; }            // ID của người dùng
        public string Email { get; set; }      // Địa chỉ email (unique)
        
        public string Password { get; set; }   // Mật khẩu (lưu trữ mật khẩu an toàn)
        public string Name { get; set; }       // Tên của người dùng

      
    }
}
