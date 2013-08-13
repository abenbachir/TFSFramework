
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using TeamFoundation.Common.Proxies;
    using TeamFoundation.Common.Translators;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public class BranchRepository : IRepository<BranchObject>
    {
        private readonly ITFSBranchProxy m_proxy;

        public BranchRepository(ITFSBranchProxy proxy)
        {
            m_proxy = proxy;
        }

        public IEnumerable<BranchObject> GetBranchesByProject(string name)
        {
            return this.m_proxy.GetBranchesByProject(name);
        }

        public BranchObject Find(int id)
        {
            throw new NotImplementedException();
        }

        public BranchObject Find(string path)
        {
            return m_proxy.GetBranch(EntityTranslator.DecodePath(path));
        }

        public BranchObject FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BranchObject> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BranchObject> FindAll()
        {
            return m_proxy.GetBranchesByProjectCollection();
        }
    }
}
