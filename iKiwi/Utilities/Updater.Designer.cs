/*
 * Creato da SharpDevelop.
 * Utente: nicola
 * Data: 05/07/2011
 * Ora: 18:55
 * 
 * Per modificare questo modello usa Strumenti | Opzioni | Codice | Modifica Intestazioni Standard
 */
namespace iKiwi.Utilities
{
	partial class Updater
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.button_GetUpdate = new System.Windows.Forms.Button();
			this.label_Info = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 46);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(260, 23);
			this.progressBar1.TabIndex = 2;
			// 
			// button_GetUpdate
			// 
			this.button_GetUpdate.Location = new System.Drawing.Point(12, 75);
			this.button_GetUpdate.Name = "button_GetUpdate";
			this.button_GetUpdate.Size = new System.Drawing.Size(260, 23);
			this.button_GetUpdate.TabIndex = 1;
			this.button_GetUpdate.Text = "Update iKiwi";
			this.button_GetUpdate.UseVisualStyleBackColor = true;
			this.button_GetUpdate.Click += new System.EventHandler(this.Button_GetUpdateClick);
			// 
			// label_Info
			// 
			this.label_Info.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label_Info.Location = new System.Drawing.Point(13, 9);
			this.label_Info.Name = "label_Info";
			this.label_Info.Size = new System.Drawing.Size(259, 34);
			this.label_Info.TabIndex = 3;
			this.label_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Updater
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 110);
			this.Controls.Add(this.label_Info);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button_GetUpdate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::iKiwi.Properties.Resources.ikiwi;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Updater";
			this.Text = "Updater";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.UpdaterLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label_Info;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button button_GetUpdate;
	}
}
