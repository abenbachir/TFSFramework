
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using TeamFoundation.Common.ExpressionVisitors;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public interface ITFSChangesetProxy
    {
        Changeset GetChangeset(int changesetId);

        IEnumerable<Changeset> GetChangesetsByProject(string projectName, FilterNode rootFilterNode = null, int topRequestValue = int.MaxValue);

        IEnumerable<Changeset> GetChangesetsByProjectCollection(FilterNode rootFilterNode, int topRequestValue = int.MaxValue);
        
        IEnumerable<Changeset> GetChangesetsByBuild(string projectName, string buildDefinition, string buildNumber);

        IEnumerable<Changeset> GetChangesetsByBranch(string path, FilterNode rootFilterNode = null, int topRequestValue = int.MaxValue);
    }
}
