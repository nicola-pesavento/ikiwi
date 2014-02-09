using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace iKiwi.GUI.Custom_components
{
	/// <summary>
	/// The panel to search the files in the net.
	/// </summary>
	public partial class FileSearchPanel : UserControl
	{
		#region Ctor
		
		/// <summary>
		/// 
		/// </summary>
		public FileSearchPanel()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Data Members
		
		private bool m_bOpenContextMenuStripFilesFoundGrid = false;
		
		/// <summary>
		/// The last searched file.
		/// </summary>
		private string m_lastSearchedFile = null;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public DataGridView FilesFoundGrid
		{
			get { return this.dataGridView_FilesFound; }
			set { this.dataGridView_FilesFound = value; }
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Update the panel.
		/// </summary>
		public new void Update()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					DataGridViewRowCollection rows = FilesFoundGrid.Rows;

					// clear the dataGrid
					this.Invoke(new MethodInvoker(delegate { rows.Clear(); }));

					if (this.m_lastSearchedFile != null)
					{
						List<Lists.FilesFoundList.File> list = Lists.FilesFoundList.SearchFileByText(m_lastSearchedFile);

						for (int i = 0; i < list.Count; i++)
						{
							this.Invoke(new MethodInvoker(delegate { rows.Add(list[i].Name, Utilities.Converterer.AutoConvertSizeFromByte(list[i].Size), list[i].SHA1); }));
						}
					}
				}
			));

			t.Name = "UpdateFileSearchPanel";
			t.IsBackground = true;
			t.Start();
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Searches a file.
		/// </summary>
		/// <param name="FileName">The name of the file to search.</param>
		private void SearchFile(string FileName)
		{
			// save the file name
			this.m_lastSearchedFile = FileName;
			
			// search the file
			Utilities.FileSearcher.SearchFile(FileName);
		}
		
		#endregion
		
		#region Events
		
		void Button_SearchFileClick(object sender, EventArgs e)
		{
			this.SearchFile(textBox_SearchFile.Text);
		}
		
		void TextBox_SearchFileKeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				this.SearchFile(textBox_SearchFile.Text);
			}
		}
		
		void DataGridView_FilesFoundMouseDown(object sender, MouseEventArgs e)
		{
			DataGridView.HitTestInfo info = dataGridView_FilesFound.HitTest(e.X, e.Y);
			
			if (info.RowIndex > -1)
			{
				if (dataGridView_FilesFound.SelectedRows != null)
				{
					dataGridView_FilesFound.ClearSelection();
				}
				
				dataGridView_FilesFound.Rows[info.RowIndex].Selected = true;
				
				this.m_bOpenContextMenuStripFilesFoundGrid = true;
			}
		}
		
		void ContextMenuStrip_FilesFoundGridOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			while (true)
			{
				if (this.m_bOpenContextMenuStripFilesFoundGrid == true)
				{
					this.m_bOpenContextMenuStripFilesFoundGrid = false;
					
					break;
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}
		
		void DownloadToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.DownloadToolStripMenuItemClick(sender, e); }));
			}
			else
			{
				// get file information
				
				DataGridViewRow row = dataGridView_FilesFound.SelectedRows[0];
				
				if (row.Cells[0].Value != null)
				{
					string fileName = row.Cells[0].Value.ToString();
					
					string fileID = row.Cells[2].Value.ToString();
					
					// start the download
					Daemons.Downloader.AddDownload(fileName, fileID);
					
					// change the color of the row
					row.DefaultCellStyle.ForeColor = Color.Green;
				}
			}
		}
		
		#endregion
	}
}
