using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Used to check open ports.
	/// </summary>
	public static class PortChecker
	{
		/// <summary>
		/// Indicates if a port can be used by iKiwi in this machine.
		/// </summary>
		/// <param name="Port">Number of port.</param>
		/// <returns>Returns True if the port can be used or is already in use by iKiwi, else return False.</returns>
		public static bool IsCanUsePort(int Port)
		{
			if (Global.UseUpnp == true)
			{
				// get the info about this UPnP port
				Utilities.UPnP.UpnpPort upnpPort = Utilities.UPnP.GetInfoUpnpPort(Port);
				
				// control if the port exist
				if (upnpPort == null)
				{
					return true;
				}
				else
				{
					// control if the port is used by a other machine
					if (upnpPort.InternalIP != Global.GetMyLocalIP())
					{
						return false;
					}
					else
					{
						// control if the port is used by an other program in this machine
						if (IsUsedPort(Port) == false)
						{
							return true;
						}
						else
						{
							// control if be this iKiwi client to use this port
							if (Daemons.Bouncer.ListeningPort == Port && Daemons.Bouncer.Enabled == true)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			else
			{
				try
				{
					TcpListener listener = new TcpListener(IPAddress.Any, Port);
					
					listener.Start();
					
					TcpClient client = new TcpClient();
					
					client.Connect(Global.MyRemoteIP, Port);
					
					client.Close();
					listener.Stop();
					
					return true;
				}
				catch
				{
					return false;
				}
			}
		}
		
		/// <summary>
		/// Indicates if other programs (in this machine) are using this port.
		/// </summary>
		/// <param name="Port">The port number to control.</param>
		/// <returns>Returns False if the port is not used by other programs in this machine, else return True.</returns>
		public static bool IsUsedPort (int Port)
		{
			// get info about the tcp connections in this machine
			
			IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			
			TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
			
			// control the tcp port used by other program in this machine
			for (int i = 0; i < tcpConnInfoArray.Length; i++)
			{
				if (tcpConnInfoArray[i].LocalEndPoint.Port == Port)
				{
					return false;
				}
			}
			
			return true;
		}
		
		/// <summary>
		/// Checks if the listening port number is good, else will open the port helper or automatically select random port number if the UPnP is enabled.
		/// </summary>
		public static void CheckListeningPort()
		{
			if (IsCanUsePort(Global.ListeningPort) == false)
			{
				if (Global.UseUpnp == true)
				{
					int randomPortNumber = Utilities.UPnP.GetRandomPort();
					
					Global.ListeningPort = randomPortNumber;
					
					Utilities.Configurator.SaveAll();
				}
				else
				{
					Utilities.PortHelper.Start();
				}
			}
		}
	}
}
