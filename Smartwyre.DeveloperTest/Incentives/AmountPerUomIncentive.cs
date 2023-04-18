using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Incentives
{
	public class AmountPerUomIncentive : IncentiveBase
	{
		public AmountPerUomIncentive() : base(IncentiveType.AmountPerUom, SupportedIncentiveType.AmountPerUom) { }

		protected override decimal CalculateIncentive(Product product, Rebate rebate, decimal volume) => rebate.Amount * volume;

		protected override void ValidateInputsAndThrow(Product product, Rebate rebate, decimal volume)
		{
			base.ValidateInputsAndThrow(product, rebate, volume);
			if (rebate.Amount == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(rebate.Amount));
			}

			if (volume == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(volume));
			}
		}
	}
}
