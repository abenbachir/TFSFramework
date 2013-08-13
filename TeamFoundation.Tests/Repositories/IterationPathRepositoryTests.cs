
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

    [TestClass]
    public class IterationPathRepositoryTests : BaseRepositoryTest<IterationPathRepository, TFSIterationPathProxy>
    {
       
        [TestMethod]
        public void ItShouldGetOneIterationPath()
        {
            var iteration = Repository.Find("projectPortfolio<Project");

            Assert.IsNotNull(iteration);
            Assert.AreEqual(iteration.Name, "Project");
            Assert.AreEqual(iteration.Path, "projectPortfolio<Project");
        }

        [TestMethod]
        public void ItShouldGetAllIterationsPath()
        {
            var results = Repository.FindAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
        }

        [TestMethod]
        public void ItShouldGetSubIterationsPath()
        {
            var results = Repository.FindSubIterationsByPath("projectPortfolio<Project");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
        }
        
        [TestMethod]
        public void ItShouldGetAllIterationPathsForAGivenProject()
        {
            var results = Repository.FindByProject("Project");

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
        }
          
    }
}
