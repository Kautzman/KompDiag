using System;
using System.Collections.Generic;
using System.Management;
using System.Linq;
using System.Text;

namespace sysinfo
{
    class Drivers
    {
        public string getDrivers()
        {
            string description;
            string deviceClass;
            string friendlyName;
            string name;
            string hardwareID;
            string date;
            string driverName;
            string version;
            string formattedDate;

            string output;

            output = "<table> \r\n<col style=\"width:500px\"> \r\n<col style=\"width:500px\"> \r\n<thead> \r\n<tr> \r\n<th colspan=\"2\">Devices and Drives -- Descriptions, Hardware ID, Version, and Date</th> \r\n</tr> \r\n</thead> \r\n<tbody>";

            foreach (ManagementObject managementObject in new ManagementObjectSearcher(new ObjectQuery("select * from Win32_PnPSignedDriver")).Get())
            {

                deviceClass = Convert.ToString(managementObject.GetPropertyValue("DeviceClass"));
                version = Convert.ToString(managementObject.GetPropertyValue("DriverVersion"));

                if (string.IsNullOrEmpty(deviceClass) == false)
                {
                    if (!deviceClass.Contains("VOLUME"))
                    {
                        if (!deviceClass.Contains("LEGACYDRIVER"))
                        {
                            if (!deviceClass.Contains("USB"))
                            {
                                if (!version.Contains("6.1.760")) //Windows 7 Default Drivers.  Need some for Windows Vista, 8 and Vista.
                                {

                                    description = Convert.ToString(managementObject.GetPropertyValue("Description"));
                                    friendlyName = Convert.ToString(managementObject.GetPropertyValue("FriendlyName"));
                                    name = Convert.ToString(managementObject.GetPropertyValue("Name"));
                                    hardwareID = Convert.ToString(managementObject.GetPropertyValue("HardWareID"));
                                    date = Convert.ToString(managementObject.GetPropertyValue("DriverDate"));
                                    driverName = Convert.ToString(managementObject.GetPropertyValue("DriverName"));

                                    if(date.Length > 7)
                                    {
                                        formattedDate = date.Substring(4, 2) + " / " + date.Substring(6, 2) + " / " + date.Substring(0, 4);
                                    }
                                    else
                                    {
                                        formattedDate = "";
                                    }

                                    output += "\r\n<tr> \r\n<td colspan=\"2\" style=\"font-weight:bold\">" + description + "</td> \r\n</tr> \r\n<tr> \r\n<td colspan=\"2\">" + hardwareID + "</td>\r\n</tr>\r\n" +
                                        "<tr> \r\n<td> " + version + "</td>\r\n<td> " + formattedDate + "</td> \r\n</tr><tr><td colspan=\"2\">&nbsp</td></tr>";
                                }
                            }
                        }
                    }
                }
            }
            output += "</tbody> \r\n</table>";
            return output;
        }
    }
}
