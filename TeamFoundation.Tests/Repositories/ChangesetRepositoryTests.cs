
namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamFoundation.Common.ExpressionVisitors;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.Data.Services.Toolkit.QueryModel;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ChangesetRepositoryTests : BaseRepositoryTest<ChangesetRepository, TFSChangesetProxy>
    {
        [TestMethod]
        public void ItShouldGetOneChangeset()
        {
            Changeset changeset = Repository.Find(568);

            Assert.IsNotNull(changeset);
            Assert.AreEqual(changeset.ChangesetId, 568);
            Assert.AreEqual(changeset.Owner.ToUpper(), ("Domaine\\username").ToUpper());
            Assert.AreEqual(changeset.Comment, "***NO_CI*** Gestion des langages");

        }

        [TestMethod]
        public void ItShouldGetAllChangesetsForAGivenProject()
        {
            var results = Repository.FindByProject("project");
            Assert.IsNotNull(results);
            Assert.AreNotEqual(results.Count(), 0);
        }

        [TestMethod]
        public void ItShouldGetAllChangesetsForAGivenCollection()
        {
            var results = Repository.FindAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() >= 670);
        }
    }
}
