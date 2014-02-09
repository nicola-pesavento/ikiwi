
namespace iKiwi.Utilities
{
	partial class DirectoryHelper
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.textBox_SharedDirectory = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button_SaveAndClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::iKiwi.Properties.Resources.Shared_directory;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(91, 100);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(110, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(212, 30);
			this.label1.TabIndex = 2;
			this.label1.Text = "iKiwi needs a shared directory to save the downloaded files and share in net the " +
			"files.";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(292, 61);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(30, 23);
			this.button4.TabIndex = 8;
			this.button4.Text = "...";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// textBox_SharedDirectory
			// 
			this.textBox_SharedDirectory.Location = new System.Drawing.Point(110, 63);
			this.textBox_SharedDirectory.Name = "textBox_SharedDirectory";
			this.textBox_SharedDirectory.Size = new System.Drawing.Size(176, 20);
			this.textBox_SharedDirectory.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(110, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 15);
			this.label2.TabIndex = 9;
			this.label2.Text = "Shared directory";
			// 
			// button_SaveAndClose
			// 
			this.button_SaveAndClose.Location = new System.Drawing.Point(110, 89);
			this.button_SaveAndClose.Name = "button_SaveAndClose";
			this.button_SaveAndClose.Size = new System.Drawing.Size(212, 23);
			this.button_SaveAndClose.TabIndex = 10;
			this.button_SaveAndClose.Text = "Save and Close";
			this.button_SaveAndClose.UseVisualStyleBackColor = true;
			this.button_SaveAndClose.Click += new System.EventHandler(this.Button_SaveAndCloseClick);
			// 
			// DirectoryHelper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 124);
			this.Controls.Add(this.button_SaveAndClose);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox_SharedDirectory);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = global::iKiwi.Properties.Resources.ikiwi;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DirectoryHelper";
			this.Text = "Directory Helper";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button button_SaveAndClose;
		private System.Windows.Forms.TextBox textBox_SharedDirectory;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
