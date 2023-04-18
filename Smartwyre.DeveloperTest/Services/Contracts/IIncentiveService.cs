using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Contracts
{
	public interface IIncentiveService
	{
		CalculateRebateResult CalculateRebateResult(Product product, Rebate rebate, decimal volume);
	}
}
