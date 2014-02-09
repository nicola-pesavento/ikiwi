using System;
using System.IO;
using System.Security.Cryptography;

namespace iKiwi.Objects
{
	/// <summary>
	/// Shared file object.
	/// </summary>
	public class SharedFile
	{
		#region Properties

        /// <summary>
        /// The name of the file.
        /// </summary>
		public string Name
		{ get; set; }

        /// <summary>
        /// The size of the file.
        /// </summary>
		public long Size
		{ get; set; }

        /// <summary>
        /// The path of the file.
        /// </summary>
		public string Path
		{ get; set; }

        /// <summary>
        /// The sha1 hash value of the file.
        /// </summary>
		public string SHA1
		{ get; set; }
		
		/// <summary>
		/// The modified date of the file.
		/// </summary>
		public DateTime ModifiedDate
		{ get; set; }

		#endregion

		#region Ctor

		/// <summary>
		/// Shared file.
		/// </summary>
		/// <param name="Name">The name of the file.</param>
		/// <param name="Size">The size of the file.</param>
		/// <param name="Path">The path of the file.</param>
		/// <param name="SHA1">The sha1 hash value of the file.</param>
		/// <param name="ModifiedDate">The modified date of the file.</param>
		public SharedFile(string Name, long Size, string Path, string SHA1, DateTime ModifiedDate)
		{
			this.Name = Name;
			this.Size = Size;
			this.Path = Path;
			this.SHA1 = SHA1;
			this.ModifiedDate = ModifiedDate;
		}

		/// <summary>
		/// Create a File object and automatic add this information: name, size, path, sha1.
		/// </summary>
		/// <param name="Path">Path of the file.</param>
		public SharedFile(string Path)
		{
			FileInfo fileInfo = new FileInfo(Path);
			SHA1 sha1 = new SHA1CryptoServiceProvider();
			FileStream fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			byte[] fileByte = new byte[fileInfo.Length];

			fileStream.Read(fileByte, 0, fileByte.Length);
			fileStream.Close();

			this.Name = fileInfo.Name;
			this.Size = fileInfo.Length;
			this.Path = Path;
			this.SHA1 = BitConverter.ToString(sha1.ComputeHash(fileByte)).Replace("-", "");
			this.ModifiedDate = fileInfo.LastWriteTimeUtc;
		}

		#endregion
	}
}
