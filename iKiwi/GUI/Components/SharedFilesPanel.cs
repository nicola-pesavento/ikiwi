using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace iKiwi.GUI
{
	/// <summary>
	/// The panel of the shared files.
	/// </summary>
	public partial class SharedFilesPanel : UserControl
	{
		#region Ctor
		
		public SharedFilesPanel()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// The SharedFilesGrid object.
		/// </summary>
		public DataGridView SharedFilesGrid
		{
			get { return dataGridView_SharedFiles; }
			set { dataGridView_SharedFiles = value; }
		}
		
		#endregion
		
		#region Methods
		
		public void Update()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					this.UpdateSharedFilesGrid();
				}
			));

			t.Name = "Form1UpdateSharedFilesGrid";
			t.IsBackground = true;
			t.Start();
		}

		#endregion
		
		#region Private Methods
		
		/// <summary>
		/// Update the grid of the shared files.
		/// </summary>
		private void UpdateSharedFilesGrid()
		{
			DataGridViewRowCollection rows = this.SharedFilesGrid.Rows;
			
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(delegate { rows.Clear(); }));
			}
			else
			{
				rows.Clear();
			}
			
			for (int i = 0; i < FilesList.Count; i++)
			{
				if (this.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(delegate { rows.Add(FilesList.List[i].Name, FilesList.List[i].Size, FilesList.List[i].SHA1, FilesList.List[i].Path); }));
				}
				else
				{
					rows.Add(FilesList.List[i].Name, FilesList.List[i].Size, FilesList.List[i].SHA1, FilesList.List[i].Path);
				}
			}
		}
		
		#endregion
		
		#region Events
		
		void RebuildListClick(object sender, EventArgs e)
		{
			// rebuild the list
			FilesList.Build();
			
			this.Update();
		}
		
		#endregion
	}
}
