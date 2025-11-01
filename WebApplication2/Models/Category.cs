using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public List<Prodacts> Prodacts { get; set; }
    }
}
