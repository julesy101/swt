using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Incentives
{
	public class FixedRateIncentive : IncentiveBase
	{
		public FixedRateIncentive() : base(IncentiveType.FixedRateRebate, SupportedIncentiveType.FixedRateRebate) { }

		protected override decimal CalculateIncentive(Product product, Rebate rebate, decimal volume) => product.Price * rebate.Percentage * volume;

		protected override void ValidateInputsAndThrow(Product product, Rebate rebate, decimal volume)
		{
			base.ValidateInputsAndThrow(product, rebate, volume);
			if (rebate.Percentage == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(rebate.Percentage));
			}

			if(product.Price == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(product.Price));
			}

			if(volume == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(volume));
			}
		}
	}
}
