namespace MyStore.Domain.ProductMgmt.Queries
{
    public class ProductOrderSummaryDto
    {
        public string ProductName { get; set; }
        public int OrderCount { get; set; }
        public int TotalQuantitySold { get; set; }
    }
}
