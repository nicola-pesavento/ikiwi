using System;
using System.Collections.Generic;
using System.Text;

namespace iKiwi.Messages
{
	class FileFound : AbstractMessage
	{
		#region Classes

		public class File
		{
			#region Properties

			public string Name
			{ get; set; }

			public int Size
			{ get; set; }

			public string SHA1
			{ get; set; }

			#endregion
		}

		#endregion

		#region Data Members

		private string m_strSearchedFileName;

		private FileFound.File[] m_arrFiles;

		#endregion

		#region Properties

		public override string Parameters
		{
			get
			{
				string parameters = string.Format("SF {0}", m_strSearchedFileName);
				
				if(m_arrFiles != null)
				{
					for (int i = 0; i < m_arrFiles.Length; i++)
					{
						parameters += string.Format("\nFN {0}\nSZ {1}\nID {2}", m_arrFiles[i].Name, m_arrFiles[i].Size, m_arrFiles[i].SHA1);
					}
				}

				return (parameters);
			}
		}

		public override byte[] ParametersByte
		{
			get
			{
				string parameters = string.Format("SF {0}", m_strSearchedFileName);
				
				for (int i = 0; i < m_arrFiles.Length; i++)
				{
					parameters += string.Format("\nFN {0}\nSZ {1}\nID {2}", m_arrFiles[i].Name, m_arrFiles[i].Size, m_arrFiles[i].SHA1);
				}
				parameters.Trim();

				return (ASCIIEncoding.Unicode.GetBytes(parameters));
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// Create a FF-message.
		/// </summary>
		/// <param name="MessageID">The message ID.</param>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters with prefixes.</param>
		public FileFound(string MessageID, int TTL, bool Encryption, string[] Parameters)
			: base(Commands.FF, MessageID, TTL, Encryption)
		{
			if (((Parameters.Length - 1) % 3) == 0)
			{
				Parameters[0] = Parameters[0].Remove(0, 2).Trim();

				int nFiles = (Parameters.Length - 1) / 3;

				int i;
				
				for (int n = 1; n <= nFiles; n++)
				{
					i = n * 3;

					Parameters[(i - 2)] = Parameters[(i - 2)].Remove(0, 3).Trim();
					Parameters[(i - 1)] = Parameters[(i - 1)].Remove(0, 3).Trim();
					Parameters[i] = Parameters[i].Remove(0, 3).Trim();
				}
				
				this.CreateFileFound(Parameters);
			}
		}

		/// <summary>
		/// Create a FF-message.
		/// </summary>
		/// <param name="TTL">The time to live.</param>
		/// <param name="Encryption">Indicates if the message is encrypted or not.</param>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		public FileFound(int TTL, bool Encryption, params string[] Parameters)
			: base(Commands.FF, string.Empty, TTL, Encryption)
		{
			if (((Parameters.Length - 1) % 3) == 0)
			{
				this.CreateFileFound(Parameters);
			}
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Create a FF-message.
		/// </summary>
		/// <param name="Parameters">The list of the parameters without prefixes.</param>
		private void CreateFileFound(params string[] Parameters)
		{
			this.m_strSearchedFileName = Parameters[0];

			int nFiles = (Parameters.Length - 1) / 3;
			
			m_arrFiles = new FileFound.File[nFiles];

			int i;
			
			for (int n = 1; n <= nFiles; n++)
			{
				i = n * 3;

				FileFound.File file = new FileFound.File();

				file.Name = Parameters[(i - 2)];
				file.Size = int.Parse(Parameters[(i - 1)]);
				file.SHA1 = Parameters[i];

				m_arrFiles[(n - 1)] = file;
			}
			
		}
		
		#endregion

		#region Methods

		public override void Process()
		{
			// read all files found and add them to Lists.FilesFoundList.cs;
			for (int i = 0; i < m_arrFiles.Length; i++)
			{
				Lists.FilesFoundList.CreateAndAddFile(m_arrFiles[i].Name, m_arrFiles[i].Size, m_arrFiles[i].SHA1, this.SenderPeer.IP, this.SenderPeer.ID);
			}
			
			Global.NewFfMessage = true;
		}

		#endregion
	}
}
