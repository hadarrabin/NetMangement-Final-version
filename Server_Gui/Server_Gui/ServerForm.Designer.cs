

namespace Server_Gui
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UUIDbox = new System.Windows.Forms.TextBox();
            this.UserNameBox = new System.Windows.Forms.TextBox();
            this.OsBox = new System.Windows.Forms.TextBox();
            this.CPUBox = new System.Windows.Forms.TextBox();
            this.CpuNumBox = new System.Windows.Forms.TextBox();
            this.RAMBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Clients = new System.Windows.Forms.ListBox();
            this.processes = new System.Windows.Forms.DataGridView();
            this.computersBasicDDataSet = new Server_Gui.ComputersBasicDDataSet();
            this.AddClientB = new System.Windows.Forms.Button();
            this.TUsingBox = new System.Windows.Forms.TextBox();
            this.UsageSettings = new System.Windows.Forms.DomainUpDown();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computersBasicDDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 305);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(217, 103);
            this.textBox1.TabIndex = 1;
            // 
            // UUIDbox
            // 
            this.UUIDbox.Location = new System.Drawing.Point(3, 29);
            this.UUIDbox.Name = "UUIDbox";
            this.UUIDbox.Size = new System.Drawing.Size(186, 20);
            this.UUIDbox.TabIndex = 3;
            this.UUIDbox.Text = "UUID: ";
            this.UUIDbox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // UserNameBox
            // 
            this.UserNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.UserNameBox.Location = new System.Drawing.Point(3, 55);
            this.UserNameBox.Name = "UserNameBox";
            this.UserNameBox.Size = new System.Drawing.Size(186, 23);
            this.UserNameBox.TabIndex = 4;
            this.UserNameBox.Text = "user name: ";
            // 
            // OsBox
            // 
            this.OsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OsBox.Location = new System.Drawing.Point(3, 166);
            this.OsBox.Name = "OsBox";
            this.OsBox.Size = new System.Drawing.Size(186, 22);
            this.OsBox.TabIndex = 5;
            this.OsBox.Text = "OS version: ";
            // 
            // CPUBox
            // 
            this.CPUBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CPUBox.Location = new System.Drawing.Point(3, 138);
            this.CPUBox.Name = "CPUBox";
            this.CPUBox.Size = new System.Drawing.Size(186, 22);
            this.CPUBox.TabIndex = 6;
            this.CPUBox.Text = "processor: ";
            // 
            // CpuNumBox
            // 
            this.CpuNumBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CpuNumBox.Location = new System.Drawing.Point(3, 111);
            this.CpuNumBox.Name = "CpuNumBox";
            this.CpuNumBox.Size = new System.Drawing.Size(186, 21);
            this.CpuNumBox.TabIndex = 7;
            this.CpuNumBox.Text = "CPU\'s: ";
            // 
            // RAMBox
            // 
            this.RAMBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.RAMBox.Location = new System.Drawing.Point(3, 84);
            this.RAMBox.Name = "RAMBox";
            this.RAMBox.Size = new System.Drawing.Size(186, 21);
            this.RAMBox.TabIndex = 8;
            this.RAMBox.Text = "RAM size: ";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.IPBox);
            this.flowLayoutPanel1.Controls.Add(this.UUIDbox);
            this.flowLayoutPanel1.Controls.Add(this.UserNameBox);
            this.flowLayoutPanel1.Controls.Add(this.RAMBox);
            this.flowLayoutPanel1.Controls.Add(this.CpuNumBox);
            this.flowLayoutPanel1.Controls.Add(this.CPUBox);
            this.flowLayoutPanel1.Controls.Add(this.OsBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(206, 194);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(3, 3);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(186, 20);
            this.IPBox.TabIndex = 14;
            this.IPBox.Text = "IP:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Location = new System.Drawing.Point(12, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 43);
            this.button1.TabIndex = 10;
            this.button1.Text = "start server";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button2.Location = new System.Drawing.Point(643, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 51);
            this.button2.TabIndex = 11;
            this.button2.Text = "Get processes for Ip";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Clients
            // 
            this.Clients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Clients.FormattingEnabled = true;
            this.Clients.HorizontalScrollbar = true;
            this.Clients.ItemHeight = 15;
            this.Clients.Location = new System.Drawing.Point(256, 25);
            this.Clients.Name = "Clients";
            this.Clients.ScrollAlwaysVisible = true;
            this.Clients.Size = new System.Drawing.Size(117, 94);
            this.Clients.TabIndex = 12;
            this.Clients.SelectedIndexChanged += new System.EventHandler(this.Clients_SelectedIndexChanged);
            // 
            // processes
            // 
            this.processes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.processes.Location = new System.Drawing.Point(413, 13);
            this.processes.MultiSelect = false;
            this.processes.Name = "processes";
            this.processes.ReadOnly = true;
            this.processes.Size = new System.Drawing.Size(342, 221);
            this.processes.TabIndex = 13;
            // 
            // computersBasicDDataSet
            // 
            this.computersBasicDDataSet.DataSetName = "ComputersBasicDDataSet";
            this.computersBasicDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AddClientB
            // 
            this.AddClientB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.AddClientB.Location = new System.Drawing.Point(119, 217);
            this.AddClientB.Name = "AddClientB";
            this.AddClientB.Size = new System.Drawing.Size(110, 43);
            this.AddClientB.TabIndex = 14;
            this.AddClientB.Text = "Add a Client";
            this.AddClientB.UseVisualStyleBackColor = true;
            this.AddClientB.Visible = false;
            this.AddClientB.Click += new System.EventHandler(this.AddClientB_Click);
            // 
            // TUsingBox
            // 
            this.TUsingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.TUsingBox.Location = new System.Drawing.Point(532, 256);
            this.TUsingBox.Multiline = true;
            this.TUsingBox.Name = "TUsingBox";
            this.TUsingBox.Size = new System.Drawing.Size(92, 35);
            this.TUsingBox.TabIndex = 15;
            this.TUsingBox.Text = "Usage:";
            this.TUsingBox.TextChanged += new System.EventHandler(this.TUsingBox_TextChanged);
            // 
            // UsageSettings
            // 
            this.UsageSettings.Items.Add("1");
            this.UsageSettings.Items.Add("2");
            this.UsageSettings.Items.Add("3");
            this.UsageSettings.Items.Add("4");
            this.UsageSettings.Items.Add("5");
            this.UsageSettings.Items.Add("6");
            this.UsageSettings.Items.Add("7");
            this.UsageSettings.Items.Add("8");
            this.UsageSettings.Items.Add("9");
            this.UsageSettings.Items.Add("10");
            this.UsageSettings.Items.Add("11");
            this.UsageSettings.Items.Add("12");
            this.UsageSettings.Items.Add("13");
            this.UsageSettings.Items.Add("14");
            this.UsageSettings.Items.Add("15");
            this.UsageSettings.Items.Add("16");
            this.UsageSettings.Items.Add("17");
            this.UsageSettings.Items.Add("18");
            this.UsageSettings.Items.Add("19");
            this.UsageSettings.Items.Add("20");
            this.UsageSettings.Items.Add("21");
            this.UsageSettings.Items.Add("22");
            this.UsageSettings.Items.Add("23");
            this.UsageSettings.Items.Add("24");
            this.UsageSettings.Items.Add("25");
            this.UsageSettings.Items.Add("26");
            this.UsageSettings.Items.Add("27");
            this.UsageSettings.Items.Add("28");
            this.UsageSettings.Items.Add("29");
            this.UsageSettings.Items.Add("30");
            this.UsageSettings.Items.Add("31");
            this.UsageSettings.Items.Add("32");
            this.UsageSettings.Items.Add("33");
            this.UsageSettings.Items.Add("34");
            this.UsageSettings.Items.Add("35");
            this.UsageSettings.Items.Add("36");
            this.UsageSettings.Items.Add("37");
            this.UsageSettings.Items.Add("38");
            this.UsageSettings.Items.Add("39");
            this.UsageSettings.Items.Add("40");
            this.UsageSettings.Items.Add("41");
            this.UsageSettings.Items.Add("42");
            this.UsageSettings.Items.Add("43");
            this.UsageSettings.Items.Add("44");
            this.UsageSettings.Items.Add("45");
            this.UsageSettings.Items.Add("46");
            this.UsageSettings.Items.Add("47");
            this.UsageSettings.Items.Add("48");
            this.UsageSettings.Items.Add("49");
            this.UsageSettings.Items.Add("50");
            this.UsageSettings.Items.Add("51");
            this.UsageSettings.Items.Add("52");
            this.UsageSettings.Items.Add("53");
            this.UsageSettings.Items.Add("54");
            this.UsageSettings.Items.Add("55");
            this.UsageSettings.Items.Add("56");
            this.UsageSettings.Items.Add("57");
            this.UsageSettings.Items.Add("58");
            this.UsageSettings.Items.Add("59");
            this.UsageSettings.Items.Add("60");
            this.UsageSettings.Items.Add("61");
            this.UsageSettings.Items.Add("62");
            this.UsageSettings.Items.Add("63");
            this.UsageSettings.Items.Add("64");
            this.UsageSettings.Items.Add("65");
            this.UsageSettings.Items.Add("66");
            this.UsageSettings.Items.Add("67");
            this.UsageSettings.Items.Add("68");
            this.UsageSettings.Items.Add("69");
            this.UsageSettings.Items.Add("70");
            this.UsageSettings.Items.Add("71");
            this.UsageSettings.Items.Add("72");
            this.UsageSettings.Items.Add("73");
            this.UsageSettings.Items.Add("74");
            this.UsageSettings.Items.Add("75");
            this.UsageSettings.Items.Add("76");
            this.UsageSettings.Items.Add("77");
            this.UsageSettings.Items.Add("78");
            this.UsageSettings.Items.Add("79");
            this.UsageSettings.Items.Add("80");
            this.UsageSettings.Items.Add("81");
            this.UsageSettings.Items.Add("82");
            this.UsageSettings.Items.Add("83");
            this.UsageSettings.Items.Add("84");
            this.UsageSettings.Items.Add("85");
            this.UsageSettings.Items.Add("86");
            this.UsageSettings.Items.Add("87");
            this.UsageSettings.Items.Add("88");
            this.UsageSettings.Items.Add("89");
            this.UsageSettings.Items.Add("90");
            this.UsageSettings.Items.Add("91");
            this.UsageSettings.Items.Add("92");
            this.UsageSettings.Items.Add("93");
            this.UsageSettings.Items.Add("94");
            this.UsageSettings.Items.Add("95");
            this.UsageSettings.Items.Add("96");
            this.UsageSettings.Items.Add("97");
            this.UsageSettings.Items.Add("98");
            this.UsageSettings.Items.Add("99");
            this.UsageSettings.Location = new System.Drawing.Point(253, 135);
            this.UsageSettings.Name = "UsageSettings";
            this.UsageSettings.Size = new System.Drawing.Size(120, 20);
            this.UsageSettings.TabIndex = 16;
            this.UsageSettings.Text = "Usage Limit";
            this.UsageSettings.SelectedItemChanged += new System.EventHandler(this.UsageSettings_SelectedItemChanged);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 420);
            this.Controls.Add(this.UsageSettings);
            this.Controls.Add(this.TUsingBox);
            this.Controls.Add(this.AddClientB);
            this.Controls.Add(this.processes);
            this.Controls.Add(this.Clients);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerForm";
            this.Text = "Server Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computersBasicDDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox UUIDbox;
        public System.Windows.Forms.TextBox UserNameBox;
        public System.Windows.Forms.TextBox OsBox;
        public System.Windows.Forms.TextBox CPUBox;
        public System.Windows.Forms.TextBox CpuNumBox;
        public System.Windows.Forms.TextBox RAMBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListBox Clients;
        public System.Windows.Forms.DataGridView processes;
        public System.Windows.Forms.TextBox IPBox;
        public System.Windows.Forms.Button AddClientB;
        public System.Windows.Forms.TextBox TUsingBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn pIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usingDataGridViewTextBoxColumn;
        public System.Windows.Forms.DomainUpDown UsageSettings;
        private ComputersBasicDDataSet computersBasicDDataSet;
    }
}

