using System;
using System.Management;
using System.Windows.Forms;

namespace sysinfo
{
  internal class RAM
  {
    public string getMemory()
    {
      try
      {
        string str1 = "";
        double Capacity = 9999.0;
        int Speed = 9999;
        foreach (ManagementObject managementObject in new ManagementObjectSearcher(new ObjectQuery("select * from Win32_PhysicalMemory")).Get())
        {
          Capacity += Convert.ToDouble(managementObject.GetPropertyValue("Capacity"));
          Speed = Convert.ToInt32(managementObject.GetPropertyValue("Speed"));
        }
        string output = str1 + "<table> \r\n<col style=\"width:200px\"> \r\n<col style=\"width:200px\"> \r\n<thead> \r\n<tr> \r\n<th>Memory</th> \r\n<th>Speed</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n<tr> \r\n<td>" +  Math.Round(Capacity / 1048576.0, 0).ToString() + " MB </td> \r\n<td>" + Speed.ToString() + " MHz</td> \r\n</tr> \r\n</tbody> \r\n</table>";
        //main.addText("RAM Completed Successfully");
        return output;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the RAM Info :( \n\n Error: \n" + (object) ex);
        //main.addText("RAM Completed with Errors");
      }
      return "Error in RAM Retrieval";
    }
  }
}
