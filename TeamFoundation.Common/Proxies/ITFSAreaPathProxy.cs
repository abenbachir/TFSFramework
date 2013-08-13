
namespace TeamFoundation.Common.Proxies
{
    using System.Collections.Generic;
    using TeamFoundation.Common.Entities;

    public interface ITFSAreaPathProxy
    {
        IEnumerable<AreaPath> GetAllAreaPaths();

        IEnumerable<AreaPath> GetSubAreas(string rootAreaName);

        IEnumerable<AreaPath> GetAreaPathsByProject(string projectName);
    }
}
