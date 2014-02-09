namespace iKiwi.GUI
{
	partial class SharedFilesPanel
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.RebuildList = new System.Windows.Forms.Button();
			this.dataGridView_SharedFiles = new System.Windows.Forms.DataGridView();
			this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_SharedFiles)).BeginInit();
			this.SuspendLayout();
			// 
			// RebuildList
			// 
			this.RebuildList.Location = new System.Drawing.Point(4, 4);
			this.RebuildList.Name = "RebuildList";
			this.RebuildList.Size = new System.Drawing.Size(75, 23);
			this.RebuildList.TabIndex = 8;
			this.RebuildList.Text = "Rebuild List";
			this.RebuildList.UseVisualStyleBackColor = true;
			this.RebuildList.Click += new System.EventHandler(this.RebuildListClick);
			// 
			// dataGridView_SharedFiles
			// 
			this.dataGridView_SharedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_SharedFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView_SharedFiles.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView_SharedFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView_SharedFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dataGridView_SharedFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_SharedFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.FileName,
									this.dataGridViewTextBoxColumn2,
									this.dataGridViewTextBoxColumn3,
									this.Path});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_SharedFiles.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_SharedFiles.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView_SharedFiles.Location = new System.Drawing.Point(0, 33);
			this.dataGridView_SharedFiles.Name = "dataGridView_SharedFiles";
			this.dataGridView_SharedFiles.ReadOnly = true;
			this.dataGridView_SharedFiles.RowHeadersVisible = false;
			this.dataGridView_SharedFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_SharedFiles.Size = new System.Drawing.Size(500, 267);
			this.dataGridView_SharedFiles.TabIndex = 9;
			// 
			// FileName
			// 
			this.FileName.HeaderText = "Name";
			this.FileName.Name = "FileName";
			this.FileName.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.HeaderText = "Size";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.HeaderText = "ID";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// Path
			// 
			this.Path.HeaderText = "Path";
			this.Path.Name = "Path";
			this.Path.ReadOnly = true;
			// 
			// SharedFilesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView_SharedFiles);
			this.Controls.Add(this.RebuildList);
			this.Name = "SharedFilesPanel";
			this.Size = new System.Drawing.Size(500, 300);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_SharedFiles)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.DataGridViewTextBoxColumn Path;
		private System.Windows.Forms.Button RebuildList;
		private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridView dataGridView_SharedFiles;
	}
}
