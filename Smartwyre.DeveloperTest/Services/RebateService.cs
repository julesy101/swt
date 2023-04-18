using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Contracts;
using Smartwyre.DeveloperTest.Services.Contracts;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore _rebateDataStore;
	private readonly IProductDataStore _productDataStore;
    private readonly IIncentiveService _incentiveService;
	public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IIncentiveService incentiveService)
	{
		_rebateDataStore = rebateDataStore;
		_productDataStore = productDataStore;
		_incentiveService = incentiveService;
	}

	public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        var result = _incentiveService.CalculateRebateResult(product, rebate, request.Volume);
		if (result.Success)
		{
			_rebateDataStore.StoreCalculationResult(rebate, result.Amount);
		}

		return result;
    }
}
