using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFoundation.Common.Exceptions
{
    public class ProxyNotFoundException : Exception
    {

        public ProxyNotFoundException()
        {
        }

        public ProxyNotFoundException(string message)
            : base(message)
        {
        }
    }
}
