

namespace FarmacorpPOS.Domain.Express
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
