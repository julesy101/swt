using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data.Contracts
{
	public interface IProductDataStore
	{
		Product GetProduct(string productIdentifier);
	}
}
