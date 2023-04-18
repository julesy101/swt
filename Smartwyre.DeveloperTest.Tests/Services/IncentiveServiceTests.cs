using Moq;
using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Tests.Core;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
	public class IncentiveServiceTests : IncentivesTestBase
	{
		private IncentiveService _sut;
		public IncentiveServiceTests() 
		{
			var mockedIncentive = BuildMockIncentive();
			_sut = new IncentiveService(new[] { mockedIncentive.Object });		
		}
		[Fact]
		public void ReturnsSuccessFalseIfNoMatchingIncentiveFound()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedRateRebate);
			var rebate = BuildRebate(IncentiveType.FixedRateRebate);
			var volume = 0.12m;

			var result = _sut.CalculateRebateResult(product, rebate, volume);
			Assert.False(result.Success);
		}

		[Fact]
		public void ReturnsSuccessFalseIfErrorThrownCalculatingIncentive()
		{
			var mockedIncentive = BuildMockIncentive();
			_sut = new IncentiveService(new[] { mockedIncentive.Object });
			mockedIncentive.Setup(x => x.CalculateRebateAmount(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>())).Throws<ArgumentOutOfRangeException>();
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.AmountPerUom);
			var volume = 0.12m;

			var result = _sut.CalculateRebateResult(product, rebate, volume);
			Assert.False(result.Success);
		}

		[Fact]
		public void ThrowsArgumentNullExceptionIfRebateIsNull()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedRateRebate);
			var volume = 0.12m;

			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateResult(product, null, volume));
		}

		private static Mock<IIncentive> BuildMockIncentive()
		{
			var mocked = new Mock<IIncentive>();
			mocked.SetupGet(x => x.Incentive).Returns(IncentiveType.AmountPerUom);
			mocked.Setup(x => x.CalculateRebateAmount(It.IsAny<Product>(), It.IsAny<Rebate>(), It.IsAny<decimal>())).Returns(123m);

			return mocked;
		}
	}
}
