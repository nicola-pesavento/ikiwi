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
		
		/// <summary>
		/// 
		/// </summary>
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
		
		/// <summary>
		/// Update.
		/// </summary>
		public new void Update()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					if (Lists.FilesList.BuildingProgress != 100)
					{
						string originalText = this.RebuildList.Text;
						
						this.Invoke(new MethodInvoker(
							delegate
							{
								// show the progress bar
								this.progressBar_BuildingProgress.Value = 0;
								this.progressBar_BuildingProgress.Visible = true;
								
								// lock the rebuild button
								this.RebuildList.Enabled = false;
								this.RebuildList.Text = "Building 0%";
							}));
						
						// control if the list of the shared files is completed or it is building its
						while(true)
						{
							this.Invoke(new MethodInvoker(
								delegate
								{
									this.progressBar_BuildingProgress.Value = Lists.FilesList.BuildingProgress;
									
									this.RebuildList.Text = "Building " + Lists.FilesList.BuildingProgress.ToString() + "%";
								}));
							
							
							Thread.Sleep(500);
							
							if (this.progressBar_BuildingProgress.Value == 100)
							{
								this.Invoke(new MethodInvoker(
									delegate
									{
										// hide the progress bar
										this.progressBar_BuildingProgress.Visible = false;
										
										// unlock the rebuild button
										this.RebuildList.Text = originalText;
										this.RebuildList.Enabled = true;
									}));
								
								break;
							}
						}
					}
					
					// update the grid
					this.UpdateSharedFilesGrid();
				}
			));

			t.Name = "UpdateSharedFilesGrid";
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
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() => { this.UpdateSharedFilesGrid(); }));
			}
			else
			{
				DataGridViewRowCollection rows = this.SharedFilesGrid.Rows;
				
				bool updateAllOtherRows = false;

				int i = 0;
				
				for (int a = (rows.Count - 1); a > 0; a--, i++)
				{
					if (i < Lists.FilesList.Count)
					{
						try
						{
							if (updateAllOtherRows == true || rows[i].Cells[2].Value.ToString() != Lists.FilesList.List[i].SHA1)
							{
								rows[i].Cells[0].Value = Lists.FilesList.List[i].Name;
								rows[i].Cells[1].Value = Utilities.Converterer.AutoConvertSizeFromByte(Lists.FilesList.List[i].Size);
								rows[i].Cells[2].Value = Lists.FilesList.List[i].SHA1;
								rows[i].Cells[3].Value = Lists.FilesList.List[i].Path;

								// update all the other rows
								updateAllOtherRows = true;
							}
						}
						catch
						{
						}
					}
					else
					{
						// remove the extra rows
						for (int n = (rows.Count - 1) - i; n > 0; n--)
						{
							rows.RemoveAt(i);
						}

						break;
					}
				}
				
				// if necessary add new rows
				for (; i < Lists.FilesList.Count; i++)
				{
					rows.Add(Lists.FilesList.List[i].Name, Utilities.Converterer.AutoConvertSizeFromByte(Lists.FilesList.List[i].Size), Lists.FilesList.List[i].SHA1, Lists.FilesList.List[i].Path);
				}
			}
		}
		
		#endregion
		
		#region Events
		
		void RebuildListClick(object sender, EventArgs e)
		{
			// rebuild the list
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					Lists.FilesList.Build();
				}
			));

			t.Name = "SharedFilesGridPanel_BuildingList";
			t.IsBackground = true;
			t.Start();
			
			Thread.Sleep(1000);
			
			this.Update();
		}
		
		#endregion
	}
}
