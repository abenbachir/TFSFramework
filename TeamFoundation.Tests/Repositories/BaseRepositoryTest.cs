
namespace TeamFoundation.Tests.Repositories
{
    using System;
    using System.Net;
    using System.Reflection;
    using TeamFoundation.Common;
    using TeamFoundation.Common.Factories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class BaseRepositoryTest<RepositoryType, ProxyType>
    {
        private string tfsUrl = "http://tfstest:8080/tfs/DefaultCollection";
        private RepositoryType repository;

        public RepositoryType Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        public ICredentials TFSCredentials
        {
            get { return new NetworkCredential("username", "pass", "Domaine"); }
        }

        [TestInitialize]
        public void InitTest()
        {
            //TFSProxyFactory proxyFactory = new TFSProxyFactory(new Uri(tfsUrl), TFSCredentials);
            ////ProxyType proxy = proxyFactory.CreateProxy<ProxyType>();
            //ProxyType proxy = (ProxyType)proxyFactory.GetProxy(typeof(ProxyType));
            //Repository = RepositoryFactory.CreateRepository<RepositoryType>(proxy);

            TFSManager tfs = new TFSManager(new Uri(tfsUrl), TFSCredentials);
            Repository = tfs.GetRepository<RepositoryType>();
        }

        

    }
}
