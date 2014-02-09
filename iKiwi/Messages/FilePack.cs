using System;
using System.Collections.Generic;
using System.Text;

namespace iKiwi.Messages
{
	class FilePack : AbstractMessage
	{
		#region Data Members

		private string m_strFileName;

		private string m_strFileID;

		private string m_strPackID;

		private int m_nStartPoint;

		private byte[] m_bytBinaryPack;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				string parameters = string.Format("FN {0}\nID {1}\nPI {2}\nST {3}\nBN\n{4}",
				                                  m_strFileName,
				                                  m_strFileID,
				                                  m_strPackID,
				                                  m_nStartPoint.ToString(),
				                                  Convert.ToBase64String(m_bytBinaryPack));

				return (parameters);
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				byte[] firstPart = ASCIIEncoding.Unicode.GetBytes(
					string.Format("FN {0}\nID {1}\nPI {2}\nST {3}\nBN\n",
					              m_strFileName,
					              m_strFileID,
					              m_strPackID,
					              m_nStartPoint.ToString())
				);

				byte[] parametersByte = new byte[firstPart.Length + m_bytBinaryPack.Length];

				firstPart.CopyTo(parametersByte, 0);

				m_bytBinaryPack.CopyTo(parametersByte, firstPart.Length);

				return (parametersByte);
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// Create a FP-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		/// <param name="BinaryPart">The binary part.</param>
		public FilePack(string MessageID, int TTL, bool Encryption, string[] Parameters, byte[] BinaryPart)
			: base(Commands.FP, MessageID, TTL, Encryption)
		{
			if (Parameters.Length == 4)
			{
				Parameters[0] = Parameters[0].Remove(0, 3);
				Parameters[1] = Parameters[1].Remove(0, 3);
				Parameters[2] = Parameters[2].Remove(0, 3);
				Parameters[3] = Parameters[3].Remove(0, 3);
				
				this.CreateFilePack(Parameters, BinaryPart);
			}
		}
		
		/// <summary>
		/// Create a FP-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		/// <param name="BinaryPart">The binary part.</param>
		public FilePack(int TTL, bool Encryption, string[] Parameters, byte[] BinaryPart)
			: base(Commands.FP, string.Empty, TTL, Encryption)
		{
			if (Parameters.Length == 4)
			{
				this.CreateFilePack(Parameters, BinaryPart);
			}
		}
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a FP-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		/// <param name="BinaryPart">The binary part.</param>
		private void CreateFilePack(string[] Parameters, byte[] BinaryPart)
		{
			this.m_strFileName = Parameters[0];
			this.m_strFileID = Parameters[1];
			this.m_strPackID = Parameters[2];
			this.m_nStartPoint = int.Parse(Parameters[3]);
			this.m_bytBinaryPack = BinaryPart;
		}
		
		#endregion

		#region Methods

		public override void Process()
		{
			// process the file pack
			Daemons.Downloader.ProcessFilePack(m_strFileName, m_strFileID, m_nStartPoint, m_strPackID, m_bytBinaryPack, this.SenderPeer);
		}

		#endregion
	}
}
