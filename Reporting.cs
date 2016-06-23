using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace sysinfo
{
  internal class Reporting
  {
      // 
      // No Longer in Use
      //
    private void writeReport(string text)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.InitialDirectory = Convert.ToString((object) Environment.SpecialFolder.Personal);
      saveFileDialog.Filter = "Text Document (*.txt)|*.txt";
      string str = "C:\\sysinfodump.txt";
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
        str = saveFileDialog.FileName;
      using (StreamWriter streamWriter = new StreamWriter(str))
        streamWriter.Write(text);
      Process.Start("notepad.exe", str);
    }
  }
}
