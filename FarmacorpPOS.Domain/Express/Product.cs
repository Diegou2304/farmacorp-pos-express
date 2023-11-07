
using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express.JoinEntities;

namespace FarmacorpPOS.Domain.Express
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price {  get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Observations { get; set; }
        
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public ICollection<ExpressSale> ExpressSales { get; } = new List<ExpressSale>();
        public ICollection <Category> Categories { get; } = new List<Category>();
        public ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();
        public BarCode? BarCode { get; set; }
        public ErpProduct? ErpProduct { get; set; }

    }
}
