using System;
using System.Collections.Generic;
using System.Management;
using System.IO;

namespace sysinfo
{
  public class HardDrive
  {
    public string buildData()
    {
      string str1 = "";
      try
      {
        Dictionary<int, HDD> dictionary = new Dictionary<int, HDD>();
        ManagementObjectSearcher managementObjectSearcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
        int key = 0;
        foreach (ManagementObject managementObject in managementObjectSearcher1.Get())
        {
          dictionary.Add(key, new HDD()
          {
            Model = managementObject["Model"].ToString(),
            Type = managementObject["InterfaceType"].ToString().Trim()
          });
          ++key;
        }
        ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
        int index1 = 0;
        foreach (ManagementObject managementObject in managementObjectSearcher2.Get())
        {
          if (index1 < dictionary.Count)
          {
            dictionary[index1].Serial = managementObject["SerialNumber"] == null ? "None" : managementObject["SerialNumber"].ToString().Trim();
            ++index1;
          }
          else
            break;
        }
        ManagementObjectSearcher managementObjectSearcher3 = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
        managementObjectSearcher3.Scope = new ManagementScope("\\root\\wmi");
        managementObjectSearcher3.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictStatus");
        int index2 = 0;
        foreach (ManagementObject managementObject in managementObjectSearcher3.Get())
        {
          dictionary[index2].IsOK = !(bool) managementObject.Properties["PredictFailure"].Value;
          ++index2;
        }
        managementObjectSearcher3.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
        int index3 = 0;
        foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher3.Get())
        {
          byte[] numArray = (byte[]) managementBaseObject.Properties["VendorSpecific"].Value;
          for (int index4 = 0; index4 < 30; ++index4)
          {
            try
            {
              int index5 = (int) numArray[index4 * 12 + 2];
              bool flag = ((int) numArray[index4 * 12 + 4] & 1) == 1;
              int num1 = (int) numArray[index4 * 12 + 5];
              int num2 = (int) numArray[index4 * 12 + 6];
              int num3 = BitConverter.ToInt32(numArray, index4 * 12 + 7);
              if (index5 != 0)
              {
                Smart smart = dictionary[index3].Attributes[index5];
                smart.Current = num1;
                smart.Worst = num2;
                smart.Data = num3;
                smart.IsOK = !flag;
              }
            }
            catch
            {
            }
          }
          ++index3;
        }
        managementObjectSearcher3.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
        int index6 = 0;
        foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher3.Get())
        {
          byte[] numArray = (byte[]) managementBaseObject.Properties["VendorSpecific"].Value;
          for (int index4 = 0; index4 < 30; ++index4)
          {
            try
            {
              int index5 = (int) numArray[index4 * 12 + 2];
              int num = (int) numArray[index4 * 12 + 3];
              if (index5 != 0)
                dictionary[index6].Attributes[index5].Threshold = num;
            }
            catch
            {
            }
          }
          ++index6;
        }
        foreach (KeyValuePair<int, HDD> keyValuePair1 in dictionary)
        {
          string str2 = "BAD";
          if (keyValuePair1.Value.IsOK)
            str2 = "OK";
          str1 = str1 + "<table> \r\n<col style=\"width:550px\"> \r\n<col style=\"width:100px\"> \r\n<col style=\"width:100px\"> \r\n<col style=\"width:100px\"> \r\n<col style=\"width:100px\"> \r\n<col style=\"width:50px\"> \r\n<thead> \r\n<tr> \r\n<th colspan=\"6\"> HARD DRIVE (" + str2 + ") : " + keyValuePair1.Value.Serial + " - " + keyValuePair1.Value.Model + " - " + keyValuePair1.Value.Type + "</th> \r\n</tr> \r\n<tr> \r\n<th>Attribute Name</th> \r\n<th>Value</th> \r\n<th>Current</th> \r\n<th>Worst</th> \r\n<th>Threshold</th> \r\n<th>Status</th> \r\n</tr> \r\n</thead> \r\n<tbody>\r\n";
          foreach (KeyValuePair<int, Smart> keyValuePair2 in keyValuePair1.Value.Attributes)
          {
            string str3 = "BAD";
            if (keyValuePair2.Value.IsOK)
              str3 = "OK";
            if (keyValuePair2.Value.HasData)
                str1 = str1 + (object)"<tr> \r\n<td>" + keyValuePair2.Value.Attribute + "</td> \r\n<td>" + keyValuePair2.Value.Data.ToString() + "</td> \r\n<td>" + keyValuePair2.Value.Current.ToString() + "</td> \r\n<td>" + keyValuePair2.Value.Worst.ToString() + "</td> \r\n<td>" + keyValuePair2.Value.Threshold.ToString() + "</td> \r\n<td>" + str3 + "</td> \r\n</tr> \r\n";
          }
          str1 = str1 + "</tbody> \r\n</table>";
        }
        //main.addText("Hard Drives Completed Successfully");
      }
      catch (ManagementException ex)
      {
        str1 = str1 + "<!--ERROR: An error occurred while querying for WMI data: " + ex.Message + "-->";
        //main.addText("Hard Drives Completed with Errors");
      }
      return str1;
    }
    public string getHDDSpace()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        string writeEntry = "<table> \r\n<table> \r\n<col style=\"width:200px\"> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:250px\">" +
            "\r\n<thead> \r\n<tr><th colspan=\"4\">Drive Information by Partition</th></tr>" +
            " \r\n<tr> \r\n<th>Type</th> \r\n<th>Format</th> \r\n<th>Total Size</th> \r\n<th>Free Space</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n";

        float Size = 0;
        float FreeSpace = 0;

        foreach (DriveInfo drive in drives)
        {
            Console.WriteLine(drive.Name);
            if (drive.IsReady)
            {
                Size = (float)drive.TotalSize;
                FreeSpace = (float)drive.TotalFreeSpace;

                Size = Size / 1073741824;
                FreeSpace = FreeSpace / 1073741824;

                writeEntry += "<tr> \r\n<td>" + drive.DriveType + "</td> \r\n<td>"
                + drive.DriveFormat + "</td> \r\n<td>"
                + Size.ToString("0.000") + " GB</td> \r\n<td>"
                + FreeSpace.ToString("0.000") + " GB</td> \r\n</tr>";
            }
        }

        writeEntry += "</tbody> \r\n</table>";

        return writeEntry;
    }
  }
}
