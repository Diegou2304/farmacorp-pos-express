

using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Domain.ERP
{
    public class ErpProduct
    {
        public int ErpProductId { get; set; }
        public decimal Cost { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Stock { get; set; }
        public Product? Product { get; set; }

    }
}
