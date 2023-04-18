using Smartwyre.DeveloperTest.Incentives;
using Smartwyre.DeveloperTest.Tests.Core;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Incentives
{
    public class FixedCashAmountIncentiveTests : IncentivesTestBase
	{
		FixedCashAmountIncentive _sut;

		public FixedCashAmountIncentiveTests()
		{
			_sut = new FixedCashAmountIncentive();
		}

		[Fact]
		public void IncentiveTypeSetAsFixedCashAmount()
		{
			Assert.Equal(IncentiveType.FixedCashAmount, _sut.Incentive);
		}

		[Fact]
		public void ThrowsIfRebateIsNull()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedCashAmount);
			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateAmount(product, null, 0));
		}

		[Fact]
		public void ThrowsIfRebateAmountIsZero()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.FixedCashAmount, amount: 0m);
			Assert.Throws<NotSupportedException>(() => _sut.CalculateRebateAmount(product, rebate, 0));
		}

		[Fact]
		public void ThrowsIfProductIsNull()
		{
			var rebate = BuildRebate(IncentiveType.FixedCashAmount);
			Assert.Throws<ArgumentNullException>(() => _sut.CalculateRebateAmount(null, rebate, 0));
		}

		[Fact]
		public void ThrowsIfProductDoesntSupportIncentive()
		{
			var product = BuildProduct(SupportedIncentiveType.AmountPerUom);
			var rebate = BuildRebate(IncentiveType.FixedCashAmount);
			Assert.Throws<NotSupportedException>(() => _sut.CalculateRebateAmount(product, rebate, 0));
		}

		[Fact]
		public void CalculateIncentiveReturnsFixedRebateAmount()
		{
			var product = BuildProduct(SupportedIncentiveType.FixedCashAmount);
			var rebate = BuildRebate(IncentiveType.FixedCashAmount);
			var result = _sut.CalculateRebateAmount(product, rebate, 0);

			Assert.Equal(100m, result);
		}
	}
}
