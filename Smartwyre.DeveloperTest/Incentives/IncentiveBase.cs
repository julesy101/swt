using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Incentives
{
	public abstract class IncentiveBase : IIncentive
	{
		protected IncentiveBase(IncentiveType incentiveType, SupportedIncentiveType supportedIncentive) 
		{
			Incentive = incentiveType;
			SupportedIncentive = supportedIncentive;
		}

		public IncentiveType Incentive { get; }
		public SupportedIncentiveType SupportedIncentive { get; }

		public decimal CalculateRebateAmount(Product product, Rebate rebate, decimal volume)
		{
			ValidateInputsAndThrow(product, rebate, volume);

			return CalculateIncentive(product, rebate, volume);
		}

		protected virtual void ValidateInputsAndThrow(Product product, Rebate rebate, decimal volume)
		{
			if (rebate == null || product == null)
			{
				throw new ArgumentNullException();
			}

			if (!product.SupportedIncentives.HasFlag(SupportedIncentive))
			{
				throw new NotSupportedException();
			}
		}

		protected abstract decimal CalculateIncentive(Product product, Rebate rebate, decimal volume);
	}
}
