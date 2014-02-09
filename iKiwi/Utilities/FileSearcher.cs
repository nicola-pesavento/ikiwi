using System;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Search a file in the net.
	/// </summary>
	public class FileSearcher
	{
		#region Properties
		
		/// <summary>
		/// The name of the last file searched.
		/// </summary>
		public static string LastSearch
		{ get; set; }
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Search a single file, automatic searching and sending FS messages.
		/// </summary>
		/// <param name="FileName">The name of the file to search.</param>
		public static void SearchFile(string FileName)
		{
			// save the name of the file to search
			LastSearch = FileName;
			
			// number of sent messages
			int nMess = 0;
			
			// number of messages to send
			int max = Global.NumberFSMessagesForSearch;
			
			// the FS-message
			Messages.IMessage FsMessage = Messages.MessagesFactory.Instance.CreateMessage(Messages.Commands.FS, Global.TtlMessage, (Global.MyRemoteIP + ":" + Global.ListeningPort), FileName);

			for (int i = 0; i < Lists.PeersList.List.Count; i++)
			{
				if (nMess >= max)
				{
					break;
				}

				// control if there are enough peers for send all the messages
				if ((Lists.PeersList.List.Count - i) > (max - nMess))
				{
					if (Lists.PeersList.List[i].HasFileByName(FileName))
					{
						Lists.PeersList.List[i].Send(FsMessage);
						nMess++;
					}
				}
				else
				{
					Lists.PeersList.List[i].Send(FsMessage);
					nMess++;
				}
			}
		}
		
		#endregion
	}
}
