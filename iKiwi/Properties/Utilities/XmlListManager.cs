using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Threading;

namespace iKiwi
{
    /// <summary>
    /// Manages the XML-Lists of other peers in the [Temp_dir]/List/List_***.xml.
    /// </summary>
    class XmlListManager
    {
        #region Daemons

        /// <summary>
        /// Automatic build a new XML-List every few time. It works in background.
        /// </summary>
        public static void StartAutomaticXmlBuilder()
        {
            Thread t = new Thread(new ParameterizedThreadStart(delegate
            {
                while (true)
                {
                    Build_XML_List();

                    Thread.Sleep(100000);
                }
            }));

            t.Name = "AutomaticXmlBuilder";
            t.IsBackground = true;
            t.Start();

            Debug.WriteLine("Automatic XML builder is started.", "Info");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Send a request of connection to all peers that are contained in the all XML-Lists if them aren't already connected with this client.
        /// Then deletes all XML-Lists.
        /// </summary>
        /// <param name="n">Max number of connections that will be opened. Set 0 to open all possible connections.</param>
        public static void ConnToPeers(int n = 0)
        {
            string[] lists = Directory.GetFiles((Global.TempDirectory + @"List\"), "List_*.xml", SearchOption.AllDirectories);

            foreach (string listPath in lists)
            {
                XmlDocument list = new XmlDocument();

                // load the Xml-List
                while (true)
                {
                    try
                    {
                        list.Load(listPath);
                        break;
                    }
                    catch (IOException)
                    {
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error to load a XML-List from " + listPath + " : " + ex.Message, "Error");
                        throw new IOException("Error to load a XML-List from " + listPath + " : " + ex.Message);
                    }
                }

                XmlNodeList nodes = list.DocumentElement.GetElementsByTagName("Peers");

                // get the single peers from the list
                int i = 0;
                foreach (XmlNode node in nodes)
                {
                    if (i < n)
                    {
                        string IP = node.SelectSingleNode("IP").InnerText;

                        // control if this peer is already connected with this client
                        if (PeersList.GetPeerByIP(IP) == null)
                        {
                            string RequestConnectionMessage = Bouncer.RequestMessage(IP);

                            MessageSender.SendConnectionRequest(RequestConnectionMessage, IP);
                        }

                        if (n != 0)
                        {
                            i++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // delete the list
                File.Delete(listPath);
            }
        }

        /// <summary>
        /// Count the number of temporary xml-lists saved.
        /// </summary>
        /// <returns>The number of temp xml-lists saved.</returns>
        public static int NumberOfTempLists()
        {
            string[] lists = Directory.GetFiles((Global.TempDirectory + @"\List\"), "List_*.xml", SearchOption.AllDirectories);

            return lists.Length;
        }

        /// <summary>
        /// Build a new XML_List with all the peers are contained in the PeersList.
        /// </summary>
        public static void Build_XML_List()
        {
            XmlDocument list = new XmlDocument();

            list.LoadXml("<Peers></Peers>");

            for (int i = 0; i < PeersList.Count; i++)
            {
                PeersList.Peer peer = (PeersList.Peer)PeersList.List[i];

                if (peer.ID != "" && peer.ID != null)
                {
                    XmlElement XE_peer = list.CreateElement(peer.ID);
                    list.DocumentElement.AppendChild(XE_peer);

                    XmlElement peer_IP = list.CreateElement("IP");
                    peer_IP.InnerText = peer.IP;
                    XE_peer.AppendChild(peer_IP);

                    XmlElement peer_time = list.CreateElement("Time");
                    peer_time.InnerText = peer.Date.ToString();
                    XE_peer.AppendChild(peer_time);

                    XmlElement peer_files = list.CreateElement("Files");

                    try
                    {
                        //foreach (PeersList.Peer.File file in peer.Files)
                        for (int j = 0; j < peer.Files.Count; j++)
                        {
                            PeersList.Peer.File file = (PeersList.Peer.File)peer.Files[j];

                            XmlElement file_sha = list.CreateElement(file.SHA1);

                            XmlElement file_name = list.CreateElement("Name");
                            file_name.InnerText = file.Name;
                            file_sha.AppendChild(file_name);

                            XmlElement file_size = list.CreateElement("Size");
                            file_size.InnerText = file.Size.ToString();
                            file_sha.AppendChild(file_size);

                            peer_files.AppendChild(file_sha);
                        }
                    }
                    catch
                    {
                    }

                    XE_peer.AppendChild(peer_files);
                }
            }

            // save the list
            XmlTextWriter writer = new XmlTextWriter("List.xml", null);
            writer.Formatting = Formatting.Indented;


            while (true)
            {
                try
                {
                    list.Save(writer);
                    writer.Flush();
                    writer.Close();
                    return;
                }
                catch
                {
                    Thread.Sleep(5);
                }
            }
        }

        #endregion
    }
}
