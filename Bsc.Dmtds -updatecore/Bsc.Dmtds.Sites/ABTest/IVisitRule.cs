namespace Bsc.Dmtds.Sites.ABTest
{
    public interface IVisitRule
    {
        string Name { get; set; }

        string TemplateVirtualPath { get; }

        string RuleType { get; }

        string DisplayText { get; }

        string RuleTypeDisplayName { get; }

        bool IsMatch(System.Web.HttpRequestBase httpRequest); 
    }
}