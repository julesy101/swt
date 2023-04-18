using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Incentives.Contracts
{
	public interface IIncentive
	{
		IncentiveType Incentive { get; }
		decimal CalculateRebateAmount(Product product, Rebate rebate, decimal volume);
	}
}
