using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace iKiwi.Messages
{
	class XmlList : AbstractMessage
	{
		#region Data Members

		private byte[] m_bytXmlBinary;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				string parameters = string.Format("BN\n{0}", Convert.ToBase64String(m_bytXmlBinary));

				return (parameters);
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				byte[] firstPart = ASCIIEncoding.Unicode.GetBytes("BN\n");

				byte[] parametersByte = new byte[firstPart.Length + m_bytXmlBinary.Length];

				firstPart.CopyTo(parametersByte, 0);

				m_bytXmlBinary.CopyTo(parametersByte, firstPart.Length);

				return (parametersByte);
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// Create a XL-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		/// <param name="BinaryPart">The binary part.</param>
		public XmlList(string MessageID, int TTL, bool Encryption, string[] Parameters, byte[] BinaryPart)
			: base(Commands.XL, MessageID, TTL, Encryption)
		{
			this.CreateXmlList(Parameters, BinaryPart);
		}
		
		
		/// <summary>
		/// Create a XL-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		/// <param name="BinaryPart">The binary part.</param>
		public XmlList(int TTL, bool Encryption, string[] Parameters, byte[] BinaryPart)
			: base(Commands.XL, string.Empty, TTL, Encryption)
		{
			this.CreateXmlList(Parameters, BinaryPart);
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a XL-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		/// <param name="BinaryPart">The binary part.</param>
		private void CreateXmlList(string[] Parameters, byte[] BinaryPart)
		{
			this.m_bytXmlBinary = BinaryPart;
		}
		
		#endregion

		#region Methods

		public override void Process()
		{
			// save the List.xml in [temp-directory]\List\List_[SenderID].xml
			while (true)
			{
				try
				{
					FileStream fs = new FileStream((Global.TempDirectory + @"\List\" + string.Format("List_{0}.xml", this.SenderPeer.ID)), FileMode.Create);

					try { fs.Lock(0, fs.Length); }
					catch { }

					fs.Write(m_bytXmlBinary, 0, m_bytXmlBinary.Length);

					try { fs.Unlock(0, fs.Length); }
					catch { }

					fs.Close();
					
					break;
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
