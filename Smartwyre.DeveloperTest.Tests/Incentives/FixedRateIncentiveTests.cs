using Smartwyre.DeveloperTest.Incentives;
using Smartwyre.DeveloperTest.Tests.Core;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Incentives
{
    public class FixedRateIncentiveTests : IncentivesTestBase
	{
		FixedRateIncentive _sut;

		public FixedRateIncentiveTests()
		{
			_sut = new FixedRateIncentive();
		}

		[Fact]
		public void IncentiveTypeSetAsFixedRateRebate()
		{
			Assert.Equal(IncentiveType.FixedRateRebate, _sut.Incentive);
		}

		[Fact]
		public void ThrowsIfRebateIsNull()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedRateRebate);
			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateAmount(product, null, 0));
		}

		[Fact]
		public void ThrowsIfRebatePercentIsZero()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedRateRebate);
			var rebate = BuildRebate(IncentiveType.FixedRateRebate, percentage: 0m);
			Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CalculateRebateAmount(product, rebate, 0));
		}

		[Fact]
		public void ThrowsIfProductIsNull()
		{
			var rebate = BuildRebate(IncentiveType.FixedRateRebate);
			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateAmount(null, rebate, 0));
		}

		[Fact]
		public void ThrowsIfProductDoesntSupportIncentive()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.FixedRateRebate);
			Assert.Throws<NotSupportedException>(() => _sut.CalculateRebateAmount(product, rebate, 0));
		}

		[Fact]
		public void CalculateIncentiveReturnsRebateAmountCorrectly()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedRateRebate, price: 100);
			var rebate = BuildRebate(IncentiveType.FixedRateRebate, percentage: 0.5m);

			var result = _sut.CalculateRebateAmount(product, rebate, 1);
			Assert.Equal(50m, result);
		}
	}
}
