

using FarmacorpPOS.Domain.Express.JoinEntities;

namespace FarmacorpPOS.Domain.Express
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public ICollection<Product> Products  { get; } = new List<Product>();
        public ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();

        public int? ParentCategoryId {  get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }


    }
}
