using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Case.Models;

namespace Case.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        public List<Product> Products { get; set; }

        public ProductsModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            // Fetch products from the database and populate the Products list
            Products = _dbContext.Products.ToList();
        }
    }
}
