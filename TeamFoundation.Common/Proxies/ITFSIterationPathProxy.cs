
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamFoundation.Common.Entities;

    public interface ITFSIterationPathProxy
    {
        IEnumerable<IterationPath> GetAllIterationPaths();
        IEnumerable<IterationPath> GetSubIterations(string rootIterationName);
        IEnumerable<IterationPath> GetIterationsByProject(string projectName);
    }
}
