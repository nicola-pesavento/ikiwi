using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace iKiwi.Utilities
{
	/// <summary>
	/// Supporter.
	/// </summary>
	public partial class Supporter : Form
	{
		#region Ctor
		
		/// <summary>
		/// Supporter.
		/// </summary>
		public Supporter()
		{
			InitializeComponent();
		}
		
		#endregion
		
		#region Events
		
		void Button_DonateClick(object sender, EventArgs e)
		{
			string url = "";
			
			string business     = "pesapower@gmail.com";
			string description  = "Donation";
			string country      = "EU";
			string currency     = "EUR";
			
			url += "https://www.paypal.com/cgi-bin/webscr" +
				"?cmd=" + "_donations" +
				"&business=" + business +
				"&lc=" + country +
				"&item_name=" + description +
				"&currency_code=" + currency +
				"&bn=" + "PP%2dDonationsBF";
			
			System.Diagnostics.Process.Start(url);
		}
		
		void Button_VisitIkiwiSiteClick(object sender, EventArgs e)
		{
			Process.Start("http://ikiwi.sourceforge.net/");
		}
		
		void Button_iLikeIkiwiClick(object sender, EventArgs e)
		{
			Process.Start("http://www.facebook.com/pages/iKiwi/164459346963093");
		}
		
		#endregion
	}
}
