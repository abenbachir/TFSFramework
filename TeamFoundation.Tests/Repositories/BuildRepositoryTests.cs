
namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.Data.Services.Toolkit.QueryModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class BuildRepositoryTests : BaseRepositoryTest<BuildRepository, TFSBuildProxy>
    {
        [TestMethod]
        public void ItShouldGetOneBuild()
        {

            var build = Repository.FindOneBy("project", "buildDefinition", "buildNumber");

            Assert.AreEqual(build.TeamProject, "project");
            Assert.AreEqual(build.BuildDefinition.Name, "buildDefinition");
            Assert.AreEqual(build.BuildNumber, "buildNumber");

        }

        [TestMethod]
        public void ItShouldGetAllBuildsForAGivenProject()
        {

            var results = Repository.FindBy("project");

            Assert.IsNotNull(results);
            foreach (var build in results)
                Assert.AreEqual(build.TeamProject, "project");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ItShouldGetAllBuildsForAGivenCollection()
        {
            var results = Repository.FindAll();
            Assert.IsNotNull(results);
        }
    }
}
