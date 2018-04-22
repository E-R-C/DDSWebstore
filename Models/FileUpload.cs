using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DDSWebstore.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name="Title")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name="Image1")]
        public IFormFile UploadImage { get; set; }
    }
}