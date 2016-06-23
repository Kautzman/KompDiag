using System;
using System.Management;
using System.Windows.Forms;

namespace sysinfo
{
  internal class GFX
  {
    public string getGFXUnit()
    {
      try
      {
        string str1 = "";
        int num1 = 0;
        string output1 = str1 + "<table> \r\n<col style=\"width:400px\"> \r\n<col style=\"width:125px\"> \r\n<thead> \r\n<tr> \r\n<th>GFX Unit</th> \r\n<th>Memory</th> \r\n</tr> \r\n</thead> \r\n<tbody>";
        foreach (ManagementObject managementObject in new ManagementObjectSearcher(new ObjectQuery("select * from Win32_VideoController")).Get())
        {
          Convert.ToString(managementObject.GetPropertyValue("Description"));
          uint num2 = Convert.ToUInt32(managementObject.GetPropertyValue("AdapterRAM"));
          string str3 = Convert.ToString(managementObject.GetPropertyValue("Name"));
          output1 = output1 + "<tr> \r\n<td>" + str3 + "</td> \r\n<td>" + (num2 / 1048576U).ToString() + " MB</td> \r\n</tr>";
          ++num1;
        }
        string output = output1 + "\t</tbody> \r\n</table>";
        //main.addText("GFX Completed Successfully");
        return output;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to GFX Info:( \n\n Error: \n" + (object) ex);
        //main.addText("GFX Completed with Errors");
      }
      return "Error in GFX Retrevial";
    }
  }
}
