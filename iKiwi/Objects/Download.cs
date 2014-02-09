using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace iKiwi.Objects
{
	/// <summary>
	/// Download object.
	/// </summary>
	public class Download
	{
		#region Ctor
		
		/// <summary>
		/// Create a download object.
		/// </summary>
		/// <param name="Name">The name of the file.</param>
		/// <param name="Size">The size of the file.</param>
		/// <param name="SHA1">The SHA1 of the file.</param>
		public Download(string Name, long Size, string SHA1)
		{
			this.Name = Name;
			this.Size = Size;
			this.SHA1 = SHA1;
			
			int n = (int)Size / 16384;
			
			if ( Size % 16384 != 0 )
			{
				n++;
			}
			
			this.ListFilePacks = new BitArray(n, false);
			
			this.RemainingFilePacks = n;
			
			int m = n / 128;
			
			if ( n % 128 != 0 )
			{
				m++;
			}
			
			this.ListFileParts = new short[m];
			this.RemainingFileParts = m;
			
			for ( int i = 0; i < m; i++ )
			{
				this.ListFileParts[i] = 128;
			}
			
			long e = (Size - (((m - 1) * 128) * 16384));
			
			this.ListFileParts[m - 1] = (short)(e / 16384);
			
			if (e % 16384 !=0 )
			{
				this.ListFileParts[m - 1]++;
			}
		}
		
		/// <summary>
		/// Create a download object.
		/// </summary>
		/// <param name="Name">The name of the file.</param>
		/// <param name="Size">The size of the file.</param>
		/// <param name="SHA1">The SHA1 of the file.</param>
		/// <param name="FilePacks">The list of the downloaded file packs.</param>
		public Download(string Name, int Size, string SHA1, BitArray FilePacks)
			: this(Name, Size, SHA1)
		{
			this.ListFilePacks = FilePacks;
			
			for (int i = 0; i < this.ListFilePacks.Length; i++)
			{
				if (FilePacks[i] == true)
				{
					this.ListFileParts[i / 128] -= 1;

					if (this.ListFileParts[i / 128] == 0)
					{
						this.RemainingFileParts -= 1;
					}

					this.RemainingFilePacks -= 1;
				}
			}
		}
		
		#endregion
		
		#region Properties

		/// <summary>
		/// The name of the file.
		/// </summary>
		public string Name
		{ get; set; }

		/// <summary>
		/// The complete size of the file.
		/// </summary>
		public long Size
		{ get; set; }
		
		/// <summary>
		/// The SHA1 ID of the file ( complete ).
		/// </summary>
		public string SHA1
		{ get; set; }
		
		/// <summary>
		/// The current size of the file now.
		/// </summary>
		public long CurrentSize
		{
			get
			{
				if (this.RemainingFilePacks != 0)
				{
					FileInfo fi = new FileInfo(Global.TempDirectory + this.Name);
					
					// control if file exists;
					if (fi.Exists)
					{
						return fi.Length;
					}
					else
					{
						return 0;
					}
				}
				else
				{
					return this.Size;
				}
			}
		}

		/// <summary>
		/// Indicates whether the download is active or not.
		/// </summary>
		public bool Active
		{
			get { return _Active; }
			set { _Active = value; }
		}
		private bool _Active = true;
		
		/// <summary>
		/// The percentage of completion.
		/// </summary>
		public float Progress
		{
			get { return _Progress; }
			set { _Progress = value; }
		}
		private float _Progress = 0;
		
		/// <summary>
		/// The list of downloaded File-Packs.
		/// </summary>
		public BitArray ListFilePacks
		{ get; set; }
		
		/// <summary>
		/// The list of downloaded file parts ( 1 file part = 128 file packs ).
		/// Every file part has a number (from 0 to 128) that indicates the remaining packets to download (the first packets are the first to be downloaded).
		/// </summary>
		public short[] ListFileParts
		{ get; set; }
		
		/// <summary>
		/// The number of number of file packs not yet downloaded.
		/// </summary>
		public int RemainingFilePacks
		{ get; set; }
		
		/// <summary>
		/// The number of number of file parts not yet downloaded.
		/// </summary>
		public int RemainingFileParts
		{ get; set; }
		
		/// <summary>
		/// The list of the peers that have this file.
		/// </summary>
		public List<Objects.Peer> ListPeers
		{
			get { return _ListPeers; }
			set { _ListPeers = value; }
		}
		List<Objects.Peer> _ListPeers = new List<Objects.Peer>();
		
		/// <summary>
		/// The list of the uploader peers.
		/// </summary>
		public List<Peer> ListUploaderPeers
		{
			get { return _ListUploaderPeers; }
			set { _ListUploaderPeers = value; }
		}
		List<Peer> _ListUploaderPeers = new List<Peer>();
		
		/// <summary>
		/// Indicates whether the download is completed or not.
		/// </summary>
		public bool Completed
		{
			get
			{
				if (this.RemainingFilePacks == 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		/// <summary>
		/// The download speed of the download in bytes/sec.
		/// </summary>
		public int DownloadSpeed
		{ get; set; }
		
		/// <summary>
		/// Use to save the length of the sended message to calculate the download speed of the download.
		/// </summary>
		public int CountDownloadSpeed
		{ get; set; }
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Adds a peer in the list of peers of this download.
		/// </summary>
		/// <param name="Peer">The Peer object.</param>
		public void AddPeer(Objects.Peer Peer)
		{
			if (this.ListPeers.Count > 0)
			{
				int n = 0;
				int max = ListPeers.Count - 1;
				int min = 0;
				
				while (max >= min)
				{
					n = (max + min) / 2;
					
					if (Peer.Key > this.ListPeers[n].Key)
					{
						min = n + 1;
					}
					else if (Peer.Key < this.ListPeers[n].Key)
					{
						max = n - 1;
					}
					else
					{
						return;
					}
				}
				
				if (Peer.Key < this.ListPeers[n].Key)
				{
					n--;
				}
				
				this.ListPeers.Add(this.ListPeers[this.ListPeers.Count - 1]);
				
				for(int i = this.ListPeers.Count - 1; i > n + 1; i--)
				{
					this.ListPeers[i] = this.ListPeers[i - 1];
				}
				
				this.ListPeers[n + 1] = Peer;
			}
			else
			{
				this.ListPeers.Add(Peer);
			}
		}
		
		/// <summary>
		/// Searchs a peer in the peers list of the download.
		/// </summary>
		/// <param name="IP">The IP address of the peer.</param>
		/// <returns>The peer object found.</returns>
		public Peer SearchPeer(string IP)
		{
			long key = Utilities.GetKey.IpPort(IP);
			
			int n = 0;
			int max = ListPeers.Count - 1;
			int min = 0;
			
			while (max >= min)
			{
				n = (max + min) / 2;
				
				if (key == this.ListPeers[n].Key)
				{
					return this.ListPeers[n];
				}
				
				if (key > this.ListPeers[n].Key)
				{
					min = n + 1;
				}
				else
				{
					max = n - 1;
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Finds the peers that have the file and add them in the list of peers.
		/// </summary>
		public void FindPeers()
		{
			// search the file in net
			Utilities.FileSearcher.SearchFile(this.Name);
			
			// get the file from the list of found files
			Lists.FilesFoundList.File file = Lists.FilesFoundList.GetFile(this.Name, this.SHA1);
			
			if (file != null)
			{
				for (int i = 0; i < file.ListPeers.Count; i++)
				{
					try
					{
						Objects.Peer peer = Lists.PeersList.GetPeerByIP(file.ListPeers[i].IP);
						
						if (peer != null)
						{
							AddPeer(peer);
						}
					}
					catch
					{
					}
				}
			}
		}
		
		/// <summary>
		/// Adds a peer in the list of uploader peers of this download.
		/// </summary>
		/// <param name="peer"></param>
		public void AddUploaderPeer(Objects.Peer peer)
		{
			if (this.ListUploaderPeers.Count > 0)
			{
				int n = 0;
				int max = ListPeers.Count - 1;
				int min = 0;
				
				while (max >= min)
				{
					n = (max + min) / 2;
					
					if (peer.Key > this.ListUploaderPeers[n].Key)
					{
						min = n + 1;
					}
					else if (peer.Key < this.ListUploaderPeers[n].Key)
					{
						max = n - 1;
					}
					else
					{
						return;
					}
				}
				
				if (peer.Key < this.ListUploaderPeers[n].Key)
				{
					n--;
				}
				
				this.ListUploaderPeers.Add(this.ListUploaderPeers[this.ListUploaderPeers.Count - 1]);
				
				for(int i = this.ListUploaderPeers.Count - 1; i > n + 1; i--)
				{
					this.ListUploaderPeers[i] = this.ListUploaderPeers[i - 1];
				}
				
				this.ListUploaderPeers[n + 1] = peer;
			}
			else
			{
				this.ListUploaderPeers.Add(peer);
			}
			
		}
		
		/// <summary>
		/// Searches a peer in the uploader peers list of the download.
		/// </summary>
		/// <param name="IP">The IP address of the uploader peer.</param>
		/// <returns>The peer object found.</returns>
		public Peer SearchUploaderPeer(string IP)
		{
			long key = Utilities.GetKey.IpPort(IP);
			
			int n = 0;
			int max = this.ListUploaderPeers.Count - 1;
			int min = 0;
			
			while (max >= min)
			{
				n = (max + min) / 2;
				
				if (key == this.ListUploaderPeers[n].Key)
				{
					return this.ListUploaderPeers[n];
				}
				
				if (key > this.ListUploaderPeers[n].Key)
				{
					min = n + 1;
				}
				else
				{
					max = n - 1;
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Searches a peer in the uploader peers list of the download.
		/// </summary>
		/// <param name="Key">The key of the peer.</param>
		/// <returns>The peer object found.</returns>
		public Peer SearchUploaderPeer(long Key)
		{
			int n = 0;
			int max = this.ListUploaderPeers.Count - 1;
			int min = 0;
			
			while (max >= min)
			{
				n = (max + min) / 2;
				
				if (Key == this.ListUploaderPeers[n].Key)
				{
					return this.ListUploaderPeers[n];
				}
				
				if (Key > this.ListUploaderPeers[n].Key)
				{
					min = n + 1;
				}
				else
				{
					max = n - 1;
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Logs a file pack downloaded.
		/// </summary>
		/// <param name="SenderIP">The IP address of the sender peer.</param>
		/// <param name="FilePackNumber">The number of the file pack.</param>
		public void LogFilePack(string SenderIP, int FilePackNumber)
		{
			// update the download speed of the download
			
			this.CountDownloadSpeed += 16384;
		}
		
		/// <summary>
		/// Updates the download rate info of this download.
		/// </summary>
		public void UpdateDownloadRateInfo()
		{
			this.DownloadSpeed = this.CountDownloadSpeed;
			
			this.CountDownloadSpeed = 0;
			
			// update download's informations
			this.Progress = (this.CurrentSize * 100.0f) / this.Size;
		}
		
		#endregion
	}
}