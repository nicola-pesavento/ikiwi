using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Used to update iKiwi to the last version.
	/// </summary>
	public partial class Updater : Form
	{
		#region Ctor
		
        /// <summary>
        /// default contructor
        /// </summary>
		public Updater()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Check update for iKiwi.
		/// </summary>
		/// <returns>The last version of iKiwi, return null on error.</returns>
		public static string CheckUpdate()
		{
			string reply = null;
			
			try
			{
				WebClient wc = new WebClient();

				UTF8Encoding utf8 = new UTF8Encoding();

				reply = utf8.GetString(wc.DownloadData("http://ikiwi.sourceforge.net/ikiwi-last-version.php"));
			}
			catch
			{
			}
			
			return reply;
		}
		
		/// <summary>
		/// Check and install update for iKiwi.
		/// </summary>
		public static void CheckAndInstallUpdate()
		{
			string latestVersion = CheckUpdate();
			
			if (latestVersion != null)
			{
				if (latestVersion != Global.iKiwiVersion)
				{
					// delete old update file
					
					string path = Global.iKiwiPath + "update.msi";
					
					if (File.Exists(path))
					{
						File.Delete(path);
					}
					
					// launch updater
					Application.Run(new Utilities.Updater());
				}
			}
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Download and install the update for iKiwi.
		/// </summary>
		/// <returns>If the update has been installed return true, else return false.</returns>
		private bool DownloadAndInstallUpdate()
		{
			try
			{
				this.progressBar1.Value = 0;
				
				// delete the old update file
				if (File.Exists(Global.iKiwiPath + "update.msi"))
				{
					File.Delete(Global.iKiwiPath + "update.msi");
				}
				
				WebClient webClient = new WebClient();
				
				webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
				
				webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
				
				webClient.DownloadFileAsync(new Uri("https://sourceforge.net/projects/ikiwi/files/latest"),  Global.iKiwiPath + "update.msi");
			}
			catch
			{
				return false;
			}
			
			return true;
		}
		
		private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.progressBar1.Value = e.ProgressPercentage;
		}

		private void Completed(object sender, AsyncCompletedEventArgs e)
		{
			label_Info.Text = "Download completed";
			label_Info.BackColor = System.Drawing.Color.LightGreen;
			
			DialogResult result = MessageBox.Show("Download completed. \nDo you want close iKiwi to start the installation of the latest version?", "Update iKiwi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			
			if (result == System.Windows.Forms.DialogResult.Yes)
			{
				// start the installer of iKiwi
				Process process = new Process();
				process.StartInfo.FileName = Global.iKiwiPath + "update.msi";
				process.Start();
				
				// close iKiwi
				Process.GetCurrentProcess().Kill();
			}
		}
		
		#endregion
		
		#region Events
		
		void UpdaterLoad(object sender, EventArgs e)
		{
			string latestVersion = CheckUpdate();
			
			if (latestVersion != null)
			{
				if (latestVersion == Global.iKiwiVersion)
				{
					label_Info.Text = "iKiwi is updated";
					label_Info.BackColor = System.Drawing.Color.PaleGreen;
				}
				else
				{
					label_Info.Text = "A new version of iKiwi is available";
					label_Info.BackColor = System.Drawing.Color.Orange;
				}
			}
			else
			{
				label_Info.Text = "Impossible to get the latest version";
				label_Info.BackColor = System.Drawing.Color.Tomato;
			}
		}
		
		void Button_GetUpdateClick(object sender, EventArgs e)
		{
			this.button_GetUpdate.Enabled = false;
			
			if (this.DownloadAndInstallUpdate() == true)
			{
				label_Info.Text = "Downloading...";
				label_Info.BackColor = System.Drawing.Color.LightGreen;
			}
			else
			{
				label_Info.Text = "Error";
				label_Info.BackColor = System.Drawing.Color.Tomato;
				
				this.button_GetUpdate.Enabled = true;
			}
		}
		
		#endregion
	}
}
