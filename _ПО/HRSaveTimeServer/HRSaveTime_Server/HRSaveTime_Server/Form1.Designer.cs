﻿namespace HRSaveTime_Server
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Send_btn = new System.Windows.Forms.Button();
            this.Inquiry_tBox = new System.Windows.Forms.TextBox();
            this.Monitor_tBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.EditRooms_btn = new System.Windows.Forms.Button();
            this.SaveRooms = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ComOut_cBox = new System.Windows.Forms.ComboBox();
            this.CheckOut_btn = new System.Windows.Forms.Button();
            this.StatusOut_label = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ComIn_cBox = new System.Windows.Forms.ComboBox();
            this.CheckIn_btn = new System.Windows.Forms.Button();
            this.StatusIn_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Room_lebel = new System.Windows.Forms.Label();
            this.Rooms_cBox = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AddRule_btn = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PasswordOracle_tBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ConnectToBDOracle_btn = new System.Windows.Forms.Button();
            this.LoginOracle_tBox = new System.Windows.Forms.TextBox();
            this.StatusOracle_lebel = new System.Windows.Forms.Label();
            this.StatusUpdateBDOracle_btn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AddBDAccesse_btn = new System.Windows.Forms.Button();
            this.WayToBDAccesse_tBox = new System.Windows.Forms.TextBox();
            this.StatusAccesse_lebel = new System.Windows.Forms.Label();
            this.StatusUpdateBDAccesse_btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ConnectToBDAccesse_btn = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(660, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Send_btn);
            this.tabPage3.Controls.Add(this.Inquiry_tBox);
            this.tabPage3.Controls.Add(this.Monitor_tBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(652, 400);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Лог";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Send_btn
            // 
            this.Send_btn.Location = new System.Drawing.Point(557, 364);
            this.Send_btn.Name = "Send_btn";
            this.Send_btn.Size = new System.Drawing.Size(92, 33);
            this.Send_btn.TabIndex = 2;
            this.Send_btn.Text = "Отправить";
            this.Send_btn.UseVisualStyleBackColor = true;
            this.Send_btn.Click += new System.EventHandler(this.Send_btn_Click);
            // 
            // Inquiry_tBox
            // 
            this.Inquiry_tBox.Location = new System.Drawing.Point(3, 366);
            this.Inquiry_tBox.Multiline = true;
            this.Inquiry_tBox.Name = "Inquiry_tBox";
            this.Inquiry_tBox.Size = new System.Drawing.Size(548, 31);
            this.Inquiry_tBox.TabIndex = 1;
            // 
            // Monitor_tBox
            // 
            this.Monitor_tBox.Location = new System.Drawing.Point(3, 3);
            this.Monitor_tBox.Multiline = true;
            this.Monitor_tBox.Name = "Monitor_tBox";
            this.Monitor_tBox.Size = new System.Drawing.Size(646, 355);
            this.Monitor_tBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.EditRooms_btn);
            this.tabPage2.Controls.Add(this.SaveRooms);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.Room_lebel);
            this.tabPage2.Controls.Add(this.Rooms_cBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(652, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "СКУД";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // EditRooms_btn
            // 
            this.EditRooms_btn.Location = new System.Drawing.Point(282, 24);
            this.EditRooms_btn.Name = "EditRooms_btn";
            this.EditRooms_btn.Size = new System.Drawing.Size(105, 23);
            this.EditRooms_btn.TabIndex = 7;
            this.EditRooms_btn.Text = "Редактировать";
            this.EditRooms_btn.UseVisualStyleBackColor = true;
            this.EditRooms_btn.Click += new System.EventHandler(this.EditRooms_Click);
            // 
            // SaveRooms
            // 
            this.SaveRooms.Location = new System.Drawing.Point(559, 367);
            this.SaveRooms.Name = "SaveRooms";
            this.SaveRooms.Size = new System.Drawing.Size(75, 23);
            this.SaveRooms.TabIndex = 5;
            this.SaveRooms.Text = "Сохранить";
            this.SaveRooms.UseVisualStyleBackColor = true;
            this.SaveRooms.Click += new System.EventHandler(this.SaveRooms_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ComOut_cBox);
            this.groupBox2.Controls.Add(this.CheckOut_btn);
            this.groupBox2.Controls.Add(this.StatusOut_label);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(336, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 282);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выход";
            // 
            // ComOut_cBox
            // 
            this.ComOut_cBox.FormattingEnabled = true;
            this.ComOut_cBox.Location = new System.Drawing.Point(110, 78);
            this.ComOut_cBox.Name = "ComOut_cBox";
            this.ComOut_cBox.Size = new System.Drawing.Size(121, 21);
            this.ComOut_cBox.TabIndex = 8;
            // 
            // CheckOut_btn
            // 
            this.CheckOut_btn.Location = new System.Drawing.Point(198, 139);
            this.CheckOut_btn.Name = "CheckOut_btn";
            this.CheckOut_btn.Size = new System.Drawing.Size(75, 23);
            this.CheckOut_btn.TabIndex = 7;
            this.CheckOut_btn.Text = "Проверить";
            this.CheckOut_btn.UseVisualStyleBackColor = true;
            this.CheckOut_btn.Click += new System.EventHandler(this.CheckOut_btn_Click);
            // 
            // StatusOut_label
            // 
            this.StatusOut_label.AutoSize = true;
            this.StatusOut_label.Location = new System.Drawing.Point(107, 144);
            this.StatusOut_label.Name = "StatusOut_label";
            this.StatusOut_label.Size = new System.Drawing.Size(35, 13);
            this.StatusOut_label.TabIndex = 5;
            this.StatusOut_label.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Статус";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "COM-порт";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ComIn_cBox);
            this.groupBox1.Controls.Add(this.CheckIn_btn);
            this.groupBox1.Controls.Add(this.StatusIn_label);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 282);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Вход";
            // 
            // ComIn_cBox
            // 
            this.ComIn_cBox.FormattingEnabled = true;
            this.ComIn_cBox.Location = new System.Drawing.Point(104, 78);
            this.ComIn_cBox.Name = "ComIn_cBox";
            this.ComIn_cBox.Size = new System.Drawing.Size(121, 21);
            this.ComIn_cBox.TabIndex = 7;
            // 
            // CheckIn_btn
            // 
            this.CheckIn_btn.Location = new System.Drawing.Point(192, 139);
            this.CheckIn_btn.Name = "CheckIn_btn";
            this.CheckIn_btn.Size = new System.Drawing.Size(75, 23);
            this.CheckIn_btn.TabIndex = 6;
            this.CheckIn_btn.Text = "Проверить";
            this.CheckIn_btn.UseVisualStyleBackColor = true;
            this.CheckIn_btn.Click += new System.EventHandler(this.CheckIn_btn_Click);
            // 
            // StatusIn_label
            // 
            this.StatusIn_label.AutoSize = true;
            this.StatusIn_label.Location = new System.Drawing.Point(101, 144);
            this.StatusIn_label.Name = "StatusIn_label";
            this.StatusIn_label.Size = new System.Drawing.Size(35, 13);
            this.StatusIn_label.TabIndex = 3;
            this.StatusIn_label.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Статус";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "COM-порт";
            // 
            // Room_lebel
            // 
            this.Room_lebel.AutoSize = true;
            this.Room_lebel.Location = new System.Drawing.Point(17, 29);
            this.Room_lebel.Name = "Room_lebel";
            this.Room_lebel.Size = new System.Drawing.Size(51, 13);
            this.Room_lebel.TabIndex = 2;
            this.Room_lebel.Text = "Комната";
            // 
            // Rooms_cBox
            // 
            this.Rooms_cBox.FormattingEnabled = true;
            this.Rooms_cBox.Location = new System.Drawing.Point(74, 25);
            this.Rooms_cBox.Name = "Rooms_cBox";
            this.Rooms_cBox.Size = new System.Drawing.Size(197, 21);
            this.Rooms_cBox.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.AddRule_btn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(652, 400);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Роли";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16});
            this.dataGridView1.Location = new System.Drawing.Point(17, 15);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(620, 330);
            this.dataGridView1.TabIndex = 1;
            // 
            // AddRule_btn
            // 
            this.AddRule_btn.Location = new System.Drawing.Point(17, 360);
            this.AddRule_btn.Name = "AddRule_btn";
            this.AddRule_btn.Size = new System.Drawing.Size(75, 23);
            this.AddRule_btn.TabIndex = 0;
            this.AddRule_btn.Text = "Добавить";
            this.AddRule_btn.UseVisualStyleBackColor = true;
            this.AddRule_btn.Click += new System.EventHandler(this.AddRule_btn_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(652, 400);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Настройки";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PasswordOracle_tBox);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.ConnectToBDOracle_btn);
            this.groupBox4.Controls.Add(this.LoginOracle_tBox);
            this.groupBox4.Controls.Add(this.StatusOracle_lebel);
            this.groupBox4.Controls.Add(this.StatusUpdateBDOracle_btn);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(14, 205);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(283, 179);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "База данных Oracle";
            // 
            // PasswordOracle_tBox
            // 
            this.PasswordOracle_tBox.Location = new System.Drawing.Point(71, 64);
            this.PasswordOracle_tBox.Name = "PasswordOracle_tBox";
            this.PasswordOracle_tBox.Size = new System.Drawing.Size(186, 20);
            this.PasswordOracle_tBox.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Пароль:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Логин:";
            // 
            // ConnectToBDOracle_btn
            // 
            this.ConnectToBDOracle_btn.Location = new System.Drawing.Point(172, 104);
            this.ConnectToBDOracle_btn.Name = "ConnectToBDOracle_btn";
            this.ConnectToBDOracle_btn.Size = new System.Drawing.Size(85, 23);
            this.ConnectToBDOracle_btn.TabIndex = 4;
            this.ConnectToBDOracle_btn.Text = "Подключить";
            this.ConnectToBDOracle_btn.UseVisualStyleBackColor = true;
            this.ConnectToBDOracle_btn.Click += new System.EventHandler(this.ConnectToBDOracle_btn_Click);
            // 
            // LoginOracle_tBox
            // 
            this.LoginOracle_tBox.Location = new System.Drawing.Point(71, 32);
            this.LoginOracle_tBox.Name = "LoginOracle_tBox";
            this.LoginOracle_tBox.Size = new System.Drawing.Size(186, 20);
            this.LoginOracle_tBox.TabIndex = 3;
            // 
            // StatusOracle_lebel
            // 
            this.StatusOracle_lebel.AutoSize = true;
            this.StatusOracle_lebel.Location = new System.Drawing.Point(68, 114);
            this.StatusOracle_lebel.Name = "StatusOracle_lebel";
            this.StatusOracle_lebel.Size = new System.Drawing.Size(37, 13);
            this.StatusOracle_lebel.TabIndex = 2;
            this.StatusOracle_lebel.Text = "Status";
            // 
            // StatusUpdateBDOracle_btn
            // 
            this.StatusUpdateBDOracle_btn.Location = new System.Drawing.Point(20, 136);
            this.StatusUpdateBDOracle_btn.Name = "StatusUpdateBDOracle_btn";
            this.StatusUpdateBDOracle_btn.Size = new System.Drawing.Size(75, 23);
            this.StatusUpdateBDOracle_btn.TabIndex = 1;
            this.StatusUpdateBDOracle_btn.Text = "Проверить";
            this.StatusUpdateBDOracle_btn.UseVisualStyleBackColor = true;
            this.StatusUpdateBDOracle_btn.Click += new System.EventHandler(this.ConnectToBDOracle_btn_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Статус:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ConnectToBDAccesse_btn);
            this.groupBox3.Controls.Add(this.AddBDAccesse_btn);
            this.groupBox3.Controls.Add(this.WayToBDAccesse_tBox);
            this.groupBox3.Controls.Add(this.StatusAccesse_lebel);
            this.groupBox3.Controls.Add(this.StatusUpdateBDAccesse_btn);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(14, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 149);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "База данных MS Accesse";
            // 
            // AddBDAccesse_btn
            // 
            this.AddBDAccesse_btn.Location = new System.Drawing.Point(182, 35);
            this.AddBDAccesse_btn.Name = "AddBDAccesse_btn";
            this.AddBDAccesse_btn.Size = new System.Drawing.Size(75, 23);
            this.AddBDAccesse_btn.TabIndex = 4;
            this.AddBDAccesse_btn.Text = "Добавить";
            this.AddBDAccesse_btn.UseVisualStyleBackColor = true;
            this.AddBDAccesse_btn.Click += new System.EventHandler(this.AddBDAccesse_btn_Click);
            // 
            // WayToBDAccesse_tBox
            // 
            this.WayToBDAccesse_tBox.Location = new System.Drawing.Point(23, 37);
            this.WayToBDAccesse_tBox.Name = "WayToBDAccesse_tBox";
            this.WayToBDAccesse_tBox.Size = new System.Drawing.Size(153, 20);
            this.WayToBDAccesse_tBox.TabIndex = 3;
            // 
            // StatusAccesse_lebel
            // 
            this.StatusAccesse_lebel.AutoSize = true;
            this.StatusAccesse_lebel.Location = new System.Drawing.Point(68, 88);
            this.StatusAccesse_lebel.Name = "StatusAccesse_lebel";
            this.StatusAccesse_lebel.Size = new System.Drawing.Size(37, 13);
            this.StatusAccesse_lebel.TabIndex = 2;
            this.StatusAccesse_lebel.Text = "Status";
            // 
            // StatusUpdateBDAccesse_btn
            // 
            this.StatusUpdateBDAccesse_btn.Location = new System.Drawing.Point(20, 111);
            this.StatusUpdateBDAccesse_btn.Name = "StatusUpdateBDAccesse_btn";
            this.StatusUpdateBDAccesse_btn.Size = new System.Drawing.Size(75, 23);
            this.StatusUpdateBDAccesse_btn.TabIndex = 1;
            this.StatusUpdateBDAccesse_btn.Text = "Проверить";
            this.StatusUpdateBDAccesse_btn.UseVisualStyleBackColor = true;
            this.StatusUpdateBDAccesse_btn.Click += new System.EventHandler(this.StatusUpdateBDAccesse_btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Статус:";
            // 
            // ConnectToBDAccesse_btn
            // 
            this.ConnectToBDAccesse_btn.Location = new System.Drawing.Point(172, 78);
            this.ConnectToBDAccesse_btn.Name = "ConnectToBDAccesse_btn";
            this.ConnectToBDAccesse_btn.Size = new System.Drawing.Size(85, 23);
            this.ConnectToBDAccesse_btn.TabIndex = 5;
            this.ConnectToBDAccesse_btn.Text = "Подключить";
            this.ConnectToBDAccesse_btn.UseVisualStyleBackColor = true;
            this.ConnectToBDAccesse_btn.Click += new System.EventHandler(this.ConnectToBDAccesse_btn_Click);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Имя";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Описание";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            this.Column6.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            this.Column7.Width = 80;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            this.Column8.Width = 80;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.Name = "Column9";
            this.Column9.Width = 80;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Name = "Column10";
            this.Column10.Width = 80;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.Name = "Column11";
            this.Column11.Width = 80;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Column12";
            this.Column12.Name = "Column12";
            this.Column12.Width = 80;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Column13";
            this.Column13.Name = "Column13";
            this.Column13.Width = 80;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Column14";
            this.Column14.Name = "Column14";
            this.Column14.Width = 80;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.Name = "Column15";
            this.Column15.Width = 80;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Column16";
            this.Column16.Name = "Column16";
            this.Column16.Width = 80;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button Send_btn;
        private System.Windows.Forms.TextBox Inquiry_tBox;
        private System.Windows.Forms.TextBox Monitor_tBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button SaveRooms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Room_lebel;
        private System.Windows.Forms.ComboBox Rooms_cBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button EditRooms_btn;
        private System.Windows.Forms.Button CheckOut_btn;
        private System.Windows.Forms.Label StatusOut_label;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button CheckIn_btn;
        private System.Windows.Forms.Label StatusIn_label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button StatusUpdateBDAccesse_btn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label StatusAccesse_lebel;
        private System.Windows.Forms.Button AddBDAccesse_btn;
        private System.Windows.Forms.TextBox WayToBDAccesse_tBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button AddRule_btn;
        private System.Windows.Forms.ComboBox ComOut_cBox;
        private System.Windows.Forms.ComboBox ComIn_cBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox PasswordOracle_tBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button ConnectToBDOracle_btn;
        private System.Windows.Forms.TextBox LoginOracle_tBox;
        private System.Windows.Forms.Label StatusOracle_lebel;
        private System.Windows.Forms.Button StatusUpdateBDOracle_btn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ConnectToBDAccesse_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;

    }
}

