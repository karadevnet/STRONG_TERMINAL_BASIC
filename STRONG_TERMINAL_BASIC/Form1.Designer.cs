﻿namespace STRONG_TERMINAL_BASIC
{
  partial class Form1
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
	  this.components = new System.ComponentModel.Container();
	  System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
	  this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
	  this.richTextBox1 = new System.Windows.Forms.RichTextBox();
	  this.button1 = new System.Windows.Forms.Button();
	  this.button2 = new System.Windows.Forms.Button();
	  this.textBox1 = new System.Windows.Forms.TextBox();
	  this.button3 = new System.Windows.Forms.Button();
	  this.button4 = new System.Windows.Forms.Button();
	  this.button5 = new System.Windows.Forms.Button();
	  this.button6 = new System.Windows.Forms.Button();
	  this.comboBox1 = new System.Windows.Forms.ComboBox();
	  this.comboBox2 = new System.Windows.Forms.ComboBox();
	  this.label1 = new System.Windows.Forms.Label();
	  this.button7 = new System.Windows.Forms.Button();
	  this.button8 = new System.Windows.Forms.Button();
	  this.button9 = new System.Windows.Forms.Button();
	  this.label2 = new System.Windows.Forms.Label();
	  this.checkBox1 = new System.Windows.Forms.CheckBox();
	  this.textBox2 = new System.Windows.Forms.TextBox();
	  this.label4 = new System.Windows.Forms.Label();
	  this.label3 = new System.Windows.Forms.Label();
	  this.label5 = new System.Windows.Forms.Label();
	  this.label6 = new System.Windows.Forms.Label();
	  this.button10 = new System.Windows.Forms.Button();
	  this.SuspendLayout();
	  // 
	  // serialPort1
	  // 
	  this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
	  // 
	  // richTextBox1
	  // 
	  this.richTextBox1.BackColor = System.Drawing.Color.SkyBlue;
	  this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.richTextBox1.Location = new System.Drawing.Point(18, 43);
	  this.richTextBox1.Name = "richTextBox1";
	  this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
	  this.richTextBox1.Size = new System.Drawing.Size(572, 547);
	  this.richTextBox1.TabIndex = 0;
	  this.richTextBox1.Text = "";
	  // 
	  // button1
	  // 
	  this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button1.Location = new System.Drawing.Point(12, 12);
	  this.button1.Name = "button1";
	  this.button1.Size = new System.Drawing.Size(115, 25);
	  this.button1.TabIndex = 1;
	  this.button1.Text = "GET PORTS";
	  this.button1.UseVisualStyleBackColor = true;
	  this.button1.Click += new System.EventHandler(this.button1_Click);
	  // 
	  // button2
	  // 
	  this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button2.Location = new System.Drawing.Point(325, 12);
	  this.button2.Name = "button2";
	  this.button2.Size = new System.Drawing.Size(115, 25);
	  this.button2.TabIndex = 2;
	  this.button2.Text = "CONNECT";
	  this.button2.UseVisualStyleBackColor = true;
	  this.button2.Click += new System.EventHandler(this.button2_Click);
	  // 
	  // textBox1
	  // 
	  this.textBox1.BackColor = System.Drawing.Color.SkyBlue;
	  this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.textBox1.Location = new System.Drawing.Point(18, 599);
	  this.textBox1.MaxLength = 48;
	  this.textBox1.Name = "textBox1";
	  this.textBox1.Size = new System.Drawing.Size(372, 22);
	  this.textBox1.TabIndex = 3;
	  // 
	  // button3
	  // 
	  this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button3.Location = new System.Drawing.Point(446, 12);
	  this.button3.Name = "button3";
	  this.button3.Size = new System.Drawing.Size(115, 25);
	  this.button3.TabIndex = 4;
	  this.button3.Text = "DISCONNECT";
	  this.button3.UseVisualStyleBackColor = true;
	  this.button3.Click += new System.EventHandler(this.button3_Click);
	  // 
	  // button4
	  // 
	  this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button4.Location = new System.Drawing.Point(666, 120);
	  this.button4.Name = "button4";
	  this.button4.Size = new System.Drawing.Size(62, 25);
	  this.button4.TabIndex = 5;
	  this.button4.Text = "HEX";
	  this.button4.UseVisualStyleBackColor = true;
	  this.button4.Click += new System.EventHandler(this.button4_Click);
	  // 
	  // button5
	  // 
	  this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button5.Location = new System.Drawing.Point(613, 474);
	  this.button5.Name = "button5";
	  this.button5.Size = new System.Drawing.Size(115, 25);
	  this.button5.TabIndex = 6;
	  this.button5.Text = "CLEAR";
	  this.button5.UseVisualStyleBackColor = true;
	  this.button5.Click += new System.EventHandler(this.button5_Click);
	  // 
	  // button6
	  // 
	  this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button6.Location = new System.Drawing.Point(613, 565);
	  this.button6.Name = "button6";
	  this.button6.Size = new System.Drawing.Size(115, 25);
	  this.button6.TabIndex = 8;
	  this.button6.Text = "SEND";
	  this.button6.UseVisualStyleBackColor = true;
	  this.button6.Click += new System.EventHandler(this.button6_Click);
	  // 
	  // comboBox1
	  // 
	  this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.comboBox1.FormattingEnabled = true;
	  this.comboBox1.Location = new System.Drawing.Point(133, 12);
	  this.comboBox1.Name = "comboBox1";
	  this.comboBox1.Size = new System.Drawing.Size(90, 24);
	  this.comboBox1.TabIndex = 9;
	  this.comboBox1.Text = "COM1";
	  // 
	  // comboBox2
	  // 
	  this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.comboBox2.FormattingEnabled = true;
	  this.comboBox2.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
	  this.comboBox2.Location = new System.Drawing.Point(229, 12);
	  this.comboBox2.Name = "comboBox2";
	  this.comboBox2.Size = new System.Drawing.Size(90, 24);
	  this.comboBox2.TabIndex = 10;
	  this.comboBox2.Text = "9600";
	  // 
	  // label1
	  // 
	  this.label1.AutoSize = true;
	  this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.label1.Location = new System.Drawing.Point(601, 502);
	  this.label1.Name = "label1";
	  this.label1.Size = new System.Drawing.Size(39, 18);
	  this.label1.TabIndex = 11;
	  this.label1.Text = "v1.0";
	  // 
	  // button7
	  // 
	  this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button7.Location = new System.Drawing.Point(613, 43);
	  this.button7.Name = "button7";
	  this.button7.Size = new System.Drawing.Size(115, 25);
	  this.button7.TabIndex = 12;
	  this.button7.Text = "SETTINGS";
	  this.button7.UseVisualStyleBackColor = true;
	  this.button7.Click += new System.EventHandler(this.button7_Click);
	  // 
	  // button8
	  // 
	  this.button8.BackColor = System.Drawing.Color.Red;
	  this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
	  this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button8.ForeColor = System.Drawing.Color.Yellow;
	  this.button8.Location = new System.Drawing.Point(396, 597);
	  this.button8.Name = "button8";
	  this.button8.Size = new System.Drawing.Size(62, 25);
	  this.button8.TabIndex = 13;
	  this.button8.Text = "ASCII";
	  this.button8.UseVisualStyleBackColor = false;
	  this.button8.Click += new System.EventHandler(this.button8_Click);
	  // 
	  // button9
	  // 
	  this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button9.Location = new System.Drawing.Point(613, 443);
	  this.button9.Name = "button9";
	  this.button9.Size = new System.Drawing.Size(115, 25);
	  this.button9.TabIndex = 14;
	  this.button9.Text = "HELP";
	  this.button9.UseVisualStyleBackColor = true;
	  // 
	  // label2
	  // 
	  this.label2.AutoSize = true;
	  this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.label2.Location = new System.Drawing.Point(610, 420);
	  this.label2.Name = "label2";
	  this.label2.Size = new System.Drawing.Size(119, 16);
	  this.label2.TabIndex = 15;
	  this.label2.Text = "==============";
	  // 
	  // checkBox1
	  // 
	  this.checkBox1.AutoSize = true;
	  this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.checkBox1.Location = new System.Drawing.Point(617, 14);
	  this.checkBox1.Name = "checkBox1";
	  this.checkBox1.Size = new System.Drawing.Size(111, 20);
	  this.checkBox1.TabIndex = 16;
	  this.checkBox1.Text = "ZERO BYTE";
	  this.checkBox1.UseVisualStyleBackColor = true;
	  // 
	  // textBox2
	  // 
	  this.textBox2.BackColor = System.Drawing.Color.SkyBlue;
	  this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.textBox2.Location = new System.Drawing.Point(464, 598);
	  this.textBox2.MaxLength = 11;
	  this.textBox2.Name = "textBox2";
	  this.textBox2.Size = new System.Drawing.Size(125, 22);
	  this.textBox2.TabIndex = 17;
	  // 
	  // label4
	  // 
	  this.label4.AutoSize = true;
	  this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.label4.Location = new System.Drawing.Point(618, 524);
	  this.label4.Name = "label4";
	  this.label4.Size = new System.Drawing.Size(103, 16);
	  this.label4.TabIndex = 19;
	  this.label4.Text = "============";
	  // 
	  // label3
	  // 
	  this.label3.AutoSize = true;
	  this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.label3.Location = new System.Drawing.Point(656, 96);
	  this.label3.Name = "label3";
	  this.label3.Size = new System.Drawing.Size(72, 16);
	  this.label3.TabIndex = 20;
	  this.label3.Text = "RECEIVE";
	  // 
	  // label5
	  // 
	  this.label5.AutoSize = true;
	  this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.label5.Location = new System.Drawing.Point(660, 602);
	  this.label5.Name = "label5";
	  this.label5.Size = new System.Drawing.Size(68, 16);
	  this.label5.TabIndex = 21;
	  this.label5.Text = "SEND IN";
	  // 
	  // label6
	  // 
	  this.label6.AutoSize = true;
	  this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.label6.Location = new System.Drawing.Point(610, 76);
	  this.label6.Name = "label6";
	  this.label6.Size = new System.Drawing.Size(119, 16);
	  this.label6.TabIndex = 22;
	  this.label6.Text = "==============";
	  // 
	  // button10
	  // 
	  this.button10.BackColor = System.Drawing.Color.Red;
	  this.button10.Cursor = System.Windows.Forms.Cursors.Hand;
	  this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
	  this.button10.ForeColor = System.Drawing.Color.Yellow;
	  this.button10.Location = new System.Drawing.Point(595, 598);
	  this.button10.Name = "button10";
	  this.button10.Size = new System.Drawing.Size(62, 25);
	  this.button10.TabIndex = 23;
	  this.button10.Text = "ASCII";
	  this.button10.UseVisualStyleBackColor = false;
	  this.button10.Click += new System.EventHandler(this.button10_Click);
	  // 
	  // Form1
	  // 
	  this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	  this.BackColor = System.Drawing.SystemColors.ActiveCaption;
	  this.ClientSize = new System.Drawing.Size(740, 633);
	  this.Controls.Add(this.button10);
	  this.Controls.Add(this.label6);
	  this.Controls.Add(this.label5);
	  this.Controls.Add(this.label3);
	  this.Controls.Add(this.label4);
	  this.Controls.Add(this.textBox2);
	  this.Controls.Add(this.checkBox1);
	  this.Controls.Add(this.label2);
	  this.Controls.Add(this.button9);
	  this.Controls.Add(this.button8);
	  this.Controls.Add(this.button7);
	  this.Controls.Add(this.label1);
	  this.Controls.Add(this.comboBox2);
	  this.Controls.Add(this.comboBox1);
	  this.Controls.Add(this.button6);
	  this.Controls.Add(this.button5);
	  this.Controls.Add(this.button4);
	  this.Controls.Add(this.button3);
	  this.Controls.Add(this.textBox1);
	  this.Controls.Add(this.button2);
	  this.Controls.Add(this.button1);
	  this.Controls.Add(this.richTextBox1);
	  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
	  this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
	  this.MaximizeBox = false;
	  this.Name = "Form1";
	  this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	  this.Text = "STRONG_TERMINAL_BASIC v.1.0";
	  this.Load += new System.EventHandler(this.Form1_Load);
	  this.ResumeLayout(false);
	  this.PerformLayout();

    }

    #endregion

    private System.IO.Ports.SerialPort serialPort1;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox checkBox1;
	private System.Windows.Forms.TextBox textBox2;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Button button10;
  }
}

