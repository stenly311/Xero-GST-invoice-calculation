namespace XeroGSTTaxCalculation.Types
{
    public record LineItem
    {
        public string Code { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
    }   
}
