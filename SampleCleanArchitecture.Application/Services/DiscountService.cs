
using RulesEngine.Models;

namespace SampleCleanArchitecture.Application.Services
{
    public class DiscountService(IRuleService ruleService)
    {
        private string workflowName = "Discount";

        public async Task<double> CalculateDiscount(Passenger passenger)
        {
            List<RuleResultTree> ruleResults = await ruleService.VerifyProcessAsync(passenger, workflowName);
            double result = 0;
            if(ruleResults?.Any() ??false)
            {
                var successList= ruleResults.Where(p => p.IsSuccess && p.Rule!=null ).ToList();
                successList.ForEach(r => { 
                    if(double.TryParse(r.Rule.Expression, out double converted))
                        result += converted;
                });
            }
            return result;
        
        }
    }
}
