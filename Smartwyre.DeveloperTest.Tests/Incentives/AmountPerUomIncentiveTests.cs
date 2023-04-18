using Smartwyre.DeveloperTest.Incentives;
using Smartwyre.DeveloperTest.Tests.Core;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Incentives
{
    public class AmountPerUomIncentiveTests : IncentivesTestBase
	{
		AmountPerUomIncentive _sut;

		public AmountPerUomIncentiveTests()
		{
			_sut = new AmountPerUomIncentive();
		}

		[Fact]
		public void IncentiveTypeSetAsFixedRateRebate()
		{
			Assert.Equal(IncentiveType.AmountPerUom, _sut.Incentive);
		}

		[Fact]
		public void ThrowsIfRebateIsNull()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateAmount(product, null, 1));
		}

		[Fact]
		public void ThrowsIfRebateAmountIsZero()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.AmountPerUom, amount: 0m);
			Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CalculateRebateAmount(product, rebate, 1));
		}

		[Fact]
		public void ThrowsIfVolumeIsZero()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.AmountPerUom, amount: 10m);
			Assert.Throws<ArgumentOutOfRangeException>(() => _sut.CalculateRebateAmount(product, rebate, 0));
		}

		[Fact]
		public void ThrowsIfProductIsNull()
		{
			var rebate = BuildRebate(IncentiveType.AmountPerUom);
			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateAmount(null, rebate, 1));
		}

		[Fact]
		public void ThrowsIfProductDoesntSupportIncentive()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedRateRebate);
			var rebate = BuildRebate(IncentiveType.AmountPerUom);
			Assert.Throws<NotSupportedException>(() => _sut.CalculateRebateAmount(product, rebate, 1));
		}

		[Fact]
		public void CalculateIncentiveReturnsRebateAmountCorrectly()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.AmountPerUom, amount: 100m);

			var result = _sut.CalculateRebateAmount(product, rebate, 10);
			Assert.Equal(1000m, result);
		}
	}
}
