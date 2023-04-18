using Moq;
using Smartwyre.DeveloperTest.Data.Contracts;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Contracts;
using Smartwyre.DeveloperTest.Tests.Core;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
	public class RebateServiceTests : IncentivesTestBase
	{
		private Mock<IRebateDataStore> _rebateDataStore;
		private Mock<IProductDataStore> _productDataStore;
		private Mock<IIncentiveService> _incentiveService;

		private RebateService _sut;
		public RebateServiceTests()
		{
			_rebateDataStore = new Mock<IRebateDataStore>();
			_productDataStore = new Mock<IProductDataStore>();
			_incentiveService = new Mock<IIncentiveService>();

			_sut = new RebateService(_rebateDataStore.Object, _productDataStore.Object, _incentiveService.Object);
		}

		[Fact]
		public void StoresCalcualtionOnSuccess() 
		{
			_rebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(BuildRebate(IncentiveType.AmountPerUom));
			_productDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(BuildProduct(SupportedIncentiveType.AmountPerUom));
			_incentiveService.Setup(x => x.CalculateRebateResult(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>())).Returns(new CalculateRebateResult { Amount = 100, Success = true  });

			_sut.Calculate(new CalculateRebateRequest
			{
				ProductIdentifier = "test_product",
				RebateIdentifier = "test_rebate",
				Volume = 10
			});

			_rebateDataStore.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), 100));
		}
	}
}
