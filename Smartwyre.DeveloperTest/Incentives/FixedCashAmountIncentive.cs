using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Incentives
{
	public class FixedCashAmountIncentive : IncentiveBase
	{
		public FixedCashAmountIncentive() : base(IncentiveType.FixedCashAmount, SupportedIncentiveType.FixedCashAmount) { }
		
		protected override decimal CalculateIncentive(Product product, Rebate rebate, decimal volume) => rebate.Amount;

		protected override void ValidateInputsAndThrow(Product product, Rebate rebate, decimal volume)
		{
			base.ValidateInputsAndThrow(product, rebate, volume);
			if(rebate.Amount == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(rebate.Amount));
			}
		}
	}
}
