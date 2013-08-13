
namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Linq;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.Data.Services.Toolkit.QueryModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ChangeRepositoryTests : BaseRepositoryTest<ChangeRepository, TFSChangeProxy>
    {
        [TestMethod]
        public void ItShouldGetOneChange()
        {
            var change = Repository.FindOneBy(406, "$/master");

            Assert.IsTrue(change != null);
            Assert.AreEqual(change.Item.ChangesetId, 406);
            Assert.AreEqual(change.Item.ServerItem, "$/master");
            Assert.AreEqual(change.ChangeType.ToString(), "Branch");
        }

        [TestMethod]
        public void ItShouldGetAllChangesForAGivenChangeset()
        {
            var results = Repository.FindBy(406);

            Assert.IsNotNull(results);
            Assert.AreEqual(results.Count(), 33213);            
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ItShouldThrowExceptionOnGetAll()
        {
            var results = Repository.FindAll();
        }
    }
}
