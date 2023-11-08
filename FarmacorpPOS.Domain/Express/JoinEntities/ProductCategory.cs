

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FarmacorpPOS.Domain.Express.JoinEntities
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public int IdCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public DateTime CreationDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("IdCategory")]
        public virtual Category Category { get; set; }

        public void SetCreationDate(DateTime datetime)
        {
             CreationDate = datetime;
        }
    }
}
