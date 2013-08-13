
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Proxies;

    public class AreaPathRepository : IRepository<AreaPath>
    {
        private readonly ITFSAreaPathProxy proxy;

        public AreaPathRepository(ITFSAreaPathProxy proxy)
        {
            this.proxy = proxy;
        }
        
        public AreaPath Find(int id)
        {
            throw new NotSupportedException();
        }

        public AreaPath Find(string path)
        {
            return this.proxy.GetAllAreaPaths().SingleOrDefault(a => a.Path.Equals(path));
        }

        public AreaPath FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AreaPath> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }        

        public IEnumerable<AreaPath> FindAll()
        {
            return this.proxy.GetAllAreaPaths();
        }

        public IEnumerable<AreaPath> FindSubAreasByAreaPath(string path)
        {
            return this.proxy.GetSubAreas(path);
        }

        public IEnumerable<AreaPath> FindBy(string projectName)
        {
            return this.proxy.GetAreaPathsByProject(projectName);
        }
    }
}
