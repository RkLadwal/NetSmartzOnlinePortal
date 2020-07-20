using System;
using System.ComponentModel.DataAnnotations;

namespace NetsmartzOnlinePortal.Domain.Model
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string strExpirationDate { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public short? Quantity { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Expiration Date")]
        [Required(ErrorMessage = "Expiration date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy MM dd}")]
        public DateTime? ExpirationDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool? IsActive { get; set; }

        public CategoryModel Category { get; set; }
    }
}
