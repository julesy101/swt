using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Contracts;

public interface IRebateService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}
