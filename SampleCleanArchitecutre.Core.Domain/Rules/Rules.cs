
namespace SampleCleanArchitecutre.Core.Domain.Rules
{
    public class Rules:AuditableBaseEntity
    {
        public string WorkflowName { get; set; }
        public string RuleExpression { get; set; }

        public string RuleName { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessEvent { get; set; }

    }
}
