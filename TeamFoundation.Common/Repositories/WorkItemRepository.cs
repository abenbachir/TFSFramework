
namespace TeamFoundation.Common.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Globalization;
    using System.Linq;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class WorkItemRepository : IRepository<WorkItem>
    {
        private readonly ITFSWorkItemProxy m_proxy;

        public WorkItemRepository(ITFSWorkItemProxy proxy)
        {
            m_proxy = proxy;
        }
        public WorkItem Find(int id)
        {
            return m_proxy.GetWorkItem(id);
        }
        public WorkItem Find(string id)
        {
            var workItemId = 0;
            if (!int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out workItemId))
            {
                throw new ArgumentException("The id parameter must be numeric", "id");
            }
            return Find(workItemId);
        }

        public WorkItem FindOneBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkItem> FindByProject(dynamic operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            var parameters = new WorkItemFilterExpressionVisitor(operation.FilterExpression).Eval();
            return m_proxy.GetWorkItemsByProject(operation.Key, parameters, operation);
        }

        public IEnumerable<WorkItem> FindByQuery(dynamic operation)
        {
            var queryId = default(Guid);
            if (!Guid.TryParse(operation.Keys["id"], out queryId))
            {
                throw new ArgumentException("The id paramter must be a GUID", "id");
            }

            return m_proxy.GetWorkItemsByQuery(queryId, operation);
        }

        public IEnumerable<WorkItem> FindByChangeset(int changesetId)
        {
            return m_proxy.GetWorkItemsByChangeset(changesetId);
        }
        
        public IEnumerable<WorkItem> FindByBuild(dynamic operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }

            var parameters = new WorkItemFilterExpressionVisitor(operation.FilterExpression).Eval();
            return m_proxy.GetWorkItemsByBuild(operation.Keys["project"], operation.Keys["number"], parameters);
        }

        public IEnumerable<WorkItem> FindBy(Dictionary<string, string> createria, Dictionary<string, string> orderBy = null)
        {
            throw new NotImplementedException();

            //var filterParameters = new WorkItemFilterExpressionVisitor(operation.FilterExpression).Eval();
            //return this.m_proxy.GetWorkItemsByProjectCollection(filterParameters, operation);
        }

        public IEnumerable<WorkItem> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Save(object entity)
        {
            var workItem = entity as WorkItem;
            if (workItem == null)
            {
                workItem = (entity as IEnumerable).Cast<WorkItem>().First();
            }

            if (workItem.Id > 0)
            {
                m_proxy.UpdateWorkItem(workItem);
            }
            else
            {
                throw new Exception(" not implemented : this.proxy.CreateWorkItem(workItem)");
                //this.proxy.CreateWorkItem(workItem);
            }
        }

    }
}