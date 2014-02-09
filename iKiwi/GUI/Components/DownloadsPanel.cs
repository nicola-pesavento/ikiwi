using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace iKiwi.GUI
{
	/// <summary>
	/// Description of DownloadsPanel.
	/// </summary>
	public partial class DownloadsPanel : UserControl
	{
		#region Ctor
		
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
		
		public void Update()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					this.UpdateDownloadGrid();
					
					this.UpdateDownloadInfo();
				}
			));

			t.Name = "Form1UpdateDownloadsGrid";
			t.IsBackground = true;
			t.Start();
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Update the tabControl of the selected download's info.
		/// </summary>
		private void UpdateDownloadInfo()
		{
			if (this.SelectedDownload != null)
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(
						delegate {
							this.label_downloadName.Text = this.SelectedDownload.Name;
							this.label_downloadID.Text = this.SelectedDownload.SHA1;
							this.label_downloadSize.Text = this.SelectedDownload.Size.ToString();
							this.label_downloadProgress.Text = this.SelectedDownload.Progress.ToString();
							this.label_downloadDownloadedPacks.Text = (this.SelectedDownload.ListFilePacks.Length - this.SelectedDownload.RemainingFilePacks).ToString();
							this.label_downloadUploaderPeers.Text = this.SelectedDownload.ListUploaderPeers.Count.ToString();
							this.label_downloadActive.Text = this.SelectedDownload.Active.ToString();
						}));
				}
				else
				{
					this.label_downloadName.Text = this.SelectedDownload.Name;
					this.label_downloadID.Text = this.SelectedDownload.SHA1;
					this.label_downloadSize.Text = this.SelectedDownload.Size.ToString();
					this.label_downloadProgress.Text = this.SelectedDownload.Progress.ToString();
					this.label_downloadDownloadedPacks.Text = (this.SelectedDownload.ListFilePacks.Length - this.SelectedDownload.RemainingFilePacks).ToString();
					this.label_downloadUploaderPeers.Text = this.SelectedDownload.ListUploaderPeers.Count.ToString();
					this.label_downloadActive.Text = this.SelectedDownload.Active.ToString();
				}
			}
		}
		
		/// <summary>
		/// Update the grid of the downloads.
		/// </summary>
		private void UpdateDownloadGrid()
		{
			DataGridViewRowCollection rows = this.DownloadsGrid.Rows;
			
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate { rows.Clear(); }));
			}
			else
			{
				rows.Clear();
			}
			
			for (int i = 0; i < Downloader.Count; i++)
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(delegate { rows.Add(Downloader.List[i].Name, Downloader.List[i].CurrentSize, Downloader.List[i].Progress, "SPEED", Downloader.List[i].Size, Downloader.List[i].SHA1); }));
				}
				else
				{
					rows.Add(Downloader.List[i].Name, (Downloader.List[i].Size - (Downloader.List[i].RemainingFilePacks * 16384)), Downloader.List[i].Progress, "SPEED", Downloader.List[i].Size, Downloader.List[i].SHA1);
				}
			}
		}
		
		#endregion
		
		#region Events
		
		void DataGridView_DownloadsCellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = DownloadsGrid.CurrentRow;
			
			if (row != null)
			{
				if (row.Cells[0].Value != null)
				{
					this.SelectedDownload = Downloader.SearchDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
					
					this.UpdateDownloadInfo();
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
					Downloader.RemoveDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
				}
			}
		}
		
		// re-start download
		void StartToolStripMenuItemClick(object sender, EventArgs e)
		{
			DataGridViewRow row = DownloadsGrid.CurrentRow;
			
			if (row != null)
			{
				if (row.Cells[0].Value != null)
				{
					Objects.Download download = Downloader.SearchDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
					
					if (download != null)
					{
						download.Active = true;
					}
				}
			}
		}
		
		// pause download
		void PauseToolStripMenuItemClick(object sender, EventArgs e)
		{
			DataGridViewRow row = DownloadsGrid.CurrentRow;
			
			if (row != null)
			{
				if (row.Cells[0].Value != null)
				{
					Objects.Download download = Downloader.SearchDownload(row.Cells[0].Value.ToString(), row.Cells[5].Value.ToString());
					
					if (download != null)
					{
						download.Active = false;
					}
				}
			}
		}
		
		#endregion
	}
}
