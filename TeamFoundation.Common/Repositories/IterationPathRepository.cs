
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Proxies;

    public class IterationPathRepository : IRepository<IterationPath>
    {
        private readonly ITFSIterationPathProxy m_proxy;

        public IterationPathRepository(ITFSIterationPathProxy proxy)
        {
            m_proxy = proxy;
        }
 
        public IterationPath Find(int id)
        {
            throw new NotImplementedException();
        }

        public IterationPath Find(string path)
        {
            // TODO: very bad, performance impact
            //return this.proxy.GetAllIterationPaths().SingleOrDefault(a => a.Path.Equals(path));
            return m_proxy.GetAllIterationPaths().SingleOrDefault(a => a.Path.Equals(path));
        }

        public IterationPath FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IterationPath> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IterationPath> FindAll()
        {
            return m_proxy.GetAllIterationPaths();
        }

        public IEnumerable<IterationPath> FindSubIterationsByPath(string path)
        {
            return m_proxy.GetSubIterations(path);
        }

        public IEnumerable<IterationPath> FindByProject(string projectName)
        {
            return m_proxy.GetIterationsByProject(projectName);
        }

    }
}
