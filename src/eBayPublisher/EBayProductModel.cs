namespace eBayPublisher
{
    public class EBayProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int EBayCategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
