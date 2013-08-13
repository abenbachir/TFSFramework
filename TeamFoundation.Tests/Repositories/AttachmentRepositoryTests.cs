

namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Linq;
    using TeamFoundation.Common.Repositories;
    using TeamFoundation.Common.Proxies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Reflection;
    using TeamFoundation.Common.Helpers;

    [TestClass]
    public class AttachmentRepositoryTests : BaseRepositoryTest<AttachmentRepository, TFSAttachmentProxy>
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ItShouldThrowNotSupportedInGetAll()
        {
            Repository.FindAll();
        }

        [TestMethod]
        public void ItShouldGetOneAttachment()
        {
            var attachement = Repository.Find("10215-0");

            Assert.IsNotNull(attachement);
            Assert.AreEqual(attachement.Comment, "test attachement");

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldGetOneEmptyAttachmentIsIdStartsWithTemp()
        {
            var tempId = "temp-123456789";
            var result = Repository.Find(tempId);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, tempId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldThrowIfAttachmenIdIsNotWellFormatted1()
        {
            var badId = "123456789";
            var result = Repository.Find(badId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldThrowIfAttachmenIdIsNotWellFormatted2()
        {
            var badId = "123-456-789";
            var result = Repository.Find(badId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldThrowIfWorkItemIdIsNotANumberInGetOneAttachment()
        {
            var badId = "NAN-0";
            Repository.Find(badId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldThrowIfIndexIsNotANumberInGetOneAttachment()
        {
            var badId = "10215-NAN";
            Repository.Find(badId);
        }

        [TestMethod]
        public void ItShouldGetAttachmentsForAGivenWorkItem()
        {
            var results = Repository.FindByWorkItem("10215");

            Assert.AreEqual(results.Count(), 2);
        }
    }
}
