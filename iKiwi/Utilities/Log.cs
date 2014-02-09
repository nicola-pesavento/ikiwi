using System;
using System.IO;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Used to log info.
	/// </summary>
	public class Log
	{
		#region Classes
		
		/// <summary>
		/// Log message object.
		/// </summary>
		public class LogMessage
		{
			#region Parameters
			
			/// <summary>
			/// The date of the log message.
			/// </summary>
			public DateTime Date
			{ get; set; }
			
			/// <summary>
			/// The category f the log message.
			/// </summary>
			public LogCategory Category
			{ get; set; }
			
			/// <summary>
			/// The message.
			/// </summary>
			public string Message
			{ get; set; }
			
			#endregion
			
			#region Ctor
			
			/// <summary>
			/// Create a log message object.
			/// </summary>
			/// <param name="Category">The category of the log message.</param>
			/// <param name="Message">The message.</param>
			public LogMessage (string Message, LogCategory Category)
			{
				this.Date = DateTime.UtcNow;
				this.Category = Category;
				this.Message = Message;
			}
			
			#endregion
		}
		
		#endregion
		
		#region Enum
		
        /// <summary>
        /// Categories of log.
        /// </summary>
		public enum LogCategory
		{
            /// <summary>
            /// 
            /// </summary>
			All,
            /// <summary>
            /// 
            /// </summary>
			ConnectionRequests,
            /// <summary>
            /// 
            /// </summary>
			InputMessages,
            /// <summary>
            /// 
            /// </summary>
			OutputMessages,
            /// <summary>
            /// 
            /// </summary>
			Error,
            /// <summary>
            /// 
            /// </summary>
			Info
		}
		
		#endregion

		#region Data Members
		
		private static LogMessage[] m_listLog = new LogMessage[200];
		
		private static int m_indexListLog = 0;
		
		#endregion
		
		#region Parameters
		
        /// <summary>
        /// 
        /// </summary>
		public static LogMessage[] Messages
		{
			get { return m_listLog; }
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Write a log message object.
		/// </summary>
		/// <param name="Category">The category of the log message.</param>
		/// <param name="Message">The message.</param>
		public static void Write(string Message, LogCategory Category)
		{
			if (m_indexListLog >= m_listLog.Length)
			{
				m_indexListLog = 0;
			}
			
			m_listLog[m_indexListLog] = new LogMessage(Message, Category);
			
			m_indexListLog++;
		}
		
		/// <summary>
		/// Generate a log file.
		/// </summary>
		public static void GenerateLogFile()
		{
			DateTime time = DateTime.UtcNow;
			
			string text = string.Empty;
			
			text += "iKiwi log file \n" + time.ToString() + "\n\n";
			
			LogMessage lm;
			
			for (int i = 0; i < m_listLog.Length; i++)
			{
				lm = m_listLog[i];
				
				if (lm != null)
				{
					text += lm.Date.ToString() + " || " + lm.Category + " || " + lm.Message + "\n";
				}
				else
				{
					break;
				}
			}
			
			File.WriteAllText(@"Log_ " + time.ToString(@"d-M-yyyy hh-mm-ss tt") + ".txt", text);
		}
		
		#endregion
	}
}
