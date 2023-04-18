using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Data.Contracts;
using Smartwyre.DeveloperTest.Incentives;
using Smartwyre.DeveloperTest.Incentives.Contracts;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Contracts;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
	public class RunnerOptions
	{
		[Option('p', "product", Required = true, HelpText = "choose from product-one (fixed rate), product-two (fixed cash), product-three  (amount per uom)")]
		public string ProductId { get; set; }

		[Option('r', "rebate", Required = true, HelpText = "choose from rebate-one (fixed rate), rebate-two (fixed cash), rebate-three (amount per uom)")]
		public string RebateId { get; set; }

		[Option('v', "volume", Required = false, HelpText = "volume must be a number")]
		public decimal Volume { get; set; }
	}

    static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services => {
                services.AddSingleton<IRebateDataStore, RebateDataStore>();
				services.AddSingleton<IProductDataStore, ProductDataStore>();
				services.AddSingleton<IRebateService, RebateService>();
				services.AddSingleton<IIncentiveService, IncentiveService>((serviceProvider) => new IncentiveService(serviceProvider.GetServices<IIncentive>()));
				services.AddTransient<IIncentive, FixedCashAmountIncentive>();
				services.AddTransient<IIncentive, FixedRateIncentive>();
				services.AddTransient<IIncentive, AmountPerUomIncentive>();

			}).Build();

		IRebateService rebateService = host.Services.GetRequiredService<IRebateService>();

		Parser.Default.ParseArguments<RunnerOptions>(args)
				  .WithParsed<RunnerOptions>(o =>
				  {
					  CalculateRebateRequest request = new CalculateRebateRequest
					  {
						  ProductIdentifier = o.ProductId,
						  RebateIdentifier = o.RebateId,
						  Volume = o.Volume
					  };

					  var result = rebateService.Calculate(request);

					  Console.WriteLine($"success: {result.Success}");
					  Console.WriteLine($"amount: {result.Amount}");
				  });
	}
}
