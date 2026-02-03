using Grimoire.Networking;
using System.Net;
using System.Net.Sockets;

namespace Grimoire.Utils
{
    public class NetworkUtils
    {
        public static int GetAvailablePort()
        {
            int port;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                port = ((IPEndPoint)socket.LocalEndPoint).Port;
            }
            return port;
        }

		public static Message CreateMessage(string raw)
		{
			if (raw != null && raw.Length > 0)
			{
				switch (raw.Trim()[0])
				{
					case '%':
						return new XtMessage(raw);
					case '<':
						return new XmlMessage(raw);
					case '{':
						return new JsonMessage(raw);
				}
			}

			return null;
		}
	}
}