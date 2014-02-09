using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace iKiwi.GUI
{
	/// <summary>
	/// The panel of downloads.
	/// </summary>
	public partial class DownloadsPanel : UserControl
	{
		#region Data Members
		
		private bool m_bOpenContextMenuStripDownloadsGrid = false;
		
		#endregion
		
		#region Ctor
		
		/// <summary>
		/// 
		/// </summary>
		public DownloadsPanel()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// The DownloadsGrid object.
		/// </summary>
		public DataGridView DownloadsGrid
		{
			get { return dataGridView_Downloads; }
			set { dataGridView_Downloads = value; }
		}
		
		/// <summary>
		/// The selected download in the grid.
		/// </summary>
		public Objects.Download SelectedDownload
		{ get; set; }
		
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
					this.UpdateDownloadGrid();
					
					this.UpdateSelectedDownloadInfo();
					
					this.UpdateSelectedDownloadPeers();
				}
			));

			t.Name = "UpdateDownloadsPanel";
			t.IsBackground = true;
			t.Start();
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Update the grid of the downloads.
		/// </summary>
		private void UpdateDownloadGrid()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.UpdateDownloadGrid(); }));
			}
			else
			{
				DataGridViewRowCollection rows = this.DownloadsGrid.Rows;
				
				if (rows.Count > Daemons.Downloader.Count)
				{
					rows.Clear();
				}
				
				for (int i = 0; i < Daemons.Downloader.Count; i++)
				{
					Objects.Download download = Daemons.Downloader.List[i];
					
					if (i >= rows.Count)
					{
						rows.Add(
							download.Name,
							Utilities.Converterer.AutoConvertSizeFromByte(download.CurrentSize),
							download.Progress.ToString("F") + "%",
							Utilities.Converterer.AutoConvertSizeFromByte(download.DownloadSpeed) + "/s",
							Utilities.Converterer.AutoConvertSizeFromByte(download.Size),
							download.SHA1);
					}
					else
					{

						rows[i].SetValues(
							download.Name,
							Utilities.Converterer.AutoConvertSizeFromByte(download.CurrentSize),
							download.Progress.ToString("F") + "%",
							Utilities.Converterer.AutoConvertSizeFromByte(download.DownloadSpeed) + "/s",
							Utilities.Converterer.AutoConvertSizeFromByte(download.Size),
							download.SHA1);
					}
					
					// change the color of the row
					
					Color rowColor;
					
					if (download.Active)
					{
						rowColor = Color.Blue;
					}
					else
					{
						if (download.Completed)
						{
							rowColor  = Color.Green;
						}
						else
						{
							rowColor = Color.Orange;
						}
					}
					
					rows[i].DefaultCellStyle.ForeColor = rowColor;
				}
			}
		}
		
		/// <summary>
		/// Update the info of the selected download's.
		/// </summary>
		private void UpdateSelectedDownloadInfo()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.UpdateSelectedDownloadInfo(); }));
			}
			else
			{
				if (this.SelectedDownload != null)
				{
					this.label_downloadName.Text = this.SelectedDownload.Name;
					this.label_downloadID.Text = this.SelectedDownload.SHA1;
					this.label_downloadSize.Text = Utilities.Converterer.AutoConvertSizeFromByte(this.SelectedDownload.Size);
					this.label_downloadProgress.Text = this.SelectedDownload.Progress.ToString("F") + "%";
					this.label_downloadDownloadedPacks.Text = (this.SelectedDownload.ListFilePacks.Length - this.SelectedDownload.RemainingFilePacks).ToString();
					this.label_downloadUploaderPeers.Text = this.SelectedDownload.ListUploaderPeers.Count.ToString();
					this.label_downloadActive.Text = this.SelectedDownload.Active.ToString();
					this.label_downloadDownloadSpeed.Text = Utilities.Converterer.AutoConvertSizeFromByte(SelectedDownload.DownloadSpeed) + "/s";
				}
			}
		}
		
		/// <summary>
		/// Update the info about the peers of the selected download.
		/// </summary>
		private void UpdateSelectedDownloadPeers()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.UpdateSelectedDownloadPeers(); }));
			}
			else
			{
				if (this.SelectedDownload != null)
				{
					DataGridViewRowCollection rows = this.dataGridView_SelectedDownloadPeers.Rows;
					
					if (rows.Count > this.SelectedDownload.ListPeers.Count)
					{
						rows.Clear();
					}
					
					for (int i = 0; i < this.SelectedDownload.ListPeers.Count; i++)
					{
						Objects.Peer peer = this.SelectedDownload.ListPeers[i];
						
						if (i >= rows.Count)
						{
							rows.Add(
								peer.IP,
								Utilities.Converterer.AutoConvertSizeFromByte(peer.DownloadSpeed) + "/s");
						}
						else
						{

							rows[i].SetValues(
								peer.IP,
								Utilities.Converterer.AutoConvertSizeFromByte(peer.DownloadSpeed) + "/s");
						}
					}
				}
			}
		}
		
		#endregion
		
		#region Events
		
		void DataGridView_DownloadsSelectionChanged(object sender, EventArgs e)
		{
			this.m_bOpenContextMenuStripDownloadsGrid = true;
			
			DataGridViewRow row = DownloadsGrid.CurrentRow;
			
			if (row != null)
			{
				if (row.Cells[0].Value != null)
				{
					this.SelectedDownload = Daemons.Downloader.SearchDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
					
					this.UpdateSelectedDownloadInfo();
				}
			}
		}
		
		void ContextMenuStrip_DownloadsGridOpening(object sender, CancelEventArgs e)
		{
			while (true)
			{
				if (this.m_bOpenContextMenuStripDownloadsGrid == true)
				{
					this.m_bOpenContextMenuStripDownloadsGrid = false;
					
					break;
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}
		
		// re-start download
		void StartToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.StartToolStripMenuItemClick(sender, e); }));
			}
			else
			{
				DataGridViewRow row = DownloadsGrid.CurrentRow;
				
				// change row color
				row.DefaultCellStyle.ForeColor = Color.Blue;
				
				if (row != null)
				{
					if (row.Cells[0].Value != null)
					{
						Objects.Download download = Daemons.Downloader.SearchDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
						
						if (download != null)
						{
							download.Active = true;
						}
					}
				}
			}
		}
		
		// pause download
		void PauseToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.PauseToolStripMenuItemClick(sender, e); }));
			}
			else
			{
				DataGridViewRow row = DownloadsGrid.CurrentRow;
				
				// change row color
				row.DefaultCellStyle.ForeColor = Color.Orange;
				
				if (row != null)
				{
					if (row.Cells[0].Value != null)
					{
						Objects.Download download = Daemons.Downloader.SearchDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
						
						if (download != null)
						{
							download.Active = false;
						}
					}
				}
			}
		}
		
		// remove selected download
		void RemoveToolStripMenuItemClick(object sender, EventArgs e)
		{
			DataGridViewRow row = DownloadsGrid.CurrentRow;
			
			if (row != null)
			{
				if (row.Cells[0].Value != null)
				{
					Daemons.Downloader.RemoveDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
				}
			}
		}
		
		#endregion
	}
}
