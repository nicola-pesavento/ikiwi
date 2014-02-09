using System;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Used to generate keys.
	/// </summary>
	public class GetKey
	{
		/// <summary>
		/// Generates a key from a IP address with port.
		/// </summary>
		/// <param name="IpPort">The Ip address with port.</param>
		/// <returns>The key in int64 format.</returns>
		public static long IpPort(string IpPort)
		{
			string[] subIpPort = IpPort.Split('.', ':');
			
			byte[] b = new byte[8];
			
			b[0] = byte.Parse(subIpPort[0]);
			b[1] = byte.Parse(subIpPort[1]);
			b[2] = byte.Parse(subIpPort[2]);
			b[3] = byte.Parse(subIpPort[3]);
			
			byte[] subPort = BitConverter.GetBytes(int.Parse(subIpPort[4]));
			
			Array.Copy(subPort, 0, b, 4, subPort.Length);
			
			return BitConverter.ToInt64(b, 0);
		}
	}
}
