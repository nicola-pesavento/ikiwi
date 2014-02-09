using System;
using System.Net;
using System.Text;
using System.Threading;
using iKiwi.Messages;

namespace iKiwi.Daemons
{
	/// <summary>
	/// Control if there is the minimum number of peer-connections.
	/// </summary>
	class PeerSearcher
	{
		#region Daemons

		/// <summary>
		/// Start to work in background.
		/// </summary>
		public static void Start()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					Thread.Sleep(5000);
					
					while (true)
					{
						ControlMinNumPeerConnections();

						GetAllPeersInfo();

						Thread.Sleep(30000);
					}
				}
			));

			t.Name = "PeerSearcher";
			t.IsBackground = true;
			t.Start();

			Utilities.Log.Write("PeerSearcher.cs started", Utilities.Log.LogCategory.Info);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Control if there is the minimum number of opened peer-connections, if there isn't it will open a new peer-connections.
		/// </summary>
		public static void ControlMinNumPeerConnections()
		{
			if (Lists.PeersList.Count < Global.MinimumPeerConnections)
			{
				// if there are not xml-list saved will send a XLR-message to other peer
				if (Utilities.XmlListManager.NumberOfTempLists() <= 0)
				{
					// re-publish the IP of this client in the default nova web cache server
					PublishMeInNovaWebCacheServer();
					
					// send a XLR-mess to a casual peer
					if (Lists.PeersList.Count > 0)
					{
						Random random = new Random(); // (int)DateTime.Now.Millisecond

						int n = random.Next(Lists.PeersList.Count - 1);

						string[] Params = new string[0];

						IMessage XlrMess = MessagesFactory.Instance.CreateMessage(Commands.XLR, Params);

						Lists.PeersList.List[n].Send(XlrMess);
					}
				}
				// connect to all peer in the xml-lists saved
				else
				{
					Utilities.XmlListManager.ConnToPeers((Lists.PeersList.Count - Global.MinimumPeerConnections));
				}

			}
		}

		/// <summary>
		/// Get the info of all peers contained in the Lists.PeersList with a Ping-message.
		/// </summary>
		public static void GetAllPeersInfo()
		{
			for (int i = 0; i < Lists.PeersList.List.Count; i++)
			{
				try
				{
					IMessage iPImessage = MessagesFactory.Instance.CreateMessage(Commands.PI, "");
					Lists.PeersList.List[i].Send(iPImessage);
				}
				catch
				{}
			}
		}

		/// <summary>
		/// Get the info of a single peer.
		/// </summary>
		/// <param name="PeerIP">The IP address of the peer.</param>
		public static void GetSinglePeerInfoByIP(string PeerIP)
		{
			IMessage iPImessage = MessagesFactory.Instance.CreateMessage(Commands.PI, "");
			
			Objects.Peer peer = Lists.PeersList.GetPeerByIP(PeerIP);

			if (peer != null)
			{
				peer.Send(iPImessage);
			}
		}

		/// <summary>
		/// Get the info of a single peer.
		/// </summary>
		/// <param name="PeerID">The ID of peer.</param>
		public static void GetSinglePeerInfoByID(string PeerID)
		{
			IMessage iPImessage = MessagesFactory.Instance.CreateMessage(Commands.PI, "");

			Objects.Peer peer = Lists.PeersList.GetPeerByID(PeerID);

			if (peer != null)
			{
				peer.Send(iPImessage);
			}
		}

		/// <summary>
		/// Use the default Nova Web Cache server to find new peers and connect with them.
		/// </summary>
		/// <param name="n">Max number of connections that will be opened. Set 0 to open all possible connections.</param>
		public static void UseNovaWebCacheServer(int n = 0)
		{
			try
			{
				if(n == 0)
				{
					n = 9999;
				}
				
				string query = Global.NovaWebCache + "?get_peers=1";

				WebClient wc = new WebClient();

				UTF8Encoding utf8 = new UTF8Encoding();

				string reply = "";

				reply = utf8.GetString(wc.DownloadData(query));

				string[] IPs = reply.Split(',');

				int a = 0;
				
				for (int i = 0; i < IPs.Length; i++)
				{
					if (IPs[i] != "" && IPs[i] != null)
					{
						// control that this IP Address is not mine
						if (IPs[i] != Global.MyRemoteIP + ":" + Global.ListeningPort)
						{
							string RequestConnectionMessage = Daemons.Bouncer.RequestMessage(IPs[i]);

							MessageSender.SendConnectionRequest(RequestConnectionMessage, IPs[i]);
						}

						a++;
					}

					if (a >= n)
					{
						break;
					}
				}

				Utilities.Log.Write("Got a list of peers from Nova Web Cache server.", Utilities.Log.LogCategory.Info);
				
				return;
			}
			catch(Exception ex)
			{
				Utilities.Log.Write("Can't connect to Nova Web Cache server: " + ex.Message, Utilities.Log.LogCategory.Error);
				return;
			}
			
		}

		/// <summary>
		/// Publish the IP address of this client in the default Nova Web Cache server.
		/// </summary>
		public static void PublishMeInNovaWebCacheServer()
		{
			try
			{
				string query = Global.NovaWebCache + "?publish_peer=" + Global.MyRemoteIP + ":" + Global.ListeningPort;

				WebClient wc = new WebClient();

				UTF8Encoding utf8 = new UTF8Encoding();

				string reply = "";

				reply = utf8.GetString(wc.DownloadData(query));

				Utilities.Log.Write("My IP address has been published into Nova Web Cache server.", Utilities.Log.LogCategory.Info);

				return;
			}
			catch
			{
				Utilities.Log.Write("Can't connect to Nova Web Cache server.", Utilities.Log.LogCategory.Error);
				return;
			}
		}

		#endregion
	}
}
