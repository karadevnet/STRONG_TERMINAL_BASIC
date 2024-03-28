using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
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
    byte[] data_send = new byte[48];
	byte[] data_send_END = new byte[4];


	StreamReader streamReader = null;


    string application_path = Application.ExecutablePath;
    string path_file_settings = @"settings.txt";

    string DEFAULT_COM_PORT = "DEFAULT_COM_PORT";
    string DEFAULT_COM_PORT_SPEED = "DEFAULT_COM_PORT_SPEED";
    string DEFAULT_COM_PORT_MODE = "";
    string string_ASCII_HEX_MODE = "";
    bool flag_ASCII_HEX = false;
    bool flag_BYTE = false;
	bool flag_BYTE_END = false;
	string[] settings_lines = { };
    
    byte send_value = 0;
    string string_no_spaces = "";
    string LAST_USED_COM_PORT_NAME = "";

	string send_text = "";
	string send_text_END = "";

	// ====================== SEND IN STRING FUNCTION ======================
	public void send_in_string()
    {
	  if (send_text.Length > 48)
	  {
		send_text = send_text.Substring(0, 48);
		textBox1.Text = send_text;
	  }
	  //richTextBox1.AppendText("send_state_button = " + send_state_button + "\n");
	  if (serialPort1.IsOpen)
	  { serialPort1.Write(send_text); }
	  else
	  {
		richTextBox1.AppendText("SERIAL PORT " + serialPort1.PortName + " IS CLOSED\n");
		richTextBox1.ScrollToCaret();
	  }
	} // ====================== END SEND IN STRING FUNCTION ======================

	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public void send_in_string_END()
	{
	  if (send_text_END.Length > 11)
	  {
		send_text_END = send_text_END.Substring(0, 11);
		textBox2.Text = send_text_END;
	  }
	  //richTextBox1.AppendText("send_state_button = " + send_state_button + "\n");
	  if (serialPort1.IsOpen)
	  { serialPort1.Write(send_text_END); }
	  else
	  {
		richTextBox1.AppendText("SERIAL PORT " + serialPort1.PortName + " IS CLOSED\n");
		richTextBox1.ScrollToCaret();
	  }
	}
	//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	// ====================== SEND STRING IN BYTES FUNCTION ======================
	public void send_in_string_in_bytes()
    {
	  send_text = textBox1.Text;

	  if (send_text.Length > 48)
	  {
		send_text = send_text.Substring(0, 48);
		textBox1.Text = send_text;
	  }

	  byte print_send_count = 0;
	  byte print_send_length = (byte)textBox1.TextLength;
	  richTextBox1.AppendText("print_send_length = " + print_send_length + "\n");

	  while (print_send_count < send_text.Length)
	  {
		if (send_text[print_send_count].Equals(" "))
		{ print_send_count++; }
		else
		{
		  data_send[print_send_count] = (byte)send_text[print_send_count];
		  print_send_count++;
		}
	  }

	  print_send_count = 0;

	  richTextBox1.AppendText("<<< send\n");
	  richTextBox1.AppendText("<<< ");

	  while (print_send_count < print_send_length)
	  {
		richTextBox1.AppendText(data_send[print_send_count].ToString("X") + " ");
		print_send_count++;
	  }
	  print_send_count = 0;
	  richTextBox1.AppendText("\n");
	  richTextBox1.ScrollToCaret();

	  // CHECK IF SERIAL COM PORT IS OPEN
	  if (serialPort1.IsOpen)
	  { serialPort1.Write(data_send, 0, print_send_length); }
	  else
	  {
		richTextBox1.AppendText("SERIAL PORT " + serialPort1.PortName + "IS CLOSED\n");
		richTextBox1.ScrollToCaret();
	  }
	}// ====================== END SEND STRING IN BYTES FUNCTION ======================

	// +++++++++++++++++++ SEND STRING IN BYTES FUNCTION +++++++++++++++++++++++++
	public void send_in_string_in_bytes_END()
	{
	  if (send_text_END.Length > 11)
	  {
		send_text_END = send_text_END.Substring(0, 11);
		textBox2.Text = send_text_END;
	  }

	  byte print_send_count_END = 0;
	  byte print_send_length_END = (byte)textBox2.TextLength;
	  richTextBox1.AppendText("print_send_length_END = " + print_send_length_END + "\n");

	  while (print_send_count_END < send_text_END.Length)
	  {
		if (send_text_END[print_send_count_END].Equals(" "))
		{ print_send_count_END++; }
		else
		{
		  data_send[print_send_count_END] = (byte)send_text_END[print_send_count_END];
		  print_send_count_END++;
		}
	  }

	  print_send_count_END = 0;

	  richTextBox1.AppendText("<<< send\n");
	  richTextBox1.AppendText("<<< ");

	  while (print_send_count_END < print_send_length_END)
	  {
		richTextBox1.AppendText(data_send[print_send_count_END].ToString("X") + " ");
		print_send_count_END++;
	  }
	  print_send_count_END = 0;
	  richTextBox1.AppendText("\n");
	  richTextBox1.ScrollToCaret();

	  // CHECK IF SERIAL COM PORT IS OPEN
	  if (serialPort1.IsOpen)
	  { serialPort1.Write(data_send, 0, print_send_length_END); }
	  else
	  {
		richTextBox1.AppendText("SERIAL PORT " + serialPort1.PortName + "IS CLOSED\n");
		richTextBox1.ScrollToCaret();
	  }
	}// ++++++++++++++++++++++ END SEND STRING IN BYTES FUNCTION ++++++++++++++++++++++

	// ======================  SEND HEX STRING IN HEX BYTES FUNCTION ======================
	public void send_HEX_string_in_HEX_bytes()
    {
	  byte print_send_count = 0;

	  if (send_text.Length > 48)
	  {
		send_text = send_text.Substring(0, 48);
		textBox1.Text = send_text;
	  }

	  // AUTO REMOVE PAUSE " " FROM ENTERED STRING IN FIELD
	  if (send_text.EndsWith(" "))
	  {
		send_text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
		textBox1.Text = send_text;
	  }
	  //richTextBox1.AppendText("send_state_button = " + send_state_button + "\n");
	  try
	  { 

		data_send = send_text.Split().Select(s => byte.Parse(s, NumberStyles.HexNumber)).ToArray();

		while (print_send_count < data_send.Length)
		{
		  richTextBox1.AppendText(data_send[print_send_count].ToString("X") + " ");
		  print_send_count++;
		}
		print_send_count = 0;
		richTextBox1.AppendText("\n\n");
		richTextBox1.ScrollToCaret();

		// CHECK IF SERIAL COM PORT IS OPEN
		if (serialPort1.IsOpen)
		{ serialPort1.Write(data_send, 0, data_send.Length); }
		else
		{
		  richTextBox1.AppendText("SERIAL PORT IS CLOSED\n");
		  richTextBox1.ScrollToCaret();
		}

	  }
	  catch
	  {
		richTextBox1.AppendText("\nENTERED STRING IS INVALID FORMAT OR LENGTH IS ODD !!!\n");
		richTextBox1.AppendText("CORRECT FORMAT IS\n");
		richTextBox1.AppendText("AA 11 22 33 44 55 66 77 88 99 AA BB CC DD EE FF\n");
		richTextBox1.AppendText("STRING CAN NOT END WITH EMPTY SPACE [ ]\n\n");
		richTextBox1.ScrollToCaret();
	  }
	}   // ====================== END SEND HEX STRING IN HEX BYTES FUNCTION ======================

	// +++++++++++++++++++ SEND HEX STRING IN HEX BYTES FUNCTION +++++++++++++++++++++++++
	public void send_HEX_string_in_HEX_bytes_END()
	{
	  byte print_send_count = 0;

	  if (send_text_END.Length > 48)
	  {
		send_text_END = send_text_END.Substring(0, 48);
		textBox2.Text = send_text_END;
	  }

	  // AUTO REMOVE PAUSE " " FROM ENTERED STRING IN FIELD
	  if (send_text_END.EndsWith(" "))
	  {
		send_text_END = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
		textBox2.Text = send_text_END;
	  }
	  //richTextBox1.AppendText("send_state_button = " + send_state_button + "\n");
	  try
	  {

		data_send = send_text_END.Split().Select(s => byte.Parse(s, NumberStyles.HexNumber)).ToArray();

		while (print_send_count < data_send.Length)
		{
		  richTextBox1.AppendText(data_send[print_send_count].ToString("X") + " ");
		  print_send_count++;
		}
		print_send_count = 0;
		richTextBox1.AppendText("\n\n");
		richTextBox1.ScrollToCaret();

		// CHECK IF SERIAL COM PORT IS OPEN
		if (serialPort1.IsOpen)
		{ serialPort1.Write(data_send, 0, data_send.Length); }
		else
		{
		  richTextBox1.AppendText("SERIAL PORT IS CLOSED\n");
		  richTextBox1.ScrollToCaret();
		}

	  }
	  catch
	  {
		richTextBox1.AppendText("\nENTERED STRING IS INVALID FORMAT OR LENGTH IS ODD !!!\n");
		richTextBox1.AppendText("CORRECT FORMAT IS\n");
		richTextBox1.AppendText("AA 11 22 33 44 55 66 77 88 99 AA BB CC DD EE FF\n");
		richTextBox1.AppendText("STRING CAN NOT END WITH EMPTY SPACE [ ]\n\n");
		richTextBox1.ScrollToCaret();
	  }
	}// ====================== END SEND HEX STRING IN HEX BYTES FUNCTION ======================

	// +++++++++++++++++++ SEND HEX STRING IN HEX BYTES FUNCTION +++++++++++++++++++++++++
	//============================================================================================



	public Form1()
    {
      InitializeComponent();
      var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
      var fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
      string version = "v." + fileVersion.ToString();
      label1.Text = version.Substring(0,6) + " - 2024";
    }

    private void Form1_Load(object sender, EventArgs e)
    {
	  //string textbox_send_value = textBox3.Text;
	  //byte textbox_send_BYTES = byte.Parse(textBox3.Text);
	  //label3.Text = "END STRING";
	  data_send_END[0] = 255;
	  data_send_END[1] = 255;
	  data_send_END[2] = 255;
	  data_send_END[3] = 255;

	  button4.Text = "ASCII";
	  button4.BackColor = Color.Red;
	  button4.ForeColor = Color.Yellow;
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
            comboBox1.FindString(serialPort1.PortName);
            comboBox1.SelectedItem = serialPort1.PortName;
            // set function like global and use in get port button and disconnect button
            // for read in two case same data for default project from settings file
            // disable LAST_USED_COM_PORT_NAME = serialPort1.PortName; when you done
            LAST_USED_COM_PORT_NAME = serialPort1.PortName;

            richTextBox1.AppendText("speed : " + settings_lines[project_count + 2].Substring(23) + "\n");
            serialPort1.BaudRate = int.Parse(settings_lines[project_count + 2].Substring(23));
            comboBox2.FindString(serialPort1.BaudRate.ToString());
            comboBox2.SelectedItem = serialPort1.BaudRate.ToString();


            richTextBox1.AppendText("mode ASCII / HEX : " + settings_lines[project_count + 3].Substring(22) + "\n");
            if (settings_lines[project_count + 3].Substring(22) == "ASCII")
            { flag_ASCII_HEX = true; button4.Text = "ASCII"; }
            if (settings_lines[project_count + 3].Substring(22) == "HEX")
            { flag_ASCII_HEX = false; button4.Text = "HEX"; }

           }
          project_count++;
        }
        project_count = 0;

        richTextBox1.AppendText("\nUSE BUTTON HEX / ASCII IN TOP\n");
        richTextBox1.AppendText("TO VIEW RECEIVED DATA IN HEX VALUES\n");
        richTextBox1.AppendText("OR VIEW IN REDABLE STRING MESSAGES\n\n");

        richTextBox1.AppendText("SET BUTTON BYTE TO SEND DATA IN BYTES\n");
        richTextBox1.AppendText("ENTER 16 BYTES LIKE NEXT LINE\n");

        richTextBox1.AppendText("1D 34 FF 1A 3E 90 56 11 22 33 D1 B2 C9 F4 C0 B8\n");
        richTextBox1.AppendText("MAX 32 SYMBOLS FROM 0-9 and A-F\n\n");

        richTextBox1.AppendText("OR SET BUTTON BYTE TO SEND DATA IN ASCII / HEX\n");
        richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
        richTextBox1.AppendText("example : BOARD4RELAY7OFF\n\n");
		richTextBox1.AppendText("CODING SYSTEM : Windows-1252\n");
		richTextBox1.AppendText("LINK to WEB : https://www.ascii-code.com/\n\n");
	  }

    } // END FORM LOAD

   

    private void button1_Click(object sender, EventArgs e)
    {    // GET PORTS
	  comboBox1.Items.Clear();
	  string[] ports = SerialPort.GetPortNames();
      richTextBox1.AppendText("rs232 ports found\n\n");
	 
	  foreach (string port in ports)
      {
        richTextBox1.AppendText(port + " is active\n");
        comboBox1.Items.Add(port);

      }
	  
	  var count = comboBox1.Items.Count;

	  richTextBox1.AppendText(count + " serial port are active\n");
      richTextBox1.ScrollToCaret();

      if (LAST_USED_COM_PORT_NAME != "")
      {
        serialPort1.PortName = LAST_USED_COM_PORT_NAME;
        comboBox1.FindString(LAST_USED_COM_PORT_NAME);
        comboBox1.SelectedIndex = comboBox1.FindString(LAST_USED_COM_PORT_NAME);
		comboBox1.SelectedIndex = comboBox1.FindString(ports[0]);
	  }
      else
      { LAST_USED_COM_PORT_NAME = "COM1";
        comboBox1.SelectedIndex = comboBox1.FindString(ports[0]);
		serialPort1.PortName = LAST_USED_COM_PORT_NAME;
	  }
	 
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

      if (serialPort1.IsOpen)
      {
        serialPort1.DiscardInBuffer();
        serialPort1.DiscardOutBuffer();
        serialPort1.Close(); serialPort1.Dispose();
        richTextBox1.AppendText(serialPort1.PortName.ToString() + " is CLOSED" + "\n");
      }

       try
      {
        if (!serialPort1.IsOpen)
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
       // var items = comboBox1.Items.Count;
        //  while (items > 0)
        //  {
       //     comboBox1.Items.Clear();
       //     items--;
       //   }
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
        while ( ij < data_receive.Length)
        {
          if (data_receive[ij] == 0 || data_receive[ij] == '\0')
          { break; }
          received_read_string += Encoding.ASCII.GetString(data_receive)[ij];
          ij++;
        }
        ij = 0;

        if (received_read_string.EndsWith("\n"))
        {
          richTextBox1.AppendText(">>> receive in ASCII\n");
          richTextBox1.AppendText(">>> " + received_read_string);
          //richTextBox1.AppendText("\n");
          richTextBox1.ScrollToCaret();
        }
        else
        {
          richTextBox1.AppendText(">>> receive in ASCII\n");
          richTextBox1.AppendText(">>> " + received_read_string);
          richTextBox1.AppendText("\n");
          richTextBox1.ScrollToCaret();
        }
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
        
        richTextBox1.AppendText("receive in HEX\n");
         richTextBox1.AppendText(">>> ");

        while (ij < data_receive.Length - check_zeros_in_tail)
        {
          if (data_receive[ij] == 0x00)
          {
            check_hex_for_zeros++;
          }
          ij++;
        }
        
        ij = 0;

        if (check_hex_for_zeros > 0 && checkBox1.Checked)
        {
          richTextBox1.AppendText("THE STRING CONTAINS ZERO BYTES !!!\n");
        }

         while (ij < data_receive.Length - check_zeros_in_tail)
        {
            if (data_receive[ij] < 16)
            {
              richTextBox1.AppendText("0" + data_receive[ij].ToString("X") + " ");
             }
            else if (data_receive[ij] > 15)
            {
              richTextBox1.AppendText(data_receive[ij].ToString("X") + " ");

             }
            if (ij == 15)
              {
            richTextBox1.AppendText("\n"); richTextBox1.AppendText(">>> ");
            richTextBox1.ScrollToCaret();
             }
            ij++;
         }
            ij = 0;

        richTextBox1.AppendText("\n\n");
        richTextBox1.ScrollToCaret();
      }
          // CLEAR INPUT BUFFER AND STRING RECEIVED WITH DIFFERENT LENGTH
          // AFTER RECEIVE AND PRINT ALL DATA IN BYTES AND STRINGS
          while (ij < data_receive.Length)
          {
            data_receive[ij] = (byte)0x00;
            ij++;
          }
          ij = 0;
          
         
           income_bytes = 0;

          received_read_string = "";
    }//=========== EDITED ROUTINE FOR AVOID MISSING FIRST BYTE =========

    private void button4_Click(object sender, EventArgs e)
    {     // BUTTON ASCII / HEX
      if (flag_ASCII_HEX == false)
      { flag_ASCII_HEX = true;
        richTextBox1.AppendText("\nASCII mode VIEW in RECEIVE is active\n");
        richTextBox1.ScrollToCaret();
        button4.Text = "ASCII";
		button4.BackColor = Color.Red;
		button4.ForeColor = Color.Yellow;
	  }
      else
      if (flag_ASCII_HEX == true)
      { flag_ASCII_HEX = false;
        richTextBox1.AppendText("\nHEX mode VIEW in RECEIVE is active\n");
        richTextBox1.ScrollToCaret();
        button4.Text = "HEX";
		button4.BackColor = Color.LimeGreen;
	  }

    }

    private void button6_Click(object sender, EventArgs e)
    {   // BUTTON SEND TEXT FROM TEXT LINE
       send_text = textBox1.Text;
	   send_text_END = textBox2.Text;
	  //byte print_send_count = 0;
	  byte print_send_length = (byte)textBox1.TextLength;
	  byte send_text_END_FRAME_bytes = (byte)textBox2.TextLength;

      //byte[] text_to_bytes = new byte[64];

      if (send_text.Length > 0)
      {
        richTextBox1.AppendText("print_send_length = " + print_send_length + "\n");
		richTextBox1.ScrollToCaret();
	  }

      if (send_text_END.Length > 0)
      {
        richTextBox1.AppendText("send_text_END_FRAME_bytes = " + send_text_END_FRAME_bytes + "\n");
		richTextBox1.ScrollToCaret();
	  }

      if (send_text.Length > 0 && serialPort1.IsOpen)
      {
        richTextBox1.AppendText("\n<<< send\n");
        richTextBox1.AppendText("<<< " + send_text + send_text_END + "\n\n");
		richTextBox1.ScrollToCaret();
	  }
	  

	  //=========== WORK FOR STRINGS + FF FF FF ================
	  if (send_state_button == 0 && send_text.Length > 0)
      {
        send_in_string();
		
	  }
	  //serialPort1.Write(data_send_END, 0, 3); //array 0xFF, 0xFF, 0xFF + 0xFF
	  //=====================================

	  //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	  if (send_END_state_button == 0 && send_text_END.Length > 0)
	  {
		send_in_string_END();

	  }
	  //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

	  // ======== CONVERT t0.txt="22222" WORK STRING TO BYTES ARRAY TO SEND IN BYTE MODE

	  if (send_state_button == 1 && send_text.Length > 0)
	  {
        send_in_string_in_bytes();
		
	  }

	  //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	  if (send_END_state_button == 1 && send_text_END.Length > 0)
	  {
		send_in_string_in_bytes_END();

	  }
	  //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


	  //serialPort1.Write(data_send_END, 0, 3); //array 0xFF, 0xFF, 0xFF + 0xFF

	  //=============================================================


	  //=============== CONVERT HEX STRING  WORK TO CLEAR BYTES ===============
	  //=============== AA 11 22 33 44 55 66 77 88 99 AA BB CC DD EE FF ===============
	  //=============== 0xAA 0x11 0x22 0x33 0x44 0x55 0x66 0x77 ===============
	  //==============  0x88 0x99 0xAA 0xBB 0xCC 0xDD 0xEE 0xFF ===============

	  if (send_state_button == 2 && send_text.Length > 0)
      {
        send_HEX_string_in_HEX_bytes();
        //=============== CONVERT HEX STRING  WORK TO CLEAR BYTES ===============
      }

	  if (send_END_state_button == 2 && send_text_END.Length > 0)
	  {
		send_HEX_string_in_HEX_bytes_END();

	  }

	}


	byte send_state_button = 0;
	private void button8_Click(object sender, EventArgs e)
    {     // BUTTON BYTE / ASCII FOR SEND COMMAND TEXT
	  send_state_button++;
	  if (send_state_button > 2)
	  { send_state_button = 0; }

	  if (send_state_button == 0) // BYTE MODE IS OFF
      {
		//flag_BYTE = true;
		//label3.Text = "END STRING";
		button8.BackColor = Color.Red;
		button8.ForeColor = Color.Yellow;
		button8.Text = "ASCII";
		richTextBox1.AppendText("SEND STRING DATA IN ASCII\n");
		richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
		richTextBox1.AppendText("example : BOARD4RELAY15OFF\n");
		richTextBox1.AppendText("example send : BOARD4RELAY15OFF\n\n");
		richTextBox1.ScrollToCaret();
	  }
      else if (send_state_button == 1)
	  {      // BYTE MODE IS ON
       // flag_BYTE = false;
		//label3.Text = "END BYTES";
		
		button8.BackColor = Color.LimeGreen;
		button8.ForeColor = Color.Blue;
		button8.Text = "BYTE";
		richTextBox1.AppendText("SEND ASCII STRING DATA IN BYTES\n");
		richTextBox1.AppendText("ENTER 16 BYTES LIKE NEXT LINE\n");
		richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
		richTextBox1.AppendText("example : BOARD4RELAY15OFF\n");
		richTextBox1.AppendText("example send : 42 4F 41 52 44 34 52 45 4C 41 59 31 35 4F 46 46\n\n");
		richTextBox1.ScrollToCaret();
	  }

	  else if (send_state_button == 2)
	  {      // BYTE MODE IS ON
		//flag_BYTE = false;
		//label3.Text = "END BYTES";
		button8.BackColor = Color.Blue;
		button8.ForeColor = Color.Yellow;
		button8.Text = "HEX";
		richTextBox1.AppendText("SEND DATA FROM HEX STRING\n");
		richTextBox1.AppendText("ENTER 16 BYTES LIKE NEXT LINE\n");
		richTextBox1.AppendText("ENTER STRING MESSAGE 16 LETTER / NUMBER LONG\n");
		richTextBox1.AppendText("example : 42 4F 41 52 44 34 52 45 4C 41 59 31 35 4F 46 46\n");
		richTextBox1.AppendText("example : 42 4F 41 52 44 34 52 45 4C 41 59 31 35 4F 46 46\n\n");
		richTextBox1.ScrollToCaret();
	  }

	}

	byte send_END_state_button = 0;
	private void button10_Click(object sender, EventArgs e)
	{ // BUTTON BYTE / ASCII FOR SEND END LINE TEXT
	  //flag_BYTE_END
	  send_END_state_button++;
	  if (send_END_state_button > 2)
	  { send_END_state_button = 0; }

	  if (send_END_state_button == 0) // BYTE MODE IS OFF
	  {
		//flag_BYTE = true;
		//label3.Text = "END STRING";
		button10.BackColor = Color.Red;
		button10.ForeColor = Color.Yellow;
		button10.Text = "ASCII";
		richTextBox1.AppendText("SEND STRING DATA IN ASCII\n");
		richTextBox1.AppendText("ENTER STRING MESSAGE G\n");
		richTextBox1.AppendText("MAX 11 LETTER / NUMBER LONG\n");
		richTextBox1.AppendText("example : TRANSMITOFF\n");
		richTextBox1.AppendText("example send : TRANSMITOFF\n\n");
		richTextBox1.ScrollToCaret();
		richTextBox1.ScrollToCaret();
	  }
	  else if (send_END_state_button == 1)
	  {      // BYTE MODE IS ON
			 // flag_BYTE = false;
			 //label3.Text = "END BYTES";

		button10.BackColor = Color.LimeGreen;
		button10.ForeColor = Color.Blue;
		button10.Text = "BYTE";
		richTextBox1.AppendText("SEND ASCII STRING DATA IN BYTES\n");
		richTextBox1.AppendText("ENTER 11 BYTES LIKE NEXT LINE\n");
		richTextBox1.AppendText("example : TRANSMITOFF\n");
		richTextBox1.AppendText("example send : 54 52 41 4E 53 4D 49 54 4F 46 46\n\n");
		richTextBox1.ScrollToCaret();
	  }

	  else if (send_END_state_button == 2)
	  {      // HEX MODE IS ON
			 //flag_BYTE = false;
			 //label3.Text = "END BYTES";
		button10.BackColor = Color.Blue;
		button10.ForeColor = Color.Yellow;
		button10.Text = "HEX";
		richTextBox1.AppendText("SEND DATA FROM HEX STRING\n");
		richTextBox1.AppendText("ENTER HEX STRING MESSAGE 8 LETTER / NUMBER LONG\n");
		richTextBox1.AppendText("MAX 8 SYMBOLS FROM 0-9 and A-F\n");
		richTextBox1.AppendText("example : AA BB CC DD\n");
		richTextBox1.AppendText("example send : AA BB CC DD\n\n");
		richTextBox1.ScrollToCaret();
	  }
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