namespace XeroGSTTaxCalculation.Types
{
    public record GSTTaxDetails
    {
        /// <summary>
        /// Total GST calculated
        /// </summary>
        public decimal GST { get; init; }

        /// <summary>
        /// Total excluding GST
        /// </summary>
        public decimal Subtotal { get; init; }

        /// <summary>
        /// Subtotal including GST
        /// </summary>
        public decimal Total { get; init; }
    }
}
