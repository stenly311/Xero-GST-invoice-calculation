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

        #region GST Inclusive       

        /// <summary>
        /// Testing GST rate value out of range
        /// </summary>
        /// <param name="gst"></param>
        [Theory()]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1.1)]
        public void CalculateGSTFromPriceGSTInclusive_ShouldThrowExceptionTest(double gst)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _xeroTaxCalculationService.CalculateGSTFromPriceGSTInclusive(_lineItems, gst));
        }

        /// <summary>
        /// Happy day scenarion with prices and quantities for rounding 
        /// </summary>
        [Fact]
        public void CalculateGSTFromPriceGSTInclusive_Test()
        {
            _lineItems.AddRange(new[]
                {
                    new LineItem { Code = "1", Price = 10.456m, Quantity = 5 },
                    new LineItem { Code = "2", Price = 5.3433m, Quantity = 7 },
                    new LineItem { Code = "3", Price = 80.999m, Quantity = 5 }
                }
            );

            // Price   Q   Less - GST    Line Subtotal   GST
            // ---------------------------------------------
            // 10.456  5   45.46            52.28       6.82
            // 5.3433  7   32.52            37.4        4.88
            // 80.999  5   352.17           405         52.83

            //              Subtotal        Total       GST
            // ---------------------------------------------
            //              430.15          494.68      64.53


            var result = _xeroTaxCalculationService.CalculateGSTFromPriceGSTInclusive(_lineItems, 0.15);

            result.GST.Should().Be(64.53m);
            result.Subtotal.Should().Be(430.15m);
            result.Total.Should().Be(494.68m);
            result.Total.Should().Be(result.GST + result.Subtotal);
        }

        #endregion
    }
}
