

using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Domain.ERP
{
    public class BarCode
    {
        public int BarCodeId { get; set; }
        public Guid BarCodeUniqueId { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; }
        public Product? Product { get; set; }
    }
}
