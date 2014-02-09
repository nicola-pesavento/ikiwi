using System;
using System.Text;

namespace iKiwi.Messages
{
	/// <summary>
	/// Encryption Info message.
	/// </summary>
	class EncryptionInfo : AbstractMessage
	{
		#region Data Members
		
		/// <summary>
		/// Indicates if the peer uses or not the message encryption.
		/// </summary>
		private string m_strEncryptionUsed;
		
		/// <summary>
		/// The modulus N of the asymmetric encryption key.
		/// </summary>
		private string m_strAsymmetricEncryptionKeyN;
		
		/// <summary>
		/// The public exponent E of the asymmetric encryption key.
		/// </summary>
		private string m_strAsymmetricEncryptionKeyE;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				return (string.Format("EU {0}\nAEK {1},{2}",
				                      this.m_strEncryptionUsed,
				                      this.m_strAsymmetricEncryptionKeyN,
				                      this.m_strAsymmetricEncryptionKeyE));
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
		/// Create a EI-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public EncryptionInfo(string MessageID, int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.EI, MessageID, TTL, Encryption)
		{
			if (Parameters.Length == 2)
			{
				Parameters[0] = Parameters[0].Remove(0, 3);
				Parameters[1] = Parameters[1].Remove(0, 4).Trim();
				
				this.CreateEncryptionInfo(Parameters);
			}
		}

		/// <summary>
		/// Create a EI-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public EncryptionInfo(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.EI, string.Empty, TTL, Encryption)
		{
			if (Parameters.Length == 2)
			{
				this.CreateEncryptionInfo(Parameters);
			}
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a EI-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		private void CreateEncryptionInfo(params string[] Parameters)
		{
			this.m_strEncryptionUsed = Parameters[0];
			
			string[] subStr = Parameters[1].Split(',');
			
			if (subStr.Length == 2)
			{
				this.m_strAsymmetricEncryptionKeyN = subStr[0];
				
				this.m_strAsymmetricEncryptionKeyE = subStr[1];
			}
			else
			{
				this.m_strAsymmetricEncryptionKeyN = string.Empty;
				
				this.m_strAsymmetricEncryptionKeyE = string.Empty;
			}
		}
		
		#endregion
		
		#region Methods
		
		public override void Process()
		{
			if (this.m_strEncryptionUsed == "y" || this.m_strEncryptionUsed == "Y")
			{
				if (Global.MessageEncryptionEnabled == true)
				{
					// save the asymmetric encryption key in the peer object
					
					this.SenderPeer.PublicAsymmetricEncryptionKey.N = Utilities.Converterer.ConvertHexToByte(this.m_strAsymmetricEncryptionKeyN);
					
					this.SenderPeer.PublicAsymmetricEncryptionKey.E = Utilities.Converterer.ConvertHexToByte(this.m_strAsymmetricEncryptionKeyE);
					
					// create a random symmetric encryption key and save in the peer object
					
					byte[] symmetricEncryptionKey = Utilities.Cryptography.GenerateAes128Key();
					
					this.SenderPeer.SymmetricEncryptionKey = symmetricEncryptionKey;
					
					// save the other info
					
					this.SenderPeer.SecureStream = true;
					
					// reply with a EK-message
					
					IMessage ekMess = Messages.MessagesFactory.Instance.CreateMessage(Commands.EK, Utilities.Converterer.ConvertByteToHex(symmetricEncryptionKey));
					
					this.SenderPeer.Send(ekMess);
				}
				else
				{
					string[] parameters = new string[2];
					
					parameters[0] = "n";
					parameters[1] = string.Empty;
					
					// save the info
					
					this.SenderPeer.SecureStream = false;
					
					// reply with a EI-message
					
					IMessage eiMess = Messages.MessagesFactory.Instance.CreateMessage(Commands.EI, parameters);
					
					this.SenderPeer.Send(eiMess);
				}
			}
			else
			{
				if (Global.AcceptNotEncryptedMessages == false)
				{
					this.SenderPeer.Close();
					
					Lists.PeersList.RemoveAndClosePeer(this.SenderPeer.IP);
				}
			}
		}
		
		#endregion
	}
}
