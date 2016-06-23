using System;
using System.Management;
using System.Windows.Forms;

namespace sysinfo
{
  internal class PageFile
  {
    public string getPageFile()
    {
      try
      {
        string str1 = "";
        int Usage = 9999;
        int Allocated = 9999;
        foreach (ManagementObject managementObject in new ManagementObjectSearcher(new ObjectQuery("select * from Win32_PageFileUsage")).Get())
        {
          Usage = Convert.ToInt32(managementObject.GetPropertyValue("CurrentUsage"));
          Allocated = Convert.ToInt32(managementObject.GetPropertyValue("AllocatedBaseSize"));
        }
        string output = str1 + "<table> \r\n<col style=\"width:300px\"> \r\n<col style=\"width:200px\"> \r\n<thead> \r\n<tr> \r\n<th>Page File --- Current Size</th> \r\n<th>Maximum Size</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n<tr> \r\n<td>" + Usage.ToString() + " MB</td> \r\n<td>" + Allocated.ToString() + " MB</td> \r\n</tr> \r\n</tbody> \r\n</table>";
        //main.addText("PageFile Completed Successfully");
        return output;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the Service List :( \n\n Error: \n" + (object) ex);
        //main.addText("PageFile Completed with Errors");
      }
      return "Error in Services Retrieval";
    }
  }
}
