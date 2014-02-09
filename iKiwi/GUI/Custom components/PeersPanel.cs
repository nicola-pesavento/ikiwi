using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace iKiwi.GUI.Custom_components
{
	/// <summary>
	/// The panel of peers.
	/// </summary>
	public partial class PeersPanel : UserControl
	{
		#region Ctor
		
		/// <summary>
		/// Creates the peers panel object.
		/// </summary>
		public PeersPanel()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Methods
		
		/// <summary>
		/// Updates the panel of pees.
		/// </summary>
		public new void Update()
		{
			Thread t = new Thread(new ParameterizedThreadStart(
				delegate
				{
					DataGridViewRowCollection rows = this.dataGridView_Peers.Rows;

					// clear the dataGrid
					this.Invoke(new MethodInvoker(
						delegate {
							rows.Clear();
							
							for (int i = 0; i < Lists.PeersList.List.Count; i++)
							{
								rows.Add(Lists.PeersList.List[i].ID, Lists.PeersList.List[i].IP);
							}
						}));
				}
			));

			t.Name = "UpdatePeersPanel";
			t.IsBackground = true;
			t.Start();
		}
		
		#endregion
		
		#region Events
		
		void Button_AddPeerClick(object sender, EventArgs e)
		{
			string IpPeer = textBox_AddPeer.Text;
			
			if (IpPeer != "")
			{
				// control that this IP Address is not mine
				if (this.textBox_AddPeer.Text != Global.MyRemoteIP + ":" + Global.ListeningPort)
				{
					string RequestConnectionMessage = Daemons.Bouncer.RequestMessage(IpPeer);

					MessageSender.SendConnectionRequest(RequestConnectionMessage, IpPeer);
				}
			}
		}
		
		void Button_UpdatePeersGridClick(object sender, EventArgs e)
		{
			Update();
		}
		
		#endregion
	}
}