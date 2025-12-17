

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using NuGet.Protocol.Plugins;
using WebBanHang.Repository.Validation;

namespace WebBanHang.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap ten san pham toi thieu {0} ki tu")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yeu cau nhap mo ta toi thieu {0} ki tu")]
        public string Description { get; set; }

        public string Slug { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }

        public string Image { get; set; } = "noname.jpg";

        [NotMapped]
        [FileExtensionAtrribute]
        public IFormFile? ImageUpload { get; set; }
    }
}