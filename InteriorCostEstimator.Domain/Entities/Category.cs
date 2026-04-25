using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InteriorCostEstimator.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;


        public int NumberOfProducts { get; set; }
        //public ICollection<Product> Products { get; set; }
        //    = new List<Product>();
    }
}
