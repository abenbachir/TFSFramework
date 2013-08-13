
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using TeamFoundation.Common.ExpressionVisitors;
    using Microsoft.TeamFoundation.Build.Client;

    public interface ITFSBuildProxy
    {
        IBuildDetail GetBuild(string projectName, string buildDefinition, string buildNumber);

        IEnumerable<IBuildDetail> GetBuildsByProject(string projectName, FilterNode rootFilterNode = null);

        IEnumerable<IBuildDetail> GetBuildsByProjectCollection(FilterNode rootFilterNode);
    }
}
