using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Prodacts
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [MinLength(1, ErrorMessage = "اسم المنتج يجب أن يحتوي على حرف واحد على الأقل")]
        public string Name { get; set; }
        [Required(ErrorMessage = "وصف المنتج مطلوب")]
        public string Description { get; set; }
        public string Status { get; set; }
        public string Image {  get; set; }
       
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

    }
}
