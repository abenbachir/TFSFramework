
namespace TeamFoundation.Tests.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.Data.Services.Toolkit.QueryModel;
    using Microsoft.TeamFoundation.Build.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class BuildDefinitionRepositoryTests : BaseRepositoryTest<BuildDefinitionRepository, TFSBuildDefinitionProxy>
    {
        [TestMethod]
        public void ItShouldGetOneBuildDefinition()
        {
            var buildDefinition = Repository.FindOneBy("BuildDefinition", "Project");

            Assert.IsNotNull(buildDefinition);
            Assert.AreEqual(buildDefinition.Name, "BuildDefinition");
            Assert.AreEqual(buildDefinition.TeamProject, "Project");
        }

        [TestMethod]
        public void ItShouldGetAllBuildDefinitionsForAGivenProject()
        {

            var results = Repository.FindBy("Project");
            Assert.IsNotNull(results);
            foreach (var builDefinition in results)
                Assert.AreEqual(builDefinition.TeamProject, "Project");
            

        }
    }
}
