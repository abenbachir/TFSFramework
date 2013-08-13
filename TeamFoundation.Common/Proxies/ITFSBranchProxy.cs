
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using TeamFoundation.Common.Entities;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public interface ITFSBranchProxy
    {
        BranchObject GetBranch(string path);

        IEnumerable<BranchObject> GetBranchesByProjectCollection();

        IEnumerable<BranchObject> GetBranchesByProject(string projectName);
    }
}
