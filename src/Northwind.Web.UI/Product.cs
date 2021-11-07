using System.ComponentModel.DataAnnotations;

namespace Northwind.Web.UI
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }

    public class ProductMetaData
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Please enter a product name (1 - 40 characters).")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Unitprice is required.")]
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000 for the unitprice.")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Units in stock is required.")]
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000 for the units in stock.")]
        public int UnitsInStock { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please select a supplier.")]
        public int SupplierID { get; set; }
    }
}
