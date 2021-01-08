using System;
using System.Collections.Generic;
using XeroGSTTaxCalculation.Helpers;
using XeroGSTTaxCalculation.Types;

namespace XeroGSTTaxCalculation
{
    public class XeroTaxCalculationService : IXeroTaxCalculationService
    {
        /// <summary>
        /// Calculates GST out of line items price, GST exclusive
        /// </summary>
        /// <param name="lineItems"></param>
        /// <param name="gSTRate">GST rate as decimal value in range of (0,1> </param>
        /// <returns></returns>
        public GSTTaxDetails CalculateGSTFromPriceGSTExclusive(IList<LineItem> lineItems, double gSTRate = 0.15)
        {
            if (gSTRate <= 0 || gSTRate > 1)
                throw new ArgumentOutOfRangeException(nameof(gSTRate));

            decimal gSTTotal = 0, subtotal = 0;

            foreach (var item in lineItems)
            {
                var itemTotal = item.Price.RoundTo2DP() * item.Quantity;

                gSTTotal += (itemTotal * (decimal)gSTRate).RoundTo2DP();
                subtotal += itemTotal;
            }

            return new GSTTaxDetails
            {
                GST = gSTTotal,
                Subtotal = subtotal,
                Total = gSTTotal + subtotal
            };
        }

        /// <summary>
        /// Calculates GST out of line items price, GST inclusive
        /// </summary>
        /// <param name="lineItems"></param>
        /// <param name="gSTRate"></param>
        /// <returns></returns>
        public GSTTaxDetails CalculateGSTFromPriceGSTInclusive(IList<LineItem> lineItems, double gSTRate = 0.15)
        {
            if (gSTRate <= 0 || gSTRate > 1)
                throw new ArgumentOutOfRangeException(nameof(gSTRate));

            gSTRate += 1;

            decimal subTotal = 0, totalGST = 0, total = 0;

            foreach (var item in lineItems)
            {
                var itemTotal = (item.Price * item.Quantity).RoundTo2DP();
                var itemTotalLessTax = (itemTotal / (decimal)gSTRate).RoundTo2DP();

                totalGST += itemTotal - itemTotalLessTax;
                subTotal += itemTotalLessTax;
                total += itemTotal;
            }

            return new GSTTaxDetails
            {
                GST = totalGST,
                Subtotal = subTotal,
                Total = total
            };
        }
    }
}
