using System;
using System.Collections.Generic;

namespace iKiwi.Lists
{
	class FilesFoundList
	{
		#region Classes
		
		/// <summary>
		/// File object.
		/// </summary>
		public class File
		{
			#region Classes
			
			public class Peer
			{
				#region Ctor
				
				/// <summary>
				/// Create a peer object.
				/// </summary>
				/// <param name="IP">The IP address.</param>
				/// <param name="ID">The ID.</param>
				public Peer(string IP, string ID)
				{
					this.IP = IP;
					this.ID = ID;
					this.Key = Utilities.GetKey.IpPort(IP);
				}
				
				#endregion
				
				#region Properties
				
				/// <summary>
				/// The IP address of the peer.
				/// </summary>
				public string IP
				{ get; set; }
				
				/// <summary>
				/// The ID of the peer.
				/// </summary>
				public string ID
				{ get; set; }
				
				/// <summary>
				/// The key of the peer for the list.
				/// </summary>
				public long Key
				{ get; set; }
				
				#endregion
			}
			
			#endregion
			
			#region Properties

			public string Name
			{ get; set; }

			public long Size
			{ get; set; }

			public string SHA1
			{ get; set; }

			/// <summary>
			/// The list of IDs of peers that have this file.
			/// </summary>
			public List<Peer> ListPeers
			{ get; set; }

			#endregion
			
			#region Ctor
			
			public File()
			{
				this.ListPeers = new List<Peer>();
			}
			
			#endregion
			
			#region Methods
			
			public void AddPeer(string IP, string ID)
			{
				Peer peer = new Peer(IP, ID);
				
				if (this.ListPeers.Count > 0)
				{
					int n = 0;
					int max = ListPeers.Count - 1;
					int min = 0;
					
					while (max >= min)
					{
						n = (max + min) / 2;
						
						if (peer.Key > this.ListPeers[n].Key)
						{
							min = n + 1;
						}
						else if (peer.Key < this.ListPeers[n].Key)
						{
							max = n - 1;
						}
						else
						{
							return;
						}
					}
					
					if (peer.Key < this.ListPeers[n].Key)
					{
						n--;
					}
					
					this.ListPeers.Add(this.ListPeers[this.ListPeers.Count - 1]);
					
					for(int i = this.ListPeers.Count - 1; i > n + 1; i--)
					{
						this.ListPeers[i] = this.ListPeers[i - 1];
					}
					
					this.ListPeers[n + 1] = peer;
				}
				else
				{
					this.ListPeers.Add(peer);
				}
			}
			
			public void AddPeer(Peer peer)
			{
				if (this.ListPeers.Count > 0)
				{
					int n = 0;
					int max = ListPeers.Count - 1;
					int min = 0;
					
					while (max >= min)
					{
						n = (max + min) / 2;
						
						if (peer.Key > this.ListPeers[n].Key)
						{
							min = n + 1;
						}
						else if (peer.Key < this.ListPeers[n].Key)
						{
							max = n - 1;
						}
						else
						{
							return;
						}
					}
					
					if (peer.Key < this.ListPeers[n].Key)
					{
						n--;
					}
					
					this.ListPeers.Add(this.ListPeers[this.ListPeers.Count - 1]);
					
					for(int i = this.ListPeers.Count - 1; i > n + 1; i--)
					{
						this.ListPeers[i] = this.ListPeers[i - 1];
					}
					
					this.ListPeers[n + 1] = peer;
				}
				else
				{
					this.ListPeers.Add(peer);
				}
			}
			
			/// <summary>
			/// Returns a peer by its IP address.
			/// </summary>
			/// <param name="IP">The IP address of peer to search.</param>
			/// <returns>Returns the found peer or null if the peer does not exist.</returns>
			public Lists.FilesFoundList.File.Peer GetPeerByIP(string IP)
			{
				int min = 0;
				int max = this.ListPeers.Count - 1;
				int m;
				
				long peerKey = Utilities.GetKey.IpPort(IP);
				
				while (min <= max)
				{
					m = (min + max) / 2;
					
					if(this.ListPeers[m].IP == IP)
					{
						return this.ListPeers[m];
					}
					
					if (peerKey > this.ListPeers[m].Key)
					{
						min = m + 1;
					}
					else if (peerKey < this.ListPeers[m].Key)
					{
						max = m - 1;
					}
				}
				
				return null;
			}
			
			#endregion
		}
		
		#endregion
		
		#region Data Members

		/// <summary>
		/// The list of all file found.
		/// </summary>
		private static List<FilesFoundList.File> m_filesList = new List<FilesFoundList.File>();

		#endregion

		#region Methods

		/// <summary>
		/// Creates a new file in the list or, if it already exists, update it.
		/// </summary>
		/// <param name="File">File object.</param>
		public static void AddFile(FilesFoundList.File File)
		{
			for (int i = 0; i < m_filesList.Count; i++)
			{
				try
				{
					if (m_filesList[i].SHA1 == File.SHA1 && m_filesList[i].Name == File.Name)
					{
						m_filesList[i] = UpdateFile(m_filesList[i], File);
						
						return;
					}
				}
				catch
				{
				}
			}
			
			// add the filw in the list
			m_filesList.Add(File);
		}

		/// <summary>
		/// Creates a new file object and add it to list, if the file have already existed it will be updated.
		/// </summary>
		/// <param name="Name">The name of the file.</param>
		/// <param name="Size">The size of the file.</param>
		/// <param name="SHA1">The SHA1-ID of the file.</param>
		/// <param name="PeerIP">One PeerID only.</param>
		/// <param name="PeerID"></param>
		public static void CreateAndAddFile(string Name, int Size, string SHA1, string PeerIP, string PeerID)
		{
			FilesFoundList.File file = new FilesFoundList.File();
			file.Name = Name;
			file.Size = Size;
			file.SHA1 = SHA1;
			file.AddPeer(PeerIP, PeerID);

			FilesFoundList.AddFile(file);
		}
		
		/// <summary>
		/// Returns a single file object.
		/// </summary>
		/// <param name="Name">The name of the file.</param>
		/// <param name="SHA1">The SHA1-ID of the file.</param>
		/// <returns>The file object; returns null if the file hasn't been found.</returns>
		public static FilesFoundList.File GetFile(string Name, string SHA1)
		{
			for (int i = 0; i < m_filesList.Count; i++)
			{
				try
				{
					if (m_filesList[i].SHA1 == SHA1 && m_filesList[i].Name == Name)
					{
						return m_filesList[i];
					}
				}
				catch
				{
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Searches a file that contains the text searched.
		/// </summary>
		/// <param name="Text">The text to search.</param>
		/// <returns>The list of the files found.</returns>
		public static List<FilesFoundList.File> SearchFileByText(string Text)
		{
			List<FilesFoundList.File> filesFound = new List<FilesFoundList.File>();
			
			string[] stringsToSearch = Text.Split(' ');
			
			int numFoundStrings = 0;
			
			for (int i = 0; i < m_filesList.Count; i++)
			{
				try
				{
					for (int a = 0; a < stringsToSearch.Length; a++)
					{
						if (m_filesList[i].Name.IndexOf(stringsToSearch[a], StringComparison.OrdinalIgnoreCase) >= 0)
						{
							numFoundStrings++;
						}
						else
						{
							break;
						}
					}
					
					if (numFoundStrings == stringsToSearch.Length)
					{
						filesFound.Add(m_filesList[i]);
					}
					
					numFoundStrings = 0;
				}
				catch
				{
				}
			}
			
			return filesFound;
		}
		
		/// <summary>
		/// Updates a old file with a new file.
		/// </summary>
		/// <param name="OldFile">The old file.</param>
		/// <param name="NewFile">The new file.</param>
		public static Lists.FilesFoundList.File UpdateFile(Lists.FilesFoundList.File OldFile, Lists.FilesFoundList.File NewFile)
		{
			List<int> peerToAdd = new List<int>();
			
			for (int i = 0; i < NewFile.ListPeers.Count; i++)
			{
				if (OldFile.GetPeerByIP(NewFile.ListPeers[i].IP) == null)
				{
					peerToAdd.Add(i);
				}
			}
			
			for (int n = 0; n < peerToAdd.Count; n++)
			{
				OldFile.AddPeer(NewFile.ListPeers[peerToAdd[n]]);
			}
			
			return OldFile;
		}

		#endregion
	}
}
