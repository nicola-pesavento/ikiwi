using System;
using System.Collections.Generic;

namespace iKiwi.Lists
{
	/// <summary>
	/// The list of IDs of the last processed message.
	/// </summary>
	public class MessageIDsList
	{
		#region Properties
		
		/// <summary>
		/// It is used to count the sent messages and generate the message IDs.
		/// </summary>
		public static uint MessageCounter
		{
			get { return _MessageCounter; }
			set { _MessageCounter = value; }
		}
		private static uint _MessageCounter = (uint)(new Random().Next(999));
		
		#endregion
		
		#region Data Members
		
		private static string[] m_messageIDsList = new string[40];
		
		private static int m_index = 0;
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Check if the ID is of a message that has already been processed.
		/// </summary>
		/// <param name="MessageID">The ID of the message.</param>
		/// <returns>Return true if the ID is of a message that has already been processed.</returns>
		public static bool CheckID(string MessageID)
		{
			for (int i = 0; i < m_messageIDsList.Length; i++)
			{
				if (m_messageIDsList[i] == MessageID)
				{
					return true;
				}
			}
			
			return false;
		}
		
		/// <summary>
		/// Add the ID of a message.
		/// </summary>
		/// <param name="MessageID">The ID of the message.</param>
		public static void AddID(string MessageID)
		{
			if (m_index >= m_messageIDsList.Length)
			{
				m_index = 0;
			}
			
			m_messageIDsList[m_index] = MessageID;
			
			m_index++;
		}
		
		/// <summary>
		/// Check if the ID is of a message that has already been processed, if the ID doesn't exist add it to the list.
		/// </summary>
		/// <param name="MessageID">The ID of the message.</param>
		/// <returns>Return true if the ID is of a message that has already been processed.</returns>
		public static bool CheckAndAddID(string MessageID)
		{
			if (CheckID(MessageID) == true)
			{
				return true;
			}
			else
			{
				AddID(MessageID);
				
				return false;
			}
		}
		
		#endregion
	}
}
