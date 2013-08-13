using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFoundation.Common.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {

        public RepositoryNotFoundException()
        {
        }

        public RepositoryNotFoundException(string message) : base(message)
        {
        }
    }
}
