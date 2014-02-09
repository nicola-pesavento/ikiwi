namespace iKiwi.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        	this.tabControl1 = new System.Windows.Forms.TabControl();
        	this.tabPage1 = new System.Windows.Forms.TabPage();
        	this.fileSearchPanel1 = new iKiwi.GUI.Custom_components.FileSearchPanel();
        	this.label1 = new System.Windows.Forms.Label();
        	this.tabPage2 = new System.Windows.Forms.TabPage();
        	this.downloadsPanel = new iKiwi.GUI.DownloadsPanel();
        	this.tabPage3 = new System.Windows.Forms.TabPage();
        	this.peersPanel1 = new iKiwi.GUI.Custom_components.PeersPanel();
        	this.tabPage4 = new System.Windows.Forms.TabPage();
        	this.sharedFilesPanel1 = new iKiwi.GUI.SharedFilesPanel();
        	this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        	this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
        	this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
        	this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
        	this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
        	this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.toolStrip2 = new System.Windows.Forms.ToolStrip();
        	this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
        	this.toolStripLabel_NumConnectedPeer = new System.Windows.Forms.ToolStripLabel();
        	this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        	this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
        	this.toolStripLabel_DownloadSpeed = new System.Windows.Forms.ToolStripLabel();
        	this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        	this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
        	this.toolStripLabel_UploadSpeed = new System.Windows.Forms.ToolStripLabel();
        	this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        	this.toolStripImage_Status = new System.Windows.Forms.ToolStripButton();
        	this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        	this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
        	this.contextMenuStrip_NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
        	this.showIKiwiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.tabControl1.SuspendLayout();
        	this.tabPage1.SuspendLayout();
        	this.tabPage2.SuspendLayout();
        	this.tabPage3.SuspendLayout();
        	this.tabPage4.SuspendLayout();
        	this.toolStrip1.SuspendLayout();
        	this.toolStrip2.SuspendLayout();
        	this.contextMenuStrip_NotifyIcon.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// tabControl1
        	// 
        	this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
        	this.tabControl1.Controls.Add(this.tabPage1);
        	this.tabControl1.Controls.Add(this.tabPage2);
        	this.tabControl1.Controls.Add(this.tabPage3);
        	this.tabControl1.Controls.Add(this.tabPage4);
        	this.tabControl1.Location = new System.Drawing.Point(0, 33);
        	this.tabControl1.Multiline = true;
        	this.tabControl1.Name = "tabControl1";
        	this.tabControl1.SelectedIndex = 0;
        	this.tabControl1.Size = new System.Drawing.Size(584, 361);
        	this.tabControl1.TabIndex = 0;
        	// 
        	// tabPage1
        	// 
        	this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
        	this.tabPage1.Controls.Add(this.fileSearchPanel1);
        	this.tabPage1.Controls.Add(this.label1);
        	this.tabPage1.Location = new System.Drawing.Point(4, 25);
        	this.tabPage1.Name = "tabPage1";
        	this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage1.Size = new System.Drawing.Size(576, 332);
        	this.tabPage1.TabIndex = 0;
        	this.tabPage1.Text = "Search";
        	// 
        	// fileSearchPanel1
        	// 
        	this.fileSearchPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.fileSearchPanel1.Location = new System.Drawing.Point(0, 0);
        	this.fileSearchPanel1.Name = "fileSearchPanel1";
        	this.fileSearchPanel1.Size = new System.Drawing.Size(576, 332);
        	this.fileSearchPanel1.TabIndex = 2;
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(3, 3);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(0, 13);
        	this.label1.TabIndex = 1;
        	// 
        	// tabPage2
        	// 
        	this.tabPage2.Controls.Add(this.downloadsPanel);
        	this.tabPage2.Location = new System.Drawing.Point(4, 25);
        	this.tabPage2.Name = "tabPage2";
        	this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage2.Size = new System.Drawing.Size(576, 332);
        	this.tabPage2.TabIndex = 1;
        	this.tabPage2.Text = "Downloads";
        	this.tabPage2.UseVisualStyleBackColor = true;
        	// 
        	// downloadsPanel
        	// 
        	this.downloadsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.downloadsPanel.Location = new System.Drawing.Point(0, 0);
        	this.downloadsPanel.Name = "downloadsPanel";
        	this.downloadsPanel.SelectedDownload = null;
        	this.downloadsPanel.Size = new System.Drawing.Size(576, 332);
        	this.downloadsPanel.TabIndex = 0;
        	// 
        	// tabPage3
        	// 
        	this.tabPage3.Controls.Add(this.peersPanel1);
        	this.tabPage3.Location = new System.Drawing.Point(4, 25);
        	this.tabPage3.Name = "tabPage3";
        	this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage3.Size = new System.Drawing.Size(576, 332);
        	this.tabPage3.TabIndex = 2;
        	this.tabPage3.Text = "Peers";
        	this.tabPage3.UseVisualStyleBackColor = true;
        	// 
        	// peersPanel1
        	// 
        	this.peersPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.peersPanel1.Location = new System.Drawing.Point(0, 0);
        	this.peersPanel1.Name = "peersPanel1";
        	this.peersPanel1.Size = new System.Drawing.Size(576, 332);
        	this.peersPanel1.TabIndex = 0;
        	// 
        	// tabPage4
        	// 
        	this.tabPage4.Controls.Add(this.sharedFilesPanel1);
        	this.tabPage4.Location = new System.Drawing.Point(4, 25);
        	this.tabPage4.Name = "tabPage4";
        	this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage4.Size = new System.Drawing.Size(576, 332);
        	this.tabPage4.TabIndex = 3;
        	this.tabPage4.Text = "Shared Files";
        	this.tabPage4.UseVisualStyleBackColor = true;
        	// 
        	// sharedFilesPanel1
        	// 
        	this.sharedFilesPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.sharedFilesPanel1.Location = new System.Drawing.Point(0, 0);
        	this.sharedFilesPanel1.Name = "sharedFilesPanel1";
        	this.sharedFilesPanel1.Size = new System.Drawing.Size(576, 332);
        	this.sharedFilesPanel1.TabIndex = 0;
        	// 
        	// toolStrip1
        	// 
        	this.toolStrip1.AutoSize = false;
        	this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.toolStripButton4,
        	        	        	this.toolStripButton3,
        	        	        	this.toolStripButton2,
        	        	        	this.toolStripButton6,
        	        	        	this.toolStripButton1});
        	this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
        	this.toolStrip1.Location = new System.Drawing.Point(0, 0);
        	this.toolStrip1.Name = "toolStrip1";
        	this.toolStrip1.Size = new System.Drawing.Size(584, 58);
        	this.toolStrip1.TabIndex = 3;
        	this.toolStrip1.Text = "toolStrip1";
        	// 
        	// toolStripButton4
        	// 
        	this.toolStripButton4.AutoSize = false;
        	this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripButton4.Image = global::iKiwi.Properties.Resources.Search_50;
        	this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        	this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripButton4.Name = "toolStripButton4";
        	this.toolStripButton4.Size = new System.Drawing.Size(55, 55);
        	this.toolStripButton4.Text = "toolStripButton1";
        	this.toolStripButton4.ToolTipText = "Search";
        	this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
        	// 
        	// toolStripButton3
        	// 
        	this.toolStripButton3.AutoSize = false;
        	this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripButton3.Image = global::iKiwi.Properties.Resources.Download_50;
        	this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        	this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripButton3.Name = "toolStripButton3";
        	this.toolStripButton3.Size = new System.Drawing.Size(55, 55);
        	this.toolStripButton3.Text = "toolStripButton1";
        	this.toolStripButton3.ToolTipText = "Downloads";
        	this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
        	// 
        	// toolStripButton2
        	// 
        	this.toolStripButton2.AutoSize = false;
        	this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripButton2.Image = global::iKiwi.Properties.Resources.Peers_50;
        	this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        	this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripButton2.Name = "toolStripButton2";
        	this.toolStripButton2.Size = new System.Drawing.Size(55, 55);
        	this.toolStripButton2.Text = "toolStripButton1";
        	this.toolStripButton2.ToolTipText = "Peers";
        	this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
        	// 
        	// toolStripButton6
        	// 
        	this.toolStripButton6.AutoSize = false;
        	this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripButton6.Image = global::iKiwi.Properties.Resources.Shared_files_50;
        	this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        	this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripButton6.Name = "toolStripButton6";
        	this.toolStripButton6.Size = new System.Drawing.Size(55, 55);
        	this.toolStripButton6.Text = "toolStripButton1";
        	this.toolStripButton6.ToolTipText = "Shared Files";
        	this.toolStripButton6.Click += new System.EventHandler(this.ToolStripButton6Click);
        	// 
        	// toolStripButton1
        	// 
        	this.toolStripButton1.AutoSize = false;
        	this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripButton1.Image = global::iKiwi.Properties.Resources.Tools_50;
        	this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
        	this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripButton1.Name = "toolStripButton1";
        	this.toolStripButton1.Size = new System.Drawing.Size(55, 55);
        	this.toolStripButton1.Text = "toolStripButton1";
        	this.toolStripButton1.ToolTipText = "Tools";
        	this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
        	// 
        	// timer1
        	// 
        	this.timer1.Enabled = true;
        	this.timer1.Interval = 1000;
        	this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
        	// 
        	// toolStrip2
        	// 
        	this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        	this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.toolStripLabel1,
        	        	        	this.toolStripLabel_NumConnectedPeer,
        	        	        	this.toolStripSeparator1,
        	        	        	this.toolStripLabel2,
        	        	        	this.toolStripLabel_DownloadSpeed,
        	        	        	this.toolStripSeparator2,
        	        	        	this.toolStripLabel3,
        	        	        	this.toolStripLabel_UploadSpeed,
        	        	        	this.toolStripSeparator3,
        	        	        	this.toolStripImage_Status,
        	        	        	this.toolStripSeparator4});
        	this.toolStrip2.Location = new System.Drawing.Point(0, 394);
        	this.toolStrip2.Name = "toolStrip2";
        	this.toolStrip2.Size = new System.Drawing.Size(584, 25);
        	this.toolStrip2.TabIndex = 4;
        	this.toolStrip2.Text = "toolStrip2";
        	// 
        	// toolStripLabel1
        	// 
        	this.toolStripLabel1.Name = "toolStripLabel1";
        	this.toolStripLabel1.Size = new System.Drawing.Size(110, 22);
        	this.toolStripLabel1.Text = "Connected peers:";
        	// 
        	// toolStripLabel_NumConnectedPeer
        	// 
        	this.toolStripLabel_NumConnectedPeer.Name = "toolStripLabel_NumConnectedPeer";
        	this.toolStripLabel_NumConnectedPeer.Size = new System.Drawing.Size(15, 22);
        	this.toolStripLabel_NumConnectedPeer.Text = "0";
        	// 
        	// toolStripSeparator1
        	// 
        	this.toolStripSeparator1.Name = "toolStripSeparator1";
        	this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
        	// 
        	// toolStripLabel2
        	// 
        	this.toolStripLabel2.Name = "toolStripLabel2";
        	this.toolStripLabel2.Size = new System.Drawing.Size(20, 22);
        	this.toolStripLabel2.Text = "D:";
        	// 
        	// toolStripLabel_DownloadSpeed
        	// 
        	this.toolStripLabel_DownloadSpeed.Name = "toolStripLabel_DownloadSpeed";
        	this.toolStripLabel_DownloadSpeed.Size = new System.Drawing.Size(32, 22);
        	this.toolStripLabel_DownloadSpeed.Text = "0,00";
        	// 
        	// toolStripSeparator2
        	// 
        	this.toolStripSeparator2.Name = "toolStripSeparator2";
        	this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
        	// 
        	// toolStripLabel3
        	// 
        	this.toolStripLabel3.Name = "toolStripLabel3";
        	this.toolStripLabel3.Size = new System.Drawing.Size(20, 22);
        	this.toolStripLabel3.Text = "U:";
        	// 
        	// toolStripLabel_UploadSpeed
        	// 
        	this.toolStripLabel_UploadSpeed.Name = "toolStripLabel_UploadSpeed";
        	this.toolStripLabel_UploadSpeed.Size = new System.Drawing.Size(32, 22);
        	this.toolStripLabel_UploadSpeed.Text = "0,00";
        	// 
        	// toolStripSeparator3
        	// 
        	this.toolStripSeparator3.Name = "toolStripSeparator3";
        	this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
        	// 
        	// toolStripImage_Status
        	// 
        	this.toolStripImage_Status.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
        	this.toolStripImage_Status.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        	this.toolStripImage_Status.Image = ((System.Drawing.Image)(resources.GetObject("toolStripImage_Status.Image")));
        	this.toolStripImage_Status.ImageTransparentColor = System.Drawing.Color.Magenta;
        	this.toolStripImage_Status.Name = "toolStripImage_Status";
        	this.toolStripImage_Status.Size = new System.Drawing.Size(23, 22);
        	// 
        	// toolStripSeparator4
        	// 
        	this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
        	this.toolStripSeparator4.Name = "toolStripSeparator4";
        	this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
        	// 
        	// notifyIcon1
        	// 
        	this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip_NotifyIcon;
        	this.notifyIcon1.Icon = global::iKiwi.Properties.Resources.ikiwi;
        	this.notifyIcon1.Text = "iKiwi";
        	this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1DoubleClick);
        	// 
        	// contextMenuStrip_NotifyIcon
        	// 
        	this.contextMenuStrip_NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.showIKiwiToolStripMenuItem,
        	        	        	this.exitToolStripMenuItem});
        	this.contextMenuStrip_NotifyIcon.Name = "contextMenuStrip_NotifyIcon";
        	this.contextMenuStrip_NotifyIcon.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
        	this.contextMenuStrip_NotifyIcon.Size = new System.Drawing.Size(153, 70);
        	// 
        	// showIKiwiToolStripMenuItem
        	// 
        	this.showIKiwiToolStripMenuItem.Name = "showIKiwiToolStripMenuItem";
        	this.showIKiwiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        	this.showIKiwiToolStripMenuItem.Text = "Show iKiwi";
        	this.showIKiwiToolStripMenuItem.Click += new System.EventHandler(this.ShowIKiwiToolStripMenuItemClick);
        	// 
        	// exitToolStripMenuItem
        	// 
        	this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        	this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        	this.exitToolStripMenuItem.Text = "Exit";
        	this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(584, 419);
        	this.Controls.Add(this.toolStrip1);
        	this.Controls.Add(this.toolStrip2);
        	this.Controls.Add(this.tabControl1);
        	this.Icon = global::iKiwi.Properties.Resources.ikiwi;
        	this.MinimumSize = new System.Drawing.Size(400, 200);
        	this.Name = "MainForm";
        	this.Text = " iKiwi";
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
        	this.Shown += new System.EventHandler(this.MainFormShown);
        	this.tabControl1.ResumeLayout(false);
        	this.tabPage1.ResumeLayout(false);
        	this.tabPage1.PerformLayout();
        	this.tabPage2.ResumeLayout(false);
        	this.tabPage3.ResumeLayout(false);
        	this.tabPage4.ResumeLayout(false);
        	this.toolStrip1.ResumeLayout(false);
        	this.toolStrip1.PerformLayout();
        	this.toolStrip2.ResumeLayout(false);
        	this.toolStrip2.PerformLayout();
        	this.contextMenuStrip_NotifyIcon.ResumeLayout(false);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private iKiwi.GUI.Custom_components.PeersPanel peersPanel1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showIKiwiToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_NotifyIcon;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private iKiwi.GUI.SharedFilesPanel sharedFilesPanel1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private iKiwi.GUI.DownloadsPanel downloadsPanel;
        private System.Windows.Forms.ToolStripButton toolStripImage_Status;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_UploadSpeed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_DownloadSpeed;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_NumConnectedPeer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.Timer timer1;

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private Custom_components.FileSearchPanel fileSearchPanel1;
    }
}