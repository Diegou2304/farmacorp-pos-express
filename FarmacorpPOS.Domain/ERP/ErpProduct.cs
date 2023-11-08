

using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Domain.ERP
{
    public class ErpProduct
    {
        public int ErpProductId { get; set; }
        public double Cost { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Guid UniqueCode { get;  set; }
        public int Stock { get; set; }
        public Product? Product { get; set; }

        public ErpProduct()
        {
            
        }

        public static ErpProduct CreateErpProduct(double cost, int stock)
        {
            return new ErpProduct
            {
                Cost = cost,
                Stock = stock,
                RegistrationDate = DateTime.Now,
                UniqueCode = Guid.NewGuid(),
            };
        }
    }

    
}
