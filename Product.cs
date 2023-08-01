using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Case
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("discountPercentage")]
        public decimal DiscountPercentage { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("brand")]
        public string Brand { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("thumbnail")]
        public string Thumbnail { get; set; }

        // Navigation property for related images
        public List<ProductImage> Images { get; set; }
    }
}