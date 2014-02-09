/*
    iKiwi

    Copyright (C) 2010-2011-2012  Nicola Pesavento

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace iKiwi
{
	static class Program
	{
		/// <summary>
		/// Program.cs
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			#region control if this ikiwi instance has not been already started
			
			string currentProcessPath = Application.ExecutablePath;
			int currentProcessId = Process.GetCurrentProcess().Id;
			
			Process[] ikiwiInstances = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
			
			for (int i = 0; i < ikiwiInstances.Length; i++)
			{
				if (ikiwiInstances[i].MainModule.FileName == currentProcessPath && ikiwiInstances[i].Id != currentProcessId)
				{
					MessageBox.Show("iKiwi has been already started.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					
					// close iKiwi
					Process.GetCurrentProcess().Kill();
				}
			}
			
			#endregion
			
			// system suspension handler
			SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(PowerModeHandler);
			
			// bug reporter
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Utilities.BugReporter.Start);
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			Utilities.Log.Write("iKiwi started", Utilities.Log.LogCategory.Info);
			
			// check version of iKiwi
			Utilities.Updater.CheckAndInstallUpdate();
			
			// check listening port
			Utilities.PortChecker.CheckListeningPort();
			
			// build the files list
			#region build the files list
			
			Thread th = new Thread(new ParameterizedThreadStart(
				delegate
				{
					Lists.FilesList.Build();
				}
			));

			th.Name = "BuildingFilesList";
			th.IsBackground = true;
			th.Start();
			
			#endregion
			
			// get a list of peers from the default NovaWebCache server
			Daemons.PeerSearcher.UseNovaWebCacheServer();

			// public the IP address of this client
			Daemons.PeerSearcher.PublishMeInNovaWebCacheServer();
			
			# region Daemons start

			// start the automatic-builder of xml-list
			Utilities.XmlListManager.StartAutomaticXmlBuilder();

			// start Bouncer
			Daemons.Bouncer.Start(Global.ListeningPort);
			
			// start PeersList
			Lists.PeersList.Start();

			// start Downloader
			Daemons.Downloader.Start();

			// start PeerSearcher
			Daemons.PeerSearcher.Start();
			
			// start download and upload rate counter
			Global.StartCounterDownloadUploadRate();

			#endregion
			
			// load the pending donwload's info
			Utilities.Configurator.LoadPendingDownloadsInfo();
			
			#region Supporter
			
			if (Global.FirstStart == true)
			{
				Thread t = new Thread(new ParameterizedThreadStart(
					delegate
					{
						// start supporter
						Application.Run(new Utilities.Supporter());
					}
				));
				
				t.Name = "Supporter";
				t.IsBackground = true;
				t.SetApartmentState(ApartmentState.STA);
				t.Start();
				
				Global.FirstStart = false;
				
				Utilities.Configurator.SaveAll();
			}
			
			#endregion
			
			// start ikiwi gui
			Application.Run(new GUI.MainForm(args));
			
			// stop the Bouncer daemon
			Daemons.Bouncer.Stop();
			
			// save the data
			Utilities.XmlListManager.Build_XML_List();
			Utilities.Configurator.SavePendingDownloadsInfo();
		}
		
		#region Private Methods
		
		/// <summary>
		/// Used to handler the power mode changes of this machine.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">Event arguments.</param>
		private static void PowerModeHandler(object sender, PowerModeChangedEventArgs  e)
		{
			// control if the machine is in resuming phase by a suspension
			switch (e.Mode)
			{
				case PowerModes.Suspend:
					{
						// stop bouncer
						Daemons.Bouncer.Stop();
						
						// log
						Utilities.Log.Write("The machine has been suspended", Utilities.Log.LogCategory.Info);
						
						break;
					}
				case PowerModes.Resume:
					{
						Utilities.PortChecker.CheckListeningPort();
						
						// start Bouncer
						Daemons.Bouncer.Start(Global.ListeningPort);
						
						// log
						Utilities.Log.Write("The machine has been resumed", Utilities.Log.LogCategory.Info);
						
						break;
					}
				default:
					{
						break;
					}
			}
		}
		
		#endregion
	}
}