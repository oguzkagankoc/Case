using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Case
{
    // ProductImage model representing the "product_images" table
    [Table("product_images")]
    public class ProductImage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("image_url")]
        public string? ImageUrl { get; set; }

        // Navigation property for the related product
        public Product? Product { get; set; }
    }
}
