
namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class QueryRepositoryTests : BaseRepositoryTest<QueryRepository, TFSQueryProxy>
    {
        [TestMethod]
        public void ItShouldGetOneQuery()
        {
            var queryId = Guid.Parse("8a51ea6c-6cb8-4700-b27b-ae71ccff2ae2");
            var query = Repository.Find(queryId);

            Assert.IsNotNull(query);
            Assert.AreEqual(query.Id, queryId);
            Assert.AreEqual(query.Name, "New Query 4");
            Assert.AreEqual(query.Project.Name, "MarketRequest");
        }

        [TestMethod]
        public void ItShouldGetAllQuerysForAGivenCollection()
        {
            var results = Repository.FindAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 10);

        }

        [TestMethod]
        public void ItShouldGetAllQuerysForAGivenProject()
        {

            var results = Repository.FindByProject("MarketRequest");
            Assert.IsNotNull(results);

            QueryDefinition myquery = results.Where(query => query.Name == "New Query 4").FirstOrDefault();
            Assert.IsNotNull(myquery);
            Assert.AreEqual(myquery.Name, "New Query 4");
            Assert.AreEqual(myquery.Id, Guid.Parse("8a51ea6c-6cb8-4700-b27b-ae71ccff2ae2"));

        }
  
    }
}
