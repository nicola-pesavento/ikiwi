using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace iKiwi
{
	/// <summary>
	/// Sends the messages to other peers.
	/// </summary>
	class MessageSender
	{
		#region Methods

		/// <summary>
		/// Send a message to a peer still connected.
		/// </summary>
		/// <param name="Message">Message in String(UTF16 encode only).</param>
		/// <param name="Client">TcpClient of the peer.</param>
		/// <param name="Wait">If true wait the end of this method.</param>
		public static void Send(string Message, TcpClient Client, bool Wait = false)
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// convert the UTF16 to Byte[]

					byte[] Message_Byte = new byte[Message.Length * 2];

					Message_Byte = ASCIIEncoding.Unicode.GetBytes(Message);

					try
					{
						NetworkStream stream = Client.GetStream();
						stream.Write(Message_Byte, 0, Message_Byte.Length);
						
						Global.CountUploadRate += Message_Byte.Length;
					}
					catch (Exception)
					{
						Utilities.Log.Write("A message hasn't been sent, probably the peer is offline or disconnected", Utilities.Log.LogCategory.Error);
					}

				}));

			t.Name = "MessageSender";
			t.IsBackground = true;
			t.Start();

			if (Wait == true)
			{
				t.Join();
			}
		}

		/// <summary>
		/// Send a Tcp connection request to a new peer.
		/// </summary>
		/// <param name="Message">The request message.</param>
		/// <param name="PeerIP">The IP address of the new peer [IP:Port].</param>
		public static void SendConnectionRequest(string Message, string PeerIP)
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// converts UTF16 to Byte[]

					byte[] MessageByte = ASCIIEncoding.Unicode.GetBytes(Message);

					TcpClient client = new TcpClient();
					
					NetworkStream stream = null;
					
					try
					{
						string[] subPeerIP = PeerIP.Split(':');
						
						string ip = subPeerIP[0];
						
						int port = int.Parse(subPeerIP[1]);
						
						// control the peer ip
						ControlPeerIPAddress(ref ip, port);
						
						// connect to peer
						client = new TcpClient();

						client.Connect(ip, port);

						// get stream
						stream = client.GetStream();

						// send the request
						stream.Write(MessageByte, 0, MessageByte.Length);
						
						Global.CountUploadRate += MessageByte.Length;

						// wait a reply
						System.Timers.Timer timer = new System.Timers.Timer(15000);

						timer.Elapsed += new ElapsedEventHandler(delegate { Thread.CurrentThread.Abort(); });

						timer.Enabled = true;

						byte[] reply_message_byte = new byte[3072];
						
						byte[] bytes = new byte[3072];

						stream.Read(reply_message_byte, 0, reply_message_byte.Length);
						
						int i = 0;
						int num_bytes_read = 0;
						
						do
						{
							if (stream.DataAvailable)
							{
								stream.Read(bytes, 0, bytes.Length);
								Array.Copy(bytes, 0, reply_message_byte, num_bytes_read, i);
								num_bytes_read += i;
							}
							else
							{
								Thread.Sleep(10);
								
								if (!stream.DataAvailable)
								{
									break;
								}
								
								i = 1;
							}
						} while (i != 0);
						
						timer.Stop();

						string reply = ASCIIEncoding.Unicode.GetString(reply_message_byte);

						string[] sub_reply = reply.Split('\n');

						// if the peer have accepted the connection request
						if (sub_reply[0].Substring(0, 4) == "Nova" && sub_reply[1].Substring(0, 10) == "CONNECT_OK")
						{
							Utilities.Log.Write("Connection established with " + PeerIP, Utilities.Log.LogCategory.ConnectionRequests);

							// create a new peer object
							Objects.Peer peer = new Objects.Peer(PeerIP, client, stream);
							peer.ID = "EMPTY";
							
							// send a EI-message
							
							string eiMessParam1;
							string eiMessParam2;
							
							if (Global.MessageEncryptionEnabled == true)
							{
								eiMessParam1 = "y";
								eiMessParam2 = string.Format("{0},{1}",
								                             Utilities.Converterer.ConvertByteToHex(peer.MyAsymmetricEncryptionKey.N),
								                             Utilities.Converterer.ConvertByteToHex(peer.MyAsymmetricEncryptionKey.E));
							}
							else
							{
								eiMessParam1 = "n";
								eiMessParam2 = string.Empty;
							}
							
							// create and send a EI-message
							
							Messages.IMessage eiMess = Messages.MessagesFactory.Instance.CreateMessage(Messages.Commands.EI, false, eiMessParam1, eiMessParam2);
							
							peer.Send(eiMess);
							
							// add the peer in the list of the peers
							Lists.PeersList.AddPeer(peer);
						}
						else
						{
							stream.Close();
							client.Close();
						}

						timer.Close();
					}
					catch
					{
						if (client != null)
						{
							client.Close();
						}
						if (stream != null)
						{
							stream.Close();
						}
						
						Utilities.Log.Write("The connection request hasn't been sent, probably the peer (" + PeerIP + ") is offline or disconnected", Utilities.Log.LogCategory.Error);
					}
				}));

			t.Name = "MessageSender_Connection_Request_Sending";
			t.IsBackground = true;
			t.Start();
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Controls if the IP address of a peer is fine to open a connection, or if it must be changed in a local IP (the peer is a local machine).
		/// </summary>
		/// <param name="IP">The IP address of the peer.</param>
		/// <param name="Port">The listening port number of the peer.</param>
		private static void ControlPeerIPAddress(ref string IP, int Port)
		{
			if (IP == Global.MyRemoteIP)
			{
				if (Global.UseUpnp == true)
				{
					Utilities.UPnP.UpnpPort portInfo = Utilities.UPnP.GetInfoUpnpPort(Port);
					
					if (portInfo != null)
					{
						IP = portInfo.InternalIP;
						
						return;
					}
					else
					{
						return;
					}
				}
				else
				{
					return;
				}
			}
			else
			{
				return;
			}
		}
		
		#endregion
	}
}
