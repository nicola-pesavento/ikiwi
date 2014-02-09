using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Timers;

namespace iKiwi
{
    /// <summary>
    /// Sends the messages to other peers.
    /// </summary>
    class MessageSender
    {
        #region Methods

        /// <summary>
        /// Send a message to a peer still connected.
        /// </summary>
        /// <param name="Message">Message in Byte[] or in String(UTF16 encode only).</param>
        /// <param name="PeerIP">IP:Port</param>
        /// <returns>If return False the sending is failed.</returns>     
        public static void Send(byte[] Message, string PeerIP)
        {
            Thread t = new Thread(new ParameterizedThreadStart(delegate
            {
                PeersList.Peer peer = PeersList.GetPeerByIP(PeerIP);

                try
                {
                    NetworkStream stream = peer.Client.GetStream();
                    stream.Write(Message, 0, Message.Length);
                }
                catch
                {
                    // remove the peer
                    PeersList.RemovePeer(PeerIP);

                    Debug.WriteLine("A message hasn't been sent, probably the peer is offline or disconnected", "Error");
                    
                    //MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // DEBUG
                }
            }));

            t.Name = "MessageSender";
            t.IsBackground = true;
            t.Start();

        }

        /// <summary>
        /// Send a message to a peer still connected.
        /// </summary>
        /// <param name="Message">Message in String(UTF16 encode only).</param>
        /// <param name="PeerIP">IP:Port</param>
        /// <returns>If return False the sending is failed.</returns>     
        public static void Send(string Message, string PeerIP)
        {
            Thread t = new Thread(new ParameterizedThreadStart(delegate
            {
                // convert the UTF16 to Byte[]

                byte[] Message_Byte = new byte[Message.Length * 2];

                Message_Byte = ASCIIEncoding.Unicode.GetBytes(Message);


                PeersList.Peer peer = PeersList.GetPeerByIP(PeerIP);

                try
                {
                    NetworkStream stream = peer.Client.GetStream();
                    stream.Write((byte[])Message_Byte, 0, ((byte[])Message_Byte).Length);
                }
                catch
                {
                    // remove the peer
                    PeersList.RemovePeer(PeerIP);

                    Debug.WriteLine("A message hasn't been sent, probably the peer is offline or disconnected", "Error");

                    //MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // DEBUG
                }
            }));

            t.Name = "MessageSender";
            t.IsBackground = true;
            t.Start();

        }

       
        /// <summary>
        /// Send a message to a peer still connected ( USE THIS FOR SENDING MESSAGE WITH BINARY PARTS )
        /// </summary>
        /// <param name="IMessage">Message in IMessage interface.</param>
        /// <param name="PeerIP">IP:Port</param>
        /// <returns>If return False the sending is failed.</returns>     
        public static void Send(Messages.IMessage IMessage, string PeerIP)
        {
            byte[] Message_Byte = IMessage.MessageByte;

            PeersList.Peer peer = PeersList.GetPeerByIP(PeerIP);

            try
            {
                NetworkStream stream = peer.Client.GetStream();
                stream.Write((byte[])Message_Byte, 0, ((byte[])Message_Byte).Length);
            }
            catch
            {
                // remove the peer
                PeersList.RemovePeer(PeerIP);

                Debug.WriteLine("A message hasn't been sent, probably the peer is offline or disconnected", "Error");

                //MessageBox.Show("ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // DEBUG
            }
        }


        /// <summary>
        /// Send a Tcp connection request to a new peer.
        /// </summary>
        /// <param name="Message">The request message ( byte[] or string in UTF16 format ).</param>
        /// <param name="PeerIP">The IP address of the new peer.</param>
        public static void SendConnectionRequest(object Message, string PeerIP)
        {
            Thread t = new Thread(new ParameterizedThreadStart(delegate
            {
                if (Message.GetType().ToString() != "System.String" && Message.GetType().ToString() != "System.Byte[]")
                {
                    throw new System.ArgumentException("The message format is invalid", "Message");
                }

                // if necessary converts the UTF16 to Byte[]
                if (Message.GetType().ToString() == "System.String")
                {
                    Message = ASCIIEncoding.Unicode.GetBytes((string)Message);
                }

                try
                {
                    // connect to peer
                    TcpClient client = new TcpClient();

                    client.Connect(PeerIP.Split(':')[0], int.Parse(PeerIP.Split(':')[1]));

                    // get stream
                    NetworkStream stream = client.GetStream();

                    // send the request
                    stream.Write((byte[])Message, 0, ((byte[])Message).Length);

                    // wait a reply
                    System.Timers.Timer timer = new System.Timers.Timer(15000);

                    timer.Elapsed += new ElapsedEventHandler(delegate { Thread.CurrentThread.Abort(); });

                    timer.Enabled = true;

                    byte[] reply_byte = new byte[3072];

                    stream.Read(reply_byte, 0, reply_byte.Length);

                    string reply = ASCIIEncoding.Unicode.GetString(reply_byte);

                    string[] sub_reply = reply.Split('\n');

                    // if the peer have accepted the connection request...
                    if (sub_reply[0].Substring(0, 4) == "Nova" && sub_reply[1].Substring(0, 10) == "CONNECT_OK")
                    {
                        Debug.WriteLine("Connection established with " + PeerIP, "Connection established");

                        // create a new PeersList.Peer()
                        PeersList.Peer peer = new PeersList.Peer();
                        peer.IP = PeerIP;
                        peer.Client = client;
                        peer.Stream = stream;
                        peer.ID = "";

                        // add the peer in the PeersList()
                        PeersList.AddPeer(peer);
                    }
                    else
                    {
                        stream.Close();
                        client.Close();
                    }

                    timer.Close();
                }
                catch
                {
                    Debug.WriteLine("A message hasn't been sent, probably the peer is offline or disconnected", "Error");
                }
            }));

            t.Name = "MessageSender_Request_Connection_Sending";
            t.IsBackground = true;
            t.Start();
        }

        #endregion
    }
}
