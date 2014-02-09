namespace iKiwi.GUI
{
	partial class DownloadsPanel
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView_Downloads = new System.Windows.Forms.DataGridView();
			this.Column_FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_Completed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_Progress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip_DownloadsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView_SelectedDownloadPeers = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column_DownloadSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.General = new System.Windows.Forms.TabPage();
			this.label_downloadDownloadSpeed = new System.Windows.Forms.Label();
			this.label_8 = new System.Windows.Forms.Label();
			this.label_downloadActive = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label_downloadUploaderPeers = new System.Windows.Forms.Label();
			this.label_downloadDownloadedPacks = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label_downloadProgress = new System.Windows.Forms.Label();
			this.label_downloadSize = new System.Windows.Forms.Label();
			this.label_downloadID = new System.Windows.Forms.Label();
			this.label_downloadName = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Peers = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Downloads)).BeginInit();
			this.contextMenuStrip_DownloadsGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_SelectedDownloadPeers)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.General.SuspendLayout();
			this.Peers.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView_Downloads
			// 
			this.dataGridView_Downloads.AllowUserToResizeRows = false;
			this.dataGridView_Downloads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_Downloads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView_Downloads.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView_Downloads.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView_Downloads.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dataGridView_Downloads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Downloads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Column_FileName,
									this.Column_Completed,
									this.Column_Progress,
									this.Column_Speed,
									this.dataGridViewTextBoxColumn2,
									this.dataGridViewTextBoxColumn3});
			this.dataGridView_Downloads.ContextMenuStrip = this.contextMenuStrip_DownloadsGrid;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Downloads.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_Downloads.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView_Downloads.Location = new System.Drawing.Point(0, 0);
			this.dataGridView_Downloads.Name = "dataGridView_Downloads";
			this.dataGridView_Downloads.ReadOnly = true;
			this.dataGridView_Downloads.RowHeadersVisible = false;
			this.dataGridView_Downloads.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_Downloads.Size = new System.Drawing.Size(570, 165);
			this.dataGridView_Downloads.TabIndex = 6;
			this.dataGridView_Downloads.SelectionChanged += new System.EventHandler(this.DataGridView_DownloadsSelectionChanged);
			// 
			// Column_FileName
			// 
			this.Column_FileName.HeaderText = "File Name";
			this.Column_FileName.Name = "Column_FileName";
			this.Column_FileName.ReadOnly = true;
			// 
			// Column_Completed
			// 
			this.Column_Completed.HeaderText = "Completed";
			this.Column_Completed.Name = "Column_Completed";
			this.Column_Completed.ReadOnly = true;
			// 
			// Column_Progress
			// 
			this.Column_Progress.HeaderText = "Progress";
			this.Column_Progress.Name = "Column_Progress";
			this.Column_Progress.ReadOnly = true;
			// 
			// Column_Speed
			// 
			this.Column_Speed.HeaderText = "Speed";
			this.Column_Speed.Name = "Column_Speed";
			this.Column_Speed.ReadOnly = true;
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
			// contextMenuStrip_DownloadsGrid
			// 
			this.contextMenuStrip_DownloadsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.startToolStripMenuItem,
									this.pauseToolStripMenuItem,
									this.removeToolStripMenuItem});
			this.contextMenuStrip_DownloadsGrid.Name = "contextMenuStrip_DownloadsGrid";
			this.contextMenuStrip_DownloadsGrid.ShowImageMargin = false;
			this.contextMenuStrip_DownloadsGrid.Size = new System.Drawing.Size(128, 92);
			this.contextMenuStrip_DownloadsGrid.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_DownloadsGridOpening);
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItemClick);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.pauseToolStripMenuItem.Text = "Pause";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.PauseToolStripMenuItemClick);
			// 
			// removeToolStripMenuItem
			// 
			this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
			this.removeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.removeToolStripMenuItem.Text = "Remove";
			this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItemClick);
			// 
			// dataGridView_SelectedDownloadPeers
			// 
			this.dataGridView_SelectedDownloadPeers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_SelectedDownloadPeers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView_SelectedDownloadPeers.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView_SelectedDownloadPeers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView_SelectedDownloadPeers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dataGridView_SelectedDownloadPeers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_SelectedDownloadPeers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn1,
									this.Column_DownloadSpeed});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_SelectedDownloadPeers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView_SelectedDownloadPeers.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView_SelectedDownloadPeers.Location = new System.Drawing.Point(0, 0);
			this.dataGridView_SelectedDownloadPeers.Name = "dataGridView_SelectedDownloadPeers";
			this.dataGridView_SelectedDownloadPeers.ReadOnly = true;
			this.dataGridView_SelectedDownloadPeers.RowHeadersVisible = false;
			this.dataGridView_SelectedDownloadPeers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_SelectedDownloadPeers.Size = new System.Drawing.Size(562, 133);
			this.dataGridView_SelectedDownloadPeers.TabIndex = 7;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Peer";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// Column_DownloadSpeed
			// 
			this.Column_DownloadSpeed.HeaderText = "Download Speed";
			this.Column_DownloadSpeed.Name = "Column_DownloadSpeed";
			this.Column_DownloadSpeed.ReadOnly = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.General);
			this.tabControl1.Controls.Add(this.Peers);
			this.tabControl1.Location = new System.Drawing.Point(0, 171);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(570, 159);
			this.tabControl1.TabIndex = 8;
			// 
			// General
			// 
			this.General.Controls.Add(this.label_downloadDownloadSpeed);
			this.General.Controls.Add(this.label_8);
			this.General.Controls.Add(this.label_downloadActive);
			this.General.Controls.Add(this.label7);
			this.General.Controls.Add(this.label_downloadUploaderPeers);
			this.General.Controls.Add(this.label_downloadDownloadedPacks);
			this.General.Controls.Add(this.label6);
			this.General.Controls.Add(this.label5);
			this.General.Controls.Add(this.label_downloadProgress);
			this.General.Controls.Add(this.label_downloadSize);
			this.General.Controls.Add(this.label_downloadID);
			this.General.Controls.Add(this.label_downloadName);
			this.General.Controls.Add(this.label4);
			this.General.Controls.Add(this.label3);
			this.General.Controls.Add(this.label2);
			this.General.Controls.Add(this.label1);
			this.General.Location = new System.Drawing.Point(4, 22);
			this.General.Name = "General";
			this.General.Padding = new System.Windows.Forms.Padding(3);
			this.General.Size = new System.Drawing.Size(562, 133);
			this.General.TabIndex = 1;
			this.General.Text = "General";
			this.General.UseVisualStyleBackColor = true;
			// 
			// label_downloadDownloadSpeed
			// 
			this.label_downloadDownloadSpeed.Location = new System.Drawing.Point(409, 88);
			this.label_downloadDownloadSpeed.Name = "label_downloadDownloadSpeed";
			this.label_downloadDownloadSpeed.Size = new System.Drawing.Size(100, 23);
			this.label_downloadDownloadSpeed.TabIndex = 15;
			// 
			// label_8
			// 
			this.label_8.Location = new System.Drawing.Point(299, 88);
			this.label_8.Name = "label_8";
			this.label_8.Size = new System.Drawing.Size(104, 23);
			this.label_8.TabIndex = 14;
			this.label_8.Text = "Download speed:";
			// 
			// label_downloadActive
			// 
			this.label_downloadActive.Location = new System.Drawing.Point(409, 61);
			this.label_downloadActive.Name = "label_downloadActive";
			this.label_downloadActive.Size = new System.Drawing.Size(100, 23);
			this.label_downloadActive.TabIndex = 13;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(299, 61);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 23);
			this.label7.TabIndex = 12;
			this.label7.Text = "Active:";
			// 
			// label_downloadUploaderPeers
			// 
			this.label_downloadUploaderPeers.Location = new System.Drawing.Point(409, 34);
			this.label_downloadUploaderPeers.Name = "label_downloadUploaderPeers";
			this.label_downloadUploaderPeers.Size = new System.Drawing.Size(100, 23);
			this.label_downloadUploaderPeers.TabIndex = 11;
			// 
			// label_downloadDownloadedPacks
			// 
			this.label_downloadDownloadedPacks.Location = new System.Drawing.Point(409, 7);
			this.label_downloadDownloadedPacks.Name = "label_downloadDownloadedPacks";
			this.label_downloadDownloadedPacks.Size = new System.Drawing.Size(100, 23);
			this.label_downloadDownloadedPacks.TabIndex = 10;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(299, 34);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 23);
			this.label6.TabIndex = 9;
			this.label6.Text = "Uploader peers:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(299, 7);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 23);
			this.label5.TabIndex = 8;
			this.label5.Text = "Downloaded packets:";
			// 
			// label_downloadProgress
			// 
			this.label_downloadProgress.Location = new System.Drawing.Point(113, 88);
			this.label_downloadProgress.Name = "label_downloadProgress";
			this.label_downloadProgress.Size = new System.Drawing.Size(100, 23);
			this.label_downloadProgress.TabIndex = 7;
			// 
			// label_downloadSize
			// 
			this.label_downloadSize.AutoEllipsis = true;
			this.label_downloadSize.Location = new System.Drawing.Point(113, 61);
			this.label_downloadSize.Name = "label_downloadSize";
			this.label_downloadSize.Size = new System.Drawing.Size(180, 23);
			this.label_downloadSize.TabIndex = 6;
			// 
			// label_downloadID
			// 
			this.label_downloadID.AutoEllipsis = true;
			this.label_downloadID.Location = new System.Drawing.Point(113, 34);
			this.label_downloadID.Name = "label_downloadID";
			this.label_downloadID.Size = new System.Drawing.Size(180, 23);
			this.label_downloadID.TabIndex = 5;
			// 
			// label_downloadName
			// 
			this.label_downloadName.AutoEllipsis = true;
			this.label_downloadName.Location = new System.Drawing.Point(113, 7);
			this.label_downloadName.Name = "label_downloadName";
			this.label_downloadName.Size = new System.Drawing.Size(180, 23);
			this.label_downloadName.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 3;
			this.label4.Text = "Progress:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(7, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Size:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "ID:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// Peers
			// 
			this.Peers.Controls.Add(this.dataGridView_SelectedDownloadPeers);
			this.Peers.Location = new System.Drawing.Point(4, 22);
			this.Peers.Name = "Peers";
			this.Peers.Padding = new System.Windows.Forms.Padding(3);
			this.Peers.Size = new System.Drawing.Size(562, 133);
			this.Peers.TabIndex = 2;
			this.Peers.Text = "Peers";
			this.Peers.UseVisualStyleBackColor = true;
			// 
			// DownloadsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.dataGridView_Downloads);
			this.Name = "DownloadsPanel";
			this.Size = new System.Drawing.Size(570, 330);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Downloads)).EndInit();
			this.contextMenuStrip_DownloadsGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_SelectedDownloadPeers)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.General.ResumeLayout(false);
			this.Peers.ResumeLayout(false);
			this.ResumeLayout(false);
        }
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_DownloadSpeed;
		private System.Windows.Forms.Label label_8;
		private System.Windows.Forms.Label label_downloadDownloadSpeed;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label_downloadActive;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
		private System.Windows.Forms.Label label_downloadUploaderPeers;
		private System.Windows.Forms.Label label_downloadDownloadedPacks;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label_downloadProgress;
		private System.Windows.Forms.Label label_downloadSize;
		private System.Windows.Forms.Label label_downloadID;
		private System.Windows.Forms.Label label_downloadName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabPage Peers;
		private System.Windows.Forms.TabPage General;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridView dataGridView_SelectedDownloadPeers;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_Speed;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_Progress;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_Completed;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column_FileName;
        private System.Windows.Forms.DataGridView dataGridView_Downloads;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_DownloadsGrid;
	}
}
