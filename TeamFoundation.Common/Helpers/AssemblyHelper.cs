
namespace TeamFoundation.Common.Helpers
{
    using System;
    using System.Reflection;
    using Microsoft.Win32;

    public static class AssemblyHelper
    {
        public static Type FindType(string type)
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type t = a.GetType(type);
                if (t != null)
                    return t;
            }
            return null;
        }
    }
}
