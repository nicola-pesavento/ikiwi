
namespace iKiwi.Utilities
{
	partial class Supporter
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
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.button_Donate = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button_VisitIkiwiSite = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button_iLikeIkiwi = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(260, 256);
			this.label1.TabIndex = 0;
			this.label1.Text = "Thank you for using iKiwi!\r\n\r\niKiwi is a open source project and it needs your he" +
			"lp to improve and grow,\r\n\r\nCan you help iKiwi for 30 seconds?";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Location = new System.Drawing.Point(12, 92);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(260, 336);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Support iKiwi";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.pictureBox3);
			this.groupBox4.Controls.Add(this.button_Donate);
			this.groupBox4.Location = new System.Drawing.Point(5, 237);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(248, 90);
			this.groupBox4.TabIndex = 7;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "3) Make a Donation";
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = global::iKiwi.Properties.Resources.Love;
			this.pictureBox3.Location = new System.Drawing.Point(7, 19);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(67, 65);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox3.TabIndex = 3;
			this.pictureBox3.TabStop = false;
			// 
			// button_Donate
			// 
			this.button_Donate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.button_Donate.Location = new System.Drawing.Point(80, 19);
			this.button_Donate.Name = "button_Donate";
			this.button_Donate.Size = new System.Drawing.Size(162, 65);
			this.button_Donate.TabIndex = 0;
			this.button_Donate.Text = "Donate";
			this.button_Donate.UseVisualStyleBackColor = true;
			this.button_Donate.Click += new System.EventHandler(this.Button_DonateClick);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button_VisitIkiwiSite);
			this.groupBox3.Controls.Add(this.pictureBox2);
			this.groupBox3.Location = new System.Drawing.Point(5, 141);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(248, 90);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "2) Visit iKiwi Site";
			// 
			// button_VisitIkiwiSite
			// 
			this.button_VisitIkiwiSite.Location = new System.Drawing.Point(80, 19);
			this.button_VisitIkiwiSite.Name = "button_VisitIkiwiSite";
			this.button_VisitIkiwiSite.Size = new System.Drawing.Size(162, 65);
			this.button_VisitIkiwiSite.TabIndex = 4;
			this.button_VisitIkiwiSite.Text = "Visit iKiwi Site";
			this.button_VisitIkiwiSite.UseVisualStyleBackColor = true;
			this.button_VisitIkiwiSite.Click += new System.EventHandler(this.Button_VisitIkiwiSiteClick);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::iKiwi.Properties.Resources.Internet;
			this.pictureBox2.Location = new System.Drawing.Point(7, 19);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(60, 65);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "You can:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button_iLikeIkiwi);
			this.groupBox2.Controls.Add(this.pictureBox1);
			this.groupBox2.Location = new System.Drawing.Point(6, 45);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(247, 90);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "1) Remember to Like iKiwi";
			// 
			// button_iLikeIkiwi
			// 
			this.button_iLikeIkiwi.Location = new System.Drawing.Point(79, 19);
			this.button_iLikeIkiwi.Name = "button_iLikeIkiwi";
			this.button_iLikeIkiwi.Size = new System.Drawing.Size(162, 65);
			this.button_iLikeIkiwi.TabIndex = 5;
			this.button_iLikeIkiwi.Text = "I Like iKiwi";
			this.button_iLikeIkiwi.UseVisualStyleBackColor = true;
			this.button_iLikeIkiwi.Click += new System.EventHandler(this.Button_iLikeIkiwiClick);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::iKiwi.Properties.Resources.FacebookLogo;
			this.pictureBox1.Location = new System.Drawing.Point(6, 19);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(65, 65);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// Supporter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 440);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Icon = global::iKiwi.Properties.Resources.ikiwi;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Supporter";
			this.Text = "Supporter";
			this.TopMost = true;
			this.groupBox1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button_iLikeIkiwi;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button button_VisitIkiwiSite;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button_Donate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
	}
}
