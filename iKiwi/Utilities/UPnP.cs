using System;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Class for the UPnP protocol.
	/// </summary>
	public class UPnP
	{
		#region Enum
		
		/// <summary>
		/// List of protocols.
		/// </summary>
		public enum Protocol
		{
			/// <summary>
			/// TCP protocol.
			/// </summary>
			TCP,
			/// <summary>
			/// UDP protocol.
			/// </summary>
			UDP
		}
		
		#endregion
		
		#region Classes
		
		/// <summary>
		/// Object that represents a UPnP port.
		/// </summary>
		public class UpnpPort
		{
			#region Ctor
			
			/// <summary>
			/// Create a UPnP port object.
			/// </summary>
			public UpnpPort()
			{
			}
			
			#endregion
			
			#region Properties
			
			/// <summary>
			/// The internal IP address.
			/// </summary>
			public string InternalIP
			{ get; set; }
			
			/// <summary>
			/// The external IP address.
			/// </summary>
			public string ExternalIP
			{ get; set; }
			
			/// <summary>
			/// The port number.
			/// </summary>
			public int Port
			{ get; set; }
			
			/// <summary>
			/// Indicates if the UPnP port is enabled.
			/// </summary>
			public bool Enabled
			{ get; set; }
			
			/// <summary>
			/// The protocol used for the UPnP port.
			/// </summary>
			public Protocol Protocol
			{ get; set; }
			
			/// <summary>
			/// The description of the UPnP port.
			/// </summary>
			public string Description
			{ get; set; }
			
			#endregion
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Opens a port with the UPnP protocol.
		/// </summary>
		/// <param name="PortNumber">The number of the port to add.</param>
		/// <param name="protocol">The protocol used for the port.</param>
		public static void OpenPort(int PortNumber, Protocol protocol)
		{
			NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
			
			NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;
			
			if (mappings != null)
			{
				mappings.Add(PortNumber, protocol.ToString(), PortNumber, Global.GetMyLocalIP(), true, "iKiwi");
				
				Utilities.Log.Write("Opened the port " + PortNumber + " with the UPnP protocol", Utilities.Log.LogCategory.Info);
			}
		}
		
		/// <summary>
		/// Closes a port with the UPnP protocol.
		/// </summary>
		/// <param name="PortNumber">The number of the port to close.</param>
		/// <param name="protocol">The protocol used for the port.</param>
		public static void ClosePort(int PortNumber, Protocol protocol)
		{
			NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
			
			NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;
			
			if (mappings != null)
			{
				mappings.Remove(PortNumber, protocol.ToString());
				
				Utilities.Log.Write("Closed the port " + PortNumber + " with the UPnP protocol", Utilities.Log.LogCategory.Info);
			}
		}
		
		/// <summary>
		/// Checks if the port is already used with the UPnP protocol or not.
		/// </summary>
		/// <param name="PortNumber">The port number to check.</param>
		/// <returns>Returns true if the port is used, else returns false.</returns>
		public static bool IsUsedPort(int PortNumber)
		{
			NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
			
			NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;
			
			if (mappings != null)
			{
				foreach(NATUPNPLib.IStaticPortMapping portMapping in mappings)
				{
					if (portMapping.ExternalPort == PortNumber)
					{
						return true;
					}
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Gets info about a UPnP port.
		/// </summary>
		/// <param name="PortNumber">The number of the port to get info.</param>
		/// <returns>The info about the UPnP port. Return null if is the UPnP port is not exist.</returns>
		public static UpnpPort GetInfoUpnpPort(int PortNumber)
		{
			NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
			
			NATUPNPLib.IStaticPortMappingCollection mappings = upnpnat.StaticPortMappingCollection;
			
			if (mappings != null)
			{
				foreach(NATUPNPLib.IStaticPortMapping portMapping in mappings)
				{
					if (portMapping.ExternalPort == PortNumber)
					{
						UpnpPort port = new UpnpPort();
						
						port.Description = portMapping.Description;
						port.Enabled = portMapping.Enabled;
						port.InternalIP = portMapping.InternalClient;
						port.ExternalIP = portMapping.ExternalIPAddress;
						port.Port = portMapping.ExternalPort;
						port.Protocol = (Protocol)Enum.Parse(typeof(Protocol), portMapping.Protocol, true);
						
						return port;
					}
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Opens a random port (TCP protocol) and controls that it is free for be used.
		/// </summary>
		/// <returns>Returns the open port number.</returns>
		public static int OpenRandomPort()
		{
			int nPort = GetRandomPort();
			
			OpenPort(nPort, Utilities.UPnP.Protocol.TCP);
			
			return nPort;
		}
		
		/// <summary>
		/// Get random port number (TCP protocol) and controls that it is free for be used.
		/// </summary>
		/// <returns>Returns the port number.</returns>
		public static int GetRandomPort()
		{
			Random random = new Random();
			
			int i = 5000;
			
			while (true)
			{
				i = random.Next(5000, 64000);
				
				if (IsUsedPort(i) == false)
				{
					return i;
				}
			}
		}
		
		/// <summary>
		/// Get the remote IP of this machine.
		/// </summary>
		/// <returns>Returns the remote IP of this machine, else if there is a problem returns Null.</returns>
		public static string GetRemoteIP()
		{
			UpnpPort port = GetInfoUpnpPort(Global.ListeningPort);
			
			if (port != null)
			{
				return port.ExternalIP;
			}
			else
			{
				int nPort = OpenRandomPort();
				
				port = GetInfoUpnpPort(nPort);
				
				ClosePort(nPort, Protocol.TCP);
				
				if (port != null)
				{
					return port.ExternalIP;
				}
				else
				{
					return null;
				}
			}
		}
		
		#endregion
	}
}