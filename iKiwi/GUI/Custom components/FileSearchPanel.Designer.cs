
namespace iKiwi.GUI.Custom_components
{
	partial class FileSearchPanel
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.button_SearchFile = new System.Windows.Forms.Button();
			this.dataGridView_FilesFound = new System.Windows.Forms.DataGridView();
			this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip_FilesFoundGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.textBox_SearchFile = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_FilesFound)).BeginInit();
			this.contextMenuStrip_FilesFoundGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_SearchFile
			// 
			this.button_SearchFile.Location = new System.Drawing.Point(0, 32);
			this.button_SearchFile.Name = "button_SearchFile";
			this.button_SearchFile.Size = new System.Drawing.Size(75, 23);
			this.button_SearchFile.TabIndex = 8;
			this.button_SearchFile.Text = "Search";
			this.button_SearchFile.UseVisualStyleBackColor = true;
			this.button_SearchFile.Click += new System.EventHandler(this.Button_SearchFileClick);
			// 
			// dataGridView_FilesFound
			// 
			this.dataGridView_FilesFound.AllowUserToResizeRows = false;
			this.dataGridView_FilesFound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_FilesFound.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView_FilesFound.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView_FilesFound.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView_FilesFound.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dataGridView_FilesFound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_FilesFound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Column_Name,
									this.Column_Size,
									this.Column_ID});
			this.dataGridView_FilesFound.ContextMenuStrip = this.contextMenuStrip_FilesFoundGrid;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_FilesFound.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_FilesFound.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView_FilesFound.Location = new System.Drawing.Point(0, 61);
			this.dataGridView_FilesFound.Name = "dataGridView_FilesFound";
			this.dataGridView_FilesFound.ReadOnly = true;
			this.dataGridView_FilesFound.RowHeadersVisible = false;
			this.dataGridView_FilesFound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_FilesFound.Size = new System.Drawing.Size(500, 239);
			this.dataGridView_FilesFound.TabIndex = 7;
			this.dataGridView_FilesFound.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView_FilesFoundMouseDown);
			// 
			// Column_Name
			// 
			this.Column_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column_Name.HeaderText = "Name";
			this.Column_Name.Name = "Column_Name";
			this.Column_Name.ReadOnly = true;
			// 
			// Column_Size
			// 
			this.Column_Size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column_Size.HeaderText = "Size";
			this.Column_Size.Name = "Column_Size";
			this.Column_Size.ReadOnly = true;
			// 
			// Column_ID
			// 
			this.Column_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column_ID.HeaderText = "ID";
			this.Column_ID.Name = "Column_ID";
			this.Column_ID.ReadOnly = true;
			// 
			// contextMenuStrip_FilesFoundGrid
			// 
			this.contextMenuStrip_FilesFoundGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.downloadToolStripMenuItem});
			this.contextMenuStrip_FilesFoundGrid.Name = "contextMenuStrip1";
			this.contextMenuStrip_FilesFoundGrid.Size = new System.Drawing.Size(153, 48);
			this.contextMenuStrip_FilesFoundGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_FilesFoundGridOpening);
			// 
			// downloadToolStripMenuItem
			// 
			this.downloadToolStripMenuItem.Image = global::iKiwi.Properties.Resources.Download_50;
			this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
			this.downloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.downloadToolStripMenuItem.Text = "Download";
			this.downloadToolStripMenuItem.Click += new System.EventHandler(this.DownloadToolStripMenuItemClick);
			// 
			// textBox_SearchFile
			// 
			this.textBox_SearchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_SearchFile.Location = new System.Drawing.Point(3, 6);
			this.textBox_SearchFile.Name = "textBox_SearchFile";
			this.textBox_SearchFile.Size = new System.Drawing.Size(494, 20);
			this.textBox_SearchFile.TabIndex = 6;
			this.textBox_SearchFile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_SearchFileKeyUp);
			// 
			// FileSearchPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button_SearchFile);
			this.Controls.Add(this.dataGridView_FilesFound);
			this.Controls.Add(this.textBox_SearchFile);
			this.Name = "FileSearchPanel";
			this.Size = new System.Drawing.Size(500, 300);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_FilesFound)).EndInit();
			this.contextMenuStrip_FilesFoundGrid.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_FilesFoundGrid;
		private System.Windows.Forms.TextBox textBox_SearchFile;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_ID;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_Size;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
		private System.Windows.Forms.DataGridView dataGridView_FilesFound;
		private System.Windows.Forms.Button button_SearchFile;
	}
}
