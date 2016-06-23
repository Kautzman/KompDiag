using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace sysinfo
{
  internal class ProcessList
  {
    public string getProcessList()
    {
      try
      {
        string str1 = "";
        Process[] processes = Process.GetProcesses();
        string str2 = str1 + "<table> \r\n<col style=\"width:400px\"> \r\n<col style=\"width:200px\"> \r\n<thead> \r\n<tr> \r\n<th>Process Name</th> \r\n<th>Memory Usage</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n";
        foreach (Process process in processes)
        {
          double num = Math.Round((double) process.PrivateMemorySize64 / 1048576.0, 2);
          str2 = str2 + (object)"<tr> \r\n<td>" + process.ProcessName + "</td> \r\n<td>" + num.ToString() + " MB </td> \r\n</tr> \r\n";
        }
        string str3 = str2 + "</tbody> \r\n</table>";
        //main.addText("Process List Completed Successfully");
        return str3;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("A bad thing happened when attempting to get the Process List :( \n\n Error: \n" + (object) ex);
        //main.addText("Process List Completed with Errors");
      }
      return "Error in Process List Retrieval";
    }
  }
}
