using System;
using System.Management;
using System.Windows.Forms;

namespace sysinfo
{
  internal class CPU
  {
    public string getCPU()
    {
      try
      {
        string output1  = "" + "<table> \r\n<col style=\"width:400px\"> \r\n<col style=\"width:125px\"> \r\n<col style=\"width:125px\"> \r\n<thead> \r\n<tr> \r\n<th>CPU</th> \r\n<th>Speed</th> \r\n<th>Width</th> \r\n</tr> \r\n</thead> \r\n<tbody>\r\n";
        foreach (ManagementObject managementObject in new ManagementObjectSearcher(new ObjectQuery("select * from Win32_Processor")).Get())
        {
          int ClockSpeed = Convert.ToInt32(managementObject.GetPropertyValue("MaxClockSpeed"));
          int DataWidth = Convert.ToInt32(managementObject.GetPropertyValue("DataWidth"));
          string Name = Convert.ToString(managementObject.GetPropertyValue("name"));
          output1 = output1 + "<tr> \r\n<td>" + Name + "</td> \r\n<td>" +  ClockSpeed.ToString() + " MHz</td> \r\n<td>" + DataWidth.ToString() + "-bit</td> \r\n</tr> \r\n";
        }
        string output = output1 + "</tbody> \r\n</table>";
        //main.addText("CPU Completed Successfully");
        return output;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the CPU Info :( \n\n Error: \n" + (object) ex);
        //main.addText("CPU Completed with Errors");
      }
      return "Error in CPU Retrieval";
    }
  }
}
