

using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap ten danh muc toi thieu {0} ki tu")]
        public string Name { get; set; }
        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap ten danh muc toi thieu {0} ki tu")]
        public string Description { get; set; }
        public int Status { get; set; }
    }
}