using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Core
{
    public class IncentivesTestBase
    {
        public Product BuildProduct(SupportedIncentiveType supportedIncentives, string identifier = "test_identifier", decimal price = 100.00m, string uom = "tons")
        {
            return new Product
            {
                Id = 10,
                SupportedIncentives = supportedIncentives,
                Identifier = identifier,
                Price = price,
                Uom = uom
            };
        }

        public Rebate BuildRebate(IncentiveType incentiveType, string identifier = "test_rebate", decimal amount = 100.00m, decimal percentage = 0.0m)
        {
            return new Rebate
            {
                Identifier = identifier,
                Amount = amount,
                Percentage = percentage,
                Incentive = incentiveType
            };
        }
    }
}
