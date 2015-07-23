using System.Collections.Generic;
using Bsc.Dmtds.Content.Models;
using Bsc.Dmtds.Core;
using Bsc.Dmtds.Core.Persistence.Non_Relational;

namespace Bsc.Dmtds.Content.Persistence
{
    public interface IWorkflowProvider : IContentElementProvider<Workflow>
    {
         
    }
    public interface IPendingWorkflowItemProvider : IProvider<PendingWorkflowItem>
    {
        IEnumerable<PendingWorkflowItem> All(Repository repository, string roleName);
    }
    public interface IWorkflowHistoryProvider : IProvider<WorkflowHistory>
    {
        IEnumerable<WorkflowHistory> All(TextContent content);
    }
}