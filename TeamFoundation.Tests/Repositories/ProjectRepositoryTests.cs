
namespace TeamFoundation.Tests.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.TeamFoundation.Server;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ProjectRepositoryTests
    {
        [TestMethod]
        public void ItShouldGetOneProject()
        {
            var mockProxy = new Mock<ITFSProjectProxy>();
            var projects = new List<ProjectInfo>();

            projects.Add(new ProjectInfo { Name = "Project 01" });
            projects.Add(new ProjectInfo { Name = "Project 02" });

            mockProxy.Setup(p => p.GetProjectsByProjectCollection())
                 .Returns(projects)
                 .Verifiable();

            var repository = new ProjectRepository(mockProxy.Object);

            var project = repository.Find("Project 01");

            Assert.IsTrue(project != null);
            Assert.AreEqual(project.Name, "Project 01");
            mockProxy.VerifyAll();
        }

        [TestMethod]
        public void ItShouldGetAllProjectsForAGivenCollection()
        {
            var mockProxy = new Mock<ITFSProjectProxy>();
            var projects = new List<ProjectInfo>();

            projects.Add(new ProjectInfo { Name = "Project 01" });
            projects.Add(new ProjectInfo { Name = "Project 02" });

            mockProxy.Setup(p => p.GetProjectsByProjectCollection())
                 .Returns(projects)
                 .Verifiable();

            var repository = new ProjectRepository(mockProxy.Object);
            var results = repository.FindAll();

            Assert.IsTrue(results != null);
            Assert.IsTrue(results.SequenceEqual<ProjectInfo>(projects), "The expected projects for a given collection don't much the results");
        }
    }
}
