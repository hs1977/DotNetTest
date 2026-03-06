using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace DriveInformation
{
    static public class ManagementObjectExtension
    {
        public static string GetAsString(this ManagementObject mo, string propertyName)
        {
            if(mo == null)
            {
                return "";
            }

            object obj = mo[propertyName];
            if(obj == null)
            {
                return "";
            }

            return obj.ToString() ?? "";
        }

        public static uint GetAsUInt32(this ManagementObject mo, string propertyName)
        {
            if(mo == null)
            {
                return 0;
            }

            object obj = mo[propertyName];
            if(obj == null)
            {
                return 0;
            }

            return (uint)obj;
        }
    }

    static public class ManagementBaseObjectExtension
    {
        public static string GetAsString(this ManagementBaseObject mbo, string propertyName)
        {
            if(mbo == null)
            {
                return "";
            }

            PropertyData pd = mbo.Properties[propertyName];
            if(pd == null)
            {
                return "";
            }

            object obj = pd.Value;
            if(obj == null)
            {
                return "";
            }

            return obj.ToString() ?? "";
        }
    }
}
