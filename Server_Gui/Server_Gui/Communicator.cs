using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Data.SQLite;

namespace Server_Gui
{

    class Communicator
    {
        public static string data = null;
        //handles the communication between the Form and the python programm
        //NamedPipeServerStream server;
        Socket S;
        byte[] bytes;
        ServerForm Form_1;
        public string IPC;
        //BinaryReader br;
        //BinaryWriter bw;
        ComputersBasicDDataSet computersBasicDDataSet;
        /// <summary>
        ///  this function establishes the connection with the python server engine
        /// </summary>
        public Communicator(ServerForm Form)
        {
            this.Form_1 = Form;
            this.computersBasicDDataSet = new ComputersBasicDDataSet();
            try
            {
                //creates a socket connection
                this.bytes = new Byte[2048];
                IPAddress ipAddress = IPAddress.Any;
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11555);
                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(1);
                //server = new NamedPipeServerStream("myPipee");
                this.Form_1.textBox1.AppendText(" Waiting for Connection");
                //creates the python engine
                this.Form_1.CreatePythonEngine();
                //server.WaitForConnection();
                Socket sock = listener.Accept();
                this.S = sock;
                Debug.Print("Connected to python multi threding server");
                //var bro = new BinaryReader(server);
                //this.br = bro;
                //var bwo = new BinaryWriter(server);
                //this.bw = bwo;
                string hello = "Welcome python engine, you have connected c# winform";
                //this.bw.Write((uint)hello.Length);
                //this.bw.Write(hello);
                data = null;
                byte[] msgg = Encoding.ASCII.GetBytes(hello);
                this.S.Send(msgg);
            }
            catch (Exception e)
            {
                this.Form_1.textBox1.Text = e.ToString();
            }
        }
        /// <summary>
        ///  this function reeeds from the server socket connection
        /// </summary>
        public string Reead()
        {
            //var lenn = (int)this.br.ReadUInt32();            // Read string length
            //var str = new string(this.br.ReadChars(lenn));
            //return str;
            int bytesRec = this.S.Receive(this.bytes);
            string datad = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return datad;
        }
        /// <summary>
        ///  this function is writes to the connection with the server
        /// </summary>
        public void Wriite( string str)
        {
        //    this.bw.Write((uint)str.Length);
        //    this.bw.Write(str);
            byte[] msg = Encoding.ASCII.GetBytes(str);
            this.S.Send(msg);
        }
        public void NewClient()
        {
            // need to connect and add IP to db and also to connect the combo box to the db
            // Create a new row. FROM (MSDN)
            // NorthwindDataSet.RegionRow newRegionRow;
            //newRegionRow = northwindDataSet.Region.NewRegionRow();
            //newRegionRow.RegionID = 5;
            //newRegionRow.RegionDescription = "NorthWestern";

            //// Add the row to the Region table
            //this.northwindDataSet.Region.Rows.Add(newRegionRow);

            //// Save the new row to the database
            //this.regionTableAdapter.Update(this.northwindDataSet.Region);
            ComputersBasicDDataSet.Basic_TableRow newTable1Row;
            newTable1Row = this.computersBasicDDataSet.Basic_Table.NewBasic_TableRow();
            string Ip = Reead();
            newTable1Row.IP = Ip;
            string UUId = Reead();
            int indexdots = UUId.IndexOf(':');
            indexdots += 1;
            UUId = UUId.Substring(indexdots);
            newTable1Row.UUID = UUId;
            string username = Reead();
            username = username.Substring(indexdots);
            newTable1Row.user_name = username;
            string Os = Reead();
            Os = Os.Substring(indexdots);
            newTable1Row.OS_version = Os;
            string CPu = Reead();
            CPu = CPu.Substring(indexdots);
            newTable1Row.Processor = CPu;
            string CPuNum = Reead();
            CPuNum = CPuNum.Substring(indexdots);
            newTable1Row.CPUs = CPuNum;
            string RAm = Reead();
            RAm = RAm.Substring(indexdots);
            newTable1Row.RAM_size = RAm;
            this.computersBasicDDataSet.Basic_Table.Rows.Add(newTable1Row);
            this.Form_1.Invoke(new MethodInvoker(delegate()
            {
                this.Form_1.Clients.BeginUpdate();
                this.Form_1.Clients.Items.Add(Ip);
                this.Form_1.Clients.SelectedItem = Ip;
                this.Form_1.Clients.Update();
                this.Form_1.Clients.EndUpdate();
                }));
            
        }
        /// <summary>
        ///  this function is designed to replace each of the data cells values with the correct client ones.
        ///  it is done by approaching the MS access DB and the basic table in it. it copyes the values to the correct cells.
        /// </summary>
        public void UpdateBoxes(string Ip)
        {
            System.Data.DataRow copyingvaluesRow = this.computersBasicDDataSet.Basic_Table.Rows.Find(Ip);
            this.Form_1.Invoke(new MethodInvoker(delegate()
           {
               this.Form_1.Clients.BeginUpdate();
               this.Form_1.Clients.SelectedItem = Ip;
               this.Form_1.Clients.Update();
               this.Form_1.Clients.EndUpdate();
               this.Form_1.IPBox.Text = Convert.ToString(copyingvaluesRow["IP"]);
               this.Form_1.UUIDbox.Text = Convert.ToString(copyingvaluesRow["UUID"]);
               this.Form_1.UserNameBox.Text = Convert.ToString(copyingvaluesRow["user name"]);
               this.Form_1.RAMBox.Text = Convert.ToString(copyingvaluesRow["RAM size"]);
               this.Form_1.CpuNumBox.Text = Convert.ToString(copyingvaluesRow["CPUs"]);
               this.Form_1.CPUBox.Text = Convert.ToString(copyingvaluesRow["Processor"]);
               this.Form_1.OsBox.Text = Convert.ToString(copyingvaluesRow["OS version"]);
           }));
        }
        public void slep(Object sender,
                           EventArgs e)
        {
            System.Threading.Thread.Sleep(1000000000);
        }
        /// <summary>
        ///  the operation target is to update constantly the Using: box
        //   its done by an infinite thread that requires the Ip from the server
        /// </summary>
        public void UpdateUsingTB()
        {
           
            while (true)
            {
                bool ok = true;
                System.Threading.Thread.Sleep(100000);
                this.Form_1.button1.Click += new EventHandler(this.slep);
                this.Wriite(IPC + ":Get Total Using");
                string info = this.Reead();
                if (info == "New client arrives")
                {
                    this.NewClient();
                    ok = false;
                }
                if (ok)
                {
                    string EqualltyCheck = info.Substring(0, this.IPC.Length);
                    if (EqualltyCheck.Equals(this.IPC))
                    {
                        this.Form_1.Invoke(new MethodInvoker(delegate()
                        {
                            this.Form_1.TUsingBox.Text = info.Substring(this.IPC.Length + 1);
                        }));
                    }
                    if (!(EqualltyCheck.Equals(this.IPC)))
                    {
                        int Ipin = info.IndexOf(':') + 1;
                        string Ipn = info.Substring(0, Ipin);
                        string Datta = info.Substring(Ipin);
                        int Parami = Datta.IndexOf(':');
                        string Parmat = Datta.Substring(0, Parami);

                        this.Form_1.Invoke(new MethodInvoker(delegate()
                        {
                            this.IPC = Convert.ToString(this.Form_1.Clients.SelectedItem);
                            System.Data.DataRow copyingvaluesRow = this.computersBasicDDataSet.Basic_Table.Rows.Find(Ipn);
                            copyingvaluesRow.BeginEdit();
                            this.Form_1.TUsingBox.Text = Convert.ToString(copyingvaluesRow[Parmat]);
                            copyingvaluesRow[Parmat] = Datta;
                        }));
                    }
                }

                this.Form_1.button1.Click += new EventHandler(this.slep);

                }
                
        }

        public void Execute(string st1)
        {
            if (st1 == "New client arrives")
            {
                this.NewClient();
            }
            // more tasks in future if needed
        }


    }
}
