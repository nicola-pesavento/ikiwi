using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iKiwi.Messages
{
	class FileSearch : AbstractMessage
	{
		#region Data Members

		/// <summary>
		/// Searcher peer IP.
		/// </summary>
		private string m_strIp;

		/// <summary>
		/// Searcher peer port.
		/// </summary>
		private int m_nPort;

		/// <summary>
		/// Search file.
		/// </summary>
		private string m_nFilename;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				return (string.Format("{0}:{1}\nFN {2}",
				                      this.m_strIp,
				                      this.m_nPort,
				                      this.m_nFilename));
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				return (
					ASCIIEncoding.Unicode.GetBytes(this.Parameters)
				);
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// Create a FS-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public FileSearch(string MessageID, int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.FS, MessageID, TTL, Encryption)
		{
			Parameters[1] = Parameters[1].Remove(0, 3).Trim(' ');
			
			this.CreateFileSearch(Parameters);
		}

		/// <summary>
		/// Create a FS-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public FileSearch(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.FS, string.Empty, TTL, Encryption)
		{
			this.CreateFileSearch(Parameters);
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a FS-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		private void CreateFileSearch(params string[] Parameters)
		{
			string[] arrStrings = Parameters[0].Split(':');
			
			this.m_strIp = arrStrings[0];
			
			this.m_nPort = int.Parse(arrStrings[1]);
			
			this.m_nFilename = Parameters[1];
		}
		
		#endregion

		#region Methods

		public override void Process()
		{
			// Security: control that search fileName has at least 3 letters, except "." and "*" from them.
			if (m_nFilename.Length >= (3 + (m_nFilename.Split('*', '.').Length - 1)))
			{
				List<Objects.SharedFile> filesFound = Lists.FilesList.SearchFileByText(m_nFilename);

				if (filesFound.Count > 0)
				{
					string[] FF_parameters = new string[filesFound.Count * 3 + 1];
					
					FF_parameters[0] = m_nFilename;

					int n = 1;
					
					for (int i = 0; i < filesFound.Count; i++)
					{
						FF_parameters[n] = filesFound[i].Name;
						FF_parameters[n + 1] = filesFound[i].Size.ToString();
						FF_parameters[n + 2] = filesFound[i].SHA1;

						n += 3;
					}

					// create a Imessage object
					IMessage FF_message = MessagesFactory.Instance.CreateMessage(Commands.FF, FF_parameters);

					// reply with a FF-message
					this.SenderPeer.Send(FF_message);
				}

				// spreading
				if (this.TTL > 0)
				{
					this.TTL--;

					// number of sent messages
					int j = 0;

					// max number of messages to send
					int max = 10;

					for (int i = 0; i < Lists.PeersList.List.Count; i++)
					{
						// max 10 messages to spread
						if (j >= max)
						{
							break;
						}

						// control if there are enough peers for send 10 messages
						if ((Lists.PeersList.List.Count - i) > (max - j))
						{
							if (Lists.PeersList.List[i].HasFileByName(m_nFilename))
							{
								if(Lists.PeersList.List[i].IP != this.SenderPeer.IP)
								{
									Lists.PeersList.List[i].Send(this);
									j++;
								}
							}
						}
						else
						{
							if(Lists.PeersList.List[i].IP != this.SenderPeer.IP)
							{
								Lists.PeersList.List[i].Send(this);
								j++;
							}
						}
					}
				}
			}
		}

		#endregion
	}
}
