using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Timers;

namespace iKiwi.Daemons
{
	/// <summary>
	/// Manages the download operations.
	/// </summary>
	class Downloader // TODO: improve the managements of the downloads (speed)
	{
		public enum Status_FilePack
		{
			Written,
			Damaged
		}

		#region Data Members

		/// <summary>
		/// The list of the files in downloading. Contains Download objects.
		/// </summary>
		private static List<Objects.Download> m_downloadsList = new List<Objects.Download>();
		
		/// <summary>
		/// The main thread of the this daemon.
		/// </summary>
		private static Thread m_mainThread = null;

		#endregion
		
		#region Properties
		
		/// <summary>
		/// The number of peers for each downloads.
		/// </summary>
		public static int NumPeersForEachDownloads
		{
			get { return _NumPeersForEachDownloads; }
			set { _NumPeersForEachDownloads = value; }
		}
		private static int _NumPeersForEachDownloads = 5;
		
		/// <summary>
		///  A flag indicating if is added or updated a download.
		/// </summary>
		public static bool NewOrUpdatedDownload
		{
			get { return _NewOrUpdatedDownload; }
			set { _NewOrUpdatedDownload = value; }
		}
		private static bool _NewOrUpdatedDownload = false;
		
		/// <summary>
		/// The number of downloads.
		/// </summary>
		public static int Count
		{
			get { return m_downloadsList.Count; }
		}
		
		/// <summary>
		/// The list of downloads.
		/// </summary>
		public static List<Objects.Download> List
		{
			get { return m_downloadsList; }
		}
		
		#endregion

		#region Daemons

		/// <summary>
		/// Start to manages, in background, the download-operations.
		/// </summary>
		public static void Start()
		{
			if (m_mainThread != null)
			{
				// close the deamon
				Close();
			}
			
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// start a timer to update the downloads
					
					System.Timers.Timer timer = new System.Timers.Timer(1000);
					
					timer.Elapsed += new ElapsedEventHandler(
						delegate
						{
							for (int i = 0; i < m_downloadsList.Count; i++)
							{
								try
								{
									// update the download rate info of this downloads
									m_downloadsList[i].UpdateDownloadRateInfo();
								}
								catch
								{
								}
							}
						});
					
					timer.Start();
					
					// manage the downloads
					while (true)
					{
						for(int i = 0; i < m_downloadsList.Count; i++)
						{
							Objects.Download download = m_downloadsList[i];
							
							if (download.Active == true)
							{
								ManageUploaderPeers(download);
								
								// control if the download speed is 0
								if (download.DownloadSpeed == 0)
								{
									for (int n = 0; n < download.ListPeers.Count; n++)
									{
										DownloadFirstNotDownloadedFilePack(download, download.ListPeers[n]);
									}
								}
							}
						}

						Thread.Sleep(5000);
					}
				}));

			t.Name = "Downloader";
			t.IsBackground = true;
			t.Start();
			
			m_mainThread = t;
		}

		#endregion

		#region Private Methods
		
		/// <summary>
		/// Manage the uploader peers of a download.
		/// </summary>
		/// <param name="Download">The download object.</param>
		private static void ManageUploaderPeers(Objects.Download Download)
		{
			CheckMinNumberOfUploaderPeers(Download);

			// search the slowest uploader peer
			
			int slowestDownloadSpeed = 0;
			
			int b = 0;
			
			for(int i = 0; i < Download.ListUploaderPeers.Count; i++)
			{
				if (Download.ListUploaderPeers[i].DownloadSpeed < slowestDownloadSpeed)
				{
					b = i;
					
					slowestDownloadSpeed = Download.ListUploaderPeers[i].DownloadSpeed;
				}
				
				// control if the download speed of the peer is zero
				if (Download.ListUploaderPeers[i].DownloadSpeed == 0)
				{
					// send a new FPR-message to the peer
					DownloadFirstNotDownloadedFilePack(Download, Download.ListUploaderPeers[i]);
				}
			}
			
			if(Download.ListUploaderPeers.Count >= NumPeersForEachDownloads)
			{
				// remove the slowest uploader peer
				Download.ListUploaderPeers.RemoveAt(b);
				
				// add a new uploader peer
				CheckMinNumberOfUploaderPeers(Download);
			}
		}
		
		/// <summary>
		/// Control if there is the minimum number of uploader peers else get other uploader peers.
		/// </summary>
		/// <param name="download">The download object.</param>
		/// <returns>Return true if there is the minimum number of the uploader peers else return false.</returns>
		private static bool CheckMinNumberOfUploaderPeers(Objects.Download download)
		{
			if(download.ListUploaderPeers.Count < NumPeersForEachDownloads)
			{
				System.Random random = new Random();
				
				while(download.ListUploaderPeers.Count < NumPeersForEachDownloads)
				{
					// control if there are peers that have this file
					if (download.ListPeers.Count == 0)
					{
						download.FindPeers();
					}
					
					if (download.ListPeers.Count > download.ListUploaderPeers.Count)
					{
						// get a casual peer
						Objects.Peer peer = download.ListPeers[random.Next(download.ListPeers.Count)];
						
						// control if the peer is already an uploader
						if (download.SearchUploaderPeer(peer.Key) == null)
						{
							// add the peer in the list of the uploader peers of the download
							download.AddUploaderPeer(peer);
							
							// send a FPR message to the new uploader peer
							if (StartDownloadNextFilePart(download, peer) == false)
							{
								DownloadFirstNotDownloadedFilePack(download, peer);
							}
						}
					}
					else
					{
						return false;
					}
				}
				
				return true;
			}
			else
			{
				return true;
			}
		}
		
		/// <summary>
		/// Send a new FPR to the peer for download the next not-downloaded file pack of a this file part.
		/// </summary>
		/// <param name="filePackNum">The number of the file pack.</param>
		/// <param name="filePartNum">The number of the file part.</param>
		/// <param name="download">The download object.</param>
		/// <param name="Peer">The IP address of peer to send the FPR message.</param>
		/// <returns>Return true if the message has been sended; return false if the file part is completed.</returns>
		private static bool DownloadNextFilePackOfAFilePart (int filePackNum, int filePartNum, Objects.Download download, Objects.Peer Peer)
		{
			int nPack = filePackNum;
			int partEnd = (filePartNum * 128) + 128;
			
			for(int i = 0; i < 128; i++)
			{
				if (nPack < download.ListFilePacks.Length)
				{
					if (download.ListFilePacks[nPack] == false)
					{
						// download this file pack
						
						string[] Params = new string[3];
						Params[0] = download.Name;
						Params[1] = download.SHA1;
						Params[2] = (nPack * 16384).ToString();

						Messages.IMessage FprMess = Messages.MessagesFactory.Instance.CreateMessage(Messages.Commands.FPR, Params);

						Peer.Send(FprMess);
						
						return true;
					}
				}
				else
				{
					return false;
				}
				
				if (nPack < partEnd)
				{
					nPack++;
				}
				else
				{
					nPack = filePartNum * 128;
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Send a new FPR to the peer for download the next not-downloaded file pack of a this file part.
		/// </summary>
		/// <param name="download">The download object.</param>
		/// <param name="Peer">The IP address of peer to send the FPR message.</param>
		/// <returns>Return true if the message has been sended.</returns>
		private static bool DownloadFirstNotDownloadedFilePack (Objects.Download download, Objects.Peer Peer)
		{
			for (int i = 0; i < download.ListFileParts.Length; i++)
			{
				if (download.ListFileParts[i] > 0)
				{
					int a = i * 128;
					
					for (int n = 0; n < 128; n++)
					{
						if (download.ListFilePacks[a + n] == false)
						{
							// start to download the file part
							
							string[] Params = new string[3];
							Params[0] = download.Name;
							Params[1] = download.SHA1;
							Params[2] = ((a + n) * 16384).ToString();

							Messages.IMessage FprMess = Messages.MessagesFactory.Instance.CreateMessage(Messages.Commands.FPR, Params);

							Peer.Send(FprMess);
							
							return true;
						}
					}
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Send a new FPR to the peer for download a file pack of a not-downloading file part.
		/// </summary>
		/// <param name="download">The download object.</param>
		/// <param name="Peer">The IP address of peer to send the FPR message.</param>
		/// <returns>Return true if the message has been sended.</returns>
		private static bool StartDownloadNextFilePart (Objects.Download download, Objects.Peer Peer)
		{
			// the index of the not-downloaded file parts
			int[] index = new int[download.RemainingFileParts];
			
			int n = 0;
			
			// get the not-downloaded file parts
			for (int i = 0; i < download.ListFileParts.Length; i++)
			{
				if (download.ListFileParts[i] == 128 || (download.ListFileParts[i] > 0 && i == (download.ListFileParts.Length - 1)) )
				{
					index[n] = i;
					
					n++;
				}
			}
			
			// select a random not-downloaded file part to download
			
			Random random = new Random();
			
			if (n > 0)
			{
				while(true)
				{
					int a = 0;
					
					if (download.RemainingFileParts != 0)
					{
						a = random.Next(n);
						
						if (download.ListFileParts[index[a]] != 0)
						{
							// start to download the file part
							
							string[] Params = new string[3];
							Params[0] = download.Name;
							Params[1] = download.SHA1;
							Params[2] = (index[a] * 16384).ToString();

							Messages.IMessage FprMess = Messages.MessagesFactory.Instance.CreateMessage(Messages.Commands.FPR, Params);

							Peer.Send(FprMess);
							
							return true;
						}
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Closes the downloader demon.
		/// </summary>
		public static void Close()
		{
			if (m_mainThread != null)
			{
				m_mainThread.Abort();
			}
		}

		/// <summary>
		/// Creates a new download from a exist file in the list of the found files.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The ID of the file.</param>
		public static void AddDownload(string FileName, string FileID)
		{
			Lists.FilesFoundList.File file = Lists.FilesFoundList.GetFile(FileName, FileID);
			
			Objects.Download download = new Objects.Download(file.Name, file.Size, file.SHA1);
			download.Active = true;
			
			download.FindPeers();
			
			if (List.Count > 0)
			{
				int n = 0;
				int max = List.Count - 1;
				int min = 0;
				int compare = 0;
				
				while (max >= min)
				{
					n = (max + min) / 2;
					
					compare = FileID.CompareTo(List[n].SHA1);
					
					if (compare > 0)
					{
						min = n + 1;
					}
					else if (compare < 0)
					{
						max = n - 1;
					}
					else
					{
						return;
					}
				}
				
				if (compare < 0)
				{
					n--;
				}
				
				List.Add(List[List.Count - 1]);
				
				for(int i = List.Count - 1; i > n + 1; i--)
				{
					List[i] = List[i - 1];
				}
				
				List[n + 1] = download;
			}
			else
			{
				List.Add(download);
			}
			
			NewOrUpdatedDownload = true;
		}
		
		/// <summary>
		/// Adds a download object.
		/// </summary>
		/// <param name="download">The download object.</param>
		public static void AddDownload(Objects.Download download)
		{
			download.Active = true;
			
			download.FindPeers();
			
			if (List.Count > 0)
			{
				int n = 0;
				int max = List.Count - 1;
				int min = 0;
				int compare = 0;
				
				while (max >= min)
				{
					n = (max + min) / 2;
					
					compare = download.SHA1.CompareTo(List[n].SHA1);
					
					if (compare > 0)
					{
						min = n + 1;
					}
					else if (compare < 0)
					{
						max = n - 1;
					}
					else
					{
						return;
					}
				}
				
				if (compare < 0)
				{
					n--;
				}
				
				List.Add(List[List.Count - 1]);
				
				for(int i = List.Count - 1; i > n + 1; i--)
				{
					List[i] = List[i - 1];
				}
				
				List[n + 1] = download;
			}
			else
			{
				List.Add(download);
			}
			
			NewOrUpdatedDownload = true;
		}

		/// <summary>
		/// Control if exist a determinate download.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The ID of the file.</param>
		/// <returns>1 exists, 0 doesn't exist.</returns>
		public static bool ExistDownload(string FileName, string FileID)
		{
			int n = 0;
			int max = List.Count - 1;
			int min = 0;
			int compare = 0;
			
			while (max >= min)
			{
				n = (max + min) / 2;
				
				compare = FileID.CompareTo(List[n].SHA1);
				
				if (compare == 0)
				{
					return true;
				}
				
				if (compare > 0)
				{
					min = n + 1;
				}
				else if (compare < 0)
				{
					max = n - 1;
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Search a existing download.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The ID of the file.</param>
		/// <returns>The download object (null if it doesn't exist).</returns>
		public static Objects.Download SearchDownload(string FileName, string FileID)
		{
			int n = 0;
			int max = List.Count - 1;
			int min = 0;
			int compare = 0;
			
			while (max >= min)
			{
				n = (max + min) / 2;
				
				compare = FileID.CompareTo(List[n].SHA1);
				
				if (compare == 0)
				{
					return List[n];
				}
				
				if (compare > 0)
				{
					min = n + 1;
				}
				else if (compare < 0)
				{
					max = n - 1;
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// Remove a download.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The ID of the file.</param>
		public static void RemoveDownload(string FileName, string FileID)
		{
			int n = 0;
			int max = List.Count - 1;
			int min = 0;
			int compare = 0;
			
			while (max >= min)
			{
				n = (max + min) / 2;
				
				compare = FileID.CompareTo(List[n].SHA1);
				
				if (compare == 0)
				{
					List.RemoveAt(n);
					
					NewOrUpdatedDownload = true;
					
					break;
				}
				
				if (compare > 0)
				{
					min = n + 1;
				}
				else if (compare < 0)
				{
					max = n - 1;
				}
			}
			
			// remove the temp file
			
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					Thread.Sleep(1000);
					
					try
					{
						File.Delete(Global.TempDirectory + FileName);
					}
					catch
					{
					}
				}
			));
			
			t.Name = "Remove temp file";
			t.IsBackground = true;
			t.Start();
		}
		
		/// <summary>
		/// Process a file pack.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The ID of the file.</param>
		/// <param name="StartPoint">The star point of the file pack.</param>
		/// <param name="PackID">The ID of the binary data.</param>
		/// <param name="Binary">The binary data.</param>
		/// <param name="SenderPeer">The sender peer object.</param>
		public static void ProcessFilePack(string FileName, string FileID, int StartPoint, string PackID, byte[] Binary, Objects.Peer SenderPeer)
		{
			Objects.Download download = Downloader.SearchDownload(FileName, FileID);
			
			// control if exist this download and if it is actived
			if (download != null && download.Active == true)
			{
				// control that the binaryPack isn't damaged

				SHA1 sha1 = new SHA1CryptoServiceProvider();

				if (PackID == BitConverter.ToString(sha1.ComputeHash(Binary)).Replace("-", ""))
				{
					#region Write to the file
					
					while (true)
					{
						try
						{
							// open ( or create ) the file ( in temp-directory )
							FileStream fs = new FileStream((Global.TempDirectory + @"\" + FileName), FileMode.OpenOrCreate);

							// lock the access to file
							fs.Lock(StartPoint, 16384);

							// set the start-point
							fs.Seek(StartPoint, SeekOrigin.Begin);
							
							int count = 16384;
							
							if((download.Size - StartPoint) < 16384)
							{
								count = ((int)download.Size - StartPoint);
							}
							
							// write the filePack in the file ( temp )
							fs.Write(Binary, 0, count);

							// unlock the access to file
							fs.Unlock(StartPoint, 16384);

							fs.Close();

							// log the file pack
							Downloader.LogFilePack(download, StartPoint, SenderPeer, Downloader.Status_FilePack.Written);
							
							break;
						}

						catch (IOException) // the file is already locked
						{
							Thread.Sleep(1);
						}

						catch (Exception ex) // other problem
						{
							Utilities.Log.Write("Downloader.cs: error to write in a file: \n" + ex.Message, Utilities.Log.LogCategory.Error);
							break;
						}
					}

					#endregion
				}
				else
				{
					// log
					// communicate to Downloader that the FilePack is damaged
					Downloader.LogFilePack(download, StartPoint, SenderPeer, Downloader.Status_FilePack.Damaged);
				}
			}
		}
		
		#endregion
		
		#region Private Methods

		/// <summary>
		/// Logs a downloaded file pack and send a new FPR message if is necessary.
		/// </summary>
		/// <param name="Download">The download object.</param>
		/// <param name="StartPoint">The star point of the file pack.</param>
		/// <param name="SenderPeer">The sender peer object.</param>
		/// <param name="Status">If the file-pack has been written or not ( other ).</param>
		public static void LogFilePack(Objects.Download Download, int StartPoint, Objects.Peer SenderPeer, Status_FilePack Status)
		{
			// indicate the downloading of a file pack
			SenderPeer.FilePackDownloaded = true;
			
			if (Status == Status_FilePack.Written)
			{
				#region ...
				
				int filePackNum = StartPoint / 16384;
				int filePartNum = StartPoint / 2097152;
				
				// control if the file pack is already been downloaded
				if (Download.ListFilePacks[filePackNum] == false)
				{
					// update the list of file packs
					Download.ListFilePacks[filePackNum] = true;
					
					// update the number of remaining file packs
					Download.RemainingFilePacks--;
					
					// update the list of file parts
					Download.ListFileParts[filePartNum]--;
					
					// control if the file part is completed
					if (Download.ListFileParts[filePartNum] == 0)
					{
						// update the number of remaining file parts
						Download.RemainingFileParts--;
						
						// control if the download is finished
						if (Download.RemainingFilePacks == 0)
						{
							// move the completed download from temp-directory to shared-directory
							File.Move(Global.TempDirectory + Download.Name, Global.SharedDirectory + Download.Name);
							
							// update download's informations
							Download.Progress = 100;
							Download.Active = false;
						}
						else
						{
							// send a new FPR to the peer for the next not downloaded file pack of a new file part
							StartDownloadNextFilePart(Download, SenderPeer);
						}
					}
					else
					{
						// send a new FPR to the peer for the next not downloaded file pack of a this file part
						if (DownloadNextFilePackOfAFilePart(filePackNum, filePartNum, Download, SenderPeer) == false)
						{
							StartDownloadNextFilePart(Download, SenderPeer);
						}
					}
					
					// log the file pack
					Download.LogFilePack(SenderPeer.IP, filePackNum);
					
					NewOrUpdatedDownload = true;
				}
				
				#endregion
			}
			
			// if the FilePack is damaged, will send a new FPR-mess to the peer;
			else if (Status == Status_FilePack.Damaged)
			{
				#region ...
				
				string[] Params = new string[3];
				Params[0] = Download.Name;
				Params[1] = Download.SHA1;
				Params[2] = StartPoint.ToString();

				Messages.IMessage FprMess = Messages.MessagesFactory.Instance.CreateMessage(Messages.Commands.FPR, Params);

				SenderPeer.Send(FprMess);
				
				#endregion
			}
		}

		#endregion
	}
}