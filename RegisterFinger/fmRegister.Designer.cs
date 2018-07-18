namespace RegisterFinger
{
    partial class fmRegister
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
            this.label16 = new System.Windows.Forms.Label();
            this.cmbdevices = new System.Windows.Forms.ComboBox();
            this.labsearchdev = new System.Windows.Forms.Label();
            this.btConnect = new System.Windows.Forms.Button();
            this.lbstatus = new System.Windows.Forms.Label();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.cbFinger = new System.Windows.Forms.CheckBox();
            this.cbFace = new System.Windows.Forms.CheckBox();
            this.btUpload = new System.Windows.Forms.Button();
            this.cbCard = new System.Windows.Forms.CheckBox();
            this.txtFaceCount = new System.Windows.Forms.TextBox();
            this.txtFingerCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lsdevices = new System.Windows.Forms.ListView();
            this.colip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.coltype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblStatustb2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbVaildTb2 = new System.Windows.Forms.CheckBox();
            this.lvDevicestb2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.cbUserTb2 = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnConnecttb3 = new System.Windows.Forms.Button();
            this.lblStatustb3 = new System.Windows.Forms.Label();
            this.cbDevicestb3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUploadTb3 = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.txtCardNumbertb3 = new System.Windows.Forms.TextBox();
            this.lvDevicestb3 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.cbUserTb3 = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnConnecttb4 = new System.Windows.Forms.Button();
            this.btnReadLog = new System.Windows.Forms.Button();
            this.lblStatustb4 = new System.Windows.Forms.Label();
            this.cbDevicestb4 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(34, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Select device:";
            // 
            // cmbdevices
            // 
            this.cmbdevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbdevices.FormattingEnabled = true;
            this.cmbdevices.Location = new System.Drawing.Point(112, 27);
            this.cmbdevices.Name = "cmbdevices";
            this.cmbdevices.Size = new System.Drawing.Size(121, 21);
            this.cmbdevices.TabIndex = 6;
            this.cmbdevices.SelectedIndexChanged += new System.EventHandler(this.cmbdevices_SelectedIndexChanged);
            // 
            // labsearchdev
            // 
            this.labsearchdev.AutoSize = true;
            this.labsearchdev.Location = new System.Drawing.Point(469, 63);
            this.labsearchdev.Name = "labsearchdev";
            this.labsearchdev.Size = new System.Drawing.Size(0, 13);
            this.labsearchdev.TabIndex = 8;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(253, 25);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(120, 30);
            this.btConnect.TabIndex = 11;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // lbstatus
            // 
            this.lbstatus.AutoSize = true;
            this.lbstatus.Location = new System.Drawing.Point(250, 76);
            this.lbstatus.Name = "lbstatus";
            this.lbstatus.Size = new System.Drawing.Size(38, 13);
            this.lbstatus.TabIndex = 12;
            this.lbstatus.Text = "Status";
            // 
            // cbUser
            // 
            this.cbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbUser.Location = new System.Drawing.Point(112, 76);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(121, 21);
            this.cbUser.TabIndex = 48;
            this.cbUser.SelectedIndexChanged += new System.EventHandler(this.cbUser_SelectedIndexChanged_1);
            this.cbUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbUser_KeyDown);
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(112, 121);
            this.txtCardNumber.MaxLength = 10;
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(121, 20);
            this.txtCardNumber.TabIndex = 54;
            // 
            // cbFinger
            // 
            this.cbFinger.AutoSize = true;
            this.cbFinger.Checked = true;
            this.cbFinger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFinger.Location = new System.Drawing.Point(253, 124);
            this.cbFinger.Name = "cbFinger";
            this.cbFinger.Size = new System.Drawing.Size(88, 17);
            this.cbFinger.TabIndex = 55;
            this.cbFinger.Text = "Finger Count";
            this.cbFinger.UseVisualStyleBackColor = true;
            // 
            // cbFace
            // 
            this.cbFace.AutoSize = true;
            this.cbFace.Checked = true;
            this.cbFace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFace.Location = new System.Drawing.Point(403, 124);
            this.cbFace.Name = "cbFace";
            this.cbFace.Size = new System.Drawing.Size(81, 17);
            this.cbFace.TabIndex = 56;
            this.cbFace.Text = "Face Count";
            this.cbFace.UseVisualStyleBackColor = true;
            // 
            // btUpload
            // 
            this.btUpload.Location = new System.Drawing.Point(253, 329);
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(120, 30);
            this.btUpload.TabIndex = 57;
            this.btUpload.Text = "Upload && Download ";
            this.btUpload.UseVisualStyleBackColor = true;
            this.btUpload.Click += new System.EventHandler(this.btUpload_Click);
            // 
            // cbCard
            // 
            this.cbCard.AutoSize = true;
            this.cbCard.Checked = true;
            this.cbCard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCard.Location = new System.Drawing.Point(20, 124);
            this.cbCard.Name = "cbCard";
            this.cbCard.Size = new System.Drawing.Size(89, 17);
            this.cbCard.TabIndex = 58;
            this.cbCard.Text = "Card Number";
            this.cbCard.UseVisualStyleBackColor = true;
            // 
            // txtFaceCount
            // 
            this.txtFaceCount.Enabled = false;
            this.txtFaceCount.Location = new System.Drawing.Point(489, 121);
            this.txtFaceCount.MaxLength = 2;
            this.txtFaceCount.Name = "txtFaceCount";
            this.txtFaceCount.Size = new System.Drawing.Size(49, 20);
            this.txtFaceCount.TabIndex = 60;
            // 
            // txtFingerCount
            // 
            this.txtFingerCount.Enabled = false;
            this.txtFingerCount.Location = new System.Drawing.Point(348, 121);
            this.txtFingerCount.MaxLength = 2;
            this.txtFingerCount.Name = "txtFingerCount";
            this.txtFingerCount.Size = new System.Drawing.Size(49, 20);
            this.txtFingerCount.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Users";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(580, 429);
            this.tabControl1.TabIndex = 67;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.lsdevices);
            this.tabPage1.Controls.Add(this.btUpload);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmbdevices);
            this.tabPage1.Controls.Add(this.txtFingerCount);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtFaceCount);
            this.tabPage1.Controls.Add(this.labsearchdev);
            this.tabPage1.Controls.Add(this.cbCard);
            this.tabPage1.Controls.Add(this.btConnect);
            this.tabPage1.Controls.Add(this.cbFace);
            this.tabPage1.Controls.Add(this.lbstatus);
            this.tabPage1.Controls.Add(this.cbFinger);
            this.tabPage1.Controls.Add(this.cbUser);
            this.tabPage1.Controls.Add(this.txtCardNumber);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(572, 403);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Upload & Download";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // lsdevices
            // 
            this.lsdevices.CheckBoxes = true;
            this.lsdevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colip,
            this.colio,
            this.coltype,
            this.status});
            this.lsdevices.GridLines = true;
            this.lsdevices.Location = new System.Drawing.Point(20, 157);
            this.lsdevices.Name = "lsdevices";
            this.lsdevices.Size = new System.Drawing.Size(518, 166);
            this.lsdevices.TabIndex = 68;
            this.lsdevices.UseCompatibleStateImageBehavior = false;
            this.lsdevices.View = System.Windows.Forms.View.Details;
            // 
            // colip
            // 
            this.colip.Text = "IPAddress";
            this.colip.Width = 100;
            // 
            // colio
            // 
            this.colio.Text = "InOut";
            this.colio.Width = 52;
            // 
            // coltype
            // 
            this.coltype.Text = "Device Type";
            this.coltype.Width = 88;
            // 
            // status
            // 
            this.status.Text = "Status";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblStatustb2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.cbVaildTb2);
            this.tabPage2.Controls.Add(this.lvDevicestb2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbUserTb2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(572, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Invaild User ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblStatustb2
            // 
            this.lblStatustb2.AutoSize = true;
            this.lblStatustb2.Location = new System.Drawing.Point(309, 29);
            this.lblStatustb2.Name = "lblStatustb2";
            this.lblStatustb2.Size = new System.Drawing.Size(38, 13);
            this.lblStatustb2.TabIndex = 92;
            this.lblStatustb2.Text = "Status";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(238, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 37);
            this.button1.TabIndex = 80;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbVaildTb2
            // 
            this.cbVaildTb2.AutoSize = true;
            this.cbVaildTb2.Location = new System.Drawing.Point(164, 300);
            this.cbVaildTb2.Name = "cbVaildTb2";
            this.cbVaildTb2.Size = new System.Drawing.Size(58, 17);
            this.cbVaildTb2.TabIndex = 79;
            this.cbVaildTb2.Text = "Enable";
            this.cbVaildTb2.UseVisualStyleBackColor = true;
            // 
            // lvDevicestb2
            // 
            this.lvDevicestb2.CheckBoxes = true;
            this.lvDevicestb2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvDevicestb2.GridLines = true;
            this.lvDevicestb2.Location = new System.Drawing.Point(45, 78);
            this.lvDevicestb2.Name = "lvDevicestb2";
            this.lvDevicestb2.Size = new System.Drawing.Size(475, 196);
            this.lvDevicestb2.TabIndex = 75;
            this.lvDevicestb2.UseCompatibleStateImageBehavior = false;
            this.lvDevicestb2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "IPAddress";
            this.columnHeader1.Width = 104;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "InOut";
            this.columnHeader2.Width = 47;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Device Type";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            this.columnHeader4.Width = 231;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Employee";
            // 
            // cbUserTb2
            // 
            this.cbUserTb2.FormattingEnabled = true;
            this.cbUserTb2.Location = new System.Drawing.Point(120, 26);
            this.cbUserTb2.Name = "cbUserTb2";
            this.cbUserTb2.Size = new System.Drawing.Size(172, 21);
            this.cbUserTb2.TabIndex = 73;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnConnecttb3);
            this.tabPage3.Controls.Add(this.lblStatustb3);
            this.tabPage3.Controls.Add(this.cbDevicestb3);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.btnUploadTb3);
            this.tabPage3.Controls.Add(this.checkBox4);
            this.tabPage3.Controls.Add(this.txtCardNumbertb3);
            this.tabPage3.Controls.Add(this.lvDevicestb3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.cbUserTb3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(572, 403);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Register Card";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnConnecttb3
            // 
            this.btnConnecttb3.Location = new System.Drawing.Point(267, 24);
            this.btnConnecttb3.Name = "btnConnecttb3";
            this.btnConnecttb3.Size = new System.Drawing.Size(112, 31);
            this.btnConnecttb3.TabIndex = 92;
            this.btnConnecttb3.Text = "Connect";
            this.btnConnecttb3.UseVisualStyleBackColor = true;
            this.btnConnecttb3.Click += new System.EventHandler(this.btnConnecttb3_Click);
            // 
            // lblStatustb3
            // 
            this.lblStatustb3.AutoSize = true;
            this.lblStatustb3.Location = new System.Drawing.Point(399, 33);
            this.lblStatustb3.Name = "lblStatustb3";
            this.lblStatustb3.Size = new System.Drawing.Size(38, 13);
            this.lblStatustb3.TabIndex = 91;
            this.lblStatustb3.Text = "Status";
            // 
            // cbDevicestb3
            // 
            this.cbDevicestb3.FormattingEnabled = true;
            this.cbDevicestb3.Location = new System.Drawing.Point(118, 28);
            this.cbDevicestb3.Name = "cbDevicestb3";
            this.cbDevicestb3.Size = new System.Drawing.Size(133, 21);
            this.cbDevicestb3.TabIndex = 89;
            this.cbDevicestb3.SelectedValueChanged += new System.EventHandler(this.cbDevicestb3_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 90;
            this.label5.Text = "Select device:";
            // 
            // btnUploadTb3
            // 
            this.btnUploadTb3.Location = new System.Drawing.Point(222, 311);
            this.btnUploadTb3.Name = "btnUploadTb3";
            this.btnUploadTb3.Size = new System.Drawing.Size(157, 36);
            this.btnUploadTb3.TabIndex = 88;
            this.btnUploadTb3.Text = "Upload";
            this.btnUploadTb3.UseVisualStyleBackColor = true;
            this.btnUploadTb3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(267, 82);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(89, 17);
            this.checkBox4.TabIndex = 86;
            this.checkBox4.Text = "Card Number";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // txtCardNumbertb3
            // 
            this.txtCardNumbertb3.Location = new System.Drawing.Point(374, 80);
            this.txtCardNumbertb3.Name = "txtCardNumbertb3";
            this.txtCardNumbertb3.Size = new System.Drawing.Size(133, 20);
            this.txtCardNumbertb3.TabIndex = 85;
            // 
            // lvDevicestb3
            // 
            this.lvDevicestb3.CheckBoxes = true;
            this.lvDevicestb3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lvDevicestb3.GridLines = true;
            this.lvDevicestb3.Location = new System.Drawing.Point(43, 119);
            this.lvDevicestb3.Name = "lvDevicestb3";
            this.lvDevicestb3.Size = new System.Drawing.Size(464, 175);
            this.lvDevicestb3.TabIndex = 83;
            this.lvDevicestb3.UseCompatibleStateImageBehavior = false;
            this.lvDevicestb3.View = System.Windows.Forms.View.Details;
            this.lvDevicestb3.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDevicestb3_ItemCheck);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "IPAddress";
            this.columnHeader5.Width = 96;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "InOut";
            this.columnHeader6.Width = 44;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Device Type";
            this.columnHeader7.Width = 71;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Status";
            this.columnHeader8.Width = 231;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Result";
            this.columnHeader9.Width = 71;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Employee";
            // 
            // cbUserTb3
            // 
            this.cbUserTb3.FormattingEnabled = true;
            this.cbUserTb3.Location = new System.Drawing.Point(118, 82);
            this.cbUserTb3.Name = "cbUserTb3";
            this.cbUserTb3.Size = new System.Drawing.Size(133, 21);
            this.cbUserTb3.TabIndex = 81;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnReadLog);
            this.tabPage4.Controls.Add(this.lblStatustb4);
            this.tabPage4.Controls.Add(this.cbDevicestb4);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.btnConnecttb4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(572, 403);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Read Log";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnConnecttb4
            // 
            this.btnConnecttb4.Location = new System.Drawing.Point(315, 59);
            this.btnConnecttb4.Name = "btnConnecttb4";
            this.btnConnecttb4.Size = new System.Drawing.Size(112, 31);
            this.btnConnecttb4.TabIndex = 101;
            this.btnConnecttb4.Text = "Connect";
            this.btnConnecttb4.UseVisualStyleBackColor = true;
            this.btnConnecttb4.Click += new System.EventHandler(this.btnConnecttb4_Click);
            // 
            // btnReadLog
            // 
            this.btnReadLog.Location = new System.Drawing.Point(210, 166);
            this.btnReadLog.Name = "btnReadLog";
            this.btnReadLog.Size = new System.Drawing.Size(149, 31);
            this.btnReadLog.TabIndex = 105;
            this.btnReadLog.Text = "Save Log";
            this.btnReadLog.UseVisualStyleBackColor = true;
            this.btnReadLog.Click += new System.EventHandler(this.btnReadLog_Click);
            // 
            // lblStatustb4
            // 
            this.lblStatustb4.AutoSize = true;
            this.lblStatustb4.Location = new System.Drawing.Point(450, 66);
            this.lblStatustb4.Name = "lblStatustb4";
            this.lblStatustb4.Size = new System.Drawing.Size(38, 13);
            this.lblStatustb4.TabIndex = 104;
            this.lblStatustb4.Text = "Status";
            // 
            // cbDevicestb4
            // 
            this.cbDevicestb4.FormattingEnabled = true;
            this.cbDevicestb4.Location = new System.Drawing.Point(152, 63);
            this.cbDevicestb4.Name = "cbDevicestb4";
            this.cbDevicestb4.Size = new System.Drawing.Size(133, 21);
            this.cbDevicestb4.TabIndex = 102;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 103;
            this.label6.Text = "Select device:";
            // 
            // fmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 429);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "fmRegister";
            this.Text = "Uplaod & Download Data";
            this.Load += new System.EventHandler(this.fmRegister_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbdevices;
        private System.Windows.Forms.Label labsearchdev;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Label lbstatus;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.CheckBox cbFinger;
        private System.Windows.Forms.CheckBox cbFace;
        private System.Windows.Forms.Button btUpload;
        private System.Windows.Forms.CheckBox cbCard;
        private System.Windows.Forms.TextBox txtFaceCount;
        private System.Windows.Forms.TextBox txtFingerCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lsdevices;
        private System.Windows.Forms.ColumnHeader colip;
        private System.Windows.Forms.ColumnHeader colio;
        private System.Windows.Forms.ColumnHeader coltype;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbVaildTb2;
        private System.Windows.Forms.ListView lvDevicestb2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbUserTb2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnUploadTb3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox txtCardNumbertb3;
        private System.Windows.Forms.ListView lvDevicestb3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbUserTb3;
        private System.Windows.Forms.Label lblStatustb3;
        private System.Windows.Forms.ComboBox cbDevicestb3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnecttb3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblStatustb2;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnReadLog;
        private System.Windows.Forms.Label lblStatustb4;
        private System.Windows.Forms.ComboBox cbDevicestb4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConnecttb4;
    }
}

