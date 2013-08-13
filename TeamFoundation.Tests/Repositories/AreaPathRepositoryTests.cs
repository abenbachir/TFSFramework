

namespace TeamFoundation.Tests.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class AreaPathRepositoryTests
    {
        [TestMethod]
        public void ItShouldGetOneAreaPath()
        {
            var mockProxy = new Mock<ITFSAreaPathProxy>();
            var areas = new List<AreaPath>();

            areas.Add(new AreaPath { Name = "Area 1", Path = "myproject\\area1" });
            areas.Add(new AreaPath { Name = "Area 2", Path = "myproject\\area2" });

            mockProxy.Setup(p => p.GetAllAreaPaths())
                 .Returns(areas)
                 .Verifiable();

            var repository = new AreaPathRepository(mockProxy.Object);

            var area = repository.Find("myproject\\area1");

            Assert.IsTrue(area != null);
            Assert.AreEqual(area.Name, "Area 1");
            Assert.AreEqual(area.Path, "myproject\\area1");
        }

        [TestMethod]
        public void ItShouldGetAllAreasForAGivenCollection()
        {
            var mockProxy = new Mock<ITFSAreaPathProxy>();
            var areas = new List<AreaPath>();

            areas.Add(new AreaPath { Name = "Area 1", Path = "myproject\\area1" });
            areas.Add(new AreaPath { Name = "Area 2", Path = "myproject\\area2" });

            mockProxy.Setup(p => p.GetAllAreaPaths())
                 .Returns(areas)
                 .Verifiable();

            var repository = new AreaPathRepository(mockProxy.Object);

            var results = repository.FindAll();

            Assert.IsTrue(results.SequenceEqual<AreaPath>(areas), "The expected areas for a collection don't match the results");
            mockProxy.VerifyAll();
        }

        [TestMethod]
        public void ItShouldGetAllAreasForAGivenProject()
        {
            var mockProxy = new Mock<ITFSAreaPathProxy>();
            var areas = new List<AreaPath>();

            areas.Add(new AreaPath { Name = "Area 1", Path = "myproject\\area1" });
            areas.Add(new AreaPath { Name = "Area 2", Path = "myproject\\area2" });

            mockProxy.Setup(p => p.GetAreaPathsByProject(It.Is<string>(s => s == "Project 1")))
                 .Returns(areas)
                 .Verifiable();

            var repository = new AreaPathRepository(mockProxy.Object);

            var results = repository.FindBy("Project 1");

            Assert.IsTrue(results.SequenceEqual<AreaPath>(areas), "The expected areas for a project don't match the results");
            mockProxy.VerifyAll();
        }
    }
}
