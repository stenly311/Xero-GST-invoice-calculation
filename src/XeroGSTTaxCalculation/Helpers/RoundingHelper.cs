using System;

namespace XeroGSTTaxCalculation.Helpers
{
    public static class RoundingHelper
    {
        /// <summary>
        /// Rounds value onto 2 dp with AwayFromZero rounding midpoint
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal RoundTo2DP(this decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}
