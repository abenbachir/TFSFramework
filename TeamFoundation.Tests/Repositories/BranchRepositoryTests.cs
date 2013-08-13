

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
    public class BranchPathRepositoryTests : BaseRepositoryTest<BranchRepository, TFSBranchProxy>
    {
        [TestMethod]
        public void ItShouldGetOneBranch()
        {
            var result = Repository.Find("$/master");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Properties.RootItem.Item, "$/master");
            Assert.AreEqual(result.Properties.Owner.ToUpper(), ("Domaine\\username").ToUpper());
        }

        [TestMethod]
        public void ItShouldGetAllBranchesForAGivenCollection()
        {
            var results = Repository.FindAll();
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void ItShouldGetAllBranchesForAGivenProject()
        {
            var results = Repository.GetBranchesByProject("SaaS");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 0);
        }
    }
}
