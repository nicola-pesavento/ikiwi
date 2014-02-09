using System;

namespace iKiwi.Messages
{
	class Null : IMessage
	{
		#region Properties
		
		public string MessageType
		{
			get
			{
				return string.Empty;
			}
		}
		
		public string ID
		{
			get
			{
				return string.Empty;
			}
		}
		
		public int TTL
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
		
		public bool Encryption
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
		
		public string Message
		{
			get
			{
				return string.Empty;
			}
		}

		public byte[] MessageByte
		{
			get
			{
				return new byte[0];
			}
		}

        public string EncryptedMessage
        {
        	get
        	{
        		return string.Empty;
        	}
        		
        }
        
        public byte[] EncryptedMessageByte
        {
        	get
        	{
        		return new byte[0];
        	}
        }
        
        /// <summary>
		/// The encryption algorithm used for encrypt the message.
		/// </summary>
		public Utilities.EncryptionAlgorithm EncryptionAlgorithmUsed
		{
			get
			{
				return Utilities.EncryptionAlgorithm.AES_128;
			}
			set
			{
			}
		}
		
		/// <summary>
		/// The symmetric encryption key used to encrypt the message.
		/// </summary>
		public byte[] SymmetricEncryptionKey
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
		
		/// <summary>
		/// The asymmetric encryption key used to encrypt the message.
		/// </summary>
		public Utilities.Cryptography.RsaKeys AsymmetricEncryptionKey
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Objects.Peer SenderPeer
		{
			get
			{
				return null;
			}
			set
			{
			}
		}
		
		#endregion
		
		#region Methods
		
		public void Process()
		{
		}
		
		#endregion
	}
}
