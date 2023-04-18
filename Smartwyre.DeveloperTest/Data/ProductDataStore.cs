using Smartwyre.DeveloperTest.Data.Contracts;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    public ProductDataStore() 
    {
        _products.Add("product-one", new Product
        {
            Id = 0,
            Identifier = "product-one",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 100m,
            Uom = "ton"
        });

		_products.Add("product-two", new Product
		{
			Id = 1,
			Identifier = "product-two",
			SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
			Price = 100m,
			Uom = "ton"
		});

		_products.Add("product-three", new Product
		{
			Id = 2,
			Identifier = "product-three",
			SupportedIncentives = SupportedIncentiveType.AmountPerUom,
			Price = 100m,
			Uom = "ton"
		});
	}

    public Product GetProduct(string productIdentifier)
    {
		if (!_products.ContainsKey(productIdentifier)) return null;

		return _products[productIdentifier];
	}

	private readonly Dictionary<string, Product> _products = new Dictionary<string, Product>();
}
