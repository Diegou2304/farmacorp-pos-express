

using FarmacorpPOS.Domain.Express;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacorpPOS.Domain.ERP
{
    public class BarCode
    {
        public int BarCodeId { get; set; }
        public Guid BarCodeUniqueId { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; }
        public Product? Product { get; set; }

        public BarCode()
        {

        }
        public static BarCode Create(bool isActive, Product product)
        {
            var barCode = new BarCode
            {
                BarCodeUniqueId = Guid.NewGuid(),
                IsActive = isActive,
                Product = product
            };

            return barCode;
            
        }
    }

   
}
