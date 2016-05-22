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
        bool Uth = true;
        // Creating a PythonSession object
        Communicator com;

        #endregion
        public ServerForm()
        {
            InitializeComponent();
            this.UsageSettings.SelectedItem = 26;

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
            var Currentt = this.Clients.SelectedItem;
            string Current = Convert.ToString(Currentt);
            this.com.IPC = Current;
            // changes the values by DB
            this.com.UpdateBoxes(Current);
            if (Uth == true)
            {
                Uth = false;
                // starts an ifinite thread with description in communicator class
                ThreadStart S = new ThreadStart(com.UpdateUsingB);
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
            foreach (DataGridViewRow row in this.processes.Rows)
                if (Convert.ToInt32(row.Cells["using"].Value) > Convert.ToInt32(this.UsageSettings.SelectedItem))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.com.Wriite(this.com.IPC + ":Get Process List");
            foreach (DataGridViewRow item in this.processes.SelectedRows)
            {
                this.processes.Rows.RemoveAt(item.Index);
            }

            ThreadStart SQL = new ThreadStart(Update_Grid);
            Thread SQLU = new Thread(SQL);
            SQLU.Start();
        }
        private void Update_Grid()
        {
            //System.Threading.Thread.Sleep(120000);
            //string sqll = "select * from Process_Table order by PID";
            //SQLiteCommand command = new SQLiteCommand(sqll, m_dbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    this.processes.Rows.Add(new object[] { 
            //reader.GetValue(reader.GetOrdinal("PID")),  // Or column name like this
            //reader.GetValue(reader.GetOrdinal("Pname")),
            //reader.GetValue(reader.GetOrdinal("Usage")) 
            //});
            }
        }



        
    }

