namespace iKiwi.GUI
{
	partial class ConsoleLog
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.label1 = new System.Windows.Forms.Label();
			this.dataGridView_Console = new System.Windows.Forms.DataGridView();
			this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.comboBox_Category = new System.Windows.Forms.ComboBox();
			this.button_GenerateLogFile = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Console)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Category:";
			// 
			// dataGridView_Console
			// 
			this.dataGridView_Console.AllowUserToResizeColumns = false;
			this.dataGridView_Console.AllowUserToResizeRows = false;
			this.dataGridView_Console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_Console.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView_Console.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView_Console.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView_Console.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dataGridView_Console.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Console.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Date,
									this.Message});
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Console.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_Console.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView_Console.Location = new System.Drawing.Point(12, 35);
			this.dataGridView_Console.Name = "dataGridView_Console";
			this.dataGridView_Console.ReadOnly = true;
			this.dataGridView_Console.RowHeadersVisible = false;
			this.dataGridView_Console.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.dataGridView_Console.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_Console.Size = new System.Drawing.Size(560, 217);
			this.dataGridView_Console.TabIndex = 7;
			// 
			// Date
			// 
			this.Date.HeaderText = "Date";
			this.Date.Name = "Date";
			this.Date.ReadOnly = true;
			this.Date.Width = 55;
			// 
			// Message
			// 
			this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Message.HeaderText = "Message";
			this.Message.Name = "Message";
			this.Message.ReadOnly = true;
			// 
			// comboBox_Category
			// 
			this.comboBox_Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Category.FormattingEnabled = true;
			this.comboBox_Category.Items.AddRange(new object[] {
									"All",
									"Connection requests",
									"Input messages",
									"Output messages",
									"Info",
									"Error"});
			this.comboBox_Category.Location = new System.Drawing.Point(71, 6);
			this.comboBox_Category.Name = "comboBox_Category";
			this.comboBox_Category.Size = new System.Drawing.Size(121, 21);
			this.comboBox_Category.TabIndex = 8;
			this.comboBox_Category.SelectedIndexChanged += new System.EventHandler(this.ComboBox_CategorySelectedIndexChanged);
			// 
			// button_GenerateLogFile
			// 
			this.button_GenerateLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_GenerateLogFile.Location = new System.Drawing.Point(472, 6);
			this.button_GenerateLogFile.Name = "button_GenerateLogFile";
			this.button_GenerateLogFile.Size = new System.Drawing.Size(100, 23);
			this.button_GenerateLogFile.TabIndex = 9;
			this.button_GenerateLogFile.Text = "Generate log file";
			this.button_GenerateLogFile.UseVisualStyleBackColor = true;
			this.button_GenerateLogFile.Click += new System.EventHandler(this.Button_GenerateLogFileClick);
			// 
			// ConsoleLog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 264);
			this.Controls.Add(this.button_GenerateLogFile);
			this.Controls.Add(this.comboBox_Category);
			this.Controls.Add(this.dataGridView_Console);
			this.Controls.Add(this.label1);
			this.Name = "ConsoleLog";
			this.ShowIcon = false;
			this.Text = "Console log";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConsoleLogFormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Console)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button_GenerateLogFile;
		private System.Windows.Forms.DataGridView dataGridView_Console;
		private System.Windows.Forms.ComboBox comboBox_Category;
		private System.Windows.Forms.DataGridViewTextBoxColumn Message;
		private System.Windows.Forms.DataGridViewTextBoxColumn Date;
		private System.Windows.Forms.Label label1;
	}
}
