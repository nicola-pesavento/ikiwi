using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using iKiwi.Utilities;

namespace iKiwi.GUI
{
	/// <summary>
	/// 
	/// </summary>
	public partial class MainForm : Form
	{
		#region Methods

		/// <summary>
		/// Updates the grid of the peers connected with this client.
		/// </summary>
		public void UpdatePeersGrid()
		{
			this.peersPanel1.Update();
		}
		
		/// <summary>
		/// Updates the grid of the files found.
		/// </summary>
		public void UpdateFilesFoundGrid()
		{
			this.fileSearchPanel1.Update();
		}
		
		/// <summary>
		/// Updates the grid of the downloads.
		/// </summary>
		public void UpdateDownloadsPanel()
		{
			this.downloadsPanel.Update();
		}
		
		/// <summary>
		/// Checks if the listening port is opened or closed.
		/// </summary>
		public void CheckListeningPort()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.CheckListeningPort(); }));
			}
			else
			{
				// control if the listening port is opened
				if (Utilities.PortChecker.IsCanUsePort(Global.ListeningPort))
				{
					toolStripImage_Status.Image = Properties.Resources.Status_OK;
					toolStripImage_Status.ToolTipText = "The listening port is open.";
				}
				else
				{
					toolStripImage_Status.Image = Properties.Resources.Status_Problem;
					toolStripImage_Status.ToolTipText = "The listening port is closed.";
				}
			}
		}

		#endregion

		#region Ctor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		public MainForm(string[] args = null)
		{
			InitializeComponent();
			
			// check if the listening port is opened or not
			this.CheckListeningPort();
			
			if (args != null && args.Length >= 1 && args[0] == "/MINIMIZED")
			{
				this.m_startMinimized = true;
				
				this.Hide();
				
				this.WindowState = FormWindowState.Minimized;
				
				this.notifyIcon1.Visible = true;
			}
		}

		#endregion
		
		#region Timers
		
		void Timer1Tick(object sender, EventArgs e)
		{
			if (Global.NewFfMessage == true)
			{
				UpdateFilesFoundGrid();
				
				Global.NewFfMessage = false;
			}
			
			if (Lists.PeersList.NewOrUpdatedOrDeletedPeer == true)
			{
				UpdatePeersGrid();
				
				Lists.PeersList.NewOrUpdatedOrDeletedPeer = false;
			}
			
			if (Daemons.Downloader.NewOrUpdatedDownload == true || this.m_updateDownloadsPanel != 0)
			{
				if (Daemons.Downloader.NewOrUpdatedDownload == true)
				{
					Daemons.Downloader.NewOrUpdatedDownload = false;
					
					this.m_updateDownloadsPanel = 3;
				}
				else
				{
					this.m_updateDownloadsPanel--;
					
					if (this.m_updateDownloadsPanel < 0)
					{
						this.m_updateDownloadsPanel = 0;
					}
				}
				
				UpdateDownloadsPanel();
			}
			
			this.toolStripLabel_NumConnectedPeer.Text = Lists.PeersList.Count.ToString();
			
			string downloadRate = Utilities.Converterer.AutoConvertSizeFromByte(Global.DownloadRate) + "/s";
			
			string uploadRate = Utilities.Converterer.AutoConvertSizeFromByte(Global.UploadRate) + "/s";
			
			this.toolStripLabel_DownloadSpeed.Text = downloadRate;
			
			this.toolStripLabel_UploadSpeed.Text = uploadRate;
			
			this.notifyIcon1.Text = "iKiwi\nDownload speed: " + downloadRate + "\nUpload speed: " + uploadRate;
		}
		
		#endregion
		
		#region Data Members
		
		private bool m_closeForm = false;
		
		private bool m_startMinimized = false;
		
		private short m_updateDownloadsPanel = 0;
		
		#endregion
		
		#region Events
		
		void MainFormShown(object sender, EventArgs e)
		{
			if (this.m_startMinimized == true)
			{
				this.Hide();
			}
		}
		
		private void ToolStripButton6Click(object sender, EventArgs e)
		{
			sharedFilesPanel1.Update();
			this.tabControl1.SelectedIndex = 3;
		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			this.tabControl1.SelectedIndex = 0;
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			this.tabControl1.SelectedIndex = 1;
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			this.tabControl1.SelectedIndex = 2;
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (GUI.Form_Tools.Open == false)
			{
				Thread t = new Thread(new ParameterizedThreadStart(
					delegate
					{
						Form_Tools fSettings = new Form_Tools();
						fSettings.ShowDialog();
						
						this.CheckListeningPort();
					}
				));
				
				t.SetApartmentState(ApartmentState.STA);
				t.Name = "Settings_Form";
				t.IsBackground = true;
				t.Start();
			}
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_closeForm == false)
			{
				e.Cancel = true;
				
				this.Hide();
				
				this.notifyIcon1.Visible = true;
			}
		}
		
		void NotifyIcon1DoubleClick(object sender, EventArgs e)
		{
			this.Show();
			
			this.WindowState = FormWindowState.Normal;
			
			this.notifyIcon1.Visible = false;
		}
		
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.notifyIcon1.Visible = false;
			
			this.m_closeForm = true;
			
			Application.Exit();
		}
		
		void ShowIKiwiToolStripMenuItemClick(object sender, EventArgs e)
		{
			this.Show();
			
			this.WindowState = FormWindowState.Normal;
			
			this.notifyIcon1.Visible = false;
		}
		
		#endregion
	}
}

