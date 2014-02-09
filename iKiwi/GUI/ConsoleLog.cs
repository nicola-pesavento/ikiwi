using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace iKiwi.GUI
{
	/// <summary>
	/// Console Utilities.Log.
	/// </summary>
	public partial class ConsoleLog : Form
	{
		#region Data Members
		
		private Utilities.Log.LogCategory m_selectedLogCategory = Utilities.Log.LogCategory.All;
		private int m_logMessagesIndex = 0;
		private Thread m_daemonThread;
		
		#endregion
		
		#region Ctor
		
		/// <summary>
		/// Console Utilities.Log.
		/// </summary>
		public ConsoleLog()
		{
			InitializeComponent();
			
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					this.comboBox_Category.SelectedIndex = 0;
					
					this.m_logMessagesIndex = LoadConsole(this.m_selectedLogCategory);
					
					while (true)
					{
						UpdateConsole(this.m_selectedLogCategory);
						
						Thread.Sleep(1000);
					}
				}
			));
			
			t.Name = "ConsoleLog";
			t.IsBackground = true;
			t.Start();
			
			this.m_daemonThread = t;
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Get the log category from the ComboBox.
		/// </summary>
		/// <returns>The selected log category.</returns>
		private Utilities.Log.LogCategory GetLogCategoryFromComboBox()
		{
			if (this.InvokeRequired)
			{
				return (Utilities.Log.LogCategory)this.Invoke(new Func<Utilities.Log.LogCategory>(() => this.GetLogCategoryFromComboBox()));
			}
			else
			{
				string item = string.Empty;
				
				item = this.comboBox_Category.SelectedItem.ToString();
				
				Utilities.Log.LogCategory category = Utilities.Log.LogCategory.All;
				
				#region switch (item) {...}
				
				switch (item)
				{
					case "All":
						{
							category = Utilities.Log.LogCategory.All;
							break;
						}
						
					case "Connection requests":
						{
							category = Utilities.Log.LogCategory.ConnectionRequests;
							break;
						}
						
					case "Input messages":
						{
							category = Utilities.Log.LogCategory.InputMessages;
							break;
						}
						
					case "Output messages":
						{
							category = Utilities.Log.LogCategory.OutputMessages;
							break;
						}
						
					case "Info":
						{
							category = Utilities.Log.LogCategory.Info;
							break;
						}
						
					case "Error":
						{
							category = Utilities.Log.LogCategory.Error;
							break;
						}
						
					default:
						{
							break;
						}
				}
				
				#endregion
				
				return category;
			}
		}

		/// <summary>
		/// Load the log messages on the console.
		/// </summary>
		/// <param name="Category">The category of the log messages to load.</param>
		/// <returns>The end index of the list of the log messages.</returns>
		private int LoadConsole(Utilities.Log.LogCategory Category)
		{
			if (this.InvokeRequired)
			{
				return (int)this.Invoke(new Func<int>(() => this.LoadConsole(Category)));
			}
			else
			{
				Utilities.Log.LogMessage logMess;
				
				// clear the console
				this.dataGridView_Console.Rows.Clear();
				
				// load the messages

				int i = 0;

				for (i = 0; i < Utilities.Log.Messages.Length; i++)
				{
					logMess = Utilities.Log.Messages[i];
					
					if (logMess != null)
					{
						if (Category == Utilities.Log.LogCategory.All || logMess.Category == Category)
						{
							this.dataGridView_Console.Rows.Add(
								logMess.Date.ToString(),
								logMess.Message);
						}
					}
					else
					{
						break;
					}
				}

				return i;
			}
		}
		
		/// <summary>
		/// Update the console with the last log messages.
		/// </summary>
		/// <param name="Category">The category of the log messages to update in the console.</param>
		private void UpdateConsole(Utilities.Log.LogCategory Category)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.UpdateConsole(Category); }));
			}
			else
			{
				if (this.m_logMessagesIndex >= Utilities.Log.Messages.Length)
				{
					this.m_logMessagesIndex = 0;
				}
				
				Utilities.Log.LogMessage logMess;
				
				int i = this.m_logMessagesIndex;
				
				DateTime referenceTime = DateTime.UtcNow;
				
				if (Utilities.Log.Messages[i] != null)
				{
					if (i > 0)
					{
						referenceTime = Utilities.Log.Messages[i - 1].Date;
					}
					else
					{
						if (Utilities.Log.Messages[Utilities.Log.Messages.Length - 1] != null)
						{
							referenceTime = Utilities.Log.Messages[Utilities.Log.Messages.Length - 1].Date;
						}
						else
						{
							referenceTime = Utilities.Log.Messages[0].Date;
						}
					}
				}
				
				for (i = this.m_logMessagesIndex; i < Utilities.Log.Messages.Length; i++)
				{
					logMess = Utilities.Log.Messages[i];
					
					if (logMess != null && logMess.Date > referenceTime)
					{
						if (logMess.Category == Category || Category == Utilities.Log.LogCategory.All)
						{
							this.dataGridView_Console.Rows.Add(
								logMess.Date.ToString(),
								logMess.Message);
						}
					}
					else
					{
						break;
					}
					
					if (i == (Utilities.Log.Messages.Length - 1))
					{
						if (Utilities.Log.Messages[0].Date > logMess.Date)
						{
							i = -1;
						}
					}
				}
				
				this.m_logMessagesIndex = i;
				
				// sort the console
				
				if (this.dataGridView_Console.SortedColumn != null)
				{
					ListSortDirection direction = ListSortDirection.Ascending;
					
					if (this.dataGridView_Console.SortOrder != SortOrder.Ascending)
					{
						direction = ListSortDirection.Descending;
					}
					
					this.dataGridView_Console.Sort(this.dataGridView_Console.SortedColumn, direction);
				}
			}
		}
		
		#endregion
		
		# region Events
		
		void ComboBox_CategorySelectedIndexChanged(object sender, EventArgs e)
		{
			this.m_selectedLogCategory = GetLogCategoryFromComboBox();
			
			this.m_logMessagesIndex = LoadConsole(this.m_selectedLogCategory);
		}
		
		void Button_GenerateLogFileClick(object sender, EventArgs e)
		{
			Utilities.Log.GenerateLogFile();
		}
		
		void ConsoleLogFormClosing(object sender, FormClosingEventArgs e)
		{
			this.m_daemonThread.Abort();
		}
		
		#endregion
	}
}
