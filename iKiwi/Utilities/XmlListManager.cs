using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Manages the XML-Lists of other peers in the [Temp_dir]/List/List_***.xml.
	/// </summary>
	class XmlListManager
	{
		#region Daemons

		/// <summary>
		/// Automatic build a new XML-List every few time. It works in background.
		/// </summary>
		public static void StartAutomaticXmlBuilder()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					while (true)
					{
						Build_XML_List();

						Thread.Sleep(50000);
					}
				}));

			t.Name = "AutomaticXmlBuilder";
			t.IsBackground = true;
			t.Start();

			Utilities.Log.Write("Automatic XML builder is started.", Utilities.Log.LogCategory.Info);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Send a request of connection to all peers that are contained in the all XML-Lists if them aren't already connected with this client.
		/// Then deletes all XML-Lists.
		/// </summary>
		/// <param name="n">Max number of connections that will be opened. Set 0 to open all possible connections.</param>
		public static void ConnToPeers(int n = 0)
		{
			string myIpAddress = Global.MyRemoteIP + ":" + Global.ListeningPort;
			
			string[] lists = Directory.GetFiles((Global.TempDirectory + @"List\"), "List_*.xml", SearchOption.AllDirectories);

			for (int i = 0; i < lists.Length; i++)
			{
				XmlDocument list = new XmlDocument();

				// load the Xml-List
				while (true)
				{
					try
					{
						list.Load(lists[i]);
						break;
					}
					catch (IOException)
					{
					}
					catch (Exception ex)
					{
						Utilities.Log.Write("Error to load a XML-List from " + lists[i] + " : " + ex.Message, Utilities.Log.LogCategory.Error);
						
						// delete the corrupted xml-list
						File.Delete(lists[i]);
					}
				}

				XmlNodeList nodes = list.DocumentElement.GetElementsByTagName("Peers");

				// get the single peers from the list
				int a = 0;
				
				for (int b = 0; b < nodes.Count; b++)
				{
					if (a < n)
					{
						string IP = nodes[b].SelectSingleNode("IP").InnerText;

						// control if this peer is already connected with this client
						if (IP != myIpAddress && Lists.PeersList.GetPeerByIP(IP) == null)
						{
							string RequestConnectionMessage = Daemons.Bouncer.RequestMessage(IP);
							
							MessageSender.SendConnectionRequest(RequestConnectionMessage, IP);
						}

						if (n != 0)
						{
							a++;
						}
					}
					else
					{
						break;
					}
				}

				// delete the list
				File.Delete(lists[i]);
			}
		}

		/// <summary>
		/// Count the number of temporary xml-lists saved.
		/// </summary>
		/// <returns>The number of temp xml-lists saved.</returns>
		public static int NumberOfTempLists()
		{
			string[] lists = Directory.GetFiles((Global.TempDirectory + @"\List\"), "List_*.xml", SearchOption.AllDirectories);

			return lists.Length;
		}

		/// <summary>
		/// Build a new XML_List with all the peers are contained in the Lists.PeersList.
		/// </summary>
		public static void Build_XML_List()
		{
			XmlDocument list = new XmlDocument();

			list.LoadXml("<Peers></Peers>");

			for (int i = 0; i < Lists.PeersList.Count; i++)
			{
				Objects.Peer peer = (Objects.Peer)Lists.PeersList.List[i];

				if (peer.ID != "" && peer.ID != null)
				{
					XmlElement XE_peer = list.CreateElement("_" + peer.ID);

					XmlElement peer_IP = list.CreateElement("IP");
					peer_IP.InnerText = peer.IP;
					XE_peer.AppendChild(peer_IP);

					XmlElement peer_time = list.CreateElement("Time");
					peer_time.InnerText = peer.Date.ToString();
					XE_peer.AppendChild(peer_time);

					XmlElement peer_files = list.CreateElement("Files");

					try
					{
						for (int j = 0; j < peer.Files.Count; j++)
						{
							Objects.Peer.File file = peer.Files[j];

							XmlElement file_sha = list.CreateElement("_" + file.SHA1);

							XmlElement file_name = list.CreateElement("Name");
							file_name.InnerText = file.Name;
							file_sha.AppendChild(file_name);

							XmlElement file_size = list.CreateElement("Size");
							file_size.InnerText = file.Size.ToString();
							file_sha.AppendChild(file_size);

							peer_files.AppendChild(file_sha);
						}
					}
					catch
					{
					}

					XE_peer.AppendChild(peer_files);
					
					list.DocumentElement.AppendChild(XE_peer);
				}
			}

			// save the list
			XmlTextWriter writer = new XmlTextWriter(Global.iKiwiPath + "List.xml", null);
			writer.Formatting = Formatting.Indented;


			while (true)
			{
				try
				{
					list.Save(writer);
					writer.Flush();
					writer.Close();
					return;
				}
				catch
				{
					Thread.Sleep(1);
				}
			}
		}

		#endregion
	}
}