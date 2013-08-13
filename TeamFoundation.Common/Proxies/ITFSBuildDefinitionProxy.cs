
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using TeamFoundation.Common.ExpressionVisitors;
    using Microsoft.TeamFoundation.Build.Client;

    public interface ITFSBuildDefinitionProxy
    {
        IEnumerable<IBuildDefinition> GetBuildDefinitionsByProject(string projectName);

        void QueueBuild(string projectName, string defintionName);
    }
}
