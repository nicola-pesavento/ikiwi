namespace iKiwi.GUI
{
    partial class Form_Tools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        	this.tabControl = new System.Windows.Forms.TabControl();
        	this.tabPage_General = new System.Windows.Forms.TabPage();
        	this.groupBox5 = new System.Windows.Forms.GroupBox();
        	this.checkBox_iKiwiStartMinimized = new System.Windows.Forms.CheckBox();
        	this.checkBox_iKiwiStart = new System.Windows.Forms.CheckBox();
        	this.Connection = new System.Windows.Forms.TabPage();
        	this.panel_Connection = new System.Windows.Forms.Panel();
        	this.groupBox2 = new System.Windows.Forms.GroupBox();
        	this.label5 = new System.Windows.Forms.Label();
        	this.textBox_Info_MyPeerID = new System.Windows.Forms.TextBox();
        	this.groupBox1 = new System.Windows.Forms.GroupBox();
        	this.checkBox_useUpnp = new System.Windows.Forms.CheckBox();
        	this.label_PortStatus = new System.Windows.Forms.Label();
        	this.button_CheckOpenPort = new System.Windows.Forms.Button();
        	this.textBox_ListeningPort = new System.Windows.Forms.TextBox();
        	this.label3 = new System.Windows.Forms.Label();
        	this.textBox_IpAddress = new System.Windows.Forms.TextBox();
        	this.label2 = new System.Windows.Forms.Label();
        	this.label1 = new System.Windows.Forms.Label();
        	this.tabPage_Directories = new System.Windows.Forms.TabPage();
        	this.panel_Directories = new System.Windows.Forms.Panel();
        	this.button4 = new System.Windows.Forms.Button();
        	this.textBox_SharedDir = new System.Windows.Forms.TextBox();
        	this.label6 = new System.Windows.Forms.Label();
        	this.button3 = new System.Windows.Forms.Button();
        	this.textBox_TempDir = new System.Windows.Forms.TextBox();
        	this.label4 = new System.Windows.Forms.Label();
        	this.tabPage_Nova = new System.Windows.Forms.TabPage();
        	this.groupBox6 = new System.Windows.Forms.GroupBox();
        	this.checkBox_AcceptNotEncryptedMessages = new System.Windows.Forms.CheckBox();
        	this.checkBox_MessageEncryptionEnabled = new System.Windows.Forms.CheckBox();
        	this.tabPage_Utilities = new System.Windows.Forms.TabPage();
        	this.groupBox4 = new System.Windows.Forms.GroupBox();
        	this.button6 = new System.Windows.Forms.Button();
        	this.groupBox3 = new System.Windows.Forms.GroupBox();
        	this.button5 = new System.Windows.Forms.Button();
        	this.label_Update = new System.Windows.Forms.Label();
        	this.button_CheckUpdate = new System.Windows.Forms.Button();
        	this.tabPage_Info = new System.Windows.Forms.TabPage();
        	this.button_Support = new System.Windows.Forms.Button();
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.label8 = new System.Windows.Forms.Label();
        	this.label_iKiwiVersion = new System.Windows.Forms.Label();
        	this.label7 = new System.Windows.Forms.Label();
        	this.button_Apply = new System.Windows.Forms.Button();
        	this.button2 = new System.Windows.Forms.Button();
        	this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        	this.tabControl.SuspendLayout();
        	this.tabPage_General.SuspendLayout();
        	this.groupBox5.SuspendLayout();
        	this.Connection.SuspendLayout();
        	this.panel_Connection.SuspendLayout();
        	this.groupBox2.SuspendLayout();
        	this.groupBox1.SuspendLayout();
        	this.tabPage_Directories.SuspendLayout();
        	this.panel_Directories.SuspendLayout();
        	this.tabPage_Nova.SuspendLayout();
        	this.groupBox6.SuspendLayout();
        	this.tabPage_Utilities.SuspendLayout();
        	this.groupBox4.SuspendLayout();
        	this.groupBox3.SuspendLayout();
        	this.tabPage_Info.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// tabControl
        	// 
        	this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.tabControl.Controls.Add(this.tabPage_General);
        	this.tabControl.Controls.Add(this.Connection);
        	this.tabControl.Controls.Add(this.tabPage_Directories);
        	this.tabControl.Controls.Add(this.tabPage_Nova);
        	this.tabControl.Controls.Add(this.tabPage_Utilities);
        	this.tabControl.Controls.Add(this.tabPage_Info);
        	this.tabControl.Location = new System.Drawing.Point(12, 12);
        	this.tabControl.Multiline = true;
        	this.tabControl.Name = "tabControl";
        	this.tabControl.SelectedIndex = 0;
        	this.tabControl.Size = new System.Drawing.Size(420, 309);
        	this.tabControl.TabIndex = 0;
        	// 
        	// tabPage_General
        	// 
        	this.tabPage_General.Controls.Add(this.groupBox5);
        	this.tabPage_General.Location = new System.Drawing.Point(4, 22);
        	this.tabPage_General.Name = "tabPage_General";
        	this.tabPage_General.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage_General.Size = new System.Drawing.Size(412, 283);
        	this.tabPage_General.TabIndex = 3;
        	this.tabPage_General.Text = "General";
        	this.tabPage_General.UseVisualStyleBackColor = true;
        	// 
        	// groupBox5
        	// 
        	this.groupBox5.Controls.Add(this.checkBox_iKiwiStartMinimized);
        	this.groupBox5.Controls.Add(this.checkBox_iKiwiStart);
        	this.groupBox5.Location = new System.Drawing.Point(6, 6);
        	this.groupBox5.Name = "groupBox5";
        	this.groupBox5.Size = new System.Drawing.Size(400, 75);
        	this.groupBox5.TabIndex = 2;
        	this.groupBox5.TabStop = false;
        	this.groupBox5.Text = "Windows";
        	// 
        	// checkBox_iKiwiStartMinimized
        	// 
        	this.checkBox_iKiwiStartMinimized.Location = new System.Drawing.Point(6, 49);
        	this.checkBox_iKiwiStartMinimized.Name = "checkBox_iKiwiStartMinimized";
        	this.checkBox_iKiwiStartMinimized.Size = new System.Drawing.Size(104, 24);
        	this.checkBox_iKiwiStartMinimized.TabIndex = 2;
        	this.checkBox_iKiwiStartMinimized.Text = "Start minimized";
        	this.checkBox_iKiwiStartMinimized.UseVisualStyleBackColor = true;
        	// 
        	// checkBox_iKiwiStart
        	// 
        	this.checkBox_iKiwiStart.Location = new System.Drawing.Point(6, 19);
        	this.checkBox_iKiwiStart.Name = "checkBox_iKiwiStart";
        	this.checkBox_iKiwiStart.Size = new System.Drawing.Size(180, 24);
        	this.checkBox_iKiwiStart.TabIndex = 1;
        	this.checkBox_iKiwiStart.Text = "Start iKiwi when Windows starts";
        	this.checkBox_iKiwiStart.UseVisualStyleBackColor = true;
        	// 
        	// Connection
        	// 
        	this.Connection.Controls.Add(this.panel_Connection);
        	this.Connection.Controls.Add(this.label1);
        	this.Connection.Location = new System.Drawing.Point(4, 22);
        	this.Connection.Name = "Connection";
        	this.Connection.Padding = new System.Windows.Forms.Padding(3);
        	this.Connection.Size = new System.Drawing.Size(412, 283);
        	this.Connection.TabIndex = 0;
        	this.Connection.Text = "Connection";
        	this.Connection.UseVisualStyleBackColor = true;
        	// 
        	// panel_Connection
        	// 
        	this.panel_Connection.Controls.Add(this.groupBox2);
        	this.panel_Connection.Controls.Add(this.groupBox1);
        	this.panel_Connection.Location = new System.Drawing.Point(0, 0);
        	this.panel_Connection.Name = "panel_Connection";
        	this.panel_Connection.Size = new System.Drawing.Size(412, 283);
        	this.panel_Connection.TabIndex = 2;
        	// 
        	// groupBox2
        	// 
        	this.groupBox2.Controls.Add(this.label5);
        	this.groupBox2.Controls.Add(this.textBox_Info_MyPeerID);
        	this.groupBox2.Location = new System.Drawing.Point(0, 129);
        	this.groupBox2.Name = "groupBox2";
        	this.groupBox2.Size = new System.Drawing.Size(412, 72);
        	this.groupBox2.TabIndex = 2;
        	this.groupBox2.TabStop = false;
        	this.groupBox2.Text = "Info";
        	// 
        	// label5
        	// 
        	this.label5.AutoSize = true;
        	this.label5.Location = new System.Drawing.Point(3, 16);
        	this.label5.Name = "label5";
        	this.label5.Size = new System.Drawing.Size(43, 13);
        	this.label5.TabIndex = 3;
        	this.label5.Text = "Peer ID";
        	// 
        	// textBox_Info_MyPeerID
        	// 
        	this.textBox_Info_MyPeerID.Location = new System.Drawing.Point(136, 13);
        	this.textBox_Info_MyPeerID.Name = "textBox_Info_MyPeerID";
        	this.textBox_Info_MyPeerID.ReadOnly = true;
        	this.textBox_Info_MyPeerID.Size = new System.Drawing.Size(273, 20);
        	this.textBox_Info_MyPeerID.TabIndex = 1;
        	// 
        	// groupBox1
        	// 
        	this.groupBox1.BackColor = System.Drawing.Color.Transparent;
        	this.groupBox1.Controls.Add(this.checkBox_useUpnp);
        	this.groupBox1.Controls.Add(this.label_PortStatus);
        	this.groupBox1.Controls.Add(this.button_CheckOpenPort);
        	this.groupBox1.Controls.Add(this.textBox_ListeningPort);
        	this.groupBox1.Controls.Add(this.label3);
        	this.groupBox1.Controls.Add(this.textBox_IpAddress);
        	this.groupBox1.Controls.Add(this.label2);
        	this.groupBox1.Location = new System.Drawing.Point(0, 0);
        	this.groupBox1.Name = "groupBox1";
        	this.groupBox1.Size = new System.Drawing.Size(412, 123);
        	this.groupBox1.TabIndex = 1;
        	this.groupBox1.TabStop = false;
        	this.groupBox1.Text = "General";
        	// 
        	// checkBox_useUpnp
        	// 
        	this.checkBox_useUpnp.Location = new System.Drawing.Point(3, 38);
        	this.checkBox_useUpnp.Name = "checkBox_useUpnp";
        	this.checkBox_useUpnp.Size = new System.Drawing.Size(270, 24);
        	this.checkBox_useUpnp.TabIndex = 6;
        	this.checkBox_useUpnp.Text = "Automatically open the listening port with UPnP";
        	this.checkBox_useUpnp.UseVisualStyleBackColor = true;
        	this.checkBox_useUpnp.CheckedChanged += new System.EventHandler(this.CheckBox_useUpnpCheckedChanged);
        	// 
        	// label_PortStatus
        	// 
        	this.label_PortStatus.BackColor = System.Drawing.Color.Transparent;
        	this.label_PortStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.label_PortStatus.Location = new System.Drawing.Point(217, 99);
        	this.label_PortStatus.Name = "label_PortStatus";
        	this.label_PortStatus.Size = new System.Drawing.Size(189, 14);
        	this.label_PortStatus.TabIndex = 5;
        	// 
        	// button_CheckOpenPort
        	// 
        	this.button_CheckOpenPort.BackColor = System.Drawing.Color.Transparent;
        	this.button_CheckOpenPort.Location = new System.Drawing.Point(136, 94);
        	this.button_CheckOpenPort.Name = "button_CheckOpenPort";
        	this.button_CheckOpenPort.Size = new System.Drawing.Size(75, 23);
        	this.button_CheckOpenPort.TabIndex = 4;
        	this.button_CheckOpenPort.Text = "Check Port";
        	this.button_CheckOpenPort.UseVisualStyleBackColor = false;
        	this.button_CheckOpenPort.Click += new System.EventHandler(this.Button_CheckOpenPortClick);
        	// 
        	// textBox_ListeningPort
        	// 
        	this.textBox_ListeningPort.Location = new System.Drawing.Point(136, 68);
        	this.textBox_ListeningPort.Name = "textBox_ListeningPort";
        	this.textBox_ListeningPort.Size = new System.Drawing.Size(270, 20);
        	this.textBox_ListeningPort.TabIndex = 3;
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(3, 75);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(71, 13);
        	this.label3.TabIndex = 2;
        	this.label3.Text = "Listening Port";
        	// 
        	// textBox_IpAddress
        	// 
        	this.textBox_IpAddress.Location = new System.Drawing.Point(136, 13);
        	this.textBox_IpAddress.Name = "textBox_IpAddress";
        	this.textBox_IpAddress.ReadOnly = true;
        	this.textBox_IpAddress.Size = new System.Drawing.Size(270, 20);
        	this.textBox_IpAddress.TabIndex = 1;
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(3, 16);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(58, 13);
        	this.label2.TabIndex = 0;
        	this.label2.Text = "IP Address";
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(7, 7);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(0, 13);
        	this.label1.TabIndex = 0;
        	// 
        	// tabPage_Directories
        	// 
        	this.tabPage_Directories.Controls.Add(this.panel_Directories);
        	this.tabPage_Directories.Location = new System.Drawing.Point(4, 22);
        	this.tabPage_Directories.Name = "tabPage_Directories";
        	this.tabPage_Directories.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage_Directories.Size = new System.Drawing.Size(412, 283);
        	this.tabPage_Directories.TabIndex = 1;
        	this.tabPage_Directories.Text = "Directories";
        	this.tabPage_Directories.UseVisualStyleBackColor = true;
        	// 
        	// panel_Directories
        	// 
        	this.panel_Directories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.panel_Directories.Controls.Add(this.button4);
        	this.panel_Directories.Controls.Add(this.textBox_SharedDir);
        	this.panel_Directories.Controls.Add(this.label6);
        	this.panel_Directories.Controls.Add(this.button3);
        	this.panel_Directories.Controls.Add(this.textBox_TempDir);
        	this.panel_Directories.Controls.Add(this.label4);
        	this.panel_Directories.Location = new System.Drawing.Point(0, 0);
        	this.panel_Directories.Name = "panel_Directories";
        	this.panel_Directories.Size = new System.Drawing.Size(412, 283);
        	this.panel_Directories.TabIndex = 0;
        	// 
        	// button4
        	// 
        	this.button4.Location = new System.Drawing.Point(376, 59);
        	this.button4.Name = "button4";
        	this.button4.Size = new System.Drawing.Size(30, 23);
        	this.button4.TabIndex = 6;
        	this.button4.Text = "...";
        	this.button4.UseVisualStyleBackColor = true;
        	this.button4.Click += new System.EventHandler(this.Button4Click);
        	// 
        	// textBox_SharedDir
        	// 
        	this.textBox_SharedDir.Location = new System.Drawing.Point(3, 61);
        	this.textBox_SharedDir.Name = "textBox_SharedDir";
        	this.textBox_SharedDir.Size = new System.Drawing.Size(367, 20);
        	this.textBox_SharedDir.TabIndex = 5;
        	// 
        	// label6
        	// 
        	this.label6.AutoSize = true;
        	this.label6.Location = new System.Drawing.Point(3, 45);
        	this.label6.Name = "label6";
        	this.label6.Size = new System.Drawing.Size(87, 13);
        	this.label6.TabIndex = 4;
        	this.label6.Text = "Shared directory:";
        	// 
        	// button3
        	// 
        	this.button3.Location = new System.Drawing.Point(376, 19);
        	this.button3.Name = "button3";
        	this.button3.Size = new System.Drawing.Size(30, 23);
        	this.button3.TabIndex = 3;
        	this.button3.Text = "...";
        	this.button3.UseVisualStyleBackColor = true;
        	this.button3.Click += new System.EventHandler(this.Button3Click);
        	// 
        	// textBox_TempDir
        	// 
        	this.textBox_TempDir.Location = new System.Drawing.Point(3, 22);
        	this.textBox_TempDir.Name = "textBox_TempDir";
        	this.textBox_TempDir.Size = new System.Drawing.Size(367, 20);
        	this.textBox_TempDir.TabIndex = 2;
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Location = new System.Drawing.Point(3, 6);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(80, 13);
        	this.label4.TabIndex = 1;
        	this.label4.Text = "Temp directory:";
        	// 
        	// tabPage_Nova
        	// 
        	this.tabPage_Nova.Controls.Add(this.groupBox6);
        	this.tabPage_Nova.Location = new System.Drawing.Point(4, 22);
        	this.tabPage_Nova.Name = "tabPage_Nova";
        	this.tabPage_Nova.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage_Nova.Size = new System.Drawing.Size(412, 283);
        	this.tabPage_Nova.TabIndex = 5;
        	this.tabPage_Nova.Text = "Nova";
        	this.tabPage_Nova.UseVisualStyleBackColor = true;
        	// 
        	// groupBox6
        	// 
        	this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.groupBox6.Controls.Add(this.checkBox_AcceptNotEncryptedMessages);
        	this.groupBox6.Controls.Add(this.checkBox_MessageEncryptionEnabled);
        	this.groupBox6.Location = new System.Drawing.Point(6, 6);
        	this.groupBox6.Name = "groupBox6";
        	this.groupBox6.Size = new System.Drawing.Size(400, 80);
        	this.groupBox6.TabIndex = 0;
        	this.groupBox6.TabStop = false;
        	this.groupBox6.Text = "Privacy";
        	// 
        	// checkBox_AcceptNotEncryptedMessages
        	// 
        	this.checkBox_AcceptNotEncryptedMessages.Location = new System.Drawing.Point(6, 49);
        	this.checkBox_AcceptNotEncryptedMessages.Name = "checkBox_AcceptNotEncryptedMessages";
        	this.checkBox_AcceptNotEncryptedMessages.Size = new System.Drawing.Size(180, 24);
        	this.checkBox_AcceptNotEncryptedMessages.TabIndex = 3;
        	this.checkBox_AcceptNotEncryptedMessages.Text = "Accept not encrypted messages";
        	this.checkBox_AcceptNotEncryptedMessages.UseVisualStyleBackColor = true;
        	// 
        	// checkBox_MessageEncryptionEnabled
        	// 
        	this.checkBox_MessageEncryptionEnabled.Location = new System.Drawing.Point(6, 19);
        	this.checkBox_MessageEncryptionEnabled.Name = "checkBox_MessageEncryptionEnabled";
        	this.checkBox_MessageEncryptionEnabled.Size = new System.Drawing.Size(180, 24);
        	this.checkBox_MessageEncryptionEnabled.TabIndex = 2;
        	this.checkBox_MessageEncryptionEnabled.Text = "Enable message encryption";
        	this.checkBox_MessageEncryptionEnabled.UseVisualStyleBackColor = true;
        	this.checkBox_MessageEncryptionEnabled.CheckedChanged += new System.EventHandler(this.CheckBox_MessageEncryptionEnabledCheckedChanged);
        	// 
        	// tabPage_Utilities
        	// 
        	this.tabPage_Utilities.Controls.Add(this.groupBox4);
        	this.tabPage_Utilities.Controls.Add(this.groupBox3);
        	this.tabPage_Utilities.Location = new System.Drawing.Point(4, 22);
        	this.tabPage_Utilities.Name = "tabPage_Utilities";
        	this.tabPage_Utilities.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage_Utilities.Size = new System.Drawing.Size(412, 283);
        	this.tabPage_Utilities.TabIndex = 2;
        	this.tabPage_Utilities.Text = "Utilities";
        	this.tabPage_Utilities.UseVisualStyleBackColor = true;
        	// 
        	// groupBox4
        	// 
        	this.groupBox4.Controls.Add(this.button6);
        	this.groupBox4.Location = new System.Drawing.Point(0, 85);
        	this.groupBox4.Name = "groupBox4";
        	this.groupBox4.Size = new System.Drawing.Size(412, 52);
        	this.groupBox4.TabIndex = 9;
        	this.groupBox4.TabStop = false;
        	this.groupBox4.Text = "Console log";
        	// 
        	// button6
        	// 
        	this.button6.Location = new System.Drawing.Point(7, 20);
        	this.button6.Name = "button6";
        	this.button6.Size = new System.Drawing.Size(399, 23);
        	this.button6.TabIndex = 0;
        	this.button6.Text = "Launch the console log";
        	this.button6.UseVisualStyleBackColor = true;
        	this.button6.Click += new System.EventHandler(this.Button6Click);
        	// 
        	// groupBox3
        	// 
        	this.groupBox3.Controls.Add(this.button5);
        	this.groupBox3.Controls.Add(this.label_Update);
        	this.groupBox3.Controls.Add(this.button_CheckUpdate);
        	this.groupBox3.Location = new System.Drawing.Point(0, 0);
        	this.groupBox3.Name = "groupBox3";
        	this.groupBox3.Size = new System.Drawing.Size(412, 79);
        	this.groupBox3.TabIndex = 8;
        	this.groupBox3.TabStop = false;
        	this.groupBox3.Text = "Update";
        	// 
        	// button5
        	// 
        	this.button5.Location = new System.Drawing.Point(7, 48);
        	this.button5.Name = "button5";
        	this.button5.Size = new System.Drawing.Size(100, 23);
        	this.button5.TabIndex = 7;
        	this.button5.Text = "Launch Updater";
        	this.button5.UseVisualStyleBackColor = true;
        	this.button5.Click += new System.EventHandler(this.Button5Click);
        	// 
        	// label_Update
        	// 
        	this.label_Update.BackColor = System.Drawing.Color.Transparent;
        	this.label_Update.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.label_Update.Location = new System.Drawing.Point(113, 19);
        	this.label_Update.Name = "label_Update";
        	this.label_Update.Size = new System.Drawing.Size(293, 52);
        	this.label_Update.TabIndex = 6;
        	this.label_Update.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// button_CheckUpdate
        	// 
        	this.button_CheckUpdate.Location = new System.Drawing.Point(6, 19);
        	this.button_CheckUpdate.Name = "button_CheckUpdate";
        	this.button_CheckUpdate.Size = new System.Drawing.Size(100, 23);
        	this.button_CheckUpdate.TabIndex = 0;
        	this.button_CheckUpdate.Text = "Check update";
        	this.button_CheckUpdate.UseVisualStyleBackColor = true;
        	this.button_CheckUpdate.Click += new System.EventHandler(this.Button_CheckUpdateClick);
        	// 
        	// tabPage_Info
        	// 
        	this.tabPage_Info.Controls.Add(this.button_Support);
        	this.tabPage_Info.Controls.Add(this.pictureBox1);
        	this.tabPage_Info.Controls.Add(this.label8);
        	this.tabPage_Info.Controls.Add(this.label_iKiwiVersion);
        	this.tabPage_Info.Controls.Add(this.label7);
        	this.tabPage_Info.Location = new System.Drawing.Point(4, 22);
        	this.tabPage_Info.Name = "tabPage_Info";
        	this.tabPage_Info.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPage_Info.Size = new System.Drawing.Size(412, 283);
        	this.tabPage_Info.TabIndex = 4;
        	this.tabPage_Info.Text = "Info";
        	this.tabPage_Info.UseVisualStyleBackColor = true;
        	// 
        	// button_Support
        	// 
        	this.button_Support.Location = new System.Drawing.Point(6, 176);
        	this.button_Support.Name = "button_Support";
        	this.button_Support.Size = new System.Drawing.Size(400, 101);
        	this.button_Support.TabIndex = 4;
        	this.button_Support.Text = "Support iKiwi";
        	this.button_Support.UseVisualStyleBackColor = true;
        	this.button_Support.Click += new System.EventHandler(this.Button_SupportClick);
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Image = global::iKiwi.Properties.Resources.iKiwi_splash;
        	this.pictureBox1.Location = new System.Drawing.Point(262, 0);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(150, 150);
        	this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        	this.pictureBox1.TabIndex = 3;
        	this.pictureBox1.TabStop = false;
        	// 
        	// label8
        	// 
        	this.label8.Location = new System.Drawing.Point(7, 7);
        	this.label8.Name = "label8";
        	this.label8.Size = new System.Drawing.Size(399, 143);
        	this.label8.TabIndex = 2;
        	this.label8.Text = "iKiwi\r\n\r\nCreated by Nicola Pesavento.\r\n\r\nCopyright (C) 2010-2011  Nicola Pesavent" +
        	"o.\r\n\r\n\r\n";
        	// 
        	// label_iKiwiVersion
        	// 
        	this.label_iKiwiVersion.Location = new System.Drawing.Point(113, 150);
        	this.label_iKiwiVersion.Name = "label_iKiwiVersion";
        	this.label_iKiwiVersion.Size = new System.Drawing.Size(100, 23);
        	this.label_iKiwiVersion.TabIndex = 1;
        	this.label_iKiwiVersion.Text = "...";
        	// 
        	// label7
        	// 
        	this.label7.Location = new System.Drawing.Point(7, 150);
        	this.label7.Name = "label7";
        	this.label7.Size = new System.Drawing.Size(100, 23);
        	this.label7.TabIndex = 0;
        	this.label7.Text = "iKiwi Version";
        	// 
        	// button_Apply
        	// 
        	this.button_Apply.Location = new System.Drawing.Point(272, 327);
        	this.button_Apply.Name = "button_Apply";
        	this.button_Apply.Size = new System.Drawing.Size(75, 23);
        	this.button_Apply.TabIndex = 1;
        	this.button_Apply.Text = "Apply";
        	this.button_Apply.UseVisualStyleBackColor = true;
        	this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
        	// 
        	// button2
        	// 
        	this.button2.Location = new System.Drawing.Point(353, 327);
        	this.button2.Name = "button2";
        	this.button2.Size = new System.Drawing.Size(75, 23);
        	this.button2.TabIndex = 2;
        	this.button2.Text = "Cancel";
        	this.button2.UseVisualStyleBackColor = true;
        	this.button2.Click += new System.EventHandler(this.button2_Click);
        	// 
        	// Form_Tools
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(444, 362);
        	this.Controls.Add(this.button2);
        	this.Controls.Add(this.button_Apply);
        	this.Controls.Add(this.tabControl);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        	this.Icon = global::iKiwi.Properties.Resources.ikiwi;
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "Form_Tools";
        	this.Text = "Tools";
        	this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_ToolsFormClosed);
        	this.Load += new System.EventHandler(this.Form_Settings_Load);
        	this.tabControl.ResumeLayout(false);
        	this.tabPage_General.ResumeLayout(false);
        	this.groupBox5.ResumeLayout(false);
        	this.Connection.ResumeLayout(false);
        	this.Connection.PerformLayout();
        	this.panel_Connection.ResumeLayout(false);
        	this.groupBox2.ResumeLayout(false);
        	this.groupBox2.PerformLayout();
        	this.groupBox1.ResumeLayout(false);
        	this.groupBox1.PerformLayout();
        	this.tabPage_Directories.ResumeLayout(false);
        	this.panel_Directories.ResumeLayout(false);
        	this.panel_Directories.PerformLayout();
        	this.tabPage_Nova.ResumeLayout(false);
        	this.groupBox6.ResumeLayout(false);
        	this.tabPage_Utilities.ResumeLayout(false);
        	this.groupBox4.ResumeLayout(false);
        	this.groupBox3.ResumeLayout(false);
        	this.tabPage_Info.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.CheckBox checkBox_useUpnp;
        private System.Windows.Forms.CheckBox checkBox_MessageEncryptionEnabled;
        private System.Windows.Forms.CheckBox checkBox_AcceptNotEncryptedMessages;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TabPage tabPage_Nova;
        private System.Windows.Forms.Button button_Support;
        private System.Windows.Forms.CheckBox checkBox_iKiwiStartMinimized;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_iKiwiVersion;
        private System.Windows.Forms.TabPage tabPage_Info;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.CheckBox checkBox_iKiwiStart;
        private System.Windows.Forms.TabPage tabPage_General;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabPage tabPage_Utilities;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button_CheckUpdate;
        private System.Windows.Forms.Label label_Update;
        private System.Windows.Forms.Button button_CheckOpenPort;
        private System.Windows.Forms.Label label_PortStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_SharedDir;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel_Connection;
        private System.Windows.Forms.TextBox textBox_ListeningPort;
        private System.Windows.Forms.TextBox textBox_IpAddress;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_TempDir;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel_Directories;
        private System.Windows.Forms.TabPage tabPage_Directories;
        private System.Windows.Forms.TabPage Connection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Info_MyPeerID;

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
    }
}