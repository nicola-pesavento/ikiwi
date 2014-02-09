using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace iKiwi.Messages
{
	class FilePackRequest : AbstractMessage
	{
		#region Data Members

		private string m_strFileName;

		private string m_strFileID;

		private int m_nStartPoint;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				string parameters = string.Format("FN {0}\nID {1}\nST {2}",
				                                  m_strFileName,
				                                  m_strFileID,
				                                  m_nStartPoint.ToString());

				return (parameters);
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				string parameters = string.Format("FN {0}\nID {1}\nST {2}",
				                                  m_strFileName,
				                                  m_strFileID,
				                                  m_nStartPoint.ToString());

				return (ASCIIEncoding.Unicode.GetBytes(parameters));
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// Create a FPR-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public FilePackRequest(string MessageID, int TTL, bool Encryption, string[] Parameters)
			: base(Commands.FPR, MessageID, TTL, Encryption)
		{
			if (Parameters.Length == 3)
			{
				Parameters[0] = Parameters[0].Remove(0, 3);
				Parameters[1] = Parameters[1].Remove(0, 3);
				Parameters[2] = Parameters[2].Remove(0, 3);
				
				this.CreateFilePackRequest(Parameters);
			}
		}

		/// <summary>
		/// Create a FPR-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public FilePackRequest(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.FPR, string.Empty, TTL, Encryption)
		{
			if (Parameters.Length == 3)
			{
				this.CreateFilePackRequest(Parameters);
			}
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a FPR-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		private void CreateFilePackRequest(params string[] Parameters)
		{
			this.m_strFileName = Parameters[0];
			this.m_strFileID = Parameters[1];
			this.m_nStartPoint = int.Parse(Parameters[2]);
		}
		
		#endregion

		#region Methods

		public override void Process()
		{
			Objects.SharedFile file = Lists.FilesList.GetFile(m_strFileName, m_strFileID);

			// control if the file exists
			if (file != null)
			{
				while (true)
				{
					try
					{
						FileStream fs = File.OpenRead(file.Path);

						// lock the access to file
						fs.Lock(0, 16384);

						byte[] buffer = new byte[16384];

						fs.Seek(m_nStartPoint, SeekOrigin.Begin);

						// control that the FilePack_Length isn't more big than the rest of file
						if (fs.Length > (m_nStartPoint + 16384))
						{
							fs.Read(buffer, 0, buffer.Length);
						}
						else
						{
							fs.Read(buffer, 0, (int)(fs.Length - m_nStartPoint));
						}

						// unlock the access to file
						fs.Unlock(0, 16384);

						fs.Close();

						// send the FP-message

						SHA1 sha1 = new SHA1CryptoServiceProvider();

						string[] Params = new string[4];

						Params[0] = m_strFileName;
						Params[1] = m_strFileID;
						Params[2] = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");
						Params[3] = m_nStartPoint.ToString();

						// build the message
						IMessage iMessage = MessagesFactory.Instance.CreateMessage(Commands.FP, Params, buffer);

						// send the message
						this.SenderPeer.Send(iMessage);
						
						// indicate the uploading of a file pack
						this.SenderPeer.FilePackUploaded = true;
						
						break;
					}

					catch (IOException) // the file is already locked
					{
						Thread.Sleep(1);
					}

					catch (Exception ex) // other problem
					{
						Utilities.Log.Write("Error to read from a file during a upload process: \n" + ex.Message, Utilities.Log.LogCategory.Error);
						break;
					}
				}
			}
		}

		#endregion
	}
}
