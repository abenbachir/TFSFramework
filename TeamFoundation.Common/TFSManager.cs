using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeamFoundation.Common.Exceptions;
using TeamFoundation.Common.Factories;
using TeamFoundation.Common.Helpers;
using TeamFoundation.Common.Repositories;
using Microsoft.TeamFoundation.Common;

namespace TeamFoundation.Common
{
    public class TFSManager
    {
        private readonly RepositoryFactory m_repositoryFactory;
        private readonly TFSProxyFactory m_proxyFactory;

        public TFSManager(TFSProxyFactory proxyFactory)
        {
            m_repositoryFactory = new RepositoryFactory();
            m_proxyFactory = proxyFactory;
        }

        public TFSManager(Uri uri, ICredentials credentials)
        {
            m_repositoryFactory = new RepositoryFactory();
            m_proxyFactory = new TFSProxyFactory(uri, credentials);
        }

        public T GetRepository<T>()
        {
            var field = typeof(T).GetField("m_proxy", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new ProxyNotFoundException(string.Format("No proxy found related to repository : {0}", typeof(T).Name));
            string typeName = field.FieldType.IsInterface ? field.FieldType.Name.Remove(0, 1) : field.FieldType.Name;
            // get the type related to a specifique repository
            Type proxyType = AssemblyHelper.FindType(string.Format("{0}.{1}", field.FieldType.Namespace, typeName));
            TFCommonUtil.CheckForNull(proxyType, "proxyType");
            // get the proxy
            var proxy = m_proxyFactory.GetProxy(proxyType);

            return m_repositoryFactory.GetRepository<T>(proxy);
        }

    }
}
