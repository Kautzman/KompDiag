using System;
using System.Management;
using System.Windows.Forms;

namespace sysinfo
{
  internal class OperatingSystem
  {
    public string getOS()
    {
      try
      {
        string str1 = "";
        string Name = "9999";
        string Architecture = "9999";
        long freeMemory = 9999L;
        foreach (ManagementObject managementObject in new ManagementObjectSearcher(new ObjectQuery("select * from Win32_OperatingSystem")).Get())
        {
          Name = Convert.ToString(managementObject.GetPropertyValue("Name"));
          Architecture = Convert.ToString(managementObject.GetPropertyValue("OSArchitecture"));
          freeMemory = Convert.ToInt64(managementObject.GetPropertyValue("FreePhysicalMemory"));
        }
        long Memory = freeMemory / 1024L;
        string output = str1 + "<table> \r\n<col style=\"width:300px\"> \r\n<col style=\"width:700px\"> \r\n<thead> \r\n<tr> \r\n<th>Operating System Info</td> \r\n<th>Value</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n<tr> \r\n<td>Operation System</td> \r\n<td>" + Name + " </td> \r\n</tr> \r\n<tr class=\"even\"> \r\n<td>Architecture</td> \r\n<td> " + Architecture + "</td> \r\n</tr> \r\n<tr> \r\n<td>Reported Free Physical Memory</td> \r\n<td>" + Memory.ToString() + " MB</td> \r\n</tr> \r\n</tbody> \r\n</table>";
        //main.addText("OS Completed Successfully");
        return output;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the Operating System Info :( \n\n Error: \n" + (object) ex);
        //main.addText("OS Completed with Errors");
      }
      return "Error in OS Info Retrieval";
    }
  }
}
