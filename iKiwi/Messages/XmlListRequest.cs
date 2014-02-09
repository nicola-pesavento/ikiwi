using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace iKiwi.Messages
{
	class XmlListRequest : AbstractMessage
	{
		#region Properties

		public override string Parameters
		{
			get
			{
				return (string.Empty);
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				return (new byte[0]);
			}
		}

		#endregion
		
		#region Ctor

		/// <summary>
		/// Create a XLR-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public XmlListRequest(string MessageID, int TTL, bool Encryption, string[] Parameters)
			: base(Commands.XLR, MessageID, TTL, Encryption)
		{
		}

		/// <summary>
		/// Create a XLR-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public XmlListRequest(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.XLR, string.Empty, TTL, Encryption)
		{
		}

		#endregion

		#region Methods

		public override void Process()
		{
			try
			{
				FileStream fs = File.OpenRead(Global.iKiwiPath + "List.xml");

				byte[] buffer = new byte[fs.Length];

				while (true)
				{
					try
					{
						fs.Lock(0, fs.Length);

						fs.Read(buffer, 0, (int)fs.Length);

						fs.Unlock(0, fs.Length);

						fs.Close();

						break;
					}
					catch (IOException)
					{
						Thread.Sleep(1);
					}
				}
				
				// build the message
				IMessage iXLmessage = MessagesFactory.Instance.CreateMessage(Commands.XL, buffer);

				// send the message
				this.SenderPeer.Send(iXLmessage);
			}
			catch(Exception ex)
			{
				Utilities.Log.Write("Error to open List.xml: " + ex.Message, Utilities.Log.LogCategory.Error);
				return;
			}
		}

		#endregion
	}
}
