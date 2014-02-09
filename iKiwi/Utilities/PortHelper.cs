using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace iKiwi.Utilities
{
	/// <summary>
	/// PortHelper.
	/// </summary>
	public partial class PortHelper : Form
	{
		#region Ctor
		
		/// <summary>
		/// 
		/// </summary>
		public PortHelper()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Starts port helper.
		/// </summary>
		public static void Start()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					Application.Run(new Utilities.PortHelper());
				}
			));
			
			t.Name = "PortHelper";
			t.IsBackground = true;
			t.Start();
		}
		
		#endregion
		
		#region Events
		
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
		
		void Button_PortForwardClick(object sender, EventArgs e)
		{
			Process.Start("http://portforward.com/");
		}
		
		void Button_SavePortClick(object sender, EventArgs e)
		{
			Global.ListeningPort = int.Parse(this.textBox_ListeningPort.Text);
			
			Utilities.Configurator.SaveAll();
			
			this.Close();
		}
		
		#endregion
	}
}
