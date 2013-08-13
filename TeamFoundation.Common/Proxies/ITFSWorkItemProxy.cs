
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using TeamFoundation.Common.ExpressionVisitors;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public interface ITFSWorkItemProxy
    {
        WorkItem GetWorkItem(int workItemId);

        IEnumerable<WorkItem> GetWorkItemsByProjectCollection(FilterNode rootFilterNode, dynamic operation);

        IEnumerable<WorkItem> GetWorkItemsByProject(string projectName, FilterNode rootFilterNode, dynamic operation);

        IEnumerable<WorkItem> GetWorkItemsByQuery(Guid queryId, dynamic operation);

        IEnumerable<WorkItem> GetWorkItemsByBuild(string projectName, string buildNumber, FilterNode rootFilterNode);

        IEnumerable<WorkItem> GetWorkItemsByChangeset(int changesetId);

        void UpdateWorkItem(WorkItem workItem);
    }
}
