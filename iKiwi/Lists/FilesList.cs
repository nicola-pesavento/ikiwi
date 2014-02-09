using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace iKiwi.Lists
{
	class FilesList
	{
		#region Data Members

		/// <summary>
		/// The list of all shared file.
		/// </summary>
		private static List<Objects.SharedFile> m_filesList = new List<Objects.SharedFile>();
		
		/// <summary>
		/// The progress, in percent, of building of the list.
		/// </summary>
		private static int m_buildingProgress = 0;

		#endregion

		#region Properties

		/// <summary>
		/// Get the complete list with all shared files.
		/// </summary>
		public static List<Objects.SharedFile> List
		{
			get { return m_filesList; }
		}

		/// <summary>
		/// Get the number of files in this list.
		/// </summary>
		public static int Count
		{
			get { return m_filesList.Count; }
		}
		
		/// <summary>
		/// The progress, in percent, of building of the list.
		/// </summary>
		public static int BuildingProgress
		{
			get { return m_buildingProgress; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Builds the list of shared files, but first, loads the file list saved the last time.
		/// </summary>
		public static void Build()
		{
			Utilities.Log.Write("Building list of shared files", Utilities.Log.LogCategory.Info);

			// delete the list
			Delete();
			
			// set the progress to  zero
			m_buildingProgress = 0;
			
			// get the saved list
			List<Objects.SharedFile> loadedList = GetSavedList();
			
			string[] filesPaths;

			// get the paths of the shared files in the shared directory
			try
			{
				filesPaths = Directory.GetFiles(Global.SharedDirectory, "*", SearchOption.AllDirectories);
			}
			catch
			{
				filesPaths = new string[0];
			}
			
			bool createNewFileObject = true;
			
			// add the files in the list but first control that the files are not already presented in the saved list
			for (int i = 0; i < filesPaths.Length; i++)
			{
				for (int a = 0; a < loadedList.Count; a++)
				{
					// control if the file is already presented in the saved list
					if (filesPaths[i] == loadedList[a].Path)
					{
						// control the modified date of the file
						
						FileInfo fileInfo = new FileInfo(filesPaths[i]);
						
						if (fileInfo.LastWriteTimeUtc.ToString() == loadedList[a].ModifiedDate.ToString())
						{
							// add the file, loaded from the saved list, in the list
							AddFile(loadedList[a]);
							
							createNewFileObject = false;
							
							// remove the file just added from the loaded list
							loadedList.RemoveAt(a);
						}
						
						break;
					}
				}
				
				if (createNewFileObject == true)
				{
					// add the file in the list (create a new file object)
					AddFile(filesPaths[i]);
				}
				else
				{
					createNewFileObject = true;
				}
				
				// update the progress
				m_buildingProgress = (m_filesList.Count * 100) / filesPaths.Length;
			}
			
			// the list is completed
			m_buildingProgress = 100;

			Utilities.Log.Write("List of shared files completed", Utilities.Log.LogCategory.Info);
			
			// save the list
			SaveList();
		}
		
		/// <summary>
		/// Builds the list of shared files, without loads the file list saved the last time.
		/// </summary>
		public static void BuildFromZero()
		{
			// delete the saved list
			DeleteSavedList();
			
			// build the list
			Build();
		}

		/// <summary>
		/// Automatic creates a File object and add it to this list.
		/// </summary>
		/// <param name="Path">The path of file.</param>
		public static void AddFile(string Path)
		{
			try
			{
				Objects.SharedFile file = new Objects.SharedFile(Path);

				for (int i = 0; i < m_filesList.Count; i++)
				{
					if (m_filesList[i].SHA1 == file.SHA1)
					{
						return;
					}
				}
				
				m_filesList.Add(file);
			}
			catch
			{
				return;
			}
		}
		
		/// <summary>
		/// Adds a file object in the list.
		/// </summary>
		/// <param name="File">The file object.</param>
		public static void AddFile(Objects.SharedFile File)
		{
			for (int i = 0; i < m_filesList.Count; i++)
			{
				if (m_filesList[i].SHA1 == File.SHA1)
				{
					return;
				}
			}
			
			m_filesList.Add(File);
		}

		/// <summary>
		/// Search a file that contains the name searched.
		/// </summary>
		/// <param name="FileName">The name of the file to search.</param>
		/// <returns>List that contains the file objects found.</returns>
		public static List<Objects.SharedFile> SearchFileByName(string FileName)
		{
			List<Objects.SharedFile> FilesFound = new List<Objects.SharedFile>();

			for (int i = 0; i < m_filesList.Count; i++)
			{
				if (m_filesList[i].Name.IndexOf(FileName, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					FilesFound.Add(m_filesList[i]);
				}
			}
			
			return FilesFound;
		}
		
		/// <summary>
		/// Search a file that contains the text searched.
		/// </summary>
		/// <param name="Text">The text to search.</param>
		/// <returns>List that contains the file objects found.</returns>
		public static List<Objects.SharedFile> SearchFileByText(string Text)
		{
			List<Objects.SharedFile> filesFound = new List<Objects.SharedFile>();
			
			string[] stringsToSearch = Text.Split(' ');
			
			int numFoundStrings = 0;
			
			for (int i = 0; i < m_filesList.Count; i++)
			{
				for (int a = 0; a < stringsToSearch.Length; a++)
				{
					if (m_filesList[i].Name.IndexOf(stringsToSearch[a], StringComparison.OrdinalIgnoreCase) >= 0)
					{
						numFoundStrings++;
					}
					else
					{
						break;
					}
				}
				
				if (numFoundStrings == stringsToSearch.Length)
				{
					filesFound.Add(m_filesList[i]);
				}
				
				numFoundStrings = 0;
			}
			
			return filesFound;
		}

		/// <summary>
		/// Get a single file.
		/// </summary>
		/// <param name="FileName">The name of the file.</param>
		/// <param name="FileID">The SHA1-ID of the file.</param>
		/// <returns>If returns Null means that the file doesn't exist.</returns>
		public static Objects.SharedFile GetFile(string FileName, string FileID)
		{
			for (int i = 0; i < m_filesList.Count; i++)
			{
				if (m_filesList[i].Name == FileName && m_filesList[i].SHA1 == FileID)
				{
					return m_filesList[i];
				}
			}
			return null;
		}
		
		/// <summary>
		/// Delete the list.
		/// </summary>
		public static void Delete()
		{
			m_filesList = new List<Objects.SharedFile>();
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Returns the saved list.
		/// </summary>
		/// <returns>The saved list.</returns>
		private static List<Objects.SharedFile> GetSavedList()
		{
			List<Objects.SharedFile> filesList = new List<iKiwi.Objects.SharedFile>();
			
			XmlDocument XmlDoc = new XmlDocument();
			
			string xmlPath = Global.iKiwiPath + "FilesList.xml";

			if(File.Exists(xmlPath))
			{
				// read the file
				try
				{
					XmlDoc.Load(xmlPath);
					
					XmlNode node = XmlDoc.SelectSingleNode("Files");
					
					if (node != null)
					{
						for (int i = 0; i < node.ChildNodes.Count; i++)
						{
							string name = string.Empty;
							string path = string.Empty;
							long size = 0;
							string id = node.ChildNodes[i].Name.Remove(0, 1);
							DateTime modifiedDate = DateTime.Now;
							
							for (int n = 0; n < node.ChildNodes[i].ChildNodes.Count; n++)
							{
								XmlNode node2 = node.ChildNodes[i].ChildNodes[n];
								
								switch(node2.Name)
								{
									case "Name":
										{
											name = node2.InnerText;
											break;
										}
									case "Path":
										{
											path = node2.InnerText;
											break;
										}
									case "Size":
										{
											size = long.Parse(node2.InnerText);
											break;
										}
									case "ModifiedDate":
										{
											modifiedDate = DateTime.Parse(node2.InnerText);
											break;
										}
									default:
										{
											break;
										}
								}
							}
							
							// create the file object
							Objects.SharedFile file = new iKiwi.Objects.SharedFile(name, size, path, id, modifiedDate);
							
							// add the file in the list to return
							filesList.Add(file);
						}
					}
					
					Utilities.Log.Write("Loaded the saved list of the shared files", Utilities.Log.LogCategory.Info);
				}
				catch
				{
					Utilities.Log.Write("The saved list of the shared files is corrupted", Utilities.Log.LogCategory.Error);
					
					try
					{
						// delete the corrupted file
						DeleteSavedList();
					}
					catch
					{
					}
				}
			}
			
			return filesList;
		}
		
		/// <summary>
		/// Save the list in a file.
		/// </summary>
		private static void SaveList()
		{
			// create the file
			XmlDocument XmlDoc = new XmlDocument();

			XmlDoc.LoadXml("<Files></Files>");

			Objects.SharedFile file = null;
			
			// write in the file
			for (int i = 0; i < m_filesList.Count; i++)
			{
				try
				{
					file = m_filesList[i];
					
					XmlElement xmlFile = XmlDoc.CreateElement("_" + file.SHA1);
					
					XmlElement fileName = XmlDoc.CreateElement("Name");
					fileName.InnerText = file.Name;
					xmlFile.AppendChild(fileName);
					
					XmlElement filePath = XmlDoc.CreateElement("Path");
					filePath.InnerText = file.Path;
					xmlFile.AppendChild(filePath);
					
					XmlElement fileSize = XmlDoc.CreateElement("Size");
					fileSize.InnerText = file.Size.ToString();
					xmlFile.AppendChild(fileSize);
					
					XmlElement fileModifiedDate = XmlDoc.CreateElement("ModifiedDate");
					fileModifiedDate.InnerText = file.ModifiedDate.ToString();
					xmlFile.AppendChild(fileModifiedDate);
					
					XmlDoc.DocumentElement.AppendChild(xmlFile);
				}
				catch
				{
				}
			}

			// save the file
			
			XmlTextWriter writer = new XmlTextWriter(Global.iKiwiPath + "FilesList.xml", null);
			writer.Formatting = Formatting.Indented;
			XmlDoc.Save(writer);
			writer.Close();
			
			Utilities.Log.Write("Saved the list of the shared files", Utilities.Log.LogCategory.Info);
		}
		
		/// <summary>
		/// Deletes the file where is the saved list.
		/// </summary>
		private static void DeleteSavedList()
		{
			string xmlPath = Global.iKiwiPath + "FilesList.xml";

			if(File.Exists(xmlPath))
			{
				try
				{
					// delete the corrupted file
					File.Delete(xmlPath);
					
					Utilities.Log.Write("Deleted the saved list of the shared files", Utilities.Log.LogCategory.Info);
				}
				catch
				{
				}
			}
		}
		
		#endregion
	}
}