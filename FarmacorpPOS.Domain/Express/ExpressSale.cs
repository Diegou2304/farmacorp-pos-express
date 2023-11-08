namespace FarmacorpPOS.Domain.Express
{
    public class ExpressSale
    {
        public int ExpressSaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public string ClientFullName { get; set; } = string.Empty;
        public string ProductName { get; set; }
        public Guid UniqueProductCode { get; set; } 
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }

        public ExpressSale()
        {
            
        }

        public static ExpressSale CreateSale(string clientName, Product product, int quantity, double discount)
        {
            return new ExpressSale
            {
                SaleDate = DateTime.Now,
                ClientFullName = clientName,
                ProductName = product.ProductName,
                UniqueProductCode = product.ErpProduct!.UniqueCode,
                Quantity = quantity,
                Price = product.Price * quantity, 
                Discount = discount,
                TotalPrice = (product.Price - (product.Price * discount)) * quantity,
                ProductId = product.ProductId
            };
        }
    }
}
