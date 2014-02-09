using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Used to report bugs.
	/// </summary>
	public partial class BugReporter : Form
	{
		#region Ctor
		
		/// <summary>
		/// Open the Bug Reporter.
		/// </summary>
		/// <param name="ex">The generated exception.</param>
		public BugReporter(Exception ex)
		{
			m_exception = ex;
			
			InitializeComponent();
		}
		
		#endregion
		
		#region Data Members
		
		private static Exception m_exception;
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Start Bug Reporter.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The exception.</param>
		public static void Start(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Utilities.UPnP.ClosePort(Global.ListeningPort, Utilities.UPnP.Protocol.TCP);
			}
			catch
			{
			}
			
			Application.Run(new Utilities.BugReporter((Exception)e.ExceptionObject));
		}
		
		#endregion
		
		#region Events
		
		void BugReporterLoad(object sender, EventArgs e)
		{
			textBox1.Text = 
				m_exception.Source + Environment.NewLine + Environment.NewLine +
				m_exception.Message + Environment.NewLine + Environment.NewLine +
				m_exception.StackTrace + Environment.NewLine + Environment.NewLine +
				m_exception.TargetSite;
		}
		
		void BugReporterFormClosed(object sender, FormClosedEventArgs e)
		{
			// close iKiwi
			Process.GetCurrentProcess().Kill();
		}
		
		void Button_ReportBugClick(object sender, EventArgs e)
		{
			Process.Start("http://sourceforge.net/tracker/?group_id=335988&atid=1407894");
		}
		
		#endregion		
	}
}
