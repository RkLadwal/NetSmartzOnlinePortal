using System;
using System.ComponentModel.DataAnnotations;

namespace NetsmartzOnlinePortal.Domain.Model
{
    public class CategoryModel
    {

        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(30)]
        public string CategoryName { get; set; }

        [Display(Name = "Price Range")]
        public string PriceRange { get; set; }

        [Required(ErrorMessage = "Min price is required")]
        [Display(Name = "Min Price")]
        [DataType(DataType.Currency)]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Max Price")]
        [Required(ErrorMessage = "Max price is required")]
        [DataType(DataType.Currency)]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [MaxLength()]
        public string Description { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Status")]
        public bool? IsActive { get; set; }
    }
}
