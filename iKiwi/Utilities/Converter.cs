using System;
using System.Collections;
using System.Globalization;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Used to convert.
	/// </summary>
	public class Converterer
	{
		#region Methods
		
		/// <summary>
		/// Converts a Bit array to a Byte array.
		/// </summary>
		/// <param name="bitArray">The Bit array.</param>
		/// <returns>The Byte array.</returns>
		public static byte[] ConvertBitToByte(BitArray bitArray)
		{
			int n = bitArray.Count / 8;
			
			if ((bitArray.Count % 8) != 0)
			{
				n++;
			}
			
			byte[] byteArray = new byte[n];
			
			bitArray.CopyTo(byteArray, 0);
			
			return byteArray;
		}
		
		/// <summary>
		/// Convert a Hex string to a Bit array.
		/// </summary>
		/// <param name="HexString">The Hex string.</param>
		/// <returns>The Bool array.</returns>
		public static BitArray ConvertHexToBit(string HexString)
		{
			if (HexString == null)
			{
				return null;
			}

			BitArray ba = new BitArray(4 * HexString.Length);
			
			int baMaxIndex = ba.Length - 1;
			
			for (int i = 0; i < HexString.Length; i++)
			{
				byte b = byte.Parse(HexString[i].ToString(), NumberStyles.HexNumber);
				
				for (int j = 0; j < 4; j++)
				{
					ba.Set(baMaxIndex - (i * 4 + j), (b & (1 << (3 - j))) != 0);
				}
			}
			
			return ba;
		}
		
		/// <summary>
		/// Converts a Bit array to a Hex string.
		/// </summary>
		/// <param name="bitArray">The Bit array.</param>
		/// <returns>The Hex string.</returns>
		public static string ConvertBitToHex(BitArray bitArray)
		{
			byte[] bt = Utilities.Converterer.ConvertBitToByte(bitArray);
			
			string t = string.Empty;
			
			for (int i = bt.Length - 1; i >= 0; i--)
			{
				t += bt[i].ToString("X2");
			}
			
			return t;
		}
		
		/// <summary>
		/// Converts a hex string to a byte array.
		/// </summary>
		/// <param name="Hex">The hex string.</param>
		/// <returns>The hex string converted to byte array.</returns>
		public static byte[] ConvertHexToByte(string Hex)
		{
			if (Hex.Length >= 3 && Hex[2] == '-')
			{
				Hex = Hex.Replace("-", "");
			}
			
			int NumberChars = Hex.Length;
			
			byte[] bytes = new byte[NumberChars / 2];
			
			for (int i = 0; i < NumberChars; i += 2)
			{
				bytes[i / 2] = Convert.ToByte(Hex.Substring(i, 2), 16);
			}
			
			return bytes;
		}
		
		/// <summary>
		/// Converts a byte array to a hex string.
		/// </summary>
		/// <param name="ByteArray">The byte array.</param>
		/// <returns>The byte array converted to hex string.</returns>
		public static string ConvertByteToHex(byte[] ByteArray)
		{
			return BitConverter.ToString(ByteArray).Replace("-", "");
		}
		
		/// <summary>
		/// Automatically converts a size from byte to kilobyte, megabyte or gigabyte.
		/// </summary>
		/// <param name="SizeByte">The size in byte.</param>
		/// <returns>The converted size.</returns>
		public static string AutoConvertSizeFromByte(long SizeByte)
		{
			if (SizeByte >= 1073741824)
			{
				return (SizeByte / 1073741824f).ToString("F") + " GB";
			}
			else
			{
				if (SizeByte >= 1048576)
				{
					return (SizeByte / 1048576f).ToString("F") + " MB";
				}
				else
				{
					return (SizeByte / 1024f).ToString("F") + " kB";
				}
			}
		}
		
		#endregion
	}
}
