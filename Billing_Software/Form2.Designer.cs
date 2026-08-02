#nullable enable
using System.Drawing;
using System.Windows.Forms;

namespace Billing_Software
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

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
            this.panel_Main = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel_GateWay = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ll_TransectionAccount = new System.Windows.Forms.LinkLabel();
            this.ll_TransectionInventory = new System.Windows.Forms.LinkLabel();
            this.ll_MasterInventory = new System.Windows.Forms.LinkLabel();
            this.ll_MasterAccount = new System.Windows.Forms.LinkLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_LeftSide = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.panel_Right = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel_Bottum = new System.Windows.Forms.Panel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel_Top = new System.Windows.Forms.Panel();
            this.label_CompanyName = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.label_Main = new System.Windows.Forms.Label();
            this.panel_Main.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_GateWay.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel_Right.SuspendLayout();
            this.panel_Bottum.SuspendLayout();
            this.panel_Top.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Main
            // 
            this.panel_Main.AutoSize = true;
            this.panel_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel_Main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Main.Controls.Add(this.panel1);
            this.panel_Main.Controls.Add(this.panel_GateWay);
            this.panel_Main.Controls.Add(this.splitter1);
            this.panel_Main.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_Main.Location = new System.Drawing.Point(58, 69);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(860, 533);
            this.panel_Main.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label8);
            this.panel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Location = new System.Drawing.Point(505, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 39);
            this.panel1.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(34, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(202, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Company Information";
            // 
            // panel_GateWay
            // 
            this.panel_GateWay.Controls.Add(this.linkLabel1);
            this.panel_GateWay.Controls.Add(this.ll_TransectionAccount);
            this.panel_GateWay.Controls.Add(this.ll_TransectionInventory);
            this.panel_GateWay.Controls.Add(this.ll_MasterInventory);
            this.panel_GateWay.Controls.Add(this.ll_MasterAccount);
            this.panel_GateWay.Location = new System.Drawing.Point(502, 32);
            this.panel_GateWay.Name = "panel_GateWay";
            this.panel_GateWay.Size = new System.Drawing.Size(283, 459);
            this.panel_GateWay.TabIndex = 26;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.Navy;
            this.linkLabel1.Location = new System.Drawing.Point(52, 248);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(58, 27);
            this.linkLabel1.TabIndex = 23;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Exit ";
            // 
            // ll_TransectionAccount
            // 
            this.ll_TransectionAccount.AutoSize = true;
            this.ll_TransectionAccount.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ll_TransectionAccount.ForeColor = System.Drawing.Color.Navy;
            this.ll_TransectionAccount.Location = new System.Drawing.Point(52, 156);
            this.ll_TransectionAccount.Name = "ll_TransectionAccount";
            this.ll_TransectionAccount.Size = new System.Drawing.Size(92, 27);
            this.ll_TransectionAccount.TabIndex = 20;
            this.ll_TransectionAccount.TabStop = true;
            this.ll_TransectionAccount.Text = "Back up";
            // 
            // ll_TransectionInventory
            // 
            this.ll_TransectionInventory.AutoSize = true;
            this.ll_TransectionInventory.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ll_TransectionInventory.ForeColor = System.Drawing.Color.Navy;
            this.ll_TransectionInventory.Location = new System.Drawing.Point(52, 190);
            this.ll_TransectionInventory.Name = "ll_TransectionInventory";
            this.ll_TransectionInventory.Size = new System.Drawing.Size(86, 27);
            this.ll_TransectionInventory.TabIndex = 22;
            this.ll_TransectionInventory.TabStop = true;
            this.ll_TransectionInventory.Text = "Restore";
            // 
            // ll_MasterInventory
            // 
            this.ll_MasterInventory.AutoSize = true;
            this.ll_MasterInventory.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ll_MasterInventory.ForeColor = System.Drawing.Color.Navy;
            this.ll_MasterInventory.Location = new System.Drawing.Point(52, 106);
            this.ll_MasterInventory.Name = "ll_MasterInventory";
            this.ll_MasterInventory.Size = new System.Drawing.Size(175, 27);
            this.ll_MasterInventory.TabIndex = 17;
            this.ll_MasterInventory.TabStop = true;
            this.ll_MasterInventory.Text = "Create Company";
            this.ll_MasterInventory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll_MasterInventory_LinkClicked);
            // 
            // ll_MasterAccount
            // 
            this.ll_MasterAccount.AutoSize = true;
            this.ll_MasterAccount.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ll_MasterAccount.ForeColor = System.Drawing.Color.Navy;
            this.ll_MasterAccount.Location = new System.Drawing.Point(52, 62);
            this.ll_MasterAccount.Name = "ll_MasterAccount";
            this.ll_MasterAccount.Size = new System.Drawing.Size(169, 27);
            this.ll_MasterAccount.TabIndex = 13;
            this.ll_MasterAccount.TabStop = true;
            this.ll_MasterAccount.Text = "Select Company";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(387, 529);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(21, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 40);
            this.button1.TabIndex = 9;
            this.button1.Text = "lp";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel_LeftSide
            // 
            this.panel_LeftSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel_LeftSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_LeftSide.Location = new System.Drawing.Point(0, 64);
            this.panel_LeftSide.Name = "panel_LeftSide";
            this.panel_LeftSide.Size = new System.Drawing.Size(55, 537);
            this.panel_LeftSide.TabIndex = 16;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button17);
            this.panel4.Controls.Add(this.button18);
            this.panel4.Controls.Add(this.button15);
            this.panel4.Controls.Add(this.button16);
            this.panel4.Controls.Add(this.button13);
            this.panel4.Controls.Add(this.button14);
            this.panel4.Controls.Add(this.button12);
            this.panel4.Controls.Add(this.button11);
            this.panel4.Location = new System.Drawing.Point(81, 51);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(884, 32);
            this.panel4.TabIndex = 15;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button17.Location = new System.Drawing.Point(738, 4);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(100, 25);
            this.button17.TabIndex = 7;
            this.button17.Text = "button17";
            this.button17.UseVisualStyleBackColor = false;
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button18.Location = new System.Drawing.Point(634, 4);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(100, 25);
            this.button18.TabIndex = 6;
            this.button18.Text = "button18";
            this.button18.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button15.Location = new System.Drawing.Point(528, 4);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(100, 25);
            this.button15.TabIndex = 5;
            this.button15.Text = "button15";
            this.button15.UseVisualStyleBackColor = false;
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button16.Location = new System.Drawing.Point(424, 4);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(100, 25);
            this.button16.TabIndex = 4;
            this.button16.Text = "button16";
            this.button16.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button13.Location = new System.Drawing.Point(320, 4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(100, 25);
            this.button13.TabIndex = 3;
            this.button13.Text = "button13";
            this.button13.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button14.Location = new System.Drawing.Point(216, 4);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(100, 25);
            this.button14.TabIndex = 2;
            this.button14.Text = "Print";
            this.button14.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button12.Location = new System.Drawing.Point(111, 4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(100, 25);
            this.button12.TabIndex = 1;
            this.button12.Text = "Import";
            this.button12.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button11.Location = new System.Drawing.Point(7, 4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(100, 25);
            this.button11.TabIndex = 0;
            this.button11.Text = "Export";
            this.button11.UseVisualStyleBackColor = false;
            // 
            // panel_Right
            // 
            this.panel_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel_Right.Controls.Add(this.button9);
            this.panel_Right.Controls.Add(this.button8);
            this.panel_Right.Controls.Add(this.button7);
            this.panel_Right.Controls.Add(this.button1);
            this.panel_Right.Controls.Add(this.button6);
            this.panel_Right.Controls.Add(this.button5);
            this.panel_Right.Controls.Add(this.button4);
            this.panel_Right.Controls.Add(this.button3);
            this.panel_Right.Controls.Add(this.button2);
            this.panel_Right.Controls.Add(this.button10);
            this.panel_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Right.Location = new System.Drawing.Point(924, 0);
            this.panel_Right.Name = "panel_Right";
            this.panel_Right.Size = new System.Drawing.Size(134, 649);
            this.panel_Right.TabIndex = 2;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button9.Location = new System.Drawing.Point(3, 351);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(143, 44);
            this.button9.TabIndex = 8;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button8.Location = new System.Drawing.Point(3, 308);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(143, 44);
            this.button8.TabIndex = 7;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button7.Location = new System.Drawing.Point(3, 264);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(141, 43);
            this.button7.TabIndex = 6;
            this.button7.Text = "Sales Invoice";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button6.Location = new System.Drawing.Point(3, 221);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(141, 43);
            this.button6.TabIndex = 5;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button5.Location = new System.Drawing.Point(3, 178);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(141, 43);
            this.button5.TabIndex = 4;
            this.button5.Text = "Create Company";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(3, 134);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 43);
            this.button4.TabIndex = 3;
            this.button4.Text = "Period";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button3.FlatAppearance.BorderSize = 2;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(3, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 43);
            this.button3.TabIndex = 2;
            this.button3.Text = "Date";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(3, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 43);
            this.button2.TabIndex = 1;
            this.button2.Text = "Quit Company";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button10.FlatAppearance.BorderSize = 2;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Navy;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.button10.Location = new System.Drawing.Point(3, 4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(141, 43);
            this.button10.TabIndex = 0;
            this.button10.Text = "Select Company";
            this.button10.UseVisualStyleBackColor = false;
            // 
            // panel_Bottum
            // 
            this.panel_Bottum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel_Bottum.Controls.Add(this.dateTimePicker2);
            this.panel_Bottum.Controls.Add(this.dateTimePicker1);
            this.panel_Bottum.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottum.Location = new System.Drawing.Point(0, 601);
            this.panel_Bottum.Name = "panel_Bottum";
            this.panel_Bottum.Size = new System.Drawing.Size(924, 48);
            this.panel_Bottum.TabIndex = 14;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(815, 17);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker2.TabIndex = 15;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(625, 17);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(172, 20);
            this.dateTimePicker1.TabIndex = 16;
            // 
            // panel_Top
            // 
            this.panel_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel_Top.Controls.Add(this.label_CompanyName);
            this.panel_Top.Controls.Add(this.panel7);
            this.panel_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Top.Location = new System.Drawing.Point(0, 0);
            this.panel_Top.Name = "panel_Top";
            this.panel_Top.Size = new System.Drawing.Size(924, 64);
            this.panel_Top.TabIndex = 17;
            // 
            // label_CompanyName
            // 
            this.label_CompanyName.AutoSize = true;
            this.label_CompanyName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CompanyName.Location = new System.Drawing.Point(111, 3);
            this.label_CompanyName.Name = "label_CompanyName";
            this.label_CompanyName.Size = new System.Drawing.Size(336, 32);
            this.label_CompanyName.TabIndex = 25;
            this.label_CompanyName.Text = "Dhanashree Trading Company";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.button19);
            this.panel7.Controls.Add(this.button20);
            this.panel7.Controls.Add(this.button21);
            this.panel7.Controls.Add(this.button22);
            this.panel7.Controls.Add(this.button23);
            this.panel7.Controls.Add(this.button24);
            this.panel7.Controls.Add(this.button25);
            this.panel7.Controls.Add(this.button26);
            this.panel7.Location = new System.Drawing.Point(85, 33);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(758, 28);
            this.panel7.TabIndex = 18;
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button19.Location = new System.Drawing.Point(633, 3);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(86, 22);
            this.button19.TabIndex = 7;
            this.button19.Text = "button19";
            this.button19.UseVisualStyleBackColor = false;
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button20.Location = new System.Drawing.Point(543, 3);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(86, 22);
            this.button20.TabIndex = 6;
            this.button20.Text = "button20";
            this.button20.UseVisualStyleBackColor = false;
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button21.Location = new System.Drawing.Point(453, 3);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(86, 22);
            this.button21.TabIndex = 5;
            this.button21.Text = "button21";
            this.button21.UseVisualStyleBackColor = false;
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button22.Location = new System.Drawing.Point(362, 3);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(86, 22);
            this.button22.TabIndex = 4;
            this.button22.Text = "button22";
            this.button22.UseVisualStyleBackColor = false;
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button23.Location = new System.Drawing.Point(274, 3);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(86, 22);
            this.button23.TabIndex = 3;
            this.button23.Text = "button23";
            this.button23.UseVisualStyleBackColor = false;
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button24.Location = new System.Drawing.Point(185, 3);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(86, 22);
            this.button24.TabIndex = 2;
            this.button24.Text = "Print";
            this.button24.UseVisualStyleBackColor = false;
            // 
            // button25
            // 
            this.button25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button25.Location = new System.Drawing.Point(95, 3);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(86, 22);
            this.button25.TabIndex = 1;
            this.button25.Text = "Import";
            this.button25.UseVisualStyleBackColor = false;
            // 
            // button26
            // 
            this.button26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button26.Location = new System.Drawing.Point(6, 3);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(86, 22);
            this.button26.TabIndex = 0;
            this.button26.Text = "Export";
            this.button26.UseVisualStyleBackColor = false;
            // 
            // label_Main
            // 
            this.label_Main.AutoSize = true;
            this.label_Main.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Main.Location = new System.Drawing.Point(128, 8);
            this.label_Main.Name = "label_Main";
            this.label_Main.Size = new System.Drawing.Size(336, 32);
            this.label_Main.TabIndex = 16;
            this.label_Main.Text = "Dhanashree Trading Company";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 649);
            this.Controls.Add(this.panel_LeftSide);
            this.Controls.Add(this.panel_Top);
            this.Controls.Add(this.panel_Bottum);
            this.Controls.Add(this.panel_Right);
            this.Controls.Add(this.panel_Main);
            this.Name = "Form2";
            this.Text = "Main ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel_Main.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_GateWay.ResumeLayout(false);
            this.panel_GateWay.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel_Right.ResumeLayout(false);
            this.panel_Bottum.ResumeLayout(false);
            this.panel_Top.ResumeLayout(false);
            this.panel_Top.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel_Main;
        private Button button1;
        private Panel panel_Right;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button10;
        private Panel panel_Bottum;
        private DateTimePicker dateTimePicker1;
        private Panel panel4;
        private DateTimePicker dateTimePicker2;
        private Button button13;
        private Button button14;
        private Button button12;
        private Button button11;
        private Button button17;
        private Button button18;
        private Button button15;
        private Button button16;
        private Panel panel_Top;
        private Panel panel_LeftSide;
        private Label label_Main;
        private Panel panel7;
        private Button button19;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private Button button24;
        private Button button25;
        private Button button26;
        private Splitter splitter1;
        private LinkLabel ll_MasterInventory;
        private LinkLabel ll_MasterAccount;
        private LinkLabel ll_TransectionInventory;
        private LinkLabel ll_TransectionAccount;
        private Label label_CompanyName;
        private Panel panel_GateWay;
        private Panel panel1;
        private Label label8;
        private LinkLabel linkLabel1;
    }
}