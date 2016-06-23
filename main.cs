using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

//
// Feature List:
// - 'User choose to not upload stuff'
// - Include complete meta info tab.
// - Motherboard info would be nice.
// - RAM current speed, default speed, voltage -- A check for bad OCs.
//
// Testing:
// - Verifying failure states are being reported
// 
//

namespace sysinfo
{
    public partial class main : Form
    {
        private Reporting reporting = new Reporting();
        private Hash hash = new Hash();
        private OperatingSystem os = new OperatingSystem();
        private CPU cpu = new CPU();
        private GFX gfx = new GFX();
        private HardDrive hardDrive = new HardDrive();
        private InstalledPrograms programs = new InstalledPrograms();
        private Motherboard mb = new Motherboard();
        private Minidump minidump = new Minidump();
        private PageFile pageFile = new PageFile();
        private ProcessList processes = new ProcessList();
        private RAM memory = new RAM();
        private ServiceList services = new ServiceList();
        private StartupItems startupItems = new StartupItems();
        private HardDrive hd = new HardDrive();
        private SystemLogs syslogs = new SystemLogs();
        private Drivers drivers = new Drivers();

        private string reportGeneral = "";
        private string reportPrograms = "";
        private string reportLogs = "";
        private string reportDrivers = "";

        public string reportID = "";
        private string reporturl = "";

        private const string version = "KompDiag - Version 1.0.1";

        Image GRAY = sysinfo.Properties.Resources.gray;
        Image GREEN = sysinfo.Properties.Resources.green;
        Image YELLOW = sysinfo.Properties.Resources.yellow;
        Image RED = sysinfo.Properties.Resources.red;

        public main()
        {
            InitializeComponent();
        }

        private void resetStatusLights()
        {
            changeStatus(GRAY, uploadingBox);
            changeStatus(GRAY, ntpBox);
            changeStatus(GRAY, osBox);
            changeStatus(GRAY, cpuBox);
            changeStatus(GRAY, gfxBox);
            changeStatus(GRAY, pagefileBox);
            changeStatus(GRAY, memoryBox);
            changeStatus(GRAY, hdBox);
            changeStatus(GRAY, crashBox);
            changeStatus(GRAY, processListBox);
            changeStatus(GRAY, serviceListBox);
            changeStatus(GRAY, startupItemsBox);
            changeStatus(GRAY, systemLogsBox);
        }
        private void resetReports()
        {
            reportGeneral = "";
            reportPrograms = "";
            reportLogs = "";
            reportDrivers = "";
        }
        private void createReport_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                resetStatusLights();
                resetReports();

                GenerateReport();
                uploadReport(reportGeneral, "index.html");
                uploadReport(reportPrograms, "programs.html");
                uploadReport(reportLogs, "logs.html");
                uploadReport(reportDrivers, "drivers.html");

                changeStatus(GREEN, uploadingBox);

                urlBox.Text = reporturl;

                detailsBox.AppendText(Environment.NewLine + Environment.NewLine + "Report successfully created and upload:  " + reporturl + Environment.NewLine);

            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Failed to generate/upload report \n\n Error: \n" + (object)ex);
                changeStatus(RED, uploadingBox);
            }

        }
        public void changeStatus(Image color, PictureBox box)
        {
            box.Image = color;
            box.Refresh();
        }
        public void createFtpDir()
        {
            WebRequest createDir = WebRequest.Create("ftp://kautzman.com/" + reportID);
            createDir.Method = WebRequestMethods.Ftp.MakeDirectory;
            createDir.Credentials = (ICredentials)new NetworkCredential("kompdiag@kautzman.com", "sysUpload2");
            using (var resp = (FtpWebResponse)createDir.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
        }
        public string createHeaders(int col, string styleSheet)
        {
            return "<html> \r\n<head>  \r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"../" + styleSheet + " \">" +
                "<style>tr.navbar:nth-child(odd) td:nth-child(" + col + ") {background: url(noise-diagonal.png), linear-gradient(#006, #003) !important;text-align:center;</style>" +
                "\r\n</head> \r\n<body> \r\n<table class=\"header\"> \r\n<col style=\"width:220px\">\r\n<col style=\"width:220px\">\r\n<col style=\"width:220px\"> \r\n<col style=\"width:240px\"> \r\n<thead> \r\n<tr> \r\n<th colspan=\"4\">" + version + "</th> \r\n</tr> \r\n</thead>" +
                "\r\n<tr class=\"navbar\"><td><a href=\"index.html\">General / Hardware</a></td><td><a href=\"drivers.html\">Drivers</a></td><td><a href=\"programs.html\">Programs and Services</td></a><td><a href=\"logs.html\">Crashes and Logs</a></td></tr>\r\n</table>" +
                "\r\n<table> \r\n<col style=\"width:1000px\"> \r\n<thead> \r\n<tr> \r\n<th>Report Generated On: " + DateTime.Now + " Local Time</th> \r\n</tr> \r\n</thead> \r\n</table>";
        }
        private void GenerateReport()
        {

            reportID = "";
            detailsBox.AppendText(version + Environment.NewLine);
            changeStatus(YELLOW, ntpBox);

            
            try
            {
                reportID += hash.createHash();
                detailsBox.AppendText(Environment.NewLine + "Hash created Successfully: " + reportID);
                changeStatus(GREEN, ntpBox);
            }
            catch(Exception e)
            {
                detailsBox.AppendText(Environment.NewLine + "Error creating hash:\n" + e);
                changeStatus(RED, ntpBox);
            }

            try
            {
                createFtpDir();
            }
            catch (Exception e)
            {
                detailsBox.AppendText(Environment.NewLine + "Error creating remote directory:\n" + e);
            }

            try
            {
                reportGeneral += createHeaders(1, "global3.css");
                reportPrograms += createHeaders(3 ,"global3.css");
                reportLogs += createHeaders(4, "global3nofade.css");
                reportDrivers += createHeaders(2, "global3nofade.css");
            }
            catch (Exception e)
            {
                detailsBox.AppendText(Environment.NewLine + "Error creating headers:\n" + e);
            }

            if (osCheck.Checked)
            {
                changeStatus(YELLOW, osBox);

                try
                {
                    reportGeneral += os.getOS();
                    detailsBox.AppendText(Environment.NewLine + "OS Completed Successfully");
                    changeStatus(GREEN, osBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error getting Operating System Info:\n" + e);
                    changeStatus(RED, osBox);
                }
            }

            if (memoryCheck.Checked)
            {
                changeStatus(YELLOW, memoryBox);

                try
                {
                    reportGeneral += memory.getMemory();
                    detailsBox.AppendText(Environment.NewLine + "RAM Completed Successfully");
                    changeStatus(GREEN, memoryBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error getting Memory Info:\n" + e);
                    changeStatus(RED, memoryBox);
                }
            }

            if (cpuCheck.Checked)
            {
                changeStatus(YELLOW, cpuBox);

                try
                {
                    reportGeneral += cpu.getCPU();
                    detailsBox.AppendText(Environment.NewLine + "CPU Completed Successfully");
                    changeStatus(GREEN, cpuBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error getting CPU Info:\n" + e);
                    changeStatus(RED, cpuBox);
                }
            }

            if (gfxCheck.Checked)
            {
                changeStatus(YELLOW, gfxBox);

                try
                {
                    reportGeneral+= gfx.getGFXUnit();
                    detailsBox.AppendText(Environment.NewLine + "GFX Completed Successfully");
                    changeStatus(GREEN, gfxBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error getting GFX Info:\n" + e);
                    changeStatus(RED, gfxBox);
                }
            }

            if (pagefileCheck.Checked)
            {
                changeStatus(YELLOW, pagefileBox);

                try
                {
                    reportGeneral += pageFile.getPageFile();
                    detailsBox.AppendText(Environment.NewLine + "Page File Completed Successfully");
                    changeStatus(GREEN, pagefileBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error getting Page File Info:\n" + e);
                    changeStatus(RED, pagefileBox);
                }
            }

            if (hdCheck.Checked)
            {
                changeStatus(YELLOW, hdBox);

                try
                {
                    reportGeneral += hd.buildData();
                    reportGeneral += hd.getHDDSpace();
                    detailsBox.AppendText(Environment.NewLine + "Hard Drive Completed Successfully");
                    changeStatus(GREEN, hdBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error getting Hard Drive Info:\n" + e);
                    detailsBox.AppendText(Environment.NewLine + Environment.NewLine + "Hard Drive SMART Data might only be retrievable when running as Administrator!" + Environment.NewLine);
                    changeStatus(RED, hdBox);
                }
            }

            if (crashCheck.Checked)
            {
                changeStatus(YELLOW, crashBox);

                try
                {
                    reportLogs += minidump.getCrashes(reportID);
                    detailsBox.AppendText(Environment.NewLine + "Minidump Completed Successfully");
                    changeStatus(GREEN, crashBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error processing Minidump Files:\n" + e);
                    changeStatus(RED, crashBox);
                }
            }

            if (processListCheck.Checked)
            {
                changeStatus(YELLOW, processListBox);

                try
                {
                    reportPrograms += processes.getProcessList();
                    detailsBox.AppendText(Environment.NewLine + "Process List Completed Successfully");
                    changeStatus(GREEN, processListBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error retrieving Process List:\n" + e);
                    changeStatus(RED, processListBox);
                }
            }

            if (serviceListCheck.Checked)
            {
                changeStatus(YELLOW, serviceListBox);

                try
                {
                    reportPrograms += services.getServiceList();
                    detailsBox.AppendText(Environment.NewLine + "Services Completed Successfully");
                    changeStatus(GREEN, serviceListBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error retrieving Services:\n" + e);
                    changeStatus(RED, serviceListBox);
                }
            }

            if (startupItemsCheck.Checked)
            {
                changeStatus(YELLOW, startupItemsBox);

                try
                {
                    reportPrograms += startupItems.getStartupItems();
                    detailsBox.AppendText(Environment.NewLine + "Startup Items Completed Successfully");
                    changeStatus(GREEN, startupItemsBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error retrieving Startup Items:\n" + e);
                    changeStatus(RED, startupItemsBox);
                }
            }

            if (systemLogsCheck.Checked)
            {
                changeStatus(YELLOW, systemLogsBox);

                try
                {
                    if (limitBox.Checked)
                    {
                        reportLogs += syslogs.getLogs(100);
                        detailsBox.AppendText(Environment.NewLine + "System Logs Completed Successfully");
                        changeStatus(GREEN, systemLogsBox);
                    }
                    else
                    {
                        reportLogs += syslogs.getLogs(500);
                        detailsBox.AppendText(Environment.NewLine + "System Logs Completed Successfully");
                        changeStatus(GREEN, systemLogsBox);
                    }
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error retrieving System Logs:\n" + e);
                    changeStatus(RED, systemLogsBox);
                }
            }

            if (driversCheck.Checked)
            {
                changeStatus(YELLOW, driversBox);

                try
                {
                        reportDrivers += drivers.getDrivers();
                        detailsBox.AppendText(Environment.NewLine + "Drviers Completed Successfully");
                        changeStatus(GREEN, driversBox);
                }
                catch (Exception e)
                {
                    detailsBox.AppendText(Environment.NewLine + "Error retrieving Drivers:\n" + e);
                    changeStatus(RED, driversBox);
                }
            }
        }
        public void uploadReport(string report, string fileName)
        {
            changeStatus(YELLOW, uploadingBox);
            this.reporturl = "http://www.kautzman.com/kompdiag/" + reportID + "/";
            try
            {                
                //main.addText("\r\n --- Beginning Upload ---");

                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create("ftp://www.kautzman.com/" + reportID + "/" + fileName);
                ftpWebRequest.Method = "STOR";
                ftpWebRequest.Credentials = new NetworkCredential("USERNAME", "PASSWORD"); //These Creds are placeholder!

                byte[] bytes = Encoding.UTF8.GetBytes(report);
                ftpWebRequest.ContentLength = (long)bytes.Length;

                Stream requestStream = ftpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                Console.WriteLine("Upload File Complete, status {0}", (object)ftpWebResponse.StatusDescription);
                ftpWebResponse.Close();

                //main.addText("Upload Completed Successfully!");
                //main.addText("Report available at:");
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Error uploading file to server \r\n\r\n" + (object)ex);
                //main.addText("Upload Failed!");
            }
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KompDiag is a free piece of software that can be used to help determine issues with a computer from a remote location. " +
                "This was originally created to help me diagnose issues users were having on /r/techsupport, but anyone is free to use it for their own purposes as well.\n\n" +
                "For questions or to report a bug, please email me at mkautzm@gmail.com");
        }
    }
}
