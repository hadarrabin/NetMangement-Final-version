using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Data.SQLite;

namespace Server_Gui
{
    public partial class ServerForm : Form
    {
        #region  GLOBAL VARIBALES
        // If C# and python are connectd
        bool python_con = false;
        // if the updater thread isnt working
        bool Uth = false;
        // Creating a PythonSession object
        Communicator com;
        Color DeafultColor;
        #endregion
        public ServerForm()
        {
            InitializeComponent();
            this.UsageSettings.SelectedItem = 26;
            this.DeafultColor = this.processes.Rows[0].DefaultCellStyle.BackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Connection checking
            if (!python_con)
            {
                try
                {
                    //try to create python server if there isnt one exists
                    com = new Communicator(this);
                    python_con = true;
                }
                catch
                {
                    MessageBox.Show("we have a problem connecting");
                    Environment.Exit(1);
                }
            }
            #endregion
            if (python_con)
            {
                this.textBox1.AppendText(" Connection is good");
            }
            this.button1.Visible = false;
            this.AddClientB.Visible = true;
        }

        public void CreatePythonEngine()
        {
            Process pythonProcess = new Process();
            pythonProcess.StartInfo.FileName = "C:\\Python27\\python.exe";
            pythonProcess.StartInfo.Arguments = "NetMM\\Server\\Server.py " + Application.StartupPath;
            pythonProcess.StartInfo.WorkingDirectory = Application.StartupPath;
            pythonProcess.StartInfo.UseShellExecute = false;
            pythonProcess.Start();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }



        private void Clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.openwindows.Items.Clear();
            this.TUsingBox.Text = "Usage:";
            this.processes.Rows.Clear();
            var Currentt = this.Clients.SelectedItem;
            string Current = Convert.ToString(Currentt);
            this.com.IPC = Current;
            this.com.Wriite(this.com.IPC + ":Get Total Using");
            string bb = this.com.Reead();
            this.TUsingBox.Text = bb.Substring((bb.IndexOf(":"))+1);
            // changes the values by DB
            this.com.UpdateBoxes(Current);
            if (Uth == true)
            {
                Uth = false;
                // starts an ifinite thread with description in communicator class
                ThreadStart S = new ThreadStart(com.UpdateUsingTB);
                Thread Up = new Thread(S);
                Up.Start();
            }
        }

        private void AddClientB_Click(object sender, EventArgs e)
        {
            
            this.com.Execute(this.com.Reead());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TUsingBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsageSettings_SelectedItemChanged(object sender, EventArgs e)
        {
            
            this.UsageSettings.Text = Convert.ToString(this.UsageSettings.Items[Convert.ToInt32(this.UsageSettings.SelectedIndex)]);
            this.UsageSettings.Update();
            int x = Convert.ToInt32(this.UsageSettings.Items[Convert.ToInt32(this.UsageSettings.SelectedIndex)]);
            foreach (DataGridViewRow row in this.processes.Rows)
            {
                if (Convert.ToInt32(row.Cells["Usage"].Value) > x)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = this.DeafultColor;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.com.Wriite(this.com.IPC + ":Get Process List");
            foreach (DataGridViewRow item in this.processes.SelectedRows)
            {
                this.processes.Rows.RemoveAt(item.Index);
            }

            Update_Grid();
        }
        private void Update_Grid()
        {
            Invoke(new MethodInvoker(delegate()
            {
                this.processes.Update();
                this.processes.Rows.Clear();
            }));
            string rowsrecivelen = this.com.Reead();
            if (rowsrecivelen.Equals("New client arrives"))
            {
                this.com.NewClient();
                rowsrecivelen = this.com.Reead();
            }
            bool Ipb = false;
            string PID = "";
            string procesname = "";
            string Usage = "";
            int indesdot = rowsrecivelen.IndexOf(':');
            rowsrecivelen = rowsrecivelen.Substring(indesdot + 1);
            for (int i = 0; i < Convert.ToInt32(rowsrecivelen); i++)
            {
                Ipb = false;
                string line = this.com.Reead();
                int index = line.IndexOf(':');
                string Ip = line.Substring(0, index);
                if (this.com.IPC == Ip)
                    Ipb = true;
                while (Ipb != true)
                {
                    line = this.com.Reead();
                    index = line.IndexOf(':');
                    Ip = line.Substring(0, index);
                    if (this.com.IPC == Ip)
                        Ipb = true;
                }
                index = index + 1;
                line = line.Substring(index);
                index = line.IndexOf(':');
                string PI = line.Substring(0, index);
                if (PI == "PID")
                {
                    index += 1;
                    line = line.Substring(index);
                    index = line.IndexOf(';');
                    PID = line.Substring(0, index);
                }
                else
                    Ipb = false;
                if (Ipb)
                {
                    index = index + 1;
                    line = line.Substring(index);
                    index = line.IndexOf(':');
                    string Namev = line.Substring(0, index);
                    if (Namev == "Name")
                    {
                        index = index + 1;
                        line = line.Substring(index);
                        index = line.IndexOf(';');
                        procesname = line.Substring(0, index);
                    }
                    else
                        Ipb = false;
                    if (Ipb)
                    {
                        index = index + 1;
                        line = line.Substring(index);
                        index = line.IndexOf(':');
                        string Usagev = line.Substring(0, index);
                        if (Usagev == "Usage")
                        {
                            index = index + 1;
                            line = line.Substring(index);
                            Usage = line;
                        }
                        else
                            Ipb = false;
                        }
                    }
                    if (Ipb)
                    {
                        Invoke(new MethodInvoker(delegate()
                        {
                            this.processes.Rows.Add(new object[] {
                            PID,
                            procesname,
                            Usage
                            });
                            this.processes.Update();
                        }));
                    }


                }
            //while (reader.Read())
            //{
            //    this.processes.Rows.Add(new object[] { 
            //reader.GetValue(reader.GetOrdinal("PID")),  // Or column name like this
            //reader.GetValue(reader.GetOrdinal("Pname")),
            //reader.GetValue(reader.GetOrdinal("Usage")) 
            //});
        }

        private void GetWinBut_Click(object sender, EventArgs e)
        {
            this.com.Wriite(this.com.IPC + ":Get Open Windows");
            string lenlis = this.com.Reead();
            if (lenlis.Equals("New Client arrives"))
            {
                this.com.NewClient();
                lenlis = this.com.Reead();
            }
            if ((lenlis.Substring(0, 6)).Equals("Usage"))
            {
                this.TUsingBox.Text = lenlis;
                lenlis = this.com.Reead();
            }
            lenlis = lenlis.Substring(lenlis.IndexOf(':') + 1);
            int lenl = Convert.ToInt32(lenlis);
            for (int i = 0; i < lenl; i++)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    this.openwindows.Items.Add(this.com.Reead().Substring((this.com.IPC.Length+1)));
                }));
            }
        }

        private void getusageT_Click(object sender, EventArgs e)
        {
            this.com.Wriite(this.com.IPC + ":Get Total Using");
            string info = this.com.Reead();
            string EqualltyCheck = info.Substring(0, this.com.IPC.Length);
            if (EqualltyCheck.Equals(this.com.IPC))
            {
                Invoke(new MethodInvoker(delegate()
                {
                    TUsingBox.Text = info.Substring(this.com.IPC.Length + 1);
                }));
            }
        }

        private void processes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            var cellse = this.processes.Rows[rowindex].Cells;
            DataGridViewRow d = this.processes.Rows[rowindex];
            var cell = cellse[0].Value;
            this.com.Wriite(this.com.IPC + ":Kill Process:" + cell);
            this.processes.Rows.Remove(d);
        }


        }



        
    }

