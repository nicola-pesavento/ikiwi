
namespace iKiwi.Utilities
{
	partial class PortHelper
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortHelper));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button_PortForward = new System.Windows.Forms.Button();
			this.label_PortStatus = new System.Windows.Forms.Label();
			this.button_CheckOpenPort = new System.Windows.Forms.Button();
			this.textBox_ListeningPort = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button_SavePort = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::iKiwi.Properties.Resources.Hub;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 100);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// richTextBox1
			// 
			this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Location = new System.Drawing.Point(12, 118);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(560, 80);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// button_PortForward
			// 
			this.button_PortForward.Location = new System.Drawing.Point(12, 209);
			this.button_PortForward.Name = "button_PortForward";
			this.button_PortForward.Size = new System.Drawing.Size(560, 23);
			this.button_PortForward.TabIndex = 2;
			this.button_PortForward.Text = "See Port Forward";
			this.button_PortForward.UseVisualStyleBackColor = true;
			this.button_PortForward.Click += new System.EventHandler(this.Button_PortForwardClick);
			// 
			// label_PortStatus
			// 
			this.label_PortStatus.BackColor = System.Drawing.Color.Transparent;
			this.label_PortStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label_PortStatus.Location = new System.Drawing.Point(472, 9);
			this.label_PortStatus.Name = "label_PortStatus";
			this.label_PortStatus.Size = new System.Drawing.Size(100, 23);
			this.label_PortStatus.TabIndex = 7;
			// 
			// button_CheckOpenPort
			// 
			this.button_CheckOpenPort.BackColor = System.Drawing.Color.Transparent;
			this.button_CheckOpenPort.Location = new System.Drawing.Point(391, 9);
			this.button_CheckOpenPort.Name = "button_CheckOpenPort";
			this.button_CheckOpenPort.Size = new System.Drawing.Size(75, 23);
			this.button_CheckOpenPort.TabIndex = 6;
			this.button_CheckOpenPort.Text = "Check Port";
			this.button_CheckOpenPort.UseVisualStyleBackColor = false;
			this.button_CheckOpenPort.Click += new System.EventHandler(this.Button_CheckOpenPortClick);
			// 
			// textBox_ListeningPort
			// 
			this.textBox_ListeningPort.Location = new System.Drawing.Point(195, 11);
			this.textBox_ListeningPort.Name = "textBox_ListeningPort";
			this.textBox_ListeningPort.Size = new System.Drawing.Size(190, 20);
			this.textBox_ListeningPort.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(118, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Listening Port";
			// 
			// button_SavePort
			// 
			this.button_SavePort.Location = new System.Drawing.Point(118, 37);
			this.button_SavePort.Name = "button_SavePort";
			this.button_SavePort.Size = new System.Drawing.Size(454, 23);
			this.button_SavePort.TabIndex = 10;
			this.button_SavePort.Text = "Save and Close";
			this.button_SavePort.UseVisualStyleBackColor = true;
			this.button_SavePort.Click += new System.EventHandler(this.Button_SavePortClick);
			// 
			// PortHelper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 244);
			this.Controls.Add(this.button_SavePort);
			this.Controls.Add(this.textBox_ListeningPort);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label_PortStatus);
			this.Controls.Add(this.button_CheckOpenPort);
			this.Controls.Add(this.button_PortForward);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::iKiwi.Properties.Resources.ikiwi;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PortHelper";
			this.Text = "Port Helper";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button button_SavePort;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_ListeningPort;
		private System.Windows.Forms.Button button_CheckOpenPort;
		private System.Windows.Forms.Label label_PortStatus;
		private System.Windows.Forms.Button button_PortForward;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
