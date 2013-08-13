
namespace TeamFoundation.Common.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Linq.Expressions;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using TeamFoundation.Common.Translators;
    using TeamFoundation.Common.ExpressionVisitors;
    using System.Collections;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    

    public class TFSWorkItemProxy : TFSBaseProxy, ITFSWorkItemProxy
    {
        public TFSWorkItemProxy(Uri uri, ICredentials credentials)
            : base(uri, credentials)
        {
        }

        public WorkItem GetWorkItem(int workItemId)
        {
            var wiql = string.Format(CultureInfo.InvariantCulture, "SELECT [System.Id] FROM WorkItems WHERE [System.Id] = {0}", workItemId);

            return this.QueryWorkItems(wiql)
                        .Cast<WorkItem>()
                        .FirstOrDefault();
        }
        
        public IEnumerable<WorkItem> GetWorkItemsByProject(string projectName, FilterNode rootFilterNode, dynamic operation)
        {
            return this.RequestWorkItemsByProject(projectName, rootFilterNode, operation);
        }

        public IEnumerable<WorkItem> GetWorkItemsByProjectCollection(FilterNode rootFilterNode, dynamic operation)
        {
            return this.RequestWorkItemsByProjectCollection(rootFilterNode, operation);
        }

        public IEnumerable<WorkItem> GetWorkItemsByQuery(Guid queryId, dynamic operation)
        {
            return this.RequestWorkItemsByQuery(queryId, operation);
        }

        public IEnumerable<WorkItem> GetWorkItemsByChangeset(int changesetId)
        {
            return this.RequestWorkItemsByChangeset(changesetId);
        }

        public IEnumerable<WorkItem> GetWorkItemsByBuild(string projectName, string buildNumber, FilterNode rootFilterNode)
        {
            return this.RequestWorkItemsByBuild(projectName, buildNumber, rootFilterNode);
        }


        public void UpdateWorkItem(WorkItem workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException("workItem");
            }

            workItem.PartialOpen();

            if (workItem.Fields.Cast<Field>().Where(field => !field.IsValid).Any())
            {
                var errors = new StringBuilder();
                errors.AppendLine("The WorkItem cannot be updated because the following fields are invalid:");
                foreach (var field in workItem.Fields.Cast<Field>().Where(field => !field.IsValid))
                {
                    errors.AppendLine(string.Format(CultureInfo.InvariantCulture, "Invalid field '{0}': {1} (Current Value '{2}')", field.Name, field.Status, field.Value));
                }

                throw new ArgumentException(errors.ToString(), "workItem");
            }

            workItem.Save();
        }
        
        private static string BuildWiql(FilterNode rootFilterNode, dynamic operation)
        {
            var constrains = string.Empty;
            if (rootFilterNode != null)
            {
                foreach (var filterNode in rootFilterNode)
                {
                    constrains += AddComparisonConstrainToWiql(filterNode, Constants.TFS.FieldLookup(filterNode.Key));
                }
            }

            if (constrains.StartsWith(" OR ", StringComparison.OrdinalIgnoreCase))
            {
                constrains = constrains.Substring(" OR ".Length);
            }

            if (constrains.StartsWith(" AND ", StringComparison.OrdinalIgnoreCase))
            {
                constrains = constrains.Substring(" OR ".Length);
            }

            var wiql = string.Format(CultureInfo.InvariantCulture, "SELECT [System.Id] FROM WorkItems {0} {1}", string.IsNullOrEmpty(constrains) ? string.Empty : "WHERE", constrains).Trim();

            string orderByWiql = string.Empty;
            if (operation.OrderStack.Count > 0)
            {
                orderByWiql = TFSWorkItemProxy.GenerateOrderByWiql(operation.OrderStack);
            }

            wiql = wiql + " " + orderByWiql;

            return wiql;
        }

        private static string GenerateOrderByWiql(Stack<dynamic> orderExpressionStack) //ODataOrderExpression
        {
            if (orderExpressionStack == null)
            {
                throw new ArgumentNullException("orderExpressionStack");
            }

            StringBuilder finalOrderBy = new StringBuilder("ORDER BY ");
            //When the $top param is used with WCF Data Services Toolkit, this appears to also imply ordering by 
            //key ascending (in this case work item ID), so if the user also issues an explicit $orderby param 
            //for Id there will be a duplicate. It appears that the first $oderby value (top of stack) is the
            //one provided explicitly by the user, so we take that one and track that it has been seen already.
            //Subsequent order by fields are ignored.
            ISet<string> seen = new HashSet<string>();
            while (orderExpressionStack.Count > 0)
            {
                dynamic expr = orderExpressionStack.Pop();
                UnaryExpression unExpr = ((UnaryExpression)expr.Expression);

                var opString = unExpr.Operand.ToString();
                var name = opString.Substring(opString.IndexOf('.') + 1);
                name = Constants.TFS.FieldLookup(name);

                if (!seen.Contains(name))
                {
                    if (expr.OrderMethodName.StartsWith("ThenBy"))
                    {
                        finalOrderBy.Append(" , ");
                    }
                    finalOrderBy.Append(name);

                    switch ((string)expr.OrderMethodName)
                    {
                        case "OrderBy":
                        case "ThenBy":
                            finalOrderBy.Append(" asc");
                            break;
                        case "OrderByDescending":
                        case "ThenByDescending":
                            finalOrderBy.Append(" desc");
                            break;
                    }

                    seen.Add(name);
                }
            }

            return finalOrderBy.ToString();
        }

        private static string AddComparisonConstrainToWiql(FilterNode filterNode, string tfsFieldName)
        {
            if (filterNode != null)
            {
                var sign = default(string);

                switch (filterNode.Sign)
                {
                    case FilterExpressionType.Equal:
                        sign = "=";
                        break;
                    case FilterExpressionType.NotEqual:
                        sign = "<>";
                        break;
                    case FilterExpressionType.GreaterThan:
                        sign = ">";
                        break;
                    case FilterExpressionType.GreaterThanOrEqual:
                        sign = ">=";
                        break;
                    case FilterExpressionType.LessThan:
                        sign = "<";
                        break;
                    case FilterExpressionType.LessThanOrEqual:
                        sign = "<=";
                        break;
                    case FilterExpressionType.Contains:
                        sign = filterNode.Key.Equals("AreaPath", StringComparison.OrdinalIgnoreCase) || filterNode.Key.Equals("IterationPath", StringComparison.OrdinalIgnoreCase) ? "UNDER" : "CONTAINS";
                        break;
                    case FilterExpressionType.NotContains:
                        sign = filterNode.Key.Equals("AreaPath", StringComparison.OrdinalIgnoreCase) || filterNode.Key.Equals("IterationPath", StringComparison.OrdinalIgnoreCase) ? "NOT UNDER" : "NOT CONTAINS";
                        break;

                    default:
                        throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "WorkItem {0} can only be filtered with equal, not equal, greater than, lower than, greater than or equal, lower than or equal operators", filterNode.Key));
                }

                return string.Format(CultureInfo.InvariantCulture, " {0} {1} {2} '{3}' ", filterNode.NodeRelationship.ToString(), tfsFieldName, sign, filterNode.Value);
            }

            return string.Empty;
        }
        
        private IEnumerable<WorkItem> RequestWorkItemsByQuery(Guid queryId, dynamic operation)
        {
            var workItemServer = this.TfsConnection.GetService<WorkItemStore>();
            string queryText;
            string projectName;

            try
            {
                var query = workItemServer.GetQueryDefinition(queryId);
                queryText = query.QueryText;
                projectName = query.Project.Name;
            }
            catch (ArgumentException)
            {
                // Weird bug in the TFS API, queries stored in TFS2008 cannot be accessed
                // using the new 2010 API, must use the old deprecated method.
#pragma warning disable
                var query = workItemServer.GetStoredQuery(queryId);
                queryText = query.QueryText;
                projectName = query.Project.Name;
#pragma warning restore
            }

            var wiql = Regex.Replace(queryText, "@project", string.Format(CultureInfo.InvariantCulture, "'{0}'", projectName), RegexOptions.IgnoreCase);

            IEnumerable<WorkItem> retWorkItems = null;

            if (!operation.IsCountRequest)
            {
                Query q = new Query(workItemServer, wiql);

                if (operation.TopCount == 0)
                {
                    //workaround for bug (I think) in WCF Data Services Toolkit. 
                    //It appears that dynamic.TopCount will be 0 when $select param is used and $top param is not
                    //explicitly sent by client (normally TopCount would have whatever was set for entity page size in TFSService.InitializeService()
                    operation.TopCount = Constants.DefaultEntityPageSize;
                }

                if (q.IsLinkQuery == false && q.IsTreeQuery == false)
                {
                    retWorkItems = workItemServer.Query(wiql)
                        .Cast<WorkItem>()
                        .Skip((int)operation.SkipCount).Take((int)operation.TopCount)
                        .ToArray();
                }
                else
                {
                    WorkItemLinkInfo[] linkInfo = q.RunLinkQuery();

                    List<int> wiIds = new List<int>();
                    foreach (var info in linkInfo)
                    {
                        if (info.SourceId == 0) //root items are 0
                        {
                            wiIds.Add(info.TargetId);
                        }
                    }
                    retWorkItems = workItemServer.Query(wiIds.ToArray(), "SELECT [System.Id] FROM WorkItems")
                        .Cast<WorkItem>()
                        .Skip((int)operation.SkipCount).Take((int)operation.TopCount)
                        .ToArray();
                }
            }
            else //count request
            {
                Query q = new Query(workItemServer, wiql, null, false);
                int cnt = q.RunCountQuery();

                List<WorkItem> wiBlanks = new List<WorkItem>(cnt);
                /*WorkItem blank = new WorkItem();
                for (int i = 0; i < cnt; i++)
                {
                    wiBlanks.Add(blank);
                }*/

                retWorkItems = wiBlanks;
            }

            return retWorkItems;
        }
        
        private IEnumerable<WorkItem> RequestWorkItemsByBuild(string projectName, string buildNumber, FilterNode rootFilterNode)
        {
            FilterNode newFilterNodeStartWithProject = 
                new FilterNode() { Key = "Project", Sign = FilterExpressionType.Equal, Value = projectName };

            if (!string.IsNullOrWhiteSpace(buildNumber))
            {
                newFilterNodeStartWithProject.AddNode(new FilterNode() { Key = "FoundInBuild", Sign = FilterExpressionType.Equal, Value = buildNumber, NodeRelationship = FilterNodeRelationship.And });
            }

            if (rootFilterNode != null)
            {
                newFilterNodeStartWithProject.AddNode(rootFilterNode);
            }

            return this.QueryWorkItems(BuildWiql(newFilterNodeStartWithProject, null)).Cast<WorkItem>().ToArray();
        }

        private IEnumerable<WorkItem> RequestWorkItemsByChangeset(int changesetId)
        {
            var versionControlServer = this.TfsConnection.GetService<Microsoft.TeamFoundation.VersionControl.Client.VersionControlServer>();

            var list = versionControlServer.GetChangeset(changesetId, false, false).WorkItems.ToArray();

            return list;
        }
        
        private IEnumerable<WorkItem> RequestWorkItemsByProjectCollection(FilterNode rootFilterNode, dynamic operation)
        {
            var wiql = BuildWiql(rootFilterNode, operation);
            return this.ExecuteWiqlRequest(wiql, operation);
        }

        private IEnumerable<WorkItem> RequestWorkItemsByProject(string projectName, FilterNode rootFilterNode, dynamic operation)
        {
            FilterNode newFilterNodeStartWithProject = new FilterNode() { Key = "Project", Sign = FilterExpressionType.Equal, Value = projectName };

            if (rootFilterNode != null)
            {
                newFilterNodeStartWithProject.AddNode(rootFilterNode);
            }

            var wiql = BuildWiql(newFilterNodeStartWithProject, operation);
            return this.ExecuteWiqlRequest(wiql, operation);
        }

        private IEnumerable<WorkItem> ExecuteWiqlRequest(string wiql, dynamic operation)
        {
            IEnumerable<WorkItem> retWorkItems = null;

            if (!operation.IsCountRequest)
            {
                if (operation.TopCount == 0)
                {
                    //workaround for bug (I think) in WCF Data Services Toolkit. 
                    //It appears that dynamic.TopCount will be 0 when $select param is used and $top param is not
                    //explicitly sent by client (normally TopCount would have whatever was set for entity page size in TFSService.InitializeService()
                    operation.TopCount = Constants.DefaultEntityPageSize;
                }

                retWorkItems = this.QueryWorkItems(wiql)
                    .Cast<WorkItem>()
                    .Skip((int)operation.SkipCount).Take((int)operation.TopCount)
                    .ToArray();
            }
            else
            {
                var workItemServer = this.TfsConnection.GetService<WorkItemStore>();

                Query q = new Query(workItemServer, wiql, null, false);
                int cnt = q.RunCountQuery();

                List<WorkItem> wiBlanks = new List<WorkItem>(cnt);
                /*WorkItem blank = new WorkItem();
                for (int i = 0; i < cnt; i++)
                    wiBlanks.Add(blank);*/

                retWorkItems = wiBlanks;
            }

            return retWorkItems;
        }
    }
}
