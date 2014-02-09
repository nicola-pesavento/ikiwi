using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Use for save and load the settings of iKiwi and for load the info of the pending downloads.
	/// </summary>
	public class Configurator
	{
		#region Methods
		
		/// <summary>
		/// Control if the settings are good.
		/// </summary>
		public static void ControlSettings()
		{
			// control if the Peer-ID has already been generated
			if (Global.MyPeerID == "EMPTY")
			{
				Global.MyPeerID = Utilities.PeerIdGenerator.GeneratePeerId();
			}
			
			// control if exist the shared directory
			if (Global.SharedDirectory == "EMPTY" || !Directory.Exists(Global.SharedDirectory))
			{
				Application.Run(new Utilities.DirectoryHelper());
			}
			
			// control if exist the temporary directory
			if (Global.TempDirectory == "EMPTY" || !Directory.Exists(Global.TempDirectory))
			{
				//MessageBox.Show("Select a folder for temporary files.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				
				//Global.TempDirectory = SelectFolder();
				
				Global.TempDirectory = Global.iKiwiPath + @"Temp\";
				
				Directory.CreateDirectory(Global.TempDirectory);
				
				Directory.CreateDirectory(Global.TempDirectory + "List");
			}
			
			// control the ikiwi auto startup
			
			string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			
			RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, true);
			
			key.GetValue("ikiwi");
			
			if (Global.StartIkiwiAtSystemStartup == true)
			{
				if (Global.StartIkiwiMinimized == true)
				{
					key.SetValue("ikiwi", Application.ExecutablePath + " /MINIMIZED");
				}
				else
				{
					key.SetValue("ikiwi", Application.ExecutablePath);
				}
				
				key.Close();
			}
			else
			{
				if (key.GetValue("ikiwi") != null)
				{
					key.DeleteValue("ikiwi");
					
					key.Close();
				}
			}
			
			Utilities.Configurator.SaveAll();
		}
		
		/// <summary>
		/// Read the file of the configuration and set the settings.
		/// </summary>
		public static void LoadAll()
		{
			XmlDocument XmlDoc = new XmlDocument();

			string confFilePath = Global.iKiwiPath + "Config.xml";
			
			if(File.Exists(confFilePath))
			{
				// open the file
				XmlDoc.Load(confFilePath);

				// read the file
				
				XmlNode node = XmlDoc.SelectSingleNode("SETTINGS");
				
				if (node != null)
				{
					for (int i = 0; i < node.ChildNodes.Count; i++)
					{
						switch(node.ChildNodes[i].Name)
						{
							case "PEER_ID":
								{
									Global.MyPeerID = node.ChildNodes[i].InnerText;
									break;
								}
							case "LISTENING_PORT":
								{
									Global.ListeningPort = int.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "SHARED_DIRECTORY":
								{
									Global.SharedDirectory = node.ChildNodes[i].InnerText;
									break;
								}
							case "TEMP_DIRECTORY":
								{
									Global.TempDirectory = node.ChildNodes[i].InnerText;
									break;
								}
							case "MAX_INPUT_CONNECTIONS":
								{
									Global.MaxNumberInputConnections = int.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "MIN_NUM_OF_PEERS":
								{
									Global.MinimumPeerConnections = int.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "TTL_MESSAGE":
								{
									Global.TtlMessage = int.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "NUM_FS_MESSAGE_FOR_SEARCH":
								{
									Global.NumberFSMessagesForSearch = int.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "NOVA_WEB_CACHE":
								{
									Global.NovaWebCache = node.ChildNodes[i].InnerText;
									break;
								}
							case "START_IKIWI_AT_SYSTEM_STARTUP":
								{
									Global.StartIkiwiAtSystemStartup = bool.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "START_IKIWI_MINIMIZED":
								{
									Global.StartIkiwiMinimized = bool.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "FIRST_START":
								{
									Global.FirstStart = bool.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "MESSAGE_ENCRYPTION_ENABLED":
								{
									Global.MessageEncryptionEnabled = bool.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "ACCEPT_NOT_ENCRYPTED_MESSAGES":
								{
									Global.AcceptNotEncryptedMessages = bool.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							case "USE_UPNP":
								{
									Global.UseUpnp = bool.Parse(node.ChildNodes[i].InnerText);
									break;
								}
							default:
								{
									break;
								}
						}
					}
				}
			}
		}

		/// <summary>
		/// Save all the settings on the file of the configuration.
		/// </summary>
		public static void SaveAll()
		{
			// create the file
			XmlDocument XmlDoc = new XmlDocument();

			XmlDoc.LoadXml("<SETTINGS></SETTINGS>");

			// write in the file
			
			XmlElement element = null;

			element = XmlDoc.CreateElement("PEER_ID");
			element.InnerText = Global.MyPeerID;
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("LISTENING_PORT");
			element.InnerText = Global.ListeningPort.ToString();
			XmlDoc.DocumentElement.AppendChild(element);

			element = XmlDoc.CreateElement("SHARED_DIRECTORY");
			element.InnerText = Global.SharedDirectory;
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("TEMP_DIRECTORY");
			element.InnerText = Global.TempDirectory;
			XmlDoc.DocumentElement.AppendChild(element);

			element = XmlDoc.CreateElement("MAX_INPUT_CONNECTIONS");
			element.InnerText = Global.MaxNumberInputConnections.ToString();
			XmlDoc.DocumentElement.AppendChild(element);

			element = XmlDoc.CreateElement("MIN_NUM_OF_PEERS");
			element.InnerText = Global.MinimumPeerConnections.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("TTL_MESSAGE");
			element.InnerText = Global.TtlMessage.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("NUM_FS_MESSAGE_FOR_SEARCH");
			element.InnerText = Global.NumberFSMessagesForSearch.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("NOVA_WEB_CACHE");
			element.InnerText = Global.NovaWebCache;
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("START_IKIWI_AT_SYSTEM_STARTUP");
			element.InnerText = Global.StartIkiwiAtSystemStartup.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("START_IKIWI_MINIMIZED");
			element.InnerText = Global.StartIkiwiMinimized.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("FIRST_START");
			element.InnerText = Global.FirstStart.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("MESSAGE_ENCRYPTION_ENABLED");
			element.InnerText = Global.MessageEncryptionEnabled.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("ACCEPT_NOT_ENCRYPTED_MESSAGES");
			element.InnerText = Global.AcceptNotEncryptedMessages.ToString();
			XmlDoc.DocumentElement.AppendChild(element);
			
			element = XmlDoc.CreateElement("USE_UPNP");
			element.InnerText = Global.UseUpnp.ToString();
			XmlDoc.DocumentElement.AppendChild(element);

			// save the file
			XmlTextWriter writer = new XmlTextWriter(Global.iKiwiPath + "Config.xml", null);
			writer.Formatting = Formatting.Indented;
			XmlDoc.Save(writer);
			writer.Close();
		}
		
		/// <summary>
		/// Save the info of the pending downloads in the file of the pending downloads.
		/// </summary>
		public static void SavePendingDownloadsInfo()
		{
			// create the file
			XmlDocument XmlDoc = new XmlDocument();

			XmlDoc.LoadXml("<PENDING_DOWNLOADS></PENDING_DOWNLOADS>");

			// write in the file
			
			XmlElement element = null;
			
			for (int i = 0; i < Daemons.Downloader.List.Count; i++)
			{
				Objects.Download download = Daemons.Downloader.List[i];
				
				if (download.Completed == false)
				{
					element = XmlDoc.CreateElement("_" + download.SHA1);
					
					XmlElement name = XmlDoc.CreateElement("NAME");
					name.InnerText = download.Name;
					element.AppendChild(name);
					
					XmlElement size = XmlDoc.CreateElement("SIZE");
					size.InnerText = download.Size.ToString();
					element.AppendChild(size);
					
					XmlElement filePacks = XmlDoc.CreateElement("FILE_PACKS");
					
					filePacks.InnerText = Utilities.Converterer.ConvertBitToHex(download.ListFilePacks);
					
					element.AppendChild(filePacks);
					
					XmlDoc.DocumentElement.AppendChild(element);
				}
			}
			
			// save the file
			
			XmlTextWriter writer = new XmlTextWriter(Global.iKiwiPath + "PendingDownloads.xml", null);
			writer.Formatting = Formatting.Indented;
			XmlDoc.Save(writer);
			writer.Flush();
			writer.Close();
		}
		
		/// <summary>
		/// Load the info of the pending downloads from the file of the pending downloads.
		/// </summary>
		public static void LoadPendingDownloadsInfo()
		{
			XmlDocument XmlDoc = new XmlDocument();
			
			string xmlFilePath = Global.iKiwiPath + "PendingDownloads.xml";

			if(File.Exists(xmlFilePath))
			{
				// read the file
				try
				{
					XmlDoc.Load(xmlFilePath);
					
					XmlNode node = XmlDoc.SelectSingleNode("PENDING_DOWNLOADS");
					
					if (node != null)
					{
						for (int i = 0; i < node.ChildNodes.Count; i++)
						{
							string name = string.Empty;
							int size = 0;
							BitArray filePacks  = new BitArray(0);
							
							for (int n = 0; n < node.ChildNodes[i].ChildNodes.Count; n++)
							{
								XmlNode node2 = node.ChildNodes[i].ChildNodes[n];
								
								switch(node2.Name)
								{
									case "NAME":
										{
											name = node2.InnerText;
											break;
										}
									case "SIZE":
										{
											size = int.Parse(node2.InnerText);
											break;
										}
									case "FILE_PACKS":
										{
											BitArray ba = Utilities.Converterer.ConvertHexToBit(node2.InnerText);
											
											if (size % 16384 == 0)
											{
												filePacks = new BitArray(size / 16384);
											}
											else
											{
												filePacks = new BitArray((size / 16384) + 1);
											}
											
											for (int a = 0; a < filePacks.Length; a++)
											{
												filePacks[a] = ba[a];
											}
											
											break;
										}
									default:
										{
											break;
										}
								}
							}
							
							Objects.Download download = new iKiwi.Objects.Download(name, size, node.ChildNodes[i].Name.Substring(1), filePacks);
							
							Daemons.Downloader.AddDownload(download);
						}
					}
				}
				catch
				{
					try
					{
						// delete the corrupted file
						File.Delete(xmlFilePath);
					}
					catch
					{
					}
				}
			}
		}
		
		/// <summary>
		/// Apply the settings.
		/// </summary>
		public static void ApplySettings()
		{
			// listening port
			Daemons.Bouncer.ChangeListeningPort(Global.ListeningPort);
			
			// list directory
			if(!Directory.Exists(Global.TempDirectory + "List"))
			{
				Directory.CreateDirectory(Global.TempDirectory + @"\List");
			}
			
			// ikiwi startup
			
			string subkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
			
			RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, true);
			
			key.GetValue("ikiwi");
			
			if (Global.StartIkiwiAtSystemStartup == true)
			{
				if (Global.StartIkiwiMinimized == true)
				{
					key.SetValue("ikiwi", Application.ExecutablePath + " /MINIMIZED");
				}
				else
				{
					key.SetValue("ikiwi", Application.ExecutablePath);
				}
				
				key.Close();
			}
			else
			{
				if (key.GetValue("ikiwi") != null)
				{
					key.DeleteValue("ikiwi");
					
					key.Close();
				}
			}
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Select a folder.
		/// </summary>
		/// <returns>Return the path of the folder.</returns>
		private static string SelectFolder()
		{
			FolderBrowserDialog f = new FolderBrowserDialog();
			
			f.ShowDialog();
			
			return f.SelectedPath + @"\";
		}
		
		#endregion
	}
}