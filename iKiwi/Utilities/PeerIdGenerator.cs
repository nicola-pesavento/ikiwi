using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Description of PeerIdGenerator.
	/// </summary>
	public class PeerIdGenerator
	{
		#region Methods
		
		/// <summary>
		/// Generate a Peer ID.
		/// </summary>
		/// <returns></returns>
		public static string GeneratePeerId()
		{
			// dd/mm/yyyy-hh:mm:ss:nnnnnn-255.255.255.255:165-xxxxxxxxxx ( x = casual number )
			
			StringBuilder peerID = new StringBuilder();
			
			Random rndTrailingDigitsGenerator = new Random();

			peerID.AppendFormat("{0}-", DateTime.UtcNow.ToString("dd/MM/yyyy-HH:mm:ss:FFFFFF"));
			
			peerID.AppendFormat("{0}:{1}-", Global.MyRemoteIP, Global.ListeningPort);

			for (int nDigitIndex = 0;
			     nDigitIndex < 10;
			     nDigitIndex++)
			{
				peerID.Append(rndTrailingDigitsGenerator.Next(0, 10).ToString());
			}

			SHA1 sha1ID = SHA1.Create();
			sha1ID.Initialize();

			return
				(
					BitConverter.ToString
					(
						sha1ID.ComputeHash
						(
							Array.ConvertAll<char, byte>(peerID.ToString().ToArray(), x => (byte)x)
						)
					).Replace("-", "")
				);
		}
		
		#endregion
	}
}
