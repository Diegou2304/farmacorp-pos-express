namespace FarmacorpPOS.Domain.Express
{
    public class ExpressSale
    {
        public int IdExpressSale { get; set; }
        public DateTime SaleDate { get; set; }
        public string ClientFullName { get; set; } = string.Empty;
        public string ProductName { get; set; }
        public string UniqueProductCode { get; set; }
        public double Price { get; set; }
        public string Discount { get; set; }
        public string TotalPrice { get; set; }

    }
}
