using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace iKiwi.Daemons
{
	/// <summary>
	/// Manages the requests of connection;
	/// it decides if accept or refuse a request of connection.
	/// </summary>
	class Bouncer
	{
		#region Properties

		/// <summary>
		/// Listening port number.
		/// </summary>
		public static int ListeningPort
		{
			get { return m_listeningPort; }
		}
		
		/// <summary>
		/// Indicates if this daemon is enabled.
		/// </summary>
		public static bool Enabled
		{
			get { return m_enabled; }
		}

		#endregion

		#region Data Members

		/// <summary>
		/// TcpListener.
		/// </summary>
		private static TcpListener m_tcpListener = null;
		
		/// <summary>
		/// Indicates that the client has been accepted.
		/// </summary>
		private static ManualResetEvent m_clientAccepted = new ManualResetEvent(false);
		
		/// <summary>
		/// Indicates if the listening port has been changed.
		/// </summary>
		private static bool m_changedListenigPort = false;
		
		/// <summary>
		/// Indicates if was used the UPnP protocol to open a port.
		/// </summary>
		private static bool m_usedUpnpProtocoll = false;
		
		/// <summary>
		/// The thread of this daemon.
		/// </summary>
		private static Thread m_mainThread = null;
		
		/// <summary>
		/// The listening port number.
		/// </summary>
		private static int m_listeningPort = 0;
		
		/// <summary>
		/// Indicates if this daemon is enabled.
		/// </summary>
		private static bool m_enabled = false;

		#endregion

		#region Daemons

		/// <summary>
		/// Start to work in background (open automatically the listening port).
		/// </summary>
		/// <param name="ListeningPort">The listening port number to use.</param>
		public static void Start(int ListeningPort)
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// save the listening port number
					m_listeningPort = ListeningPort;
					
					// if the UPnP is enabled automatically will open a port
					if (Global.UseUpnp == true)
					{
						Utilities.UPnP.OpenPort(ListeningPort, Utilities.UPnP.Protocol.TCP);
						
						m_usedUpnpProtocoll = true;
					}
					
					try
					{
						// start a asynchronous listening

						m_tcpListener = new TcpListener(IPAddress.Any, ListeningPort);
						
						m_tcpListener.Start();
						
						Utilities.Log.Write("Bouncer daemon started", Utilities.Log.LogCategory.Info);
						
						// communicate that bouncer is enabled now
						m_enabled = true;
						
						while (true)
						{
							m_clientAccepted.Reset();
							
							if (m_changedListenigPort == true)
							{
								m_changedListenigPort = false;
								
								m_tcpListener.Start();
							}
							
							m_tcpListener.BeginAcceptTcpClient(DoAcceptTcpClient, m_tcpListener);
							
							m_clientAccepted.WaitOne();
						}
					}
					catch(Exception ex)
					{
						Utilities.Log.Write("Bouncer error: " + ex.Message, Utilities.Log.LogCategory.Error);
						
						// stop bouncer
						Stop();
					}
				}
			));
			
			t.Name = "Bouncer";
			t.IsBackground = true;
			t.Start();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Get a string that containes the message for a request of connection.
		/// </summary>
		/// <param name="Remote_IP">The IP of the peer that will receive this message.</param>
		/// <returns>The connection request message.</returns>
		public static string RequestMessage(string Remote_IP)
		{
			string mess =   Global.P2P_Protocol + "\n" +
				"CONNECT" + "\n" +
				"Local-IP: " + Global.MyRemoteIP + ":"  +Global.ListeningPort + "\n" +
				"Remote-IP: " + Remote_IP + "\n" +
				"User-Agent: " + Global.UserAgent;

			return mess;
		}

		/// <summary>
		/// Changes the listening port used by this object; this causes a automatic restart of it.
		/// This method automatically closes the old UPnP port and opens the new UPnP port if the UPnP is enabled.
		/// </summary>
		/// <param name="Port">The new number of Listening Port.</param>
		public static void ChangeListeningPort(int Port)
		{
			// change
			if (Port != ListeningPort)
			{
				// close the opened port with the UPnP protocol
				if (m_usedUpnpProtocoll == true)
				{
					// close the listening port
					Utilities.UPnP.ClosePort(ListeningPort, Utilities.UPnP.Protocol.TCP);
					
					m_usedUpnpProtocoll = false;
				}
				
				// save the new port number
				m_listeningPort = Port;
				
				// if the UPnP is enabled automatically will open a port
				if (Global.UseUpnp == true)
				{
					// open the listening port
					Utilities.UPnP.OpenPort(ListeningPort, Utilities.UPnP.Protocol.TCP);
					
					m_usedUpnpProtocoll = true;
				}

				// restart
				
				m_tcpListener.Stop();
				
				m_tcpListener = new TcpListener(IPAddress.Any, ListeningPort);
				
				m_changedListenigPort = true;
			}
		}
		
		/// <summary>
		/// Stops this daemon.
		/// </summary>
		public static void Stop()
		{
			if (m_tcpListener != null)
			{
				// close the connection
				m_tcpListener.Stop();
			}
			
			// close the opened port with the UPnP protocol
			if (m_usedUpnpProtocoll == true)
			{
				// close the listening port
				Utilities.UPnP.ClosePort(ListeningPort, Utilities.UPnP.Protocol.TCP);
				
				m_usedUpnpProtocoll = false;
			}
			
			// close the thread of this daemon
			if (m_mainThread != null)
			{
				m_mainThread.Abort();
			}
			
			// communicate that bouncer is disabled now
			m_enabled = false;
			
			Utilities.Log.Write("Bouncer daemon stopped", Utilities.Log.LogCategory.Info);
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Manage the request connection;
		/// it decide if create a new connection or not.
		/// </summary>
		private static void DoAcceptTcpClient(IAsyncResult ar)
		{
			try
			{
				// start a new asynchronous listening
				m_clientAccepted.Set();
				
				// get the TcpClient and the NetworkStream
				TcpClient Client = ((TcpListener)ar.AsyncState).EndAcceptTcpClient(ar);
				NetworkStream Stream = Client.GetStream();

				Thread.Sleep(10);

				// get the request message in UTF16 form
				
				int i = 0;
				int num_bytes_read = 0;
				byte[] Bytes = new byte[3072];
				byte[] Req_message_byte = new byte[3072];
				
				do
				{
					if (Stream.DataAvailable)
					{
						i = Stream.Read(Bytes, 0, Bytes.Length);
						Array.Copy(Bytes, 0, Req_message_byte, num_bytes_read, i);
						num_bytes_read += i;
					}
					else
					{
						Thread.Sleep(10);
						
						if (!Stream.DataAvailable)
						{
							break;
						}
						
						i = 1;
					}
				} while (i != 0);

				string Req_message = System.Text.Encoding.Unicode.GetString(Req_message_byte);

				// control if the message is correct or not
				string[] sub_Req_message = Req_message.Split('\n');

				try
				{
					if (sub_Req_message[0].Substring(0, 4) == "Nova" && sub_Req_message[1].Substring(0, 7) == "CONNECT")
					{
						string Client_IP = sub_Req_message[2].Substring("Local-IP: ".Length).Trim();
						
						bool connectionAccepted = false;
						
						// process the request
						if (Lists.PeersList.Count < Global.MaxNumberInputConnections)
						{
							connectionAccepted = true;
						}
						else
						{
							// control if there are peers that are not uploading or downloading to or from this client
							for (int n = 0; n < Lists.PeersList.List.Count; n++)
							{
								try
								{
									if (Lists.PeersList.List[n].DownloadInProgress == false && Lists.PeersList.List[n].UploadInProgress == false)
									{
										// remove this peer from the peers list
										Lists.PeersList.RemoveAndClosePeerAt(n);
										
										// accept the input connection
										connectionAccepted = true;
										
										break;
									}
								}
								catch
								{
								}
							}
						}
						
						if (connectionAccepted == true)
						{
							Utilities.Log.Write("Connection request from " + Client_IP + " accepted", Utilities.Log.LogCategory.ConnectionRequests);

							// create a new Objects.Peer object
							Objects.Peer peer = new Objects.Peer(Client_IP, Client, Stream);
							peer.ID = "EMPTY";
							
							// the reply
							string Reply_message =
								Global.P2P_Protocol + "\n" +
								"CONNECT_OK" + "\n" +
								"Local-IP: " + Global.MyRemoteIP + ":" + Global.ListeningPort + "\n" +
								"Remote-IP: " + Client_IP + "\n" +
								"User-Agent: " + Global.UserAgent;

							// send the reply
							MessageSender.Send(Reply_message, Client, true);

							// add the peer in the Lists.PeersList()
							Lists.PeersList.AddPeer(peer);
						}
						else
						{
							Utilities.Log.Write("Connection request from " + Client_IP + " refused", Utilities.Log.LogCategory.ConnectionRequests);

							string Reply_message =
								Global.P2P_Protocol + "\n" +
								"CONNECT_NO" + "\n" +
								"Local-IP: " + Global.MyRemoteIP + ":" + Global.ListeningPort + "\n" +
								"Remote-IP: " + Client_IP + "\n" +
								"User-Agent: " + Global.UserAgent;

							byte[] buffer = System.Text.Encoding.Unicode.GetBytes(Reply_message);
							Stream.Write(buffer, 0, buffer.Length);

							Stream.Close();
							Client.Close();
						}
					}
					else
					{
						Utilities.Log.Write("Connection request isn't valid", Utilities.Log.LogCategory.ConnectionRequests);

						Stream.Close();
						Client.Close();
					}
				}
				catch(Exception)
				{
					Utilities.Log.Write("Connection request isn't valid", Utilities.Log.LogCategory.ConnectionRequests);
					
					Stream.Close();
					Client.Close();
				}
			}
			catch (Exception ex)
			{
				Utilities.Log.Write("Error to process a connection request: " + ex.Message, Utilities.Log.LogCategory.ConnectionRequests);
			}
		}
		
		#endregion
	}
}