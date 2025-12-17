


using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap ten thuong hieu toi thieu {0} ki tu")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap ten mo ta toi thieu {0} ki tu")]
        public string Description { get; set; }

        public string Slug { get; set; }

        public int Status { get; set; }
    }
}