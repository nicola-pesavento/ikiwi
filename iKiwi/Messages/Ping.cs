using System;
using System.Collections.Generic;
using System.Text;

namespace iKiwi.Messages
{
	class Ping : AbstractMessage
	{
		#region Classes

		public class File
		{
			#region Properties

			public string Name
			{ get; set; }

			public long Size
			{ get; set; }

			public string SHA1
			{ get; set; }

			#endregion
		}

		#endregion

		#region Data Members

		private string m_strPeerID;

		private Ping.File[] m_arrFiles;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				string parameters = string.Format("PID {0}", this.m_strPeerID);
				
				for (int i = 0; i < m_arrFiles.Length; i++)
				{
					parameters += string.Format("\nFN {0}\nSZ {1}\nID {2}", m_arrFiles[i].Name, m_arrFiles[i].Size, m_arrFiles[i].SHA1);
				}
				parameters.Trim();

				return (parameters);
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				return (ASCIIEncoding.Unicode.GetBytes(this.Parameters));
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// Create a PI-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public Ping(string MessageID, int TTL, bool Encryption, string[] Parameters)
			: base(Commands.PI, MessageID, TTL, Encryption)
		{
			if (((Parameters.Length - 1) % 3) == 0)
			{
				Parameters[0] = Parameters[0].Remove(0, 4);

				int nFiles = (Parameters.Length - 1) / 3;

				int i;
				
				for (int n = 1; n <= nFiles; n++)
				{
					i = n * 3;

					Ping.File file = new Ping.File();

					Parameters[(i - 2)] = Parameters[(i - 2)].Remove(0, 3);
					Parameters[(i - 1)] = (Parameters[(i - 1)]).Remove(0, 3);
					Parameters[i] = Parameters[i].Remove(0, 3);
				}
				
				this.CreatePing(Parameters);
			}
		}

		/// <summary>
		/// Create a PI-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public Ping(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.PI, string.Empty, TTL, Encryption)
		{
			if (((Parameters.Length - 1) % 3) == 0)
			{
				this.CreatePing(Parameters);
			}
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a PI-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		private void CreatePing(params string[] Parameters)
		{
			// if Parameters is empty it will automatic created
			if (Parameters[0] == "")
			{
				this.m_strPeerID = Global.MyPeerID;
				
				this.m_arrFiles = new Ping.File[Lists.FilesList.Count];
				
				for (int i = 0; i < m_arrFiles.Length; i++)
				{
					if (i < Lists.FilesList.Count)
					{
						Objects.SharedFile file = Lists.FilesList.List[i];
						
						Ping.File PIfile = new Ping.File();
						
						PIfile.Name = file.Name;
						PIfile.Size = file.Size;
						PIfile.SHA1 = file.SHA1;
						
						if (i < m_arrFiles.Length)
						{
							this.m_arrFiles[i] = PIfile;
						}
						else
						{
							break;
						}
					}
					else
					{
						Array.Resize(ref this.m_arrFiles, i);
						
						break;
					}
				}
			}
			else
			{
				this.m_strPeerID = Parameters[0];

				int nFiles = (Parameters.Length - 1) / 3;

				this.m_arrFiles = new Ping.File[nFiles];

				int i;
				
				for (int n = 1; n <= nFiles; n++)
				{
					i = n * 3;

					Ping.File file = new Ping.File();

					file.Name = Parameters[(i - 2)];
					file.Size = uint.Parse((Parameters[(i - 1)]));
					file.SHA1 = Parameters[i];

					this.m_arrFiles[(n - 1)] = file;
				}
			}
		}
		
		#endregion

		#region Methods

		public override void Process()
		{
			// read all the informations of the message sender and add them to the Lists.PeersList apposite item
			List<Objects.Peer.File> files = new List<Objects.Peer.File>();
			
			if (m_arrFiles != null)
			{
				for (int i = 0; i < m_arrFiles.Length; i++)
				{
					Objects.Peer.File pFile = new Objects.Peer.File();
					
					pFile.Name = m_arrFiles[i].Name;
					pFile.Size = m_arrFiles[i].Size;
					pFile.SHA1 = m_arrFiles[i].SHA1;

					files.Add(pFile);
				}
			}

			Lists.PeersList.UpdatePeer_ID_Files(this.SenderPeer.IP, m_strPeerID, files);

			// reply with a PO-message

			string[] PO_Params = new string[(Lists.FilesList.Count * 3) + 1];
			
			PO_Params[0] = Global.MyPeerID;

			int n = 0;
			
			for (int i = 1; i < PO_Params.Length; i += 3)
			{
				if (n < Lists.FilesList.Count)
				{
					PO_Params[i] = Lists.FilesList.List[n].Name;
					PO_Params[i + 1] = Lists.FilesList.List[n].Size.ToString();
					PO_Params[i + 2] = Lists.FilesList.List[n].SHA1;
					
					n++;
				}
				else
				{
					Array.Resize(ref PO_Params, i);
					
					break;
				}
			}

			// reply
			
			IMessage POmessage = MessagesFactory.Instance.CreateMessage(Commands.PO, PO_Params);
			
			this.SenderPeer.Send(POmessage);
		}

		#endregion
	}
}
