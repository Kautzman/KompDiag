using System;
using System.IO;
using System.Net;

namespace sysinfo
{
  internal class Minidump
  {
    private string path = "C:\\Windows\\Minidump";
    private string reportHead = "<table> \r\n<col style=\"width:300px\"> \r\n<col style=\"width:300px\"> \r\n<thead> \r\n<tr> \r\n<th>Minidump Files (3 most recent)</th> \r\n<th>Download Link</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n";
    private string noDumps = "<table> \r\n<col style=\"width:300px\"> \r\n<col style=\"width:300px\"> \r\n<thead> \r\n<tr> \r\n<th>Minidump Files (3 most recent)</th> \r\n<th>Download Link</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n<tr> \r\n<td>No Crash Reports</td> \r\n<td>No Crash Reports</td> \r\n</tr> \r\n</tbody> \r\n</table>";
    private string reportID;

    public string getCrashes(string report)
    {
      //main.addText(" - Beginning Minidump Check");
      this.reportID = report;
      return this.checkdir();
    }

    public string checkdir()
    {
      Console.WriteLine("in checkDir, path = " + this.path);
      if (Directory.Exists(this.path))
      {
        Console.WriteLine("checkDir evaluates true;");
        return this.ProcessMinidumpList();
      }
      else
      {
        Console.WriteLine("checkDir evaulates false;");
        //main.addText("Minidump Check Complete (no reports)");
        return this.noDumps;
      }
    }

    public string ProcessMinidumpList()
    {
      string str1 = this.reportHead;
      string[] files = Directory.GetFiles(this.path);
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = files.Length - index1 - 1;
        if (index2 >= 0)
        {
          int num = files[index2].LastIndexOf("\\");
          string file = files[index2].Substring(num + 1);
          Console.WriteLine("working on: " + files[index2]);
          //string str2 = this.reportID + "-" + file;
          str1 = str1 + "<tr> \r\n<td>" + file + "</td> \r\n<td><a href=\"/kompdiag/"+ reportID + "/" + file + "\">Download</a></td> \r\n</tr> \r\n";
          Console.WriteLine("FileName: " + files[index2] + "\n");
          try
          {
            Console.WriteLine("FileName After Split: " + file + "\n");
            this.UploadMinidump(file, files[index2]);
          }
          catch (Exception ex)
          {
            Console.WriteLine("Failed to upload a Minidump file -- " + (object) ex);
          }
        }
      }
      return str1 + "</tbody> \r\n</table>";
    }

    public void UploadMinidump(string file, string dir)
    {
        string str = "http://www.kautzman.com/" + reportID + "/" + file;

        //main.addText("\r\n --- Uploading Minidump File: " + file + " ---");
        FileInfo fileInfo = new FileInfo(dir);
        FtpWebRequest ftpWebRequest = (FtpWebRequest) WebRequest.Create("ftp://www.kautzman.com/" + reportID + "/" + file);
          Console.WriteLine("Request for: " + "ftp://www.kautzman.com/" + reportID + "/" + file);
        ftpWebRequest.Method = "STOR";
        ftpWebRequest.Credentials = (ICredentials) new NetworkCredential("kompdiag@kautzman.com", "sysUpload2");
        ftpWebRequest.KeepAlive = false;
        ftpWebRequest.UseBinary = true;
        ftpWebRequest.ContentLength = fileInfo.Length;
        ftpWebRequest.Method = "STOR";
        int count1 = 16384;
        byte[] buffer = new byte[count1];
        FileStream fileStream = fileInfo.OpenRead();
        try
        {
          Stream requestStream = ftpWebRequest.GetRequestStream();
          int count2;
          while ((count2 = fileStream.Read(buffer, 0, count1)) != 0)
            requestStream.Write(buffer, 0, count2);
          requestStream.Close();
          fileStream.Close();
        }
        catch (Exception ex)
        {
         Console.WriteLine("Minidump Upload Failed inside Stream Writer!" + (object) ex);
        }
        Console.WriteLine(file + " upload complete!");
      }
    }
  }