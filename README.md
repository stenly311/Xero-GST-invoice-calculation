# Xero invoice GST calculation
Use for any system integrating with Xero API services to generate an invoice from charges (line items).

Calculate your invoice GST subtotal, total and GST same way as Xero does. Use line item price with GST inclusive or exclusive.

## Quick quide of how to use the nuget package in code

```c#

        static void Main(string[] args)
        {         

            IXeroTaxCalculationService service = new XeroTaxCalculationService();

            var data = new[] { 
                new LineItem { Code = "code_1", Price = 12m, Quantity = 10 }, 
                new LineItem { Code = "code_2", Price = 120m, Quantity = 8 } 
            };

            var invoiceDetails = service.CalculateGSTFromPriceGSTInclusive(data, 0.25);

            Console.WriteLine(invoiceDetails);
            Console.ReadLine();
        }

```

Happy coding.
Any comments welcomed! 
