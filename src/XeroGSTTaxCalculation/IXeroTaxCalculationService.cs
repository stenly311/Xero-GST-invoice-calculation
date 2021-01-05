using System.Collections.Generic;
using XeroGSTTaxCalculation.Types;

namespace XeroGSTTaxCalculation
{
    public interface IXeroTaxCalculationService
    {
        GSTTaxDetails CalculateGSTFromPriceGSTExclusive(IList<LineItem> lineItems, double GSTRate = 0.15);
    }
}