using System;
using System.Text;

namespace iKiwi.Messages
{
	/// <summary>
	/// Encryption Key message.
	/// </summary>
	class EncryptionKey : AbstractMessage
	{
		#region Data Members
		
		/// <summary>
		/// The symmetric encryption key.
		/// </summary>
		private string m_strSymmetricEncryptionKey;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				return (string.Format("EK {0}",
				                      this.m_strSymmetricEncryptionKey));
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
		/// Create a EK-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public EncryptionKey(string MessageID, int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.EK, MessageID, TTL, Encryption)
		{
			if (Parameters.Length == 1)
			{
				Parameters[0] = Parameters[0].Remove(0, 3).Trim();
				
				this.CreateEncryptionKey(Parameters);
			}
		}

		/// <summary>
		/// Create a EK-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public EncryptionKey(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.EK, string.Empty, TTL, Encryption)
		{
			if (Parameters.Length == 1)
			{
				this.CreateEncryptionKey(Parameters);
			}
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a EK-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		private void CreateEncryptionKey(params string[] Parameters)
		{
			this.m_strSymmetricEncryptionKey = Parameters[0];
		}
		
		#endregion
		
		#region Methods
		
		public override void Process()
		{
			// save the symmetric encryption key
			this.SenderPeer.SymmetricEncryptionKey = Utilities.Converterer.ConvertHexToByte(this.m_strSymmetricEncryptionKey);
			
			if (Global.MessageEncryptionEnabled == true)
			{
				this.SenderPeer.SecureStream = true;
			}
		}
		
		#endregion
	}
}
