using System;
using System.Net;
using System.Net.Sockets;

namespace sysinfo
{
  internal class SimpleNTP
  {
    public static DateTime GetNetworkTime()
    {
      byte[] buffer = new byte[48];
      buffer[0] = (byte) 27;
      IPEndPoint ipEndPoint = new IPEndPoint(Dns.GetHostEntry("pool.ntp.org").AddressList[0], 123);
      Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      socket.Connect((EndPoint) ipEndPoint);
      socket.Send(buffer);
      socket.Receive(buffer);
      socket.Close();
      return new DateTime(1900, 1, 1).AddMilliseconds((double) (long) (((ulong) ((long) buffer[40] << 24 | (long) buffer[41] << 16 | (long) buffer[42] << 8) | (ulong) buffer[43]) * 1000UL + ((ulong) ((long) buffer[44] << 24 | (long) buffer[45] << 16 | (long) buffer[46] << 8) | (ulong) buffer[47]) * 1000UL / 4294967296UL));
    }
  }
}
