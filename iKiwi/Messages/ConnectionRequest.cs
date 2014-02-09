using System;
using System.Globalization;

namespace iKiwi.Messages
{
	/// <summary>
	/// Used to manage the connection request messages.
	/// </summary>
	public class ConnectionRequest
	{
		#region Enum
		
		/// <summary>
		/// The type of the connection request message.
		/// </summary>
		public enum TypeOfConnectionRequestMessage
		{
			ConnectionRequest,
			ConnectionAccepted,
			ConnectionRefused,
			InvalidProtocol,
			MessageInvalid
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Creates a connection request message.
		/// </summary>
		/// <param name="RemoteIP">The IP of the peer that will receive this message.</param>
		/// <param name="MessageType">The type of the message to create.</param>
		/// <returns>The connection request message.</returns>
		public static string CreateConnectionRequestMessage(string RemoteIP, TypeOfConnectionRequestMessage MessageType)
		{
			string type = string.Empty;
			
			switch (MessageType)
			{
				case TypeOfConnectionRequestMessage.ConnectionRequest:
					{
						type = "CONNECT";
						break;
					}
				case TypeOfConnectionRequestMessage.ConnectionAccepted:
					{
						type = "CONNECT_OK";
						break;
					}
				case TypeOfConnectionRequestMessage.ConnectionRefused:
					{
						type = "CONNECT_NO";
						break;
					}
				default:
					{
						return null;
					}
			}
			
			string mess =   Global.P2P_Protocol + "\n" +
				type + "\n" +
				"Local-IP: " + Global.MyIP + ":"  +Global.ListeningPort + "\n" +
				"Remote-IP: " + RemoteIP + "\n" +
				"User-Agent: " + Global.UserAgent;

			return mess;
		}
		
		/// <summary>
		/// Checks the type of a connection request message.
		/// </summary>
		/// <param name="ConnectionRequestMessage">The message of the connection request.</param>
		/// <returns>Returns the type of the message.</returns>
		public static TypeOfConnectionRequestMessage CheckConnectionRequest(string ConnectionRequestMessage)
		{
			// split the message
			
			char[] _char = {'\n'};
			
			string[] subConnReqMess = ConnectionRequestMessage.Split(_char, 5);
			
			// control if the message is valid
			if (subConnReqMess.Length == 5)
			{
				// check that the protocol version is not old
				if (subConnReqMess[0].Substring(0, 4) == "Nova" && float.Parse(subConnReqMess[0].Substring(5), CultureInfo.InvariantCulture.NumberFormat) == Global.NovaProtocolVersion)
				{
					switch (subConnReqMess[1])
					{
						case "CONNECT":
							{
								return TypeOfConnectionRequestMessage.ConnectionRequest;
							}
						case "CONNECT_OK":
							{
								return TypeOfConnectionRequestMessage.ConnectionAccepted;
							}
						case "CONNECT_NO":
							{
								return TypeOfConnectionRequestMessage.ConnectionRefused;
							}
						default:
							{
								return TypeOfConnectionRequestMessage.MessageInvalid;
							}
					}
				}
				else
				{
					return TypeOfConnectionRequestMessage.InvalidProtocol;
				}
			}
			else
			{
				return TypeOfConnectionRequestMessage.MessageInvalid;
			}
		}
		
		#endregion
	}
}
