
namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Data.Services.Toolkit.QueryModel;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    using TeamFoundation.Common;
    using System.Globalization;
    using System.Net;
    using Microsoft.TeamFoundation.Client;
    using System.Reflection;
    using TeamFoundation.Common.Proxies;
    using TeamFoundation.Common.Helpers;

    [TestClass]
    public class WorkItemRepositoryTests : BaseRepositoryTest<WorkItemRepository, TFSWorkItemProxy>
    {
       
        [TestMethod]
        public void ItShouldGetOneWorkItem()
        {
            var result = Repository.Find("1");

            Assert.IsTrue(result != null);
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void CheckRepositoryInterfaces()
        {
            Type type = typeof(WorkItemRepository);
            var field = type.GetField("m_proxy", BindingFlags.NonPublic | BindingFlags.Instance);
            string typeName = field.FieldType.Name;
            if (field.FieldType.IsInterface)
                typeName = typeName.Remove(0, 1);

            Type typeFound = AssemblyHelper.FindType(string.Format("{0}.{1}", field.FieldType.Namespace, typeName));
            Assert.AreEqual(typeFound.Name, typeof(TFSWorkItemProxy).Name);

        }

        
        /*[TestMethod]
        public void ItShouldGetAllWorkItemsFromQuery()
        {
            var operation = new ODataSelectManyQueryOperation();
            operation.Keys = new Dictionary<string, string>();
            operation.Keys.Add("WorkItemType", "Opportunity");

            var results = Repository.GetAll(operation);

            Assert.IsNotNull(results);

        }*/
/*
                [TestMethod]
                public void ItShouldGetAllWorkItemsForAGivenBuild()
                {
                    var mockProxy = new Mock<ITFSWorkItemProxy>();
                    var items = new List<WorkItem>();

                    items.Add(new WorkItem { Id = 1, Description = "This is the first WorkItem", CreatedBy = "johndoe", Priority = "1", Title = "Bug #1" });
                    items.Add(new WorkItem { Id = 1, Description = "This is the second WorkItem", CreatedBy = "mary", Priority = "5", Title = "Bug #2" });

                    mockProxy.Setup(p => p.GetWorkItemsByBuild("Sample Project", "123", It.IsAny<FilterNode>()))
                         .Returns(items)
                         .Verifiable();

                    var repository = new WorkItemRepository(mockProxy.Object);
                    var operation = new ODataSelectManyQueryOperation();
                    operation.Keys = new Dictionary<string, string>();
                    operation.Keys.Add("project", "Sample Project");
                    operation.Keys.Add("number", "123");

                    var results = repository.GetWorkItemsByBuild(operation);

                    Assert.IsTrue(results.SequenceEqual<WorkItem>(items), "The expected workitems for a build don't match the results");
                    mockProxy.VerifyAll();
                }

                [TestMethod]
                public void ItShouldGetAllWorkItemsForAGivenChangeset()
                {
                    var mockProxy = new Mock<ITFSWorkItemProxy>();
                    var items = new List<WorkItem>();

                    items.Add(new WorkItem { Id = 1, Description = "This is the first WorkItem", CreatedBy = "johndoe", Priority = "1", Title = "Bug #1" });
                    items.Add(new WorkItem { Id = 1, Description = "This is the second WorkItem", CreatedBy = "mary", Priority = "5", Title = "Bug #2" });

                    mockProxy.Setup(p => p.GetWorkItemsByChangeset(123))
                         .Returns(items)
                         .Verifiable();

                    var repository = new WorkItemRepository(mockProxy.Object);

                    var results = repository.GetWorkItemsByChangeset("123");

                    Assert.IsTrue(results.SequenceEqual<WorkItem>(items), "The expected workitems for a changeset don't match the results");
                    mockProxy.VerifyAll();
                }

                [TestMethod]
                public void ItShouldGetAllWorkItemsForAGivenProject()
                {
                    var mockProxy = new Mock<ITFSWorkItemProxy>();
                    var items = new List<WorkItem>();

                    items.Add(new WorkItem { Id = 1, Description = "This is the first WorkItem", CreatedBy = "johndoe", Priority = "1", Title = "Bug #1" });
                    items.Add(new WorkItem { Id = 1, Description = "This is the second WorkItem", CreatedBy = "mary", Priority = "5", Title = "Bug #2" });

                    mockProxy.Setup(p => p.GetWorkItemsByProject("myproject", It.IsAny<FilterNode>(), It.IsAny<ODataQueryOperation>()))
                         .Returns(items)
                         .Verifiable();

                    var repository = new WorkItemRepository(mockProxy.Object);
                    var operation = new ODataSelectManyQueryOperation();
                    operation.Keys = new Dictionary<string, string>();
                    operation.Keys.Add("project", "myproject");

                    var results = repository.GetWorkItemsByProject(operation);

                    Assert.IsTrue(results.SequenceEqual<WorkItem>(items), "The expected workitems for a changeset don't match the results");
                    mockProxy.VerifyAll();
                }
          */
    }
}
