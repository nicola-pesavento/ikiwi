using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace iKiwi.Objects
{
	/// <summary>
	/// Peer object.
	/// </summary>
	public class Peer
	{
		#region Classes

		/// <summary>
		/// 
		/// </summary>
		public class File
		{
			#region Properties

			/// <summary>
			/// 
			/// </summary>
			public string Name
			{
				get;
				set;
			}

			/// <summary>
			/// 
			/// </summary>
			public long Size
			{
				get;
				set;
			}

			/// <summary>
			/// 
			/// </summary>
			public string SHA1
			{
				get;
				set;
			}

			#endregion
		}

		#endregion

		#region Properties

		/// <summary>
		/// The key for the list of peers.
		/// </summary>
		public long Key
		{ get; set; }
		
		/// <summary>
		/// IP:Port.
		/// </summary>
		public string IP
		{ get; set; }

		/// <summary>
		/// The ID of the peer.
		/// </summary>
		public string ID
		{ get; set; }

		/// <summary>
		/// TcpClient object of the peer.
		/// </summary>
		public TcpClient Client
		{ get; set; }

		/// <summary>
		/// The network stream object of the peer.
		/// </summary>
		public NetworkStream Stream
		{ get; set; }
		
		/// <summary>
		/// If True another thread is reading the NetworkStream of this peer.
		/// </summary>
		public bool ReadingData
		{ get; set; }
		
		/// <summary>
		/// If True another thread is writing on the NetworkStream of this peer.
		/// </summary>
		public bool WritingData
		{ get; set; }

		/// <summary>
		/// List of all File objects.
		/// </summary>
		public List<Peer.File> Files
		{ get; set; }

		/// <summary>
		/// The UTC date-time of start of connection.
		/// </summary>
		public DateTime Date
		{ get; set; }
		
		/// <summary>
		/// The download speed from the peer in bytes/sec.
		/// </summary>
		public int DownloadSpeed
		{ get; set; }
		
		/// <summary>
		/// Used to save the length of the input message to calculate the download speed of the peer.
		/// </summary>
		public int CountDownloadSpeed
		{ get; set; }
		
		/// <summary>
		/// The upload speed to the peer in bytes/sec.
		/// </summary>
		public int UploadSpeed
		{ get; set; }
		
		/// <summary>
		/// Used to save the length of the sended message to calculate the upload speed to the peer.
		/// </summary>
		public int CountUploadSpeed
		{ get; set; }
		
		/// <summary>
		/// Indicates if the message encryption is enabled or not.
		/// </summary>
		public bool MessageEncryptionEnabled
		{ get; set; }
		
		/// <summary>
		/// The public asymmetric encryption key of the peer.
		/// </summary>
		public Utilities.Cryptography.RsaKeys PublicAsymmetricEncryptionKey
		{ get; set; }
		
		/// <summary>
		/// The symmetric encryption key of the peer.
		/// </summary>
		public byte[] SymmetricEncryptionKey
		{ get; set; }
		
		/// <summary>
		/// My asymmetric encryption key.
		/// </summary>
		public Utilities.Cryptography.RsaKeys MyAsymmetricEncryptionKey
		{ get; set; }
		
		/// <summary>
		/// Indicates if the stream between this client and the peer is encrypted (True) or not (False).
		/// </summary>
		public bool SecureStream
		{ get; set; }
		
		/// <summary>
		/// Indicates if one or more downloads are in progress with this peer.
		/// </summary>
		public bool DownloadInProgress
		{ get; set; }
		
		/// <summary>
		/// Indicates if one or more uploads are in progress with this peer.
		/// </summary>
		public bool UploadInProgress
		{ get; set; }
		
		/// <summary>
		/// Indicates if some file pack has been downloaded.
		/// </summary>
		public bool FilePackDownloaded
		{ get; set; }
		
		/// <summary>
		/// Indicates if some file pack has been uploaded.
		/// </summary>
		public bool FilePackUploaded
		{ get; set; }

		#endregion
		
		#region Data Members
		
		private List<Messages.IMessage> m_listMessagesToWrite = new List<Messages.IMessage>();
		
		#endregion
		
		#region Ctor
		
		/// <summary>
		/// Create a peer object.
		/// </summary>
		/// <param name="IP">The IP address of the peer.</param>
		/// <param name="tcpClient">The TcpClient object of the peer.</param>
		/// <param name="networkStream">The NetworkStream object of the peer.</param>
		public Peer(string IP, TcpClient tcpClient, NetworkStream networkStream)
		{
			// set parameters
			this.Key = Utilities.GetKey.IpPort(IP);
			this.IP = IP;
			this.Client = tcpClient;
			this.Stream = networkStream;
			this.ReadingData = false;
			this.WritingData = false;
			this.Client.ReceiveBufferSize = 16384;
			this.Client.SendBufferSize = 16384;
			this.MessageEncryptionEnabled = false;
			this.PublicAsymmetricEncryptionKey = new iKiwi.Utilities.Cryptography.RsaKeys();
			this.SymmetricEncryptionKey = null;
			this.SecureStream = false;
			this.DownloadInProgress = false;
			this.UploadInProgress = false;
			
			// generate asymmetric keys
			this.MyAsymmetricEncryptionKey = Utilities.Cryptography.GenerateRsaKey();
			
			// set write timeout
			this.Stream.WriteTimeout = 10000;
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Add a file object to the Files property.
		/// </summary>
		/// <param name="fileName">File name.</param>
		/// <param name="fileSize">File size in bytes.</param>
		/// <param name="fileSHA1">File hash in SHA1.</param>
		public void AddFile(string fileName, uint fileSize, string fileSHA1)
		{
			Peer.File file = new Peer.File();

			file.Name = fileName;
			file.Size = fileSize;
			file.SHA1 = fileSHA1;

			for (int i = 0; i < this.Files.Count; i++)
			{
				if (this.Files[i].Name == fileName && this.Files[i].SHA1 == fileSHA1)
				{
					return;
				}
			}
			
			this.Files.Add(file);
		}

		/// <summary>
		/// Controls if there are one or more files that have a similar name.
		/// </summary>
		/// <param name="fileName">The file name to search.</param>
		/// <returns>If there are one or more files return True, else return False.</returns>
		public bool HasFileByName(string fileName)
		{
			for (int i = 0; i < this.Files.Count; i++)
			{
				if (this.Files[i].Name.IndexOf(fileName, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Sends a message to the peer.
		/// </summary>
		/// <param name="Message">Message to send.</param>
		public void Send(Messages.IMessage Message)
		{
			this.m_listMessagesToWrite.Add(Message);
		}
		
		/// <summary>
		/// Closes the connection and the daemons.
		/// </summary>
		public void Close()
		{
			if (this.Stream != null)
			{
				this.Stream.Close();
			}
			
			if (this.Client != null)
			{
				this.Client.Close();
			}
		}
		
		/// <summary>
		/// Receives the input messages and send the output messages.
		/// </summary>
		public void SendReceiveMessages()
		{
			// get and process the input messages
			GetAndProcessMessages();
			
			// send the output messages
			SendMessages();
		}
		
		/// <summary>
		/// Updates the download/upload rate info of this peer.
		/// </summary>
		public void UpdateDownloadUploadRate()
		{
			// update the download rate
			
			this.DownloadSpeed = this.CountDownloadSpeed;
			
			this.CountDownloadSpeed = 0;
			
			// update the upload rate
			
			this.UploadSpeed = this.CountUploadSpeed;
			
			this.CountUploadSpeed = 0;
		}
		
		/// <summary>
		/// Updates the status of the downloads and uploads from and to this peer.
		/// </summary>
		public void UpdateDownloadsUploadsStatus()
		{
			if (this.FilePackDownloaded == true)
			{
				this.FilePackDownloaded = false;
				
				this.DownloadInProgress = true;
			}
			else
			{
				this.DownloadInProgress = false;
			}
			
			if (this.FilePackUploaded == true)
			{
				this.FilePackUploaded = false;
				
				this.UploadInProgress = true;
			}
			else
			{
				this.UploadInProgress = false;
			}
		}

		#endregion
		
		#region Private methods
		
		/// <summary>
		/// Get and process the messages from NetworkStream.
		/// </summary>
		private void GetAndProcessMessages()
		{
			if (this.Stream.DataAvailable)
			{
				this.ReadingData = true;
				
				try
				{
					NetworkStream Stream = this.Stream;
					
					int byteSize = 32768;

					// number of read bytes
					int i = 0;
					
					// input buffer
					byte[] Buffer = new byte[byteSize];

					// message read in byte
					byte[] MessageByte = new byte[byteSize];
					
					int messageByteIndex = 0;
					
					// used if need more memory for message
					List<byte[]> MessageByteList = new List<byte[]>();
					
					// if has been recieved some data it is True, else it is False
					bool isDataReceived = false;
					
					// number of cycles where there have not been any input data
					short numEmptyCycles = 0;
					
					while (true)
					{
						// get the input data
						
						isDataReceived = false;
						
						#region ...
						do
						{
							if (this.Stream.DataAvailable)
							{
								// read the input data and put them in the buffer
								i = Stream.Read(Buffer, 0, Buffer.Length);
								
								if (i > 0)
								{
									if (isDataReceived == false)
									{
										isDataReceived = true;
									}
									
									// count the download rate
									
									Global.CountDownloadRate += i;
									
									this.CountDownloadSpeed += i;
									
									// save the read data
									
									int bufferIndex = 0;
									
									int byteToWrite = 0;
									
									while (true)
									{
										if (i <= (MessageByte.Length - messageByteIndex))
										{
											byteToWrite = i - bufferIndex;
											
											Array.Copy(Buffer, bufferIndex, MessageByte, messageByteIndex, byteToWrite);
											
											messageByteIndex += byteToWrite;
											
											break;
										}
										else
										{
											byteToWrite = MessageByte.Length - messageByteIndex;
											
											Array.Copy(Buffer, bufferIndex, MessageByte, messageByteIndex, byteToWrite);

											MessageByteList.Add(MessageByte);

											bufferIndex += byteToWrite;
											
											MessageByte = new byte[byteSize];

											messageByteIndex = 0;
										}
										
										if (bufferIndex >= i)
										{
											break;
										}
									}
								}
							}
							else
							{
								Thread.Sleep(5);
								
								if (!Stream.DataAvailable)
								{
									break;
								}
								
								i = 1;
							}
							
						} while (i != 0);
						#endregion

						// save all the data in MessageByte
						#region
						
						if (MessageByteList.Count == 0)
						{
							Array.Resize<byte>(ref MessageByte, messageByteIndex);
						}
						else
						{
							int dataSize = 0;
							
							for (int n = 0; n < MessageByteList.Count; n++)
							{
								dataSize += MessageByteList[n].Length;
							}
							
							dataSize += messageByteIndex;
							
							byte[] data = new byte[dataSize];
							
							Array.Resize<byte>(ref MessageByte, messageByteIndex);
							
							int dataIndex = 0;
							
							for (int a = 0; a < MessageByteList.Count; a++)
							{
								Array.Copy(MessageByteList[a], 0, data, dataIndex, MessageByteList[a].Length);
								
								dataIndex += MessageByteList[a].Length;
							}
							
							Array.Copy(MessageByte, 0, data, dataIndex, MessageByte.Length);

							MessageByte = data;
						}
						
						#endregion
						
						// process the input messages
						// and control if is there a incompleted message
						
						byte[] incompletedMessage = ProcessMessages(MessageByte);
						
						if (incompletedMessage == null)
						{
							break;
						}
						else
						{
							if (isDataReceived == false)
							{
								if (numEmptyCycles >= 10)
								{
									break;
								}
								
								numEmptyCycles++;
							}
						}
						
						// if there is a incompleted message get the rest of itM
						
						int size = 32768;
						
						if (incompletedMessage.Length >= size)
						{
							size = (incompletedMessage.Length / size) * 32768 + 32768;
						}
						
						MessageByte = new byte[size];
						
						Array.Copy(incompletedMessage, 0, MessageByte, 0, incompletedMessage.Length);

						messageByteIndex = incompletedMessage.Length;
						
						MessageByteList = new List<byte[]>();
					}
				}
				catch (IOException) // the peer is offline or disconnected
				{
					Lists.PeersList.RemoveAndClosePeer(this.IP);
				}
				catch (ObjectDisposedException) // the peer is offline or disconnected
				{
					Lists.PeersList.RemoveAndClosePeer(this.IP);
				}
				catch(Exception ex)
				{
					Utilities.Log.Write("Error to get a message from " + this.IP + ": " + ex.Message, Utilities.Log.LogCategory.Error);
				}
				
				this.ReadingData = false;
			}
		}
		
		/// <summary>
		/// Process a serie of messages.
		/// </summary>
		/// <param name="MessagesByte">The messages to process.</param>
		/// <returns>If returns NULL the messages was completed instead returns the incompleted message.</returns>
		private byte[] ProcessMessages (byte[] MessagesByte)
		{
			try
			{
				#region control if there is a single message or more, and control if the message ( the command only ) is correct
				
				// list of commands
				string[] commandsList = { "FS\n", "FF\n", "FPR", "FP\n", "PI\n", "PO\n", "XLR", "XL\n", "EI\n", "EK\n" };

				// the start of the message ( in byte )
				int messagePointer = 0;
				
				// the number of '\n' chars found in a single message
				int n = 0;
				
				// the command of the message
				string commandMess = string.Empty;
				
				bool commandCorrect = false;

				// the start of the length of the parameters
				int startLength = 0;

				// the start of the parameters
				int startParameters = 0;

				// the length of the parameters
				int parametersLength = 0;

				// the end of this single message ( in byte )
				int endSingleMessage = 0;

				while (true)
				{
					#region ...

					if ((messagePointer + 6) < MessagesByte.Length)
					{
						commandCorrect = false;
						
						// get the first 3 char of the message for get the command of it
						commandMess = ASCIIEncoding.Unicode.GetString(MessagesByte, messagePointer, 6); // unicode 2 byte for char ( 3 chars = 2*3 = 6 bytes )

						// control if the gotten command is correct or not
						for (int j = 0; j < commandsList.Length; j++)
						{
							if (commandMess == commandsList[j])
							{
								commandCorrect = true;
								
								break;
							}
						}
						
						if (commandCorrect == true)
						{
							#region
							
							n = 0;

							// get the length of the parameters of this message
							for (int b = messagePointer + 8; b < MessagesByte.Length; b += 2)
							{
								if (ASCIIEncoding.Unicode.GetString(MessagesByte, b, 2) == "\n")
								{
									n++;

									// the start of the length of the parameters
									if (n == 3)
									{
										startLength = b + 2;
									}

									// the finish of the length of the parameters
									if (n == 4)
									{
										startParameters = b + 2;

										if (startLength + (startParameters - startLength - 2) <= MessagesByte.Length)
										{
											parametersLength = int.Parse(ASCIIEncoding.Unicode.GetString(MessagesByte, startLength, (startParameters - startLength - 2)));

											endSingleMessage = startParameters + parametersLength - 1;

											break;
										}
										else // the message isn't complete
										{
											return MessagesByte;
										}
									}
								}
							}
							
							// control if the message is not complete
							if (n != 4)
							{
								return MessagesByte;
							}

							// get the single message from message_byte

							// control if the message is completed or not
							if ((MessagesByte.Length - endSingleMessage) > 0)
							{
								# region process the single message
								
								byte[] singleMessage = new byte[(endSingleMessage - messagePointer + 1)];
								
								Array.Copy(MessagesByte, messagePointer, singleMessage, 0, singleMessage.Length);
								
								// process the single message
								
								Thread t1 = new Thread(new ParameterizedThreadStart(
									delegate
									{
										Messages.IMessage message = Messages.MessagesFactory.Instance.ParseMessage(singleMessage, this);
										
										if (message != null)
										{
											// control if the message has already been processed time ago
											if ((message.MessageType != Messages.Commands.FS.ToString()) || !Lists.MessageIDsList.CheckAndAddID(message.ID))
											{
												// process the message
												message.Process();
												
												Utilities.Log.Write("New " + message.MessageType + " from " + this.IP, Utilities.Log.LogCategory.InputMessages);
											}
											else
											{
												Utilities.Log.Write(message.MessageType + " from " + this.IP + " has already been processed", Utilities.Log.LogCategory.InputMessages);
											}
										}
									}));

								t1.Name = "StreamChecker_GetAndProcessMessage";
								t1.Start();

								// update charPointer
								messagePointer = endSingleMessage + 1;
								
								#endregion
							}
							else // the message is not completed and need waits a new data-block
							{
								// get the incompleted message
								byte[] incompletedMessage = new byte[(MessagesByte.Length - messagePointer)];
								
								Array.Copy(MessagesByte, messagePointer, incompletedMessage, 0, incompletedMessage.Length);
								
								return incompletedMessage;
							}
							
							#endregion
						}
						else
						{
							Utilities.Log.Write("Invalid input message from " + this.IP, Utilities.Log.LogCategory.InputMessages);
							
							return null;
						}
					}
					else if (messagePointer == MessagesByte.Length)
					{
						return null;
					}
					else
					{
						return MessagesByte;
					}
					
					#endregion
				}
				
				#endregion
			}
			catch(Exception ex)
			{
				Utilities.Log.Write("Error to process a message from " + this.IP + ": " + ex.Message, Utilities.Log.LogCategory.Error);
				
				return null;
			}
		}
		
		/// <summary>
		/// Sends the messages in the NetworkStream.
		/// </summary>
		private void SendMessages()
		{
			if (this.m_listMessagesToWrite.Count != 0 && this.WritingData == false)
			{
				this.WritingData = true;
				
				int numOfMessageToSend = this.m_listMessagesToWrite.Count;
				
				byte[] messageByte = null;
				
				// write the messages in the list of the messages to write
				for (int i = numOfMessageToSend - 1; i >= 0; i--)
				{
					try
					{
						// control if the stream is encrypted or not
						if (this.SecureStream == true && this.m_listMessagesToWrite[i].MessageType != "EI")
						{
							if (this.m_listMessagesToWrite[i].MessageType == "EK")
							{
								this.m_listMessagesToWrite[i].EncryptionAlgorithmUsed = Utilities.EncryptionAlgorithm.RSA;
								
								this.m_listMessagesToWrite[i].AsymmetricEncryptionKey = this.PublicAsymmetricEncryptionKey;
								
								messageByte = this.m_listMessagesToWrite[i].EncryptedMessageByte;
							}
							else
							{
								this.m_listMessagesToWrite[i].EncryptionAlgorithmUsed = Utilities.EncryptionAlgorithm.AES_128;
								
								this.m_listMessagesToWrite[i].SymmetricEncryptionKey = this.SymmetricEncryptionKey;
								
								this.m_listMessagesToWrite[i].Encryption = true;
								
								messageByte = this.m_listMessagesToWrite[i].EncryptedMessageByte;
							}
						}
						else
						{
							messageByte = this.m_listMessagesToWrite[i].MessageByte;
						}
						
						this.Stream.Write(messageByte, 0, messageByte.Length);
						
						// save the upload rate
						
						Global.CountUploadRate += messageByte.Length;
						
						this.CountUploadSpeed += messageByte.Length;
						
						Utilities.Log.Write("A " + this.m_listMessagesToWrite[i].MessageType + " message has been sent to " + this.IP, Utilities.Log.LogCategory.OutputMessages);
					}
					catch (Exception ex)
					{
						// remove the peer
						Lists.PeersList.RemoveAndClosePeer(this.IP);

						Utilities.Log.Write("A message hasn't been sent, probably the peer is offline or disconnected: " + ex.Message, Utilities.Log.LogCategory.Error);
					}
				}
				
				// clear the list
				for (int i = 0; i < numOfMessageToSend; i++)
				{
					this.m_listMessagesToWrite.RemoveAt(this.m_listMessagesToWrite.Count - 1);
				}
				
				this.WritingData = false;
			}
		}
		
		#endregion
	}
}
