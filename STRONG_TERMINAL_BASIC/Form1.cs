using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace STRONG_TERMINAL_BASIC
{
  public partial class Form1 : Form
  {
    byte[] data_receive = new byte[32];
    StreamReader streamReader = null;
    string application_path = Application.ExecutablePath;
    string path_file_settings = @"settings.txt";

    string DEFAULT_COM_PORT = "DEFAULT_COM_PORT";
    string DEFAULT_COM_PORT_SPPED = "DEFAULT_COM_PORT_SPPED";
    string DEFAULT_COM_PORT_MODE = "";
    string string_ASCII_HEX_MODE = "";
    bool flag_ASCII_HEX = false;
    string[] settings_lines = { };

    public Form1()
    {
      InitializeComponent();
      var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
      var fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
      string version = "v." + fileVersion.ToString();
      label1.Text = version.Substring(0,6) + " - 2023";
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      button1.PerformClick();
      richTextBox1.BackColor = Color.SkyBlue;
      textBox1.BackColor = Color.SkyBlue;
      if (File.Exists(path_file_settings))
      { settings_lines = File.ReadAllLines(@"settings.txt", Encoding.UTF8); }
      else
      { richTextBox1.AppendText("\nMISSING FILE FOR SETTINGS !!!\n");
        richTextBox1.AppendText("create file settings.txt in same folder!!!\n");
        richTextBox1.AppendText("you can use any TEXT EDITOR\n");
        richTextBox1.AppendText("like NOTEPAD++ or other text editor\n\n");
      }

      richTextBox1.AppendText("\n");
      //richTextBox1.AppendText(setings_lines[0] + "\n");
      if (File.Exists(path_file_settings))
      {
        byte project_count = 0;
        richTextBox1.AppendText("projects list :\n");
        //richTextBox1.AppendText("default load is : " + setings_lines[0].Substring(8) + "\n");
        while (project_count < settings_lines.Length)
        {
          if (settings_lines[project_count].StartsWith("PROJECT_NAME"))
          {
            richTextBox1.AppendText("project name : " + settings_lines[project_count].Substring(13) + "\n");
          }
          project_count++;
        }

        project_count = 1; // next line after STARTUP_NAME=xxx
        DEFAULT_COM_PORT_MODE = settings_lines[0].Substring(13);
        richTextBox1.AppendText("\ndefault load is : " + DEFAULT_COM_PORT_MODE + "\n");
        //richTextBox1.AppendText("\ndefault load is : " + setings_lines[0].Substring(13) + "\n");

        while (project_count < settings_lines.Length)
        {

          if (settings_lines[project_count] != "" && settings_lines[project_count].Substring(13) == DEFAULT_COM_PORT_MODE)
          {
            richTextBox1.AppendText("project : " + settings_lines[project_count].Substring(13) + "\n");

            richTextBox1.AppendText("serial : " + settings_lines[project_count + 1].Substring(17) + "\n");
            serialPort1.PortName = settings_lines[project_count + 1].Substring(17);

            richTextBox1.AppendText("speed : " + settings_lines[project_count + 2].Substring(23) + "\n");
            serialPort1.BaudRate = int.Parse(settings_lines[project_count + 2].Substring(23));

            richTextBox1.AppendText("mode ASCII / HEX : " + settings_lines[project_count + 3].Substring(22) + "\n");
            if (settings_lines[project_count + 3].Substring(22) == "ASCII")
            { flag_ASCII_HEX = true; }
            if (settings_lines[project_count + 3].Substring(22) == "HEX")
            { flag_ASCII_HEX = false; }
          }
          project_count++;
        }
        project_count = 0;
      }

    } // END FORM LOAD


    private void button1_Click(object sender, EventArgs e)
    {    // GET PORTS
      string[] ports = SerialPort.GetPortNames();
      richTextBox1.AppendText("rs232 ports found\n\n");
      comboBox1.Items.Clear();

      foreach (string port in ports)
      {
        richTextBox1.AppendText(port + " is active\n");
        comboBox1.Items.Add(port);
        comboBox1.SelectedIndex = 0;
      }
      var count = comboBox1.Items.Count;

      richTextBox1.AppendText(count + " serial port are active\n");
      richTextBox1.ScrollToCaret();

    }

    private void button2_Click(object sender, EventArgs e)
    {     // CONNECT SERIAL PORT
      serialPort1.PortName = comboBox1.SelectedItem.ToString();

      if (comboBox2.SelectedIndex == 0)
      { serialPort1.BaudRate = 9600; }
      
      if (comboBox2.SelectedIndex == 1)
      { serialPort1.BaudRate = 19200; }

      if (comboBox2.SelectedIndex == 2)
      { serialPort1.BaudRate = 38400; }

      if (comboBox2.SelectedIndex == 3)
      { serialPort1.BaudRate = 57600; }

      if (comboBox2.SelectedIndex == 4)
      { serialPort1.BaudRate = 115200; }

       try
      {
        if (!(serialPort1.IsOpen))
        {
          serialPort1.RtsEnable = true; // IMPORTANT RECEIVE FROM USB ARDUINO/PICO !!!!
          serialPort1.DtrEnable = true; // IMPORTANT RECEIVE FROM USB ARDUINO/PICO !!!!
          serialPort1.Open();
          richTextBox1.AppendText(serialPort1.PortName.ToString() + " is CONNECTED" + "\n");
          richTextBox1.AppendText(serialPort1.PortName.ToString() + " SPEED = "
                                          + serialPort1.BaudRate.ToString() + "\n\n");
        }

        button2.Text = "CONNECTED";
        button2.BackColor = Color.LightGreen;
        button2.Enabled = false;
        button1.Enabled = false;
        comboBox1.Enabled = false; comboBox2.Enabled = false;
        richTextBox1.BackColor = Color.LimeGreen;
        textBox1.BackColor = Color.LimeGreen;
      }
      catch (Exception ex)
      {
        richTextBox1.AppendText("\nserial port already open >> " + ex.Message + "\n\n");
        button3.Enabled = false;
      }
      richTextBox1.ScrollToCaret();
    }// END BUTTON CONNECT

    private void button3_Click(object sender, EventArgs e)
    {   // BUTTON DISCONNECT

      try
      {
        serialPort1.DiscardInBuffer();
        serialPort1.DiscardOutBuffer();
        serialPort1.Close(); serialPort1.Dispose();
        button2.Enabled = true;
        button1.Enabled = true;
        button2.Text = "CONNECT";
        button2.BackColor = DefaultBackColor;
        button2.ForeColor = DefaultForeColor;
        button2.UseVisualStyleBackColor = true;

        comboBox1.Enabled = true;
        comboBox2.Enabled = true;

        richTextBox1.BackColor = Color.SkyBlue;
        textBox1.BackColor = Color.SkyBlue;
      }
      catch (Exception ex)
      {
        // MessageBox.Show(ex.Message);
        richTextBox1.AppendText(serialPort1.PortName.ToString() + " is CLOSED" + "\n");

      }
        var items = comboBox1.Items.Count;
          while (items > 0)
          {
            comboBox1.Items.Clear();
            items--;
          }
        button1.PerformClick();

    }// END BUTTON DISCONNECT

    private void button5_Click(object sender, EventArgs e)
    {
      richTextBox1.Clear();
    }

    private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      int income_bytes = 0;
      byte ij = 0;
      string received_read_string = "";
      //serialPort1.Read(Init.data_receive, 0, Init.data_receive.Length);
      // Init.procces_message_receive();

      //=========== EDITED ROUTINE FOR AVOID MISSING FIRST BYTE =========

      //if you need to count and print or check number received bytes
      // ENABLE NEXT LINES !!!
      while (serialPort1.BytesToRead > 0) // For example
      {
        income_bytes++; // IMPORTANT COUNT++ TO GET MESSAGE
        serialPort1.Read(data_receive, 0, data_receive.Length);
      }


      if (flag_ASCII_HEX == true) // ASCII MODE ACTIVE
      {
        received_read_string = serialPort1.ReadLine();
        richTextBox1.AppendText("receive >>\n");
        richTextBox1.AppendText(received_read_string + "\n\n");
        //richTextBox1.AppendText("\n\n");
        richTextBox1.ScrollToCaret();
      }
      else
      {
        richTextBox1.AppendText("receive >>\n");
        while (ij < data_receive.Length)
          {
            if (data_receive[ij] < 16 && data_receive[ij] > 0)
            {
            richTextBox1.AppendText("0" + data_receive[ij].ToString("X") + " ");
            //received_read_string += "0" + data_receive[ij].ToString("X") + " ";
            }
            else if (data_receive[ij] > 15)
            {
            richTextBox1.AppendText(data_receive[ij].ToString("X") + " ");
            //received_read_string += data_receive[ij].ToString("X") + " ";
            }
         
          ij++;
          }
		  // use for PREOTEUS SIMULATION
		  // for virtual ports
        System.Threading.Thread.Sleep(100);
       // richTextBox1.AppendText(received_read_string);

        richTextBox1.AppendText("\n");

        ij = 0;
        while (ij < data_receive.Length)
        {
          data_receive[ij] = 0;
          ij++;
        }
        ij = 0;

       // richTextBox1.AppendText("\n\n");
        richTextBox1.ScrollToCaret();
        //received_read_string = "";
      //  income_bytes = 0;


      }
    }//=========== EDITED ROUTINE FOR AVOID MISSING FIRST BYTE =========

    private void button4_Click(object sender, EventArgs e)
    {
      if (flag_ASCII_HEX == false)
      { flag_ASCII_HEX = true;
        richTextBox1.AppendText("\nASCII mode active\n");
        richTextBox1.ScrollToCaret();
        button4.Text = "ASCII";
      }
      else
      if (flag_ASCII_HEX == true)
      { flag_ASCII_HEX = false;
        richTextBox1.AppendText("\nHEX mode active\n");
        richTextBox1.ScrollToCaret();
        button4.Text = "HEX";
      }

      //if (setings_lines[project_count + 3].Substring(22) == "ASCII")
      //{ flag_ASCII_HEX = true; }
      //if (setings_lines[project_count + 3].Substring(22) == "HEX")
     // { flag_ASCII_HEX = false; }
    }

    // ============== NEXT CODE PLACE HERE ================


  }
}
