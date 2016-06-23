using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace sysinfo
{
  internal class ServiceList
  {
    public string getServiceList()
    {
      try
      {
        string str1 = "";
        ServiceController[] services = ServiceController.GetServices();
        string str2 = str1 + "<table> \r\n <col style=\"width:300px\"> \r\n <col style=\"width:500px\"> \r\n <thead> \r\n <tr> \r\n <th>Service Name</th> \r\n <th>Display Name</th> \r\n </tr> \r\n </thead> \r\n <tbody> \r\n";
        foreach (ServiceController serviceController in services)
        {
          if (serviceController.Status == ServiceControllerStatus.Running)
            str2 = str2 + "<tr> \r\n <td>" + serviceController.ServiceName + "</td> \r\n <td>" + serviceController.DisplayName + "</td> \r\n </tr> \r\n";
        }
        string str3 = str2 + "\t</tbody> \r\n</table>";
        //main.addText("Services Completed Successfully");
        return str3;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the Service List :( \n\n Error: \n" + (object) ex);
        //main.addText("Services Completed with Errors");
      }
      return "Error in Services Retrieval";
    }
  }
}
