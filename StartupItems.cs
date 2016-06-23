using System;
using System.Management;
using System.Windows.Forms;

namespace sysinfo
{
  internal class StartupItems
  {
    public string getStartupItems()
    {
      try
      {
        string str1 = "" + "<table> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:700px\"> \r\n<thead> \r\n<tr> \r\n<th>Startup Item</th> \r\n<th>Command</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n";
        foreach (ManagementObject managementObject in new ManagementClass("Win32_StartupCommand").GetInstances())
        {
          string str2 = managementObject["Name"].ToString();
          string str3 = managementObject["Command"].ToString();
          str1 = str1 + "<tr> \r\n<td>" + str2 + "</td> \r\n<td>" + str3 + "</td> \r\n</tr> \r\n";
        }
        string str4 = str1 + "</tbody> \r\n</table>";
        //main.addText("Startup Items Completed Successfully");
        return str4;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the Startup Items :( \n\n Error: \n" + (object) ex);
        //main.addText("Startup Items Completed with Errors");
      }
      return "Error in Startup Item Retrieval";
    }
  }
}
