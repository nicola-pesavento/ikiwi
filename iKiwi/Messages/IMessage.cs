using System;

namespace iKiwi.Messages
{
	/// <summary>
	/// Message interface.
	/// </summary>
	public interface IMessage
	{
		#region Properties
		
		/// <summary>
		/// The message type.
		/// </summary>
		string MessageType
		{
			get;
		}
		
		/// <summary>
		/// The message ID.
		/// </summary>
		string ID
		{
			get;
		}
		
		/// <summary>
		/// The time to live.
		/// </summary>
		int TTL
		{
			get;
			set;
		}
		
		/// <summary>
		/// Indicates if the message is encrypted or not.
		/// </summary>
		bool Encryption
		{
			get;
			set;
		}
		
		/// <summary>
		/// Get the message.
		/// </summary>
		string Message
		{
			get;
		}

		/// <summary>
		/// Get the message.
		/// </summary>
		byte[] MessageByte
		{
			get;
		}
		
		/// <summary>
		/// Get the encrypted message.
		/// </summary>
		string EncryptedMessage
		{
			get;
		}
		
		/// <summary>
		/// Get the encrypted message.
		/// </summary>
		byte[] EncryptedMessageByte
		{
			get;
		}
		
		/// <summary>
		/// The encryption algorithm used for encrypt the message.
		/// </summary>
		Utilities.EncryptionAlgorithm EncryptionAlgorithmUsed
		{
			get;
			set;
		}
		
		/// <summary>
		/// The symmetric encryption key used to encrypt the message.
		/// </summary>
		byte[] SymmetricEncryptionKey
		{
			get;
			set;
		}
		
		/// <summary>
		/// The asymmetric encryption key used to encrypt the message.
		/// </summary>
		Utilities.Cryptography.RsaKeys AsymmetricEncryptionKey
		{
			get;
			set;
		}
		
		/// <summary>
		/// The sender peer.
		/// </summary>
		Objects.Peer SenderPeer
		{
			get;
			set;
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Process the message.
		/// </summary>
		void Process();

		#endregion
	}
}
