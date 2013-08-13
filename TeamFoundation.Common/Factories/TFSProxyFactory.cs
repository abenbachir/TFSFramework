using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using TeamFoundation.Common.Proxies;

namespace TeamFoundation.Common.Factories
{

    public class TFSProxyFactory : IDisposable
    {
        private readonly ICredentials m_tfsCredentials;
        private readonly Uri m_tfsUri;
        private Dictionary<string, Object> m_proxies = new Dictionary<string,object>();

        public TFSProxyFactory(Uri tfsUri, ICredentials credentials)
        {
            this.m_tfsUri = tfsUri;
            this.m_tfsCredentials = credentials;
        }

        public T GetProxy<T>()
        {
            return (T)((object)this.GetProxy(typeof(T)));
        }

        public object GetProxy(Type proxyType)
        {
            if (proxyType == null)
                throw new ArgumentNullException("proxyType");

            string proxyName = proxyType.Name;
            if (m_proxies.ContainsKey(proxyName))
            {
                return this.m_proxies[proxyName];
            }

            // create the proxy
            object proxy;
            lock (this.m_proxies)
            {
                proxy = CreateProxy(proxyType);
                m_proxies.Add(proxyName, proxy);
            }
            return proxy;
        }
        

        protected T CreateProxy<T>()
        {
            return (T)((object)this.CreateProxy(typeof(T)));
        }

        protected Object CreateProxy(Type type)
        {
            ConstructorInfo proxyConstructor = type.GetConstructor(new Type[] { typeof(Uri), typeof(NetworkCredential) });
            if (proxyConstructor == null)
            {
                throw new InvalidOperationException("Type " + type.Name + " does not contain an appropriate constructor");
            }

            return proxyConstructor.Invoke(new object[] { this.m_tfsUri, this.m_tfsCredentials });
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
