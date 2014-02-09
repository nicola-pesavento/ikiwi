using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace iKiwi.Lists
{
	/// <summary>
	/// The list of all peers connected with this host now.
	/// </summary>
	class PeersList
	{
		#region Data Members

		/// <summary>
		/// The list of all peers connected with this host now. Contains Peer objects.
		///</summary>
		private static List<Objects.Peer> m_peersList = new List<Objects.Peer>();
		
		/// <summary>
		/// The main thread.
		/// </summary>
		private static Thread m_mainThread = null;

		#endregion

		#region Properties

		/// <summary>
		/// The number of peers in the list.
		/// </summary>
		public static int Count
		{
			get { return m_peersList.Count; }
		}

		/// <summary>
		/// Return the list of all peers. Contains Peer() objects.
		/// </summary>
		public static List<Objects.Peer> List
		{
			get { return m_peersList; }
		}

		/// <summary>
		///  A flag indicating if is added a new peer.
		/// </summary>
		public static bool NewOrUpdatedOrDeletedPeer
		{
			get { return _NewOrUpdatedPeer; }
			set { _NewOrUpdatedPeer = value; }
		}
		private static bool _NewOrUpdatedPeer = false;
		
		#endregion
		
		#region Daemons
		
		/// <summary>
		/// Start of peers list.
		/// </summary>
		public static void Start()
		{
			if (m_mainThread != null)
			{
				// close the peers list
				Close();
			}
			
			Thread t1 = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// start a timer to update the download/upload speed of peers
					
					System.Timers.Timer timer = new System.Timers.Timer(1000);
					
					int n = 0;
					
					timer.Elapsed += new ElapsedEventHandler(
						delegate
						{
							try
							{
								for (int i = 0; i < m_peersList.Count; i++)
								{
									// update the info of this peer
									
									m_peersList[i].UpdateDownloadUploadRate();
									
									if (n >= 4)
									{
										n = 0;
										
										m_peersList[i].UpdateDownloadsUploadsStatus();
									}
									else
									{
										n++;
									}
								}
							}
							catch
							{
							}
						});
					
					timer.Start();
					
					// send the output messages and receive the input messages of peers
					while (true)
					{
						try
						{
							for (int i = 0; i < m_peersList.Count; i++)
							{
								try
								{
									m_peersList[i].SendReceiveMessages();
								}
								catch
								{
								}
							}
						}
						catch
						{
						}
						
						Thread.Sleep(1);
					}
				}
			));

			t1.Name = "PeersListMainThread";
			t1.IsBackground = true;
			t1.Start();
			
			m_mainThread = t1;
			
			// log
			Utilities.Log.Write("Started the list of the peers", Utilities.Log.LogCategory.Info);
		}
		
		#endregion

		#region Methods
		
		/// <summary>
		/// Closes the peers list.
		/// </summary>
		public static void Close()
		{
			if (m_mainThread != null)
			{
				m_mainThread.Abort();
				
				// log
				Utilities.Log.Write("Closed the list of the peers", Utilities.Log.LogCategory.Info);
			}
		}

		/// <summary>
		/// Adds the peer to the list.
		/// If exist another peer with same ID he will be automatically updated ( delete and write on ).
		/// </summary>
		/// <param name="peer">Peer object to add.</param>
		public static void AddPeer(Objects.Peer peer)
		{
			peer.Date = DateTime.UtcNow;

			for (int i = 0; i < m_peersList.Count; i++)
			{
				if (m_peersList[i].IP == peer.IP)
				{
					m_peersList[i] = peer;

					PeersList.NewOrUpdatedOrDeletedPeer = true;
					
					return;
				}
			}

			m_peersList.Add(peer);
			
			PeersList.NewOrUpdatedOrDeletedPeer = true;
		}

		/// <summary>
		/// Update the existing old with the new peer ( delete and write on ).
		/// If the peer doesn't exist in the list he will be created.
		/// </summary>
		/// <param name="peer">Peer object to update</param>
		public static void UpdatePeer(Objects.Peer peer)
		{
			AddPeer(peer);
		}

		/// <summary>
		/// Update ( delete and write on ) all files only of a peer.
		/// </summary>
		/// <param name="PeerID">The ID of peer that will be updated.</param>
		/// <param name="Files">The new list of shared files.</param>
		public static void UpdatePeer_Files(string PeerID, List<Objects.Peer.File> Files)
		{
			for (int i = 0; i < m_peersList.Count; i++)
			{
				if (m_peersList[i].ID == PeerID)
				{
					m_peersList[i].Files = Files;
					
					NewOrUpdatedOrDeletedPeer = true;
					
					return;
				}
			}
		}

		/// <summary>
		/// Update the ID and the Files of a peer.
		/// </summary>
		/// <param name="PeerIP">The IP:Port of peer that will be updated.</param>
		/// <param name="PeerID">The new ID.</param>
		/// <param name="Files">The new list of shared files.</param>
		public static void UpdatePeer_ID_Files(string PeerIP,string PeerID, List<Objects.Peer.File> Files)
		{
			for (int i = 0; i < m_peersList.Count; i++)
			{
				if (m_peersList[i].IP == PeerIP)
				{
					m_peersList[i].Files = Files;
					
					m_peersList[i].ID = PeerID;

					NewOrUpdatedOrDeletedPeer = true;
					
					return;
				}
			}
		}

		/// <summary>
		/// Remove a peer from the list. The peer will be closed.
		/// </summary>
		/// <param name="IP">IP of peer to be removed.</param>
		public static void RemoveAndClosePeer(string IP)
		{
			for (int i = 0; i < m_peersList.Count; i++)
			{
				if (m_peersList[i].IP == IP)
				{
					// close the peer
					m_peersList[i].Close();
					
					m_peersList.RemoveAt(i);
					
					NewOrUpdatedOrDeletedPeer = true;
					
					return;
				}
			}
		}
		
		/// <summary>
		/// Removes a peer from the list. The peer will be closed.
		/// </summary>
		/// <param name="Index">The index of the peer to remove.</param>
		public static void RemoveAndClosePeerAt(int Index)
		{
			m_peersList.RemoveAt(Index);
			
			NewOrUpdatedOrDeletedPeer = true;
		}

		/// <summary>
		/// Get Peer() from Peers_list() by his IP.
		/// </summary>
		/// <param name="IP">The IP of the peer.</param>
		/// <returns>Return NULL if there isn't the peer.</returns>
		public static Objects.Peer GetPeerByIP(string IP)
		{
			for (int i = 0; i < PeersList.List.Count; i++)
			{
				if (PeersList.List[i].IP == IP)
				{
					return PeersList.List[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Get a peer object from the list by his ID.
		/// </summary>
		/// <param name="ID">The ID of the peer.</param>
		/// <returns>If NULL the peer isn't there.</returns>
		public static Objects.Peer GetPeerByID(string ID)
		{
			for (int i = 0; i < PeersList.List.Count; i++)
			{
				if (PeersList.List[i].ID == ID)
				{
					return (PeersList.List[i]);
				}
			}
			return null;
		}

		/// <summary>
		/// Return a list that contains the peers that have the searched file.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The ID of the file.</param>
		/// <returns>The list of the peers that have the searched file.</returns>
		public static List<Objects.Peer> GetPeersByFile(string FileName, string FileID)
		{
			List<Objects.Peer> returnPeers = new List<Objects.Peer>();

			for (int i = 0; i < PeersList.List.Count; i++)
			{
				for (int j = 0; j < PeersList.List[i].Files.Count; j++)
				{
					Objects.Peer.File file = PeersList.List[i].Files[j];

					if (file.Name == FileName && file.SHA1 == FileID)
					{
						returnPeers.Add(PeersList.List[i]);
					}
				}
			}

			return returnPeers;
		}

		#endregion
	}
}
