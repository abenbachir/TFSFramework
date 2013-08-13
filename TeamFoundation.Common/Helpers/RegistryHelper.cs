
namespace TeamFoundation.Common.Helpers
{
    using Microsoft.Win32;

    public static class RegistryHelper
    {
        public static string GetMimeType(string extension)
        {
            if (!string.IsNullOrWhiteSpace(extension))
            {
                var regKey = Registry.ClassesRoot.OpenSubKey(extension);

                if ((regKey != null) && (regKey.GetValue("Content Type") != null))
                {
                    return regKey.GetValue("Content Type").ToString();
                }
            }

            return "application/unknown";
        }
    }
}
