using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace iKiwi.Utilities
{
	/// <summary>
	/// The list of the encryption algorithms used to encrypt the messages.
	/// </summary>
	public enum EncryptionAlgorithm
	{
		/// <summary>
		/// Advanced Encryption Standard 128.
		/// </summary>
		AES_128,
		/// <summary>
		///  Rivest Shamir Adleman.
		/// </summary>
		RSA
	}
	
	/// <summary>
	/// Used to encrypt and decrypt.
	/// </summary>
	public class Cryptography
	{
		#region Data Members
		
		/// <summary>
		/// The parameter P in the DSA algorithm.
		/// </summary>
		private static readonly BigInteger DSA_P_PARAM = new BigInteger(Utilities.Converterer.ConvertHexToByte("A3-43-6C-B9-2B-D7-7D-52-ED-D4-26-2A-FC-12-C6-F9-B6-7D-18-09-6C-56-09-38-7D-4C-00-D8-2A-F2-D2-E5-31-FD-75-A5-44-5A-7D-33-38-F0-0E-78-72-73-9B-2A-D4-B3-5C-AF-2D-BB-95-94-8C-7C-3C-A8-81-1A-0B-62-AC-B4-70-7B-F0-FF-42-80-78-D1-3B-A5-EE-BF-60-BB-FD-9D-0D-4A-7A-2C-13-A1-BF-17-E3-EC-80-37-05-A8-50-3B-88-AF-3F-F9-E7-18-34-D1-4F-C0-57-5E-4D-47-1D-FB-98-CF-0B-B5-3C-79-DD-BE-9A-63-92-8D-67-E1"));
		
		/// <summary>
		/// The parameter Q in the DSA algorithm.
		/// </summary>
		private static readonly int DSA_Q_PARAM = BitConverter.ToInt32(Utilities.Converterer.ConvertHexToByte("A8-2F-1A-00-DA-20-37-90-6C-9C-73-47-F0-E2-65-5A-9C-63-FF-97"), 0);
		
		/// <summary>
		/// The parameter G in the DSA algorithm.
		/// </summary>
		private static readonly BigInteger DSA_G_PARAM = new BigInteger(Utilities.Converterer.ConvertHexToByte("13-52-E3-5C-45-B2-E1-2B-56-40-3C-AA-38-57-B6-CB-70-C9-06-FE-F0-F2-2B-10-9D-6B-17-0C-B9-CE-DE-13-AF-86-7C-38-24-82-47-F9-16-92-B5-EA-08-CF-A7-ED-80-5D-87-5A-8C-08-67-D7-C2-08-91-88-87-DB-EC-04-E7-AC-75-57-EC-12-30-25-C2-8E-71-85-95-7A-76-29-9C-76-70-69-97-58-4E-5C-22-D0-4E-D1-6D-AE-7A-8F-10-48-90-0C-59-E6-68-91-CA-F8-23-14-A7-F9-76-80-4E-EF-D6-CE-17-37-0F-A3-89-28-34-B6-A6-0D-11-0D"));
		
		#endregion
		
		#region Classes
		
		/// <summary>
		/// The keys of the RSA asymmetric encryption algorithm.
		/// </summary>
		public class RsaKeys
		{
			#region Properties
			
			/// <summary>
			/// The modulus N.
			/// </summary>
			public byte[] N
			{ get; set; }
			
			/// <summary>
			/// The public exponent E.
			/// </summary>
			public byte[] E
			{ get; set; }
			
			/// <summary>
			/// The private exponent D.
			/// </summary>
			public byte[] D
			{ get; set; }
			
			/// <summary>
			/// The prime number P.
			/// </summary>
			public byte[] P
			{ get; set; }
			
			/// <summary>
			/// The prime number Q.
			/// </summary>
			public byte[] Q
			{ get; set; }
			
			/// <summary>
			/// D (mod P − 1).
			/// </summary>
			public byte[] DP
			{ get; set; }
			
			/// <summary>
			/// D (mod Q − 1).
			/// </summary>
			public byte[] DQ
			{ get; set; }
			
			/// <summary>
			/// Q^(− 1) (mod p).
			/// </summary>
			public byte[] InverseQ
			{ get; set; }
			
			#endregion
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Encrypts a byte array using the AES algorithm.
		/// </summary>
		/// <param name="Byte">The byte array to be be encrypted.</param>
		/// <param name="Key">The key used to encrypt.</param>
		/// <returns>The encrypted byte.</returns>
		public static byte[] AesEncryptByteToByte(byte[] Byte, byte[] Key)
		{
			if (Byte == null)
			{
				throw new ArgumentNullException("Byte");
			}
			if (Key == null || Key.Length == 0)
			{
				throw new ArgumentNullException("Key");
			}
			
			byte[] iv = new byte[Key.Length];
			
			Aes myAes = Aes.Create();
			
			ICryptoTransform encryptor = myAes.CreateEncryptor(Key, iv);
			
			MemoryStream memoryStream = new MemoryStream();
			
			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			
			// encrypting
			cryptoStream.Write(Byte, 0, Byte.Length);
			
			cryptoStream.FlushFinalBlock();

			byte[] encryptedByte = memoryStream.ToArray();
			
			// close the streams
			memoryStream.Close();
			cryptoStream.Close();
			
			return encryptedByte;
		}
		
		/// <summary>
		/// Decrypts a byte array using the AES algorithm.
		/// </summary>
		/// <param name="Byte">The byte array to be be decrypted.</param>
		/// <param name="Key">The key used to decrypt.</param>
		/// <returns>The decrypted byte.</returns>
		public static byte[] AesDecryptByteToByte(byte[] Byte, byte[] Key)
		{
			if (Byte == null)
			{
				throw new ArgumentNullException("Byte");
			}
			if (Key == null || Key.Length == 0)
			{
				throw new ArgumentNullException("Key");
			}
			
			byte[] iv = new byte[Key.Length];
			
			Aes myAes = Aes.Create();
			
			ICryptoTransform decryptor = myAes.CreateDecryptor(Key, iv);
			
			MemoryStream  memoryStream = new MemoryStream(Byte);
			
			CryptoStream  cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			
			byte[] decryptedByte = new byte[Byte.Length];
			
			cryptoStream.Read(decryptedByte, 0, decryptedByte.Length);
			
			// close the streams
			memoryStream.Close();
			cryptoStream.Close();
			
			return decryptedByte;
		}
		
		/// <summary>
		/// Generates a random encryption key for the AES_128 algorithm.
		/// </summary>
		/// <returns>A random encrytion key.</returns>
		public static byte[] GenerateAes128Key()
		{
			Aes myAes = Aes.Create();
			
			myAes.KeySize = 128;
			
			myAes.GenerateKey();
			
			return  myAes.Key;
		}
		
		/// <summary>
		/// Encrypts a byte array using the RSA algorithm.
		/// </summary>
		/// <param name="Byte">The byte array to be be encrypted.</param>
		/// <param name="Key">The key used to encrypt.</param>
		/// <returns>The encrypted byte.</returns>
		public static byte[] RsaEncryptByteToByte(byte[] Byte, RsaKeys Key)
		{
			RSACryptoServiceProvider myRsa = new RSACryptoServiceProvider();
			
			RSAParameters rsaParams = new RSAParameters();
			
			rsaParams.Modulus = Key.N;
			
			rsaParams.Exponent = Key.E;
			
			myRsa.ImportParameters(rsaParams);
			
			return myRsa.Encrypt(Byte, true);
		}
		
		/// <summary>
		/// Decrypts a byte array using the Rsa algorithm.
		/// </summary>
		/// <param name="Byte">The byte array to be be decrypted.</param>
		/// <param name="Key">The key used to decrypt.</param>
		/// <returns>The decrypted byte.</returns>
		public static byte[] RsaDecryptByteToByte(byte[] Byte, RsaKeys Key)
		{
			RSACryptoServiceProvider myRsa = new RSACryptoServiceProvider();
			
			RSAParameters rsaParams = new RSAParameters();
			
			rsaParams.Modulus = Key.N;
			
			rsaParams.Exponent = Key.E;
			
			rsaParams.D = Key.D;
			
			rsaParams.P = Key.P;
			
			rsaParams.Q = Key.Q;
			
			rsaParams.DP = Key.DP;
			
			rsaParams.DQ = Key.DQ;
			
			rsaParams.InverseQ = Key.InverseQ;
			
			myRsa.ImportParameters(rsaParams);
			
			return myRsa.Decrypt(Byte, true);
		}
		
		/// <summary>
		/// Generates a random encryption key for the Rsa algorithm.
		/// </summary>
		/// <returns>The random encrytion keys.</returns>
		public static RsaKeys GenerateRsaKey()
		{
			RSACryptoServiceProvider myRsa = new RSACryptoServiceProvider(2048);
			
			RSAParameters rsaParams = myRsa.ExportParameters(true);
			
			RsaKeys key = new RsaKeys();
			
			key.N = rsaParams.Modulus;
			
			key.E = rsaParams.Exponent;
			
			key.D = rsaParams.D;
			
			key.P = rsaParams.P;
			
			key.Q = rsaParams.Q;
			
			key.DP = rsaParams.DP;
			
			key.DQ = rsaParams.DQ;
			
			key.InverseQ = rsaParams.InverseQ;
			
			return key;
		}
		
		#endregion
	}
}