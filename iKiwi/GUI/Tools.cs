using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace iKiwi.GUI
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Form_Tools : Form
	{
		#region Properties
		
		/// <summary>
		/// Indicates if Tools form is open.
		/// </summary>
		public static bool Open
		{
			get { return _Open; }
			set { _Open = value; }
		}
		private static bool _Open = false;
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Select a folder and write it in the textbox.
		/// </summary>
		/// <param name="textbox">TextBox object.</param>
		public void SelectFolder(TextBox textbox)
		{
			FolderBrowserDialog f = new FolderBrowserDialog();
			
			f.ShowDialog();
			
			if(f.SelectedPath != "")
			{
				textbox.Text = f.SelectedPath + @"\";
			}
		}
		
		#endregion
		
		#region Ctor
		
		/// <summary>
		/// 
		/// </summary>
		public Form_Tools()
		{
			InitializeComponent();
			
			Open = true;
		}
		
		#endregion
		
		#region Events

		// load data in the form
		private void Form_Settings_Load(object sender, EventArgs e)
		{
			this.textBox_IpAddress.Text = Global.MyRemoteIP;
			
			this.textBox_ListeningPort.Text = Global.ListeningPort.ToString();
			
			this.textBox_Info_MyPeerID.Text = Global.MyPeerID;
			
			this.textBox_TempDir.Text = Global.TempDirectory;
			
			this.textBox_SharedDir.Text = Global.SharedDirectory;
			
			this.checkBox_iKiwiStart.Checked = Global.StartIkiwiAtSystemStartup;
			
			this.checkBox_iKiwiStartMinimized.Checked = Global.StartIkiwiMinimized;
			
			this.label_iKiwiVersion.Text = Global.iKiwiVersion;
			
			this.checkBox_MessageEncryptionEnabled.Checked = Global.MessageEncryptionEnabled;
			
			this.checkBox_AcceptNotEncryptedMessages.Checked = Global.AcceptNotEncryptedMessages;
			
			this.checkBox_useUpnp.Checked = Global.UseUpnp;
			
			if (this.checkBox_MessageEncryptionEnabled.Checked == true)
			{
				this.checkBox_AcceptNotEncryptedMessages.Enabled = true;
			}
			else
			{
				this.checkBox_AcceptNotEncryptedMessages.Enabled = false;
			}
			
			if (this.checkBox_useUpnp.Checked == true)
			{
				this.textBox_ListeningPort.ReadOnly = true;
				
				this.button_CheckOpenPort.Enabled = false;
			}
			else
			{
				this.textBox_ListeningPort.ReadOnly = false;
				
				this.button_CheckOpenPort.Enabled = true;
			}
		}
		
		private void button_Apply_Click(object sender, EventArgs e)
		{
			// save the settings
			Global.MyRemoteIP = this.textBox_IpAddress.Text;
			Global.ListeningPort = int.Parse(this.textBox_ListeningPort.Text);
			Global.SharedDirectory = this.textBox_SharedDir.Text;
			Global.TempDirectory = this.textBox_TempDir.Text;
			Global.StartIkiwiAtSystemStartup = this.checkBox_iKiwiStart.Checked;
			Global.StartIkiwiMinimized = this.checkBox_iKiwiStartMinimized.Checked;
			Global.MessageEncryptionEnabled = this.checkBox_MessageEncryptionEnabled.Checked;
			Global.AcceptNotEncryptedMessages = this.checkBox_AcceptNotEncryptedMessages.Checked;
			Global.UseUpnp = this.checkBox_useUpnp.Checked;
			
			// control the listening port
			Utilities.PortChecker.CheckListeningPort();

			// apply the settings
			Utilities.Configurator.ApplySettings();
			
			// save the settings
			Utilities.Configurator.SaveAll();
			
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			SelectFolder(this.textBox_TempDir);
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			SelectFolder(this.textBox_SharedDir);
		}
		
		void Button_CheckOpenPortClick(object sender, EventArgs e)
		{
			this.label_PortStatus.BackColor = System.Drawing.Color.Transparent;
			this.label_PortStatus.Text = "Testing...";
			
			BackgroundWorker worker = new BackgroundWorker();
			
			worker.DoWork += delegate(object _sender, DoWorkEventArgs _e)
			{
				string text = string.Empty;
				
				Color color = Color.White;
				
				if (Utilities.PortChecker.IsCanUsePort(int.Parse(this.textBox_ListeningPort.Text)))
				{
					text = "The port is open";
					color = System.Drawing.Color.PaleGreen;
				}
				else
				{
					text = "The port is closed";
					color = System.Drawing.Color.Tomato;
				}
				
				this.Invoke(new MethodInvoker(
					delegate {
						this.label_PortStatus.Text = text;
						this.label_PortStatus.BackColor = color;
					}));
			};
			
			worker.RunWorkerAsync();
		}
		
		void Button_CheckUpdateClick(object sender, EventArgs e)
		{
			this.label_Update.BackColor = System.Drawing.Color.Transparent;
			this.label_Update.Text = "Checking...";
			
			string latestVersion = Utilities.Updater.CheckUpdate();
			
			if (latestVersion != null)
			{
				if (latestVersion == Global.iKiwiVersion)
				{
					this.label_Update.Text = "iKiwi is updated";
					this.label_Update.BackColor = System.Drawing.Color.PaleGreen;
				}
				else
				{
					this.label_Update.Text = "A new version of iKiwi is available";
					this.label_Update.BackColor = System.Drawing.Color.Orange;
				}
			}
			else
			{
				this.label_Update.Text = "Impossible to get the latest version";
				this.label_Update.BackColor = System.Drawing.Color.Tomato;
			}
		}
		
		void Button5Click(object sender, EventArgs e)
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// start Updater
					Application.Run(new Utilities.Updater());
				}
			));
			
			t.Name = "Updater";
			t.IsBackground = true;
			t.Start();
		}
		
		void Button6Click(object sender, EventArgs e)
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// start console log
					Application.Run(new GUI.ConsoleLog());
				}
			));
			
			t.Name = "ConsoleLog";
			t.IsBackground = true;
			t.Start();
		}
		
		void Button_SupportClick(object sender, EventArgs e)
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					// start supporter
					Application.Run(new Utilities.Supporter());
				}
			));
			
			t.Name = "Supporter";
			t.IsBackground = true;
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
		}
		
		void Form_ToolsFormClosed(object sender, FormClosedEventArgs e)
		{
			Open = false;
		}
		
		void CheckBox_MessageEncryptionEnabledCheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBox_MessageEncryptionEnabled.Checked == true)
			{
				this.checkBox_AcceptNotEncryptedMessages.Enabled = true;
			}
			else
			{
				this.checkBox_AcceptNotEncryptedMessages.Enabled = false;
			}
		}
		
		void CheckBox_useUpnpCheckedChanged(object sender, EventArgs e)
        {
        	if (this.checkBox_useUpnp.Checked == true)
			{
				this.textBox_ListeningPort.ReadOnly = true;
				
				this.button_CheckOpenPort.Enabled = false;
			}
			else
			{
				this.textBox_ListeningPort.ReadOnly = false;
				
				this.button_CheckOpenPort.Enabled = true;
			}
        }
		
		#endregion
	}
}
