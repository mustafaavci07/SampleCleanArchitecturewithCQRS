
using RulesEngine;
using RulesEngine.Models;

using SampleCleanArchitecutre.Core.Domain.Rules;

namespace SampleCleanArchitecture.Application.Services
{
    public interface IRuleService
    {
        Task AddRuleAsync(Rules rule);
        Task<List<Rules>> GetAllRulesAsync();
        Task<List<RuleResultTree>> VerifyProcessAsync(object processData,string workflowName);
    }

   
    public class RuleService : IRuleService
    {
        private SampleContext _context {  get; set; }
        public RulesEngine.RulesEngine _rulesEngine { get; private set; }
        public RuleService(SampleContext context)
        {
            _context = context;
            _rulesEngine=new RulesEngine.RulesEngine();
            InitializeRuleService().Wait();
        }

        private async Task InitializeRuleService()
        {
            List<Rules> rules=await GetAllRulesAsync();
            var workflowRules = rules.GroupBy(r => r.WorkflowName)
                .Select(g => new Workflow 
                { WorkflowName = g.Key, 
                    Rules = 
                    g.Select(r => new Rule 
                    { 
                        RuleName = r.RuleName, 
                        Expression = r.RuleExpression, 
                        SuccessEvent=r.SuccessEvent,
                        ErrorMessage=r.ErrorMessage,
                    
                    }).ToList() }).ToList();
            _rulesEngine = new RulesEngine.RulesEngine(workflowRules.ToArray(), null);
        }
        
        private void SetRulesEngine(RulesEngine.RulesEngine rulesEngine)
        {
            _rulesEngine = rulesEngine;
        }
        public async Task AddRuleAsync(Rules rule)
        {
            _context.Rules.Add(rule);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Rules>> GetAllRulesAsync()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<List<RuleResultTree>> VerifyProcessAsync(object processData,string workflowName)
        {
            return await _rulesEngine.ExecuteAllRulesAsync(workflowName,processData);
            
        }
    }
}
