

namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.VersionControl.Client;

    public class ChangesetRepository : IRepository<Changeset>
    {
        private readonly ITFSChangesetProxy m_proxy;

        public ChangesetRepository(ITFSChangesetProxy proxy)
        {
            this.m_proxy = proxy;
        }


        
        public Changeset Find(int id)
        {
            return m_proxy.GetChangeset(id);
        }

        public Changeset Find(string id)
        {
            return m_proxy.GetChangeset(int.Parse(id));
        }

        public Changeset FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Changeset> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Changeset> FindBy(string project, string definition, string number)
        {
            return m_proxy.GetChangesetsByBuild(project, definition, number);
        }

        public IEnumerable<Changeset> FindAll()
        {
            var parameters = new ChangesetFilterExpressionVisitor(null).Eval();
            return this.m_proxy.GetChangesetsByProjectCollection(parameters);
        }

        public IEnumerable<Changeset> FindByProject(string projectName, Expression filterExpression = null)
        {
            var parameters = new ChangesetFilterExpressionVisitor(filterExpression).Eval();
            return m_proxy.GetChangesetsByProject(projectName, parameters);
        }

        public IEnumerable<Changeset> FindByBranch(string branch, Expression filterExpression = null)
        {
            var parameters = new ChangesetFilterExpressionVisitor(filterExpression).Eval();
            return m_proxy.GetChangesetsByBranch(branch, parameters);
        }
    }
}