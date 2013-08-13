
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.Build.Client;

    public class BuildRepository : IRepository<IBuildDetail>
    {
        private readonly ITFSBuildProxy m_proxy;

        public BuildRepository(ITFSBuildProxy proxy)
        {
            this.m_proxy = proxy;
        }

        public IBuildDetail Find(int id)
        {
            throw new NotImplementedException();
        }

        public IBuildDetail Find(string id)
        {
            throw new NotImplementedException();
        }

        public IBuildDetail FindOneBy(string project, string definition, string number)
        {
            return m_proxy.GetBuild(project, definition, number);
        }

        public IBuildDetail FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBuildDetail> FindBy(string project)
        {
            return m_proxy.GetBuildsByProject(project);
        }
        
        public IEnumerable<IBuildDetail> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBuildDetail> FindAll()
        {
            throw new ArgumentNullException("Dont use this method, it take very long time to execute");

            var parameters = new BuildFilterExpressionVisitor(null).Eval();
            return m_proxy.GetBuildsByProjectCollection(parameters);
        }
    }
}