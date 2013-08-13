
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.Server;

    public interface ITFSProjectProxy
    {
        IEnumerable<ProjectInfo> GetProjectsByProjectCollection();
    }
}
