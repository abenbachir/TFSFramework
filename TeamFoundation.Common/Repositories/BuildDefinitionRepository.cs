
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Linq;
    using System.Web;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.Build.Client;

    public class BuildDefinitionRepository : IRepository<IBuildDefinition>
    {
        private readonly ITFSBuildDefinitionProxy m_proxy;

        public BuildDefinitionRepository(ITFSBuildDefinitionProxy proxy)
        {
            this.m_proxy = proxy;
        }

        public IBuildDefinition Find(int id)
        {
            throw new NotImplementedException();
        }

        public IBuildDefinition Find(string id)
        {
            throw new NotImplementedException();
        }
        
        public IBuildDefinition FindOneBy(string definition, string project)
        {
            return this.m_proxy.GetBuildDefinitionsByProject(project).SingleOrDefault(t => t.Name.Equals(definition, StringComparison.OrdinalIgnoreCase));
        }

        public IBuildDefinition FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBuildDefinition> FindBy(string project)
        {
            return this.m_proxy.GetBuildDefinitionsByProject(project);
        }

        public IEnumerable<IBuildDefinition> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBuildDefinition> FindAll()
        {
            throw new NotImplementedException("The 'Build Definitions' collection cannot be enumerated as a root collection. It should depend on a Project.");
        }
    }
}