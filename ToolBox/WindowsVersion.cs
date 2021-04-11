using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox
{
    public class WindowsVersion
    {

        public static string VersionString
        {
            get
            {
                return DetermineVersionString();
            }
        }

        private static string DetermineVersionString()
        {
            //Get OperatingSystem information from the system namespace.
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            //Determine the platform.
            switch (osInfo.Platform)
            {
                //Platform is Windows 95, Windows 98, Windows 98 Second Edition, or Windows Me.
                case System.PlatformID.Win32Windows:
                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            return "Windows 95";
                        case 10:
                            if (osInfo.Version.Revision.ToString() == "2222A")
                                return "Windows 98 Second Edition";
                            else
                                return "Windows 98";
                        case 90:
                            return "Windows Me";
                    }
                    break;
                //Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000, or Windows XP.
                case System.PlatformID.Win32NT:
                    switch (osInfo.Version.Major)
                    {
                        case 3:
                        case 4:
                            return GetWindowsVersionWithServicePack("Windows NT", osInfo);
                        case 5:
                            switch (osInfo.Version.Minor)
                            {
                                case 0:
                                    return GetWindowsVersionWithServicePack("Windows 2000", osInfo);
                                case 1:
                                    return GetWindowsVersionWithServicePack("Windows XP", osInfo);
                                case 2:
                                    return GetWindowsVersionWithServicePack("Win 2003 Server", osInfo);
                            }
                            break;
                        case 6:
                            switch (osInfo.Version.Minor)
                            {
                                case 0:
                                    return GetWindowsVersionWithServicePack("Windows Vista", osInfo);
                                case 1:
                                    return GetWindowsVersionWithServicePack("Windows 7", osInfo);
                            }
                            break;
                    }
                    break;
            }
            return osInfo.VersionString;
        }

        private static string GetWindowsVersionWithServicePack(string os, OperatingSystem osInfo)
        {
            if (String.IsNullOrEmpty(osInfo.ServicePack))
            {
                return String.Format("{0} v{1}", os, osInfo.Version);
            }
            return String.Format("{0} v{1} ({2})", os, osInfo.Version, osInfo.ServicePack);
        }

    }
}


