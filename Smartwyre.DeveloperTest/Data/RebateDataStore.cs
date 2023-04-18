using Smartwyre.DeveloperTest.Data.Contracts;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    public RebateDataStore()
    {
        _rebates.Add("rebate-one", new Rebate
        {
            Amount = 100,
            Identifier = "rebate-one",
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 10.00m
        });

		_rebates.Add("rebate-two", new Rebate
		{
			Amount = 100,
			Identifier = "rebate-two",
			Incentive = IncentiveType.FixedCashAmount,
			Percentage = 10.00m
		});

		_rebates.Add("rebate-three", new Rebate
		{
			Amount = 100,
			Identifier = "rebate-three",
			Incentive = IncentiveType.AmountPerUom,
			Percentage = 10.00m
		});
	}
    public Rebate GetRebate(string rebateIdentifier)
    {
        if (!_rebates.ContainsKey(rebateIdentifier)) return null;

        return _rebates[rebateIdentifier];
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        // Update account in database, code removed for brevity
    }

    private readonly Dictionary<string, Rebate> _rebates = new Dictionary<string, Rebate>();
}
