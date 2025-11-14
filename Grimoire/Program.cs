using Grimoire.Networking;
using Grimoire.UI;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Grimoire
{
	internal static class Program
	{
		public static readonly string Version = "Li 1.8";
		public static readonly string ReleaseDate = "14-11-2025";
		public static string PluginsPath { get; private set; }
		public static Tools.Plugins.PluginManager PluginsManager { get; private set; }

		[STAThread]
		private static void Main()
		{
			Program.TryCreateDirectory(Program.PluginsPath = Path.Combine(Application.StartupPath, "Plugins"));
			if (FindAvailablePort(out int port))
			{
				Proxy.Instance.ListenerPort = port;
				PluginsManager = new Tools.Plugins.PluginManager();
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
				Application.Run(new Root());
				Program.PluginsManager.UnloadAll();
				Proxy.Instance.Stop(appClosing: false);
			}
		}

		private static void TryCreateDirectory(string dir)
		{
			try
			{
				Directory.CreateDirectory(dir);
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show(string.Format("Failed to create directory: {0}\nAccess denied", dir));
			}
			catch (PathTooLongException)
			{
				MessageBox.Show(string.Format("Failed to create directory: {0}\nThe specified path is too long.", dir) + "Try moving the Grimoire directory out of the current directory");
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Failed to create directory {0}\n{1}", dir, ex.Message));
			}
		}

		private static bool FindAvailablePort(out int port)
		{
			Random random = new Random();
			IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			TcpConnectionInformation[] activeTcpConnections;
			IPEndPoint[] activeTcpListeners;
			try
			{
				activeTcpConnections = iPGlobalProperties.GetActiveTcpConnections();
				activeTcpListeners = iPGlobalProperties.GetActiveTcpListeners();
			}
			catch (NetworkInformationException)
			{
				port = 0;
				return false;
			}
			int randPort;
			TcpConnectionInformation tcpConnectionInformation;
			IPEndPoint iPEndPoint;
			do
			{
				randPort = random.Next(1001, 65535);
				tcpConnectionInformation = activeTcpConnections.FirstOrDefault((TcpConnectionInformation c) => c.LocalEndPoint.Port == randPort);
				iPEndPoint = activeTcpListeners.FirstOrDefault((IPEndPoint l) => l.Port == randPort);
			}
			while (tcpConnectionInformation != null || iPEndPoint != null);
			port = randPort;
			return true;
		}
	}
}