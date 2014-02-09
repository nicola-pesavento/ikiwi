using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace iKiwi.Messages
{
	abstract class AbstractMessage : IMessage
	{
		#region Data Members

		// Const Data Members
		private const uint NUM_OF_TRAILING_DIGITS = 10;
		private const uint DEFAULT_TTL = 0;

		// Data Members
		private readonly Commands m_cmdCommandName;
		private readonly string m_ID;
		private int m_nTTL;
		private bool m_bEncrypted;
		private Utilities.EncryptionAlgorithm m_eEncryptionAlgorithmUsed;
		private byte[] m_bytSymmetricEncryptionKey;
		private Utilities.Cryptography.RsaKeys m_AsymmetricEncryptionKey;
		private Objects.Peer m_senderPeer = null;

		#endregion

		#region Properties

		/// <summary>
		/// The type of the message.
		/// </summary>
		public string MessageType
		{
			get
			{
				return (Enum.GetName(typeof(Commands), this.m_cmdCommandName));
			}
		}
		
		/// <summary>
		/// The ID of the message.
		/// </summary>
		public string ID
		{
			get
			{
				return (this.m_ID);
			}
		}
		
		/// <summary>
		/// The TTL of the message.
		/// </summary>
		public int TTL
		{
			get { return this.m_nTTL; }
			set { this.m_nTTL = value; }
		}
		
		/// <summary>
		/// Indicates if the message is encrypted or not.
		/// </summary>
		public bool Encryption
		{
			get { return this.m_bEncrypted; }
			set { this.m_bEncrypted = value; }
		}

		/// <summary>
		/// Get the message.
		/// </summary>
		public string Message
		{
			get
			{
				string parameters = this.Parameters;
				
				string encryption;
				
				if (this.m_bEncrypted == true)
				{
					encryption = "1";
				}
				else
				{
					encryption = "0";
				}
				
				return (string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}",
				                      Enum.GetName(typeof(Commands), this.m_cmdCommandName),
				                      this.m_ID,
				                      this.m_nTTL,
				                      encryption,
				                      (parameters.Length * 2), // UTF16: 1 char = 2 bytes
				                      parameters));
			}
		}

		/// <summary>
		/// Get the message in bytes.
		/// </summary>
		public byte[] MessageByte
		{
			get
			{
				byte[] parameters_byte = this.ParametersByte;
				
				string encryption;
				
				if (this.m_bEncrypted == true)
				{
					encryption = "1";
				}
				else
				{
					encryption = "0";
				}
				
				byte[] firstPart = ASCIIEncoding.Unicode.GetBytes(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n",
				                                                                Enum.GetName(typeof(Commands), this.m_cmdCommandName),
				                                                                this.m_ID,
				                                                                this.m_nTTL,
				                                                                encryption,
				                                                                parameters_byte.Length
				                                                               ));

				byte[] message = new byte[firstPart.Length + parameters_byte.Length];
				
				Buffer.BlockCopy(firstPart, 0, message, 0, firstPart.Length);

				Buffer.BlockCopy(parameters_byte, 0, message, firstPart.Length, parameters_byte.Length);

				return (message);
			}
		}
		
		/// <summary>
		/// Get the encrypted message.
		/// </summary>
		public string EncryptedMessage
		{
			get
			{
				string encryptedParameters = this.EncryptedParameters;
				
				string encryption = "1";
				
				return (string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}",
				                      Enum.GetName(typeof(Commands), this.m_cmdCommandName),
				                      this.m_ID,
				                      this.m_nTTL,
				                      encryption,
				                      (encryptedParameters.Length * 2), // UTF16: 1 char = 2 bytes
				                      encryptedParameters));
				
			}
		}
		
		/// <summary>
		/// Get the encrypted message.
		/// </summary>
		public byte[] EncryptedMessageByte
		{
			get
			{
				byte[] encryptedParameters_byte = this.EncryptedParametersByte;
				
				string encryption = "1";
				
				byte[] firstPart = ASCIIEncoding.Unicode.GetBytes(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n",
				                                                                Enum.GetName(typeof(Commands), this.m_cmdCommandName),
				                                                                this.m_ID,
				                                                                this.m_nTTL,
				                                                                encryption,
				                                                                encryptedParameters_byte.Length
				                                                               ));

				byte[] message = new byte[firstPart.Length + encryptedParameters_byte.Length];

				Buffer.BlockCopy(firstPart, 0, message, 0, firstPart.Length);

				Buffer.BlockCopy(encryptedParameters_byte, 0, message, firstPart.Length, encryptedParameters_byte.Length);

				return (message);
			}
		}
		
		/// <summary>
		/// The encryption algorithm used for encrypt the message.
		/// </summary>
		public Utilities.EncryptionAlgorithm EncryptionAlgorithmUsed
		{
			get
			{
				return this.m_eEncryptionAlgorithmUsed;
			}
			set
			{
				this.m_eEncryptionAlgorithmUsed = value;
			}
		}
		
		/// <summary>
		/// The symmetric encryption key used to encrypt the message.
		/// </summary>
		public byte[] SymmetricEncryptionKey
		{
			get
			{
				return this.m_bytSymmetricEncryptionKey;
			}
			set
			{
				this.m_bytSymmetricEncryptionKey = value;
			}
		}
		
		/// <summary>
		/// The asymmetric encryption key used to encrypt the message.
		/// </summary>
		public Utilities.Cryptography.RsaKeys AsymmetricEncryptionKey
		{
			get
			{
				return this.m_AsymmetricEncryptionKey;
			}
			set
			{
				this.m_AsymmetricEncryptionKey = value;
			}
		}
		
		/// <summary>
		/// The sender peer object.
		/// </summary>
		public Objects.Peer SenderPeer
		{
			get { return m_senderPeer; }
			set {m_senderPeer = value; }
		}

		/// <summary>
		/// Parameters of the message.
		/// </summary>
		public abstract string Parameters
		{
			get;
		}

		/// <summary>
		/// Parameters in bytes of the message.
		/// </summary>
		public abstract byte[] ParametersByte
		{
			get;
		}
		
		/// <summary>
		/// Returns the parameters encrypted using the encryption algorithm selected and the encryption key passed.
		/// </summary>
		public string EncryptedParameters
		{
			get
			{
				return ASCIIEncoding.Unicode.GetString(this.EncryptedParametersByte);
			}
		}
		
		/// <summary>
		/// Returns the parameters encrypted using the encryption algorithm selected and the encryption key passed.
		/// </summary>
		public byte[] EncryptedParametersByte
		{
			get
			{
				if (this.EncryptionAlgorithmUsed == Utilities.EncryptionAlgorithm.AES_128)
				{
					return Utilities.Cryptography.AesEncryptByteToByte(this.ParametersByte, this.SymmetricEncryptionKey);
				}
				else if (this.EncryptionAlgorithmUsed == Utilities.EncryptionAlgorithm.RSA)
				{
					return Utilities.Cryptography.RsaEncryptByteToByte(this.ParametersByte, this.AsymmetricEncryptionKey);
				}
				else
				{
					return null;
				}
			}
		}
		
		#endregion

		#region Ctor

		/// <summary>
		/// Generic message.
		/// </summary>
		/// <param name="CommandName">The command name.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encrypted">Indicates if the message is encrypted or not.</param>
		/// <param name="SenderPeer">The sender peer.</param>
		public AbstractMessage(Commands CommandName, int TTL, bool Encrypted,  Objects.Peer SenderPeer = null)
			: this(CommandName, string.Empty, TTL, Encrypted, SenderPeer)
		{
		}

		/// <summary>
		/// Generic message.
		/// </summary>
		/// <param name="CommandName">The command name.</param>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="SenderPeer">The sender peer.</param>
		public AbstractMessage(Commands CommandName, String MessageID, int TTL, bool Encryption, Objects.Peer SenderPeer = null)
		{
			this.m_cmdCommandName = CommandName;
			this.m_nTTL = TTL;
			this.m_bEncrypted = Encryption;
			this.m_senderPeer = SenderPeer;

			if (MessageID == String.Empty)
			{
				this.m_ID = this.GenerateID();
			}
			else
			{
				this.m_ID = MessageID;
			}
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Generate the ID of the message.
		/// </summary>
		/// <returns>A random message ID.</returns>
		private string GenerateID()
		{
			if (Lists.MessageIDsList.MessageCounter < uint.MaxValue)
			{
				Lists.MessageIDsList.MessageCounter++;
			}
			else
			{
				Lists.MessageIDsList.MessageCounter = 0;
			}
			
			return (Global.MyRemoteIP + "_" + Lists.MessageIDsList.MessageCounter.ToString());
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Process the message.
		/// </summary>
		public abstract void Process();

		#endregion
	}
}
