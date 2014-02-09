using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iKiwi.Messages
{
	/// <summary>
	/// The command of the message.
	/// </summary>
	public enum Commands
	{
		/// <summary>
		/// File Search.
		/// </summary>
		FS,
		/// <summary>
		/// File Found.
		/// </summary>
		FF,
		/// <summary>
		/// File Pack Request.
		/// </summary>
		FPR,
		/// <summary>
		/// File Pack.
		/// </summary>
		FP,
		/// <summary>
		/// Ping.
		/// </summary>
		PI,
		/// <summary>
		/// Pong.
		/// </summary>
		PO,
		/// <summary>
		/// Xml List Request.
		/// </summary>
		XLR,
		/// <summary>
		/// Xml List.
		/// </summary>
		XL,
		/// <summary>
		/// Encryption Info.
		/// </summary>
		EI,
		/// <summary>
		/// Encryption Key.
		/// </summary>
		EK
	}

	/// <summary>
	/// MessageFactory class.
	/// </summary>
	public sealed class MessagesFactory
	{
		#region Data Members

		// Static Members
		private static MessagesFactory m_mfInstance;
		
		/// <summary>
		/// The return char in UTF-16 format.
		/// </summary>
		private static readonly byte[] RETURN_CHAR_UTF16 = ASCIIEncoding.Unicode.GetBytes("\n");

		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public static MessagesFactory Instance
		{
			get
			{
				return (MessagesFactory.m_mfInstance);
			}
		}

		#endregion

		#region Ctor

		static MessagesFactory()
		{
			MessagesFactory.m_mfInstance = new MessagesFactory();
		}

		private MessagesFactory()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// Creates a message object from the input data.
		/// </summary>
		/// <param name="Message">The received message.</param>
		/// <param name="SenderPeer">IP:Port of the sender.</param>
		/// <returns>Returns the message objetc.</returns>
		public Messages.IMessage ParseMessage(byte[] Message, Objects.Peer SenderPeer)
		{
			// message parts
			
			Messages.Commands messageCommand;
			string messageID = string.Empty;
			int messageTTL = 0;
			bool messageEncryption = false;
			int messageParametersLength = 0;
			byte[] messageParameters;
			
			// split byte array
			
			int start = 0;
			int count = 0;
			
			byte[] buffer = null;
			
			while (true)
			{
				try
				{
					// get the command
					
					if (Message[start + 4] == RETURN_CHAR_UTF16[0] && Message[start + 5] == RETURN_CHAR_UTF16[1])
					{
						count = 4;
					}
					else if (Message[start + 6] == RETURN_CHAR_UTF16[0] && Message[start + 7] == RETURN_CHAR_UTF16[1])
					{
						count = 6;
					}
					else
					{
						// the message is invalid
						Utilities.Log.Write("Invalid input message from " + SenderPeer.IP, Utilities.Log.LogCategory.InputMessages);
						
						return null;
					}
					
					buffer = new byte[count];
					
					Buffer.BlockCopy(Message, start, buffer, 0, count);
					
					if (Enum.TryParse<Messages.Commands>(ASCIIEncoding.Unicode.GetString(buffer), out messageCommand))
					{
						// get the other message parts
						
						bool b = false;

						for (int a = 0; a <= 3; a++)
						{
							start += count + 2;

							b = false;

							for (int i = start; i <= Message.Length - 1; i += 2)
							{
								if (Message[i] == RETURN_CHAR_UTF16[0] && Message[i + 1] == RETURN_CHAR_UTF16[1])
								{
									count = i - start;

									b = true;

									break;
								}
							}

							if (b == true)
							{
								buffer = new Byte[count];

								Buffer.BlockCopy(Message, start, buffer, 0, count);
								
								switch (a)
								{
									case 0:
										{
											messageID = ASCIIEncoding.Unicode.GetString(buffer);
											break;
										}
									case 1:
										{
											messageTTL = int.Parse(ASCIIEncoding.Unicode.GetString(buffer));
											break;
										}
									case 2:
										{
											if (ASCIIEncoding.Unicode.GetString(buffer) == "0")
											{
												messageEncryption = false;
											}
											else
											{
												messageEncryption = true;
											}
											break;
										}
									case 3:
										{
											messageParametersLength = int.Parse(ASCIIEncoding.Unicode.GetString(buffer));
											break;
										}
								}
							}
							else
							{
								// return the incompleted message
								break;
							}
						}
						
						// get the parameters of the message
						
						start += count + 2;
						
						count = messageParametersLength;

						// control if the message is complete
						if (count <= Message.Length - start)
						{
							messageParameters = new byte[count];

							Buffer.BlockCopy(Message, start, messageParameters, 0, count);
							
							// process the single message

							#region get parameters and create the message object
							
							// control if the message is encrypted
							if (messageEncryption == true)
							{
								// decrypt the message
								if (messageCommand == Commands.EK)
								{
									messageParameters = Utilities.Cryptography.RsaDecryptByteToByte(messageParameters, SenderPeer.MyAsymmetricEncryptionKey);
								}
								else
								{
									messageParameters = Utilities.Cryptography.AesDecryptByteToByte(messageParameters, SenderPeer.SymmetricEncryptionKey);
								}
							}
							
							string[] strParameters;
							
							IMessage message;
							
							if (messageCommand == Commands.FP || messageCommand == Commands.XL)
							{
								byte[] binaryPart;
								
								if (messageCommand == Commands.FP)
								{
									strParameters = new string[4];
									
									// split the byte array
									
									int parStart = -2;
									
									int parCount = 0;
									
									byte[] parBuffer = null;
									
									bool _b = false;
									
									for (int a = 0; a <= 3; a++)
									{
										parStart = parStart + parCount + 2;
										
										_b = false;

										for (int i = parStart; i <= messageParameters.Length - parStart; i += 2)
										{
											if (messageParameters[i] == RETURN_CHAR_UTF16[0] && messageParameters[i + 1] == RETURN_CHAR_UTF16[1])
											{
												parCount = i - parStart;
												
												_b = true;
												
												break;
											}
										}
										
										if (_b == true)
										{
											parBuffer = new Byte[parCount];

											Buffer.BlockCopy(messageParameters, parStart, parBuffer, 0, parCount);
											
											strParameters[a] = ASCIIEncoding.Unicode.GetString(parBuffer);
										}
									}
									
									parStart = parStart + parCount + 8;
									
									//parCount = Parameters.Length - start;
									
									binaryPart = new byte[16384];

									Buffer.BlockCopy(messageParameters, parStart, binaryPart, 0, binaryPart.Length);
								}
								else
								{
									strParameters = new string[0];
									
									binaryPart = new Byte[messageParameters.Length - 6];
									
									Buffer.BlockCopy(messageParameters, 6, binaryPart, 0, binaryPart.Length);
								}
								
								message = CreateMessage(messageCommand, messageID, messageTTL, messageEncryption, strParameters, binaryPart);
							}
							else
							{
								strParameters = ASCIIEncoding.Unicode.GetString(messageParameters).Split('\n');
								
								strParameters[strParameters.Length - 1] = strParameters[strParameters.Length - 1].TrimEnd('\0');
								
								message = MessagesFactory.Instance.CreateMessage(messageCommand, messageID, messageTTL, messageEncryption, strParameters);
							}
							
							message.SenderPeer = SenderPeer;
							
							return message;
							
							#endregion
							
							// check if there are other messages to be processed
						}
						else
						{
							// return the incompleted message
							return null;
						}
					}
					else
					{
						// the message command is invalid
						Utilities.Log.Write("Invalid input message from " + SenderPeer.IP, Utilities.Log.LogCategory.InputMessages);
						
						return null;
					}
				}
				catch(Exception ex)
				{
					Utilities.Log.Write("Error to process a message from " + SenderPeer.IP + ": " + ex.Message, Utilities.Log.LogCategory.Error);
					
					return null;
				}
			}
		}

		/// <summary>
		/// Creates a message object with TTL = 0, auto-generated ID and Encryption = false.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <returns>Returns the message objetc.</returns>
		public IMessage CreateMessage(Commands Command, params string[] Parameters)
		{
			return (this.CreateMessage(Command, 0, Parameters));
		}

		/// <summary>
		/// Creates a message object with TTL = 0, auto-generated ID and Encryption = false.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="BinaryPart">The binary part.</param>
		/// <returns>Returns the message objetc.</returns>
		public IMessage CreateMessage(Commands Command, byte[] BinaryPart)
		{
			return (this.CreateMessage(Command, new string[0], BinaryPart));
		}

		/// <summary>
		/// Creates a message object with TTL = 0, auto-generated ID and Encryption = false.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <param name="BinaryPart">The binary part.</param>
		/// <returns>Returns the message objetc.</returns>
		public IMessage CreateMessage(Commands Command, string[] Parameters, byte[] BinaryPart)
		{
			return (this.CreateMessage(Command, 0, Parameters, BinaryPart));
		}

		/// <summary>
		/// Creates a message object with auto-generated ID and Encryption = false.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <returns>The message object.</returns>
		public IMessage CreateMessage(Commands Command, int TTL, params string[] Parameters)
		{
			return (this.CreateMessage(Command, string.Empty, TTL, false, Parameters, new byte[0]));
		}
		
		/// <summary>
		/// Creates a message object with TTL = 0 and auto-generated ID.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <returns>Returns the message objetc.</returns>
		public IMessage CreateMessage(Commands Command, bool Encryption, params string[] Parameters)
		{
			return (this.CreateMessage(Command, string.Empty, 0, Encryption, Parameters, new byte[0]));
		}

		/// <summary>
		/// Creates a message object with auto-generated ID and Encryption = false.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <param name="BinaryPart">The binary part.</param>
		/// <returns>The message object.</returns>
		public IMessage CreateMessage(Commands Command, int TTL, string[] Parameters, byte[] BinaryPart)
		{
			return (this.CreateMessage(Command, string.Empty, TTL, false, Parameters, BinaryPart));
		}
		
		/// <summary>
		/// Creates a message object.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <returns>The message object.</returns>
		public IMessage CreateMessage(Commands Command, string MessageID, int TTL, bool Encryption, string[] Parameters)
		{
			return (this.CreateMessage(Command, MessageID, TTL, Encryption, Parameters, new byte[0]));
		}

		/// <summary>
		/// Creates a message object.
		/// </summary>
		/// <param name="Command">The message command.</param>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters.</param>
		/// <param name="BinaryPart">The binary part.</param>
		/// <returns>The message object.</returns>
		public IMessage CreateMessage(Commands Command, string MessageID, int TTL, bool Encryption, string[] Parameters, byte[] BinaryPart)
		{
			IMessage imsgReturn = null;
			
			#region switch (Command)
			
			switch (Command)
			{
				case Commands.FS:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new FileSearch(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new FileSearch(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.FF:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new FileFound(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new FileFound(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.FPR:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new FilePackRequest(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new FilePackRequest(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.FP:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new FilePack(TTL, Encryption, Parameters, BinaryPart);
						}
						else
						{
							imsgReturn = new FilePack(MessageID, TTL, Encryption, Parameters, BinaryPart);
						}
						break;
					}
				case Commands.PI:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new Ping(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new Ping(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.PO:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new Pong(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new Pong(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.XLR:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new XmlListRequest(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new XmlListRequest(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.XL:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new XmlList(TTL, Encryption, Parameters, BinaryPart);
						}
						else
						{
							imsgReturn = new XmlList(MessageID, TTL, Encryption, Parameters, BinaryPart);
						}
						break;
					}
				case Commands.EI:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new EncryptionInfo(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new EncryptionInfo(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				case Commands.EK:
					{
						if (MessageID == string.Empty)
						{
							imsgReturn = new EncryptionKey(TTL, Encryption, Parameters);
						}
						else
						{
							imsgReturn = new EncryptionKey(MessageID, TTL, Encryption, Parameters);
						}
						break;
					}
				default:
					{
						break;
					}
			}
			
			#endregion
			
			return imsgReturn;
		}

		#endregion
	}
}
