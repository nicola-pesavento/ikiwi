using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace iKiwi
{
	/// <summary>
	/// Global properties and methods.
	/// </summary>
	static class Global
	{
		#region Ctor
		
		/// <summary>
		/// Initializes the Global class.
		/// </summary>
		static Global()
		{
			// control if exist the path where read/write the application files
			if (Directory.Exists(Global.iKiwiPath) == false)
			{
				Directory.CreateDirectory(Global.iKiwiPath);
			}
			
            // load the settings
            Utilities.Configurator.LoadAll();

            // control settings
            Utilities.Configurator.ControlSettings();
			
			// set the properties
			MyRemoteIP = GetMyRemoteIP();
		}
		
		#endregion
		
		#region Methods

		/// <summary>
		/// Returns the remote IP of this computer.
		/// </summary>
		public static string GetMyRemoteIP()
		{
			if (Global.UseUpnp == true)
			{
				string upnpRemoteIp = Utilities.UPnP.GetRemoteIP();
				
				if (upnpRemoteIp != null)
				{
					return upnpRemoteIp;
				}
			}
			
			string[] links = {"http://automation.whatismyip.com/n09230945.asp", "http://www.lawrencegoetz.com/programs/ipinfo/"};
			
			for (int i = 0; i < links.Length; i++)
			{
				try
				{
					string whatIsMyIp = links[i];
					WebClient wc = new WebClient();
					UTF8Encoding utf8 = new UTF8Encoding();
					string requestHtml = "";

					requestHtml = utf8.GetString(wc.DownloadData(whatIsMyIp));

					IPAddress externalIp;
					
					string stringExternalIp = "";
					
					switch (links[i])
					{
						case "http://automation.whatismyip.com/n09230945.asp":
							{
								stringExternalIp = requestHtml;
								
								break;
							}
						case "http://www.lawrencegoetz.com/programs/ipinfo/":
							{
								string[] strs = {"<h1>Your IP address is<BR>\r\n"};
								
								stringExternalIp = requestHtml.Split(strs, 2, StringSplitOptions.RemoveEmptyEntries)[1];
								
								strs[0] = "\r\n";
								
								stringExternalIp = stringExternalIp.Split(strs, 2, StringSplitOptions.RemoveEmptyEntries)[0];
								
								stringExternalIp = stringExternalIp.Trim();

								break;
							}
						default:
							{
								break;
							}
					}
					
					try
					{
						externalIp = IPAddress.Parse(stringExternalIp);
						
						return externalIp.ToString();
					}
					catch
					{
					}
				}
				catch
				{
				}
			}
			
			Utilities.Log.Write("Impossible get the IP Address of this client", Utilities.Log.LogCategory.Error);
			return "ERROR";
		}
		
		/// <summary>
		/// Returns the local IP of this computer.
		/// </summary>
		/// <returns>Returns local IP of this machine.</returns>
		public static string GetMyLocalIP()
		{
			string hostName = "";
			
			hostName = Dns.GetHostName();

			IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostName);

			IPAddress[] addresses = ipEntry.AddressList;

			for (int i = 0; i < addresses.Length; i++)
			{
				if (addresses[i].AddressFamily == AddressFamily.InterNetwork && addresses[i] != IPAddress.Loopback)
				{
					return addresses[i].ToString();
				}
			}
			
			return string.Empty;
		}

		#endregion

		#region Properties
		
		/// <summary>
		/// The version of iKiwi.
		/// </summary>
		public static string iKiwiVersion
		{
			get { return _iKiwiVersion; }
			set { _iKiwiVersion = value; }
		}
		private static string _iKiwiVersion = Application.ProductVersion;
		
		/// <summary>
		/// The path of iKiwi where reading/writing the files.
		/// </summary>
		public static string iKiwiPath
		{
			get { return _iKiwiPath; }
			set { _iKiwiPath = value; }
		}
		#if (DEBUG)
        private static string _iKiwiPath = @"\Debug_temp\";//Application.ExecutablePath.Replace(Process.GetCurrentProcess().ProcessName + ".exe", "").Replace(Process.GetCurrentProcess().ProcessName + ".EXE", "");
		#else
		private static string _iKiwiPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\iKiwi\";// Application.ExecutablePath.Replace(Process.GetCurrentProcess().ProcessName + ".exe", "").Replace(Process.GetCurrentProcess().ProcessName + ".EXE", "");
		#endif
		
		/// <summary>
		/// The remote IP of this machine.
		/// </summary>
		public static string MyRemoteIP
		{
			get { return _MyRemoteIP; }
			set { _MyRemoteIP = value; }
		}
		private static string _MyRemoteIP = "EMPTY";

		/// <summary>
		/// The name of this P2P client.
		/// </summary>
		public static string UserAgent
		{
			get { return _UserAgent; }
			set { _UserAgent = value; }
		}
		private static string _UserAgent = "iKiwi " + Global.iKiwiVersion;

		/// <summary>
		/// The name of the protocol used.
		/// </summary>
		public static string P2P_Protocol
		{
			get { return _P2P_Protocol; }
			set { _P2P_Protocol = value; }
		}
		private static string _P2P_Protocol = "Nova 0.4";
		
		/// <summary>
		/// The version of the nova protocol used.
		/// </summary>
		public static float NovaProtocolVersion
		{
			get { return _NovaProtocolVersion; }
			set { _NovaProtocolVersion = value; }
		}
		private static float _NovaProtocolVersion = 0.4f;
		
		/// <summary>
		/// The default listening port.
		/// </summary>
		public static int ListeningPort
		{
			get { return _ListeningPort; }
			set { _ListeningPort = value; }
		}
		private static int _ListeningPort = 16592;
		
		/// <summary>
		/// The max number if input connections.
		/// </summary>
		public static int MaxNumberInputConnections
		{
			get { return _MaxNumberInputConnections; }
			set { _MaxNumberInputConnections = value; }
		}
		private static int _MaxNumberInputConnections = 100;


		/// <summary>
		/// The directory where there are the shared files.
		/// </summary>
		public static string SharedDirectory
		{
			get { return _SharedDirectory; }
			set { _SharedDirectory = value; }
		}
		private static string _SharedDirectory = "EMPTY";


		/// <summary>
		/// The directory where there are the temporary files ( files in downdloading status ).
		/// </summary>
		public static string TempDirectory
		{
			get { return _TempDirectory; }
			set { _TempDirectory = value; }
		}
		private static string _TempDirectory = "EMPTY";


		/// <summary>
		/// The Peer ID of this client.
		/// </summary>
		public static string MyPeerID
		{
			get { return _MyPeerID; }
			set { _MyPeerID = value; }
		}
		private static string _MyPeerID = "EMPTY";


		/// <summary>
		/// The NovaWebCache for the research of peers.
		/// </summary>
		public static string NovaWebCache
		{
			get { return _NovaWebCache; }
			set { _NovaWebCache = value; }
		}
		private static string _NovaWebCache = "http://ikiwi.sourceforge.net/nwc.php";


		/// <summary>
		/// The minimum number of opened peer-connections.
		/// </summary>
		public static int MinimumPeerConnections
		{
			get { return _MinimumPeerConnections; }
			set { _MinimumPeerConnections = value; }
		}
		private static int _MinimumPeerConnections = 10;
		
		
		/// <summary>
		/// The number of the FS-messages to send for each search.
		/// </summary>
		public static int NumberFSMessagesForSearch
		{
			get { return _NumberFSMessagesForSearch; }
			set { _NumberFSMessagesForSearch = value; }
		}
		private static int _NumberFSMessagesForSearch = 10;
		
		/// <summary>
		/// The Time To Live of the messages.
		/// </summary>
		public static int TtlMessage
		{
			get { return _TtlMessage; }
			set { _TtlMessage = value; }
		}
		private static int _TtlMessage = 6;
		
		/// <summary>
		/// Used to count the download rate.
		/// </summary>
		public static int CountDownloadRate
		{
			get { return _CountDownloadRate; }
			set { _CountDownloadRate = value; }
		}
		private static int _CountDownloadRate = 0;
		
		/// <summary>
		/// The download rate in byte/sec.
		/// </summary>
		public static int DownloadRate
		{
			get { return _DownloadRate; }
			set { _DownloadRate = value; }
		}
		private static int _DownloadRate = 0;
		
		/// <summary>
		/// Used to count the upload rate.
		/// </summary>
		public static int CountUploadRate
		{
			get { return _CountUploadRate; }
			set { _CountUploadRate = value; }
		}
		private static int _CountUploadRate = 0;
		
		/// <summary>
		/// The upload rate in byte/sec.
		/// </summary>
		public static int UploadRate
		{
			get { return _UploadRate; }
			set { _UploadRate = value; }
		}
		private static int _UploadRate = 0;
		
		/// <summary>
		/// Flag used to indicate if has arrived a new FF message.
		/// </summary>
		public static bool NewFfMessage
		{
			get { return _NewFfMessage; }
			set { _NewFfMessage = value; }
		}
		private static bool _NewFfMessage = false;
		
		/// <summary>
		/// Start iKiwi when system starts.
		/// </summary>
		public static bool StartIkiwiAtSystemStartup
		{
			get { return _StartIkiwiAtSystemStartup; }
			set { _StartIkiwiAtSystemStartup = value; }
		}
		private static bool _StartIkiwiAtSystemStartup = true;
		
		/// <summary>
		/// Starting iKiwi minimized when system starts.
		/// </summary>
		public static bool StartIkiwiMinimized
		{
			get { return _StartIkiwiMinimized; }
			set { _StartIkiwiMinimized = value; }
		}
		private static bool _StartIkiwiMinimized = true;
		
		/// <summary>
		/// Returns true if this is the first iKiwi start.
		/// </summary>
		public static bool FirstStart
		{
			get { return _FirstStart; }
			set { _FirstStart = value; }
		}
		private static bool _FirstStart = true;
		
		/// <summary>
		/// Indicates if the message encryption is used or not.
		/// </summary>
		public static bool MessageEncryptionEnabled
		{
			get { return _MessageEncryptionEnabled; }
			set { _MessageEncryptionEnabled = value; }
		}
		private static bool _MessageEncryptionEnabled = true;
		
		/// <summary>
		/// Indicates if accept the not encrypted messages or not.
		/// </summary>
		public static bool AcceptNotEncryptedMessages
		{
			get { return _AcceptNotEncryptedMessages; }
			set { _AcceptNotEncryptedMessages = value; }
		}
		private static bool _AcceptNotEncryptedMessages = true;
		
		/// <summary>
		/// Indicates if use the UPnP protocol for the port mapping or not.
		/// </summary>
		public static bool UseUpnp
		{
			get { return _UseUpnp; }
			set { _UseUpnp = value; }
		}
		private static bool _UseUpnp = true;
		
		#endregion
		
		#region Daemons
		
		/// <summary>
		/// Start the download and upload rate counter.
		/// </summary>
		public static void StartCounterDownloadUploadRate()
		{
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += new ElapsedEventHandler(
				delegate
				{
					DownloadRate = CountDownloadRate;
					CountDownloadRate = 0;
					
					UploadRate = CountUploadRate;
					CountUploadRate = 0;
				});
			timer.Interval = 1000;
			timer.Enabled = true;
			
			Utilities.Log.Write("Download and upload rate counter started", Utilities.Log.LogCategory.Info);
		}
		
		#endregion
	}
}
