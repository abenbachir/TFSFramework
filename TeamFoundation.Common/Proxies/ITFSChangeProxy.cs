
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public interface ITFSChangeProxy
    {
        IEnumerable<Change> GetChangesByChangeset(int changesetId, int topRequestValue = int.MaxValue);
    }
}
