namespace POE_ST10152183.Models
{
    public class Products
    {
        public Products()
        {
        }

        public Products(int productId, string productName, string productType, int price, int farmerId, DateTime uploadDate)
        {
            ProductId = productId;
            ProductName = productName;
            ProductType = productType;
            Price = price;
            FarmerId = farmerId;
            UploadDate = uploadDate;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int Price { get; set; }
        public int FarmerId {get;set;}
        public DateTime UploadDate { get; set; }
    }
}
