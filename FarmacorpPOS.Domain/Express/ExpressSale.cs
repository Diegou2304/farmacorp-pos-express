namespace FarmacorpPOS.Domain.Express
{
    public class ExpressSale
    {
        public int ExpressSaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string ClientFullName { get; set; } = string.Empty;
        public string ProductName { get; set; }
        public Guid UniqueProductCode { get; set; } = Guid.NewGuid();
        public double Price { get; set; }
        public string Discount { get; set; }
        public string TotalPrice { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }

    }
}
