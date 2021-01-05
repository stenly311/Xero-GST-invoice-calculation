using System;
using Xunit;
using XeroGSTTaxCalculation;
using XeroGSTTaxCalculation.Types;
using System.Collections.Generic;
using FluentAssertions;

namespace XeroGSTTaxCalculationTest
{
    public class XeroTaxCalculationServiceTests
    {
        IXeroTaxCalculationService _xeroTaxCalculationService = new XeroTaxCalculationService();
        List<LineItem> _lineItems = new List<LineItem>();

        /// <summary>
        /// Testing GST rate value out of range
        /// </summary>
        /// <param name="gst"></param>
        [Theory()]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1.1)] 
        public void CalculateGSTFromPriceGSTExclusive_ShouldThrowExceptionTest(double gst)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _xeroTaxCalculationService.CalculateGSTFromPriceGSTExclusive(_lineItems, gst));
        }

        /// <summary>
        /// Happy day scenarion with prices and quantities for rounding 
        /// </summary>
        [Fact]
        public void CalculateGSTFromPriceGSTExclusive_Test()
        {
            _lineItems.AddRange(new[]
                { 
                    new LineItem { Code = "1", Price = 1.456m, Quantity = 5 },
                    new LineItem { Code = "2", Price = 8.999m, Quantity = 5 }
                }
            );

            // Price   Q   GST      Subtotal
            // -------------------------------
            // 1.456   5   1.1      7.3
            // 8.999   5   6.75     45


            //              7.85    52.3

            var result = _xeroTaxCalculationService.CalculateGSTFromPriceGSTExclusive(_lineItems, 0.15);
            
            result.GST.Should().Be(7.85m);
            result.Subtotal.Should().Be(52.3m);
            result.Total.Should().Be(result.GST + result.Subtotal);            
        }
    }
}
