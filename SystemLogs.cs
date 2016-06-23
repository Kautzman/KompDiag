using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace sysinfo
{
    class SystemLogs
    {
        public string getLogs()
        {
            String writeEntry = "";
            String machine = "."; // local machine
            String tablehead = "<table> \r\n<table> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:200px\">";
            String tablecolumns = "\r\n<thead> \r\n<tr> \r\n<th>Entry Type</th> \r\n<th>Time Written</th> \r\n<th>Source</th> \r\n<th>Event ID</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n";
            String tablefooter = "</tbody> \r\n</table>";

            String log = "system";
            EventLog aLog = new EventLog(log, machine);
            EventLogEntry entry;
            EventLogEntryCollection entries = aLog.Entries;
            Stack<EventLogEntry> stack = new Stack<EventLogEntry>();
            for (int i = entries.Count - 1; i > 1; i--)
            {
                if(entries[i].EntryType != EventLogEntryType.Information)
                { 
                    entry = entries[i];

                    writeEntry += "<tr> \r\n<td>" + entry.EntryType + "</td> \r\n<td>"
                        + entry.TimeWritten + "</td> \r\n<td>"
                        + entry.Source + "</td> \r\n<td>"
                        + entry.InstanceId + "</td> \r\n</tr> \r\n<tr> \r\n<td colspan=\"4\">"
                        + entry.Message + "</td> \r\n</tr> \r\n<tr colspan=\"4\"> \r\n<td>&nbsp \r\n</td> \r\n</tr>";

                    //writeEntry += "\n[EntryType]\t" + entry.EntryType +
                    //"\n[TimeWritten]\t" + entry.TimeWritten +
                    //"\n[Source]\t" + entry.Source +
                    //"\n[EventID]\t" + entry.EventID +
                    //"\n[Message]\t" + entry.Message +
                    //"\n---------------------------------------------------\n";
                }
            }
            return tablehead + tablecolumns + writeEntry + tablefooter;
        }

        public string getLogs(int count)
        {
            String writeEntry = "";
            String machine = "."; // local machine
            String tablehead = "<table> \r\n<table> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:250px\"> \r\n<col style=\"width:200px\">";
            String tablecolumns = "\r\n<thead> \r\n<tr> \r\n<th>Entry Type</th> \r\n<th>Time Written</th> \r\n<th>Source</th> \r\n<th>Event ID</th> \r\n</tr> \r\n</thead> \r\n<tbody> \r\n";
            String tablefooter = "</tbody> \r\n</table>";

            String log = "system";
            EventLog aLog = new EventLog(log, machine);
            EventLogEntry entry;
            EventLogEntryCollection entries = aLog.Entries;
            Stack<EventLogEntry> stack = new Stack<EventLogEntry>();
            for (int i = entries.Count - 1; i > 1; i--)
            {
                if (entries[i].EntryType != EventLogEntryType.Information)
                {
                    entry = entries[i];

                    writeEntry += "<tr> \r\n<td>" + entry.EntryType + "</td> \r\n<td>"
                        + entry.TimeWritten + "</td> \r\n<td>"
                        + entry.Source + "</td> \r\n<td>"
                        + entry.InstanceId + "</td> \r\n</tr> \r\n<tr> \r\n<td colspan=\"4\">"
                        + entry.Message + "</td> \r\n</tr> \r\n<tr colspan=\"4\"> \r\n<td>&nbsp \r\n</td> \r\n</tr>";

                    //writeEntry += "\n[EntryType]\t" + entry.EntryType +
                    //"\n[TimeWritten]\t" + entry.TimeWritten +
                    //"\n[Source]\t" + entry.Source +
                    //"\n[EventID]\t" + entry.EventID +
                    //"\n[Message]\t" + entry.Message +
                    //"\n---------------------------------------------------\n";

                    count--;
                    if(count < 1)
                    {
                        break;
                    }
                }
            }
            return tablehead + tablecolumns + writeEntry + tablefooter;
        }
    }
}
