using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Services.Contracts;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Services
{
	public class IncentiveService : IIncentiveService
	{
		private readonly Dictionary<IncentiveType, IIncentive> _registeredIncentives;

		public IncentiveService(IEnumerable<IIncentive> registeredIncentives) 
		{
			_registeredIncentives = registeredIncentives.ToDictionary(incentive => incentive.Incentive, incentive => incentive);
		}
		public CalculateRebateResult CalculateRebateResult(Product product, Rebate rebate, decimal volume)
		{
			if(rebate == null)
			{
				throw new ArgumentNullException(nameof(rebate));
			}

			CalculateRebateResult result = new CalculateRebateResult();
			if (!_registeredIncentives.ContainsKey(rebate.Incentive))
			{
				result.Success = false;
			}

			try
			{
				IIncentive incentive = _registeredIncentives[rebate.Incentive];
				result.Amount = incentive.CalculateRebateAmount(product, rebate, volume);
				result.Success = true;
			}
			catch
			{
				result.Success = false;
			}

			return result;
		}
	}
}
