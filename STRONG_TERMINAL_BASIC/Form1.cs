using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
    byte[] data_send = new byte[32];
    StreamReader streamReader = null;
    string application_path = Application.ExecutablePath;
    string path_file_settings = @"settings.txt";

    string DEFAULT_COM_PORT = "DEFAULT_COM_PORT";
    string DEFAULT_COM_PORT_SPPED = "DEFAULT_COM_PORT_SPPED";
    string DEFAULT_COM_PORT_MODE = "";
    string string_ASCII_HEX_MODE = "";
    bool flag_ASCII_HEX = false;
    bool flag_BYTE = false;
    string[] settings_lines = { };
    byte high_value = 0;
    byte low_value = 0;
    byte send_value = 0;
    string string_no_spaces = "";

    
    
    
    public byte Convert_to_byte(byte count_string) // convert ASCII HEX MESSAGE FROM LINE TO BYTE
    {

     byte string_letter = 0;
      string send_text_from_line = textBox1.Text; 
      //string send_text_from_line = string_no_spaces;


      if (send_text_from_line[count_string].ToString() == "0") { string_letter = 0; }
        if (send_text_from_line[count_string].ToString() == "1") { string_letter = 1; }
        if (send_text_from_line[count_string].ToString() == "2") { string_letter = 2; }
        if (send_text_from_line[count_string].ToString() == "3") { string_letter = 3; }
        if (send_text_from_line[count_string].ToString() == "4") { string_letter = 4; }
 
        if (send_text_from_line[count_string].ToString() == "5") { string_letter = 5; }
        if (send_text_from_line[count_string].ToString() == "6") { string_letter = 6; }
        if (send_text_from_line[count_string].ToString() == "7") { string_letter = 7; }
        if (send_text_from_line[count_string].ToString() == "8") { string_letter = 8; }
        if (send_text_from_line[count_string].ToString() == "9") { string_letter = 9; }

        if (send_text_from_line[count_string].ToString() == "A") { string_letter = 10; }
        if (send_text_from_line[count_string].ToString() == "B") { string_letter = 11; }
        if (send_text_from_line[count_string].ToString() == "C") { string_letter = 12; }
        if (send_text_from_line[count_string].ToString() == "D") { string_letter = 13; }
        if (send_text_from_line[count_string].ToString() == "E") { string_letter = 14; }
        if (send_text_from_line[count_string].ToString() == "F") { string_letter = 15; }

      return string_letter;
    }

    public void send_byte_to_serial()
    {     
       byte count_string = 0;
      string send_text_from_line = textBox1.Text;
      //string send_text_from_line = string_no_spaces;
      byte array_count = 0;


      while (count_string < send_text_from_line.Length) //check 32 letters / numbers
      {
        high_value = (byte) (Convert_to_byte(count_string) << 4);
        low_value = (byte)(Convert_to_byte((byte)(count_string + 1)));
         data_send[array_count] = (byte)(high_value + low_value);
        count_string += 2; array_count++;
       }

      count_string = 0; array_count = 0;

      richTextBox1.AppendText("INPUT IN LINE IS :\n");
      richTextBox1.AppendText(send_text_from_line + "\n");

      //while (count_string < send_text_from_line.Length / 2) //check 32 letters / numbers
     // {
      //  if (data_send[count_string] < 16)
      //  { richTextBox1.AppendText("0" + data_send[count_string].ToString("X") + " "); }
      //  else
      //  { richTextBox1.AppendText(data_send[count_string].ToString("X") + " "); }
      //  count_string++;
     // }
      count_string = 0;
      richTextBox1.AppendText("\n");
      send_text_from_line = "";
    }



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

        richTextBox1.AppendText("\nUSE BUTTON HEX / ASCII IN TOP\n");
        richTextBox1.AppendText("TO VIEW RECEIVED DATA IN HEX VALUES\n");
        richTextBox1.AppendText("OR VIEW IN REDABLE STRING MESSAGES\n\n");

        richTextBox1.AppendText("SET BUTTON BYTE TO SEND DATA IN BYTES\n");
        richTextBox1.AppendText("ENTER 16 BYTES LIKE NEXT LINE\n");
        //richTextBox1.AppendText("1D 34 FF 1A 3E 90 56 11 22 33 D1 B2 C9 F4 C0 B8\n\n");
        richTextBox1.AppendText("1D34FF1A3E9056112233D1B2C9F4C0B8\n");
        richTextBox1.AppendText("MAX 32 SYMBOLS FROM 0-9 and A-F\n\n");


        richTextBox1.AppendText("OR SET BUTTON BYTE TO SEND DATA IN ASCII / HEX\n");
        richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
        richTextBox1.AppendText("example : BOARD4RELAY7OFF\n\n");
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
        button3.Enabled = true;
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
    //======================================================================================
    private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      int income_bytes = 0;
      byte bytes_to_print = 0;
      byte check_zeros_in_tail = 0;
      byte check_ascii_for_zeros = 0;
      byte check_hex_for_zeros = 0;
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
        ij = 31;
        while (data_receive.Length > ij)
        {
          if (data_receive[ij] == 0) { check_zeros_in_tail++; }
          if (data_receive[ij] != 0) { break; }
          ij--;
        }
        //richTextBox1.AppendText("check_zeros_in_tail = " + check_zeros_in_tail + "\n");
        ij = 0;


        while ( ij < data_receive.Length - check_zeros_in_tail)
        {
          if (data_receive[ij] == 0)
          { data_receive[ij] = 0x30; }

            received_read_string += Encoding.ASCII.GetString(data_receive)[ij];
          ij++;
        }
        ij = 0;

        if (received_read_string.Contains("0"))
        {
          richTextBox1.AppendText("receive in ASCII >>>\n");
          richTextBox1.AppendText("THE STRING CONTAINS ZERO BYTES !!!\n");
          richTextBox1.AppendText(received_read_string + "\n");
          richTextBox1.ScrollToCaret();
        }
        else
        {
          richTextBox1.AppendText("receive in ASCII >>>\n");
          richTextBox1.AppendText(received_read_string + "\n");
          //richTextBox1.AppendText("\n");
          richTextBox1.ScrollToCaret();
        }
           received_read_string = "";
      }
      else
      {
        // HEX MODE ACTIVE
        // deconding ASCII FROM RECEIVED BYTES
        //received_read_string = Encoding.ASCII.GetString(data_receive);
        //data_receive = Encoding.ASCII.GetBytes(received_read_string);

        // check is sum of two next bytes are zero or not
        ij = 31;

        while (data_receive.Length > ij)
        {
          if (data_receive[ij] == 0) { check_zeros_in_tail++; }
          if (data_receive[ij] != 0) { break; }
          ij--;
        }
        //richTextBox1.AppendText("check_zeros_in_tail = " + check_zeros_in_tail + "\n");
        ij = 0;
        // END CHECK LAST ALL BYTES IN TAIL IF ARE ZEROS TO NOT PRINT THEM !!! 
        richTextBox1.AppendText("receive in HEX >>>\n");
        while (ij < data_receive.Length - check_zeros_in_tail)
        {
          if (data_receive[ij] == 0x00)
          {
            check_hex_for_zeros++;
          }
          ij++;
        }
        
        ij = 0;

        if (check_hex_for_zeros > 0)
        {
          //richTextBox1.AppendText("receive in HEX >>>\n");
          richTextBox1.AppendText("THE STRING CONTAINS ZERO BYTES !!!\n");
         // richTextBox1.AppendText(received_read_string + "\n");
          //richTextBox1.ScrollToCaret();
        }

        while (ij < data_receive.Length - check_zeros_in_tail)
        {
          if (data_receive[ij] < 16)
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
        ij = 0;
        
        while (ij < data_receive.Length)
        {
          data_receive[ij] = (byte)0x00;
          ij++;
        }
        ij = 0;
        richTextBox1.ScrollToCaret();
         richTextBox1.AppendText("\n\n");
        income_bytes = 0;

      }
    }//=========== EDITED ROUTINE FOR AVOID MISSING FIRST BYTE =========

    private void button4_Click(object sender, EventArgs e)
    {     // BUTTON ASCII / HEX
      if (flag_ASCII_HEX == false)
      { flag_ASCII_HEX = true;
        richTextBox1.AppendText("\nASCII mode VIEW in RECEIVE is active\n");
        richTextBox1.ScrollToCaret();
        button4.Text = "ASCII";
      }
      else
      if (flag_ASCII_HEX == true)
      { flag_ASCII_HEX = false;
        richTextBox1.AppendText("\nHEX mode VIEW in RECEIVE is active\n");
        richTextBox1.ScrollToCaret();
        button4.Text = "HEX";
      }

    }

    private void button6_Click(object sender, EventArgs e)
    {   // BUTTON SEND TEXT FROM LINE
      string send_text = textBox1.Text;
      byte print_send_count = 0;
      char[] space_count = send_text.ToCharArray();
      byte check_space_count = 0;
      //============= TEST SPACE IN SEND LINE ==========================
      
      //============= TEST SPACE IN SEND LINE ==========================
      if (flag_BYTE == false)
      {
        if (textBox1.TextLength > 0)
        {
          richTextBox1.AppendText("<<< send\n");
          richTextBox1.AppendText(send_text + "\n\n");
          richTextBox1.ScrollToCaret();
          if (serialPort1.IsOpen)
          {
            serialPort1.Write(send_text);
          }
          else
          {
            serialPort1.Close();
            richTextBox1.AppendText("SERIAL PORT IS CLOSED !!!\n\n");
            button1.PerformClick();
          }

        }
        else
        { richTextBox1.AppendText("TYPE DATA IN TEXT LINE !!!\n"); }

      }
      else
      {
        if (textBox1.TextLength > 0)
        {
          //==================================================================
          // ADD CODE TO EDIT SENDA DATA BYTES WITH SPACE BETWEEN BYTES
          //==================================================================
          send_byte_to_serial();
          richTextBox1.AppendText("<<< send\n");

          while (print_send_count < send_text.Length / 2)
          { richTextBox1.AppendText(data_send[print_send_count].ToString("X") + " ");
            print_send_count+=1;
          }

          print_send_count = 0;
         // richTextBox1.AppendText("send_text.Length = " + send_text.Length);
          richTextBox1.AppendText("\n\n");
          
          if (serialPort1.IsOpen)
          {
            serialPort1.Write(data_send, 0, send_text.Length);
          }
          else
          { serialPort1.Close();
            richTextBox1.AppendText("SERIAL PORT IS CLOSED !!!\n\n");
            button1.PerformClick();
          }
          //serialPort1.Write(255.ToString());

          while (print_send_count < send_text.Length)
          {
            data_send[print_send_count] = 0;
            print_send_count++;
          }
          print_send_count = 0;

        }
        else
        {
          richTextBox1.AppendText("TYPE DATA IN TEXT LINE !!!\n");
        }
        richTextBox1.ScrollToCaret();

      }

    }

    private void button8_Click(object sender, EventArgs e)
    {     // BUTTON BYTE
      if (flag_BYTE == false) // BYTE MODE IS OFF
      {
        flag_BYTE = true;
        button8.BackColor = Color.LimeGreen;
        button8.ForeColor = Color.Blue;
        button8.Text = "BYTE";
        richTextBox1.AppendText("SEND DATA IN BYTES\n");
        richTextBox1.AppendText("ENTER 16 BYTES LIKE NEXT LINE\n");
        //richTextBox1.AppendText("1D 34 FF 1A 3E 90 56 11 22 33 D1 B2 C9 F4 C0 B8\n\n");
        richTextBox1.AppendText("1D34FF1A3E9056112233D1B2C9F4C0B8\n");
        richTextBox1.AppendText("MAX 32 SYMBOLS FROM 0-9 and A-F\n\n");
        richTextBox1.ScrollToCaret();
      }
      else
      {      // BYTE MODE IS ON
        flag_BYTE = false;
        button8.BackColor = Color.Red;
        button8.ForeColor = Color.Yellow;
        button8.Text = "ASCII";
        richTextBox1.AppendText("SEND DATA IN ASCII\n");
        richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
        richTextBox1.AppendText("example : BOARD4RELAY15OFF\n\n");
        richTextBox1.ScrollToCaret();
      }
      
      //button8.BackColor = Color.LimeGreen;
      //button8.ForeColor = Color.Blue;

    }

    private void button7_Click(object sender, EventArgs e)
    {     // BUTTON SETTINGS
      richTextBox1.AppendText("WILL WORK LATER\n\n");
      //richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
      //richTextBox1.AppendText("example : BOARD4RELAY7OFF\n\n");

      
      richTextBox1.ScrollToCaret();
    }
    // ============== NEXT CODE PLACE HERE ================


  }
}
/*
          while (check_space_count < space_count.Length - 1)
          {
            if (space_count[check_space_count] == ' ')
            {
              space_count[check_space_count] = send_text[check_space_count + 1];
            }

            check_space_count+=2;
          }

          richTextBox1.AppendText(check_space_count + "\n");

          check_space_count = 0;

          while (check_space_count < space_count.Length )
           {
            richTextBox1.AppendText(space_count[check_space_count].ToString());
             richTextBox1.AppendText(space_count[check_space_count + 1].ToString() + " ");

           check_space_count += 2;
           }
          check_space_count = 0;

          while (check_space_count < space_count.Length)
          {
            //richTextBox1.AppendText(space_count[check_space_count].ToString());
            //richTextBox1.AppendText(space_count[check_space_count + 1].ToString() + " ");
            string_no_spaces += space_count[check_space_count];
            check_space_count ++;
          }
          check_space_count = 0;
          richTextBox1.AppendText(string_no_spaces.Length + "\n");
          richTextBox1.AppendText("\n");
         */