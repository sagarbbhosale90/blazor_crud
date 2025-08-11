using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp_Crud.Model
{
    [Index(nameof(ProductName), IsUnique = true)]
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public required string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        public double? Quantity { get; set; }
    }
}
