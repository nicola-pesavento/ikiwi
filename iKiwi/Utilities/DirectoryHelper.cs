using System;
using System.IO;
using System.Windows.Forms;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Directory Helper.
	/// </summary>
	public partial class DirectoryHelper : Form
	{
		#region Ctor
		
        /// <summary>
        /// 
        /// </summary>
		public DirectoryHelper()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Select a folder and write it in the textbox.
		/// </summary>
		/// <param name="textbox">TextBox object.</param>
		private void SelectFolder(TextBox textbox)
		{
			FolderBrowserDialog f = new FolderBrowserDialog();
			
			f.ShowDialog();
			
			if(f.SelectedPath != "")
			{
				textbox.Text = f.SelectedPath + @"\";
			}
		}
		
		#endregion
		
		#region Events
		
		void Button4Click(object sender, EventArgs e)
		{
			SelectFolder(this.textBox_SharedDirectory);
		}
		
		void Button_SaveAndCloseClick(object sender, EventArgs e)
		{
			if (Directory.Exists(this.textBox_SharedDirectory.Text))
			{
				Global.SharedDirectory = this.textBox_SharedDirectory.Text;
				
				Configurator.SaveAll();
				
				this.Close();
			}
		}
		
		#endregion
	}
}
