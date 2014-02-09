
namespace iKiwi.GUI.Custom_components
{
	partial class PeersPanel
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.textBox_AddPeer = new System.Windows.Forms.TextBox();
			this.button_AddPeer = new System.Windows.Forms.Button();
			this.button_UpdatePeersGrid = new System.Windows.Forms.Button();
			this.dataGridView_Peers = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Peers)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox_AddPeer
			// 
			this.textBox_AddPeer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_AddPeer.Location = new System.Drawing.Point(89, 5);
			this.textBox_AddPeer.Name = "textBox_AddPeer";
			this.textBox_AddPeer.Size = new System.Drawing.Size(322, 20);
			this.textBox_AddPeer.TabIndex = 12;
			// 
			// button_AddPeer
			// 
			this.button_AddPeer.Location = new System.Drawing.Point(3, 3);
			this.button_AddPeer.Name = "button_AddPeer";
			this.button_AddPeer.Size = new System.Drawing.Size(80, 23);
			this.button_AddPeer.TabIndex = 11;
			this.button_AddPeer.Text = "Add Peer";
			this.button_AddPeer.UseVisualStyleBackColor = true;
			this.button_AddPeer.Click += new System.EventHandler(this.Button_AddPeerClick);
			// 
			// button_UpdatePeersGrid
			// 
			this.button_UpdatePeersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_UpdatePeersGrid.Location = new System.Drawing.Point(417, 3);
			this.button_UpdatePeersGrid.Name = "button_UpdatePeersGrid";
			this.button_UpdatePeersGrid.Size = new System.Drawing.Size(80, 23);
			this.button_UpdatePeersGrid.TabIndex = 10;
			this.button_UpdatePeersGrid.Text = "Update";
			this.button_UpdatePeersGrid.UseVisualStyleBackColor = true;
			this.button_UpdatePeersGrid.Click += new System.EventHandler(this.Button_UpdatePeersGridClick);
			// 
			// dataGridView_Peers
			// 
			this.dataGridView_Peers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView_Peers.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView_Peers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView_Peers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_Peers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_Peers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Peers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn4,
									this.dataGridViewTextBoxColumn5});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView_Peers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView_Peers.GridColor = System.Drawing.SystemColors.Window;
			this.dataGridView_Peers.Location = new System.Drawing.Point(0, 31);
			this.dataGridView_Peers.Name = "dataGridView_Peers";
			this.dataGridView_Peers.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_Peers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView_Peers.RowHeadersVisible = false;
			this.dataGridView_Peers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_Peers.Size = new System.Drawing.Size(500, 269);
			this.dataGridView_Peers.TabIndex = 9;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn4.HeaderText = "Peer";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn5.HeaderText = "IP Address";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			// 
			// PeersPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBox_AddPeer);
			this.Controls.Add(this.button_AddPeer);
			this.Controls.Add(this.button_UpdatePeersGrid);
			this.Controls.Add(this.dataGridView_Peers);
			this.Name = "PeersPanel";
			this.Size = new System.Drawing.Size(500, 300);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Peers)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridView dataGridView_Peers;
		private System.Windows.Forms.Button button_UpdatePeersGrid;
		private System.Windows.Forms.Button button_AddPeer;
		private System.Windows.Forms.TextBox textBox_AddPeer;
	}
}
