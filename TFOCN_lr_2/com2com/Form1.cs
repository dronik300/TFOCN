using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace lr_1
{
    public partial class Com_port : Form
    {
        SerialPort ComPort;
        Thread ReadThread;
        String PortNumber;
        private Staffing staffing;
        int Change = 0;
        static bool CanRead = false;
        static string[] Ports = SerialPort.GetPortNames();
        public Com_port()
        {
            InitializeComponent();
            this.FormClosing += Com_FormClosing;
            richTextBox1.AppendText("");                                            //Add ports in ComboBox
            ComboBox.Items.Add("port not selected");
            ComboBox.Items.AddRange(Ports);
            ComboBox.SelectedItem = "port not selected";
            Change--; //отвечает за изменение пользователем 1-пользователь, 0-изменение в коде
            ReadThread = new Thread(read);
            ReadThread.Start();
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {        //if received messasge in port, signal to read it
            richTextBox1.AppendText("\ngot message: ");
            CanRead = true;
        }

        private void read() {                                        // Thread for check new message in port
            while (true) {
                if (CanRead) 
                {
                    try {
                        OutputBox.Invoke((MethodInvoker)delegate 
                        {
                            string message;
                            message = ComPort.ReadLine();
                            //for (int index = 0; index < message.Length; index++)
                            //{
                            //    string output = "";
                            //    if ((message[index] == '@') && (message[index + 1] == '+'))
                            //    {
                            //        output = "@+";
                            //        richTextBox1.SelectionColor = Color.Red;
                            //        richTextBox1.AppendText(output);
                            //        richTextBox1.SelectionColor = Color.Black;
                            //        output = "";
                            //        index++;
                            //    }
                            //    else
                            //    {
                            //        if ((message[index] == '$'))
                            //        {
                            //            output = "$";
                            //            richTextBox1.SelectionColor = Color.Green;
                            //            richTextBox1.AppendText(output);
                            //            richTextBox1.SelectionColor = Color.Black;
                            //            output = "";
                            //            index++;
                            //        }
                            //        else
                            //        {
                            //            output = "";
                            //            output += message[index];
                            //            richTextBox1.AppendText(output);
                            //        }
                            //    }
                            //}
                            staffing = new Staffing();
                            message = Staffing.unByteStaffing(message);
                            OutputBox.Items.Add(message); 
                        });
                    }
                    catch (TimeoutException e)      { Console.WriteLine("TimeoutException -... " + e.Message); }
                    catch (ObjectDisposedException) { Console.WriteLine("ObjectDisposedException \n"); }
                    finally { CanRead = false;
                    }
                }
            }
        }

        private void ComboBox_SelectedIndexChanged_1(object sender, EventArgs e) {                      
            Change++;
            string PreviousName = PortNumber;
            PortNumber = ComboBox.SelectedItem.ToString();
            if (PortNumber == "port not selected")
            {
                InputBox.Enabled = false;
            }

            if (PortNumber != "port not selected")
            {
                InputBox.Enabled = true;
                if (Change == 1)
                {
                    Change--;
                    try
                    {
                        if (ComPort != null) { ComPort.Close(); }
                        ComPort = new SerialPort(PortNumber);
                        ComPort.Encoding = Encoding.Unicode;
                        ComPort.Open();
                        richTextBox1.AppendText("\nSelect " + PortNumber);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        ComPort.Close();
                        ComPort = new SerialPort(PreviousName);
                        ComPort.Encoding = Encoding.Unicode;
                        ComPort.Open();
                        richTextBox1.AppendText("\nCom Port is Closed. \nConnect to " + PreviousName);
                        Change--;
                        ComboBox.SelectedItem = PreviousName;
                    }
                    catch (IOException) { richTextBox1.AppendText("\nThe port response time has expired. \nConnect to " + PreviousName); Change--; ComboBox.SelectedItem = PreviousName; }
                    finally { 
                        ComPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived); }
                }
            }
            if(PortNumber == "port not selected" && PreviousName != null) { ComPort.Close(); Change--; }
        }

        private void SendButton_Click(object sender, EventArgs e){
            if (PortNumber != "port not selected")
            {
                richTextBox1.AppendText("\nsend message: ");
                try
                {
                    string message = Convert.ToString(InputBox.Text);
                    staffing = new Staffing();
                    message = Staffing.byteStaffing(message);
                    for (int index = 0; index < message.Length; index++)
                    {
                        string output = "";
                        if ((message[index] == '@') && (message[index + 1] == '+'))
                        {
                            output = "@+";
                            richTextBox1.SelectionColor = Color.Red;
                            richTextBox1.AppendText(output);
                            richTextBox1.SelectionColor = Color.Black;
                            output = "";
                            index++;
                        }
                        else
                        {
                            if ((message[index] == '$'))
                            {
                                output = "$";
                                richTextBox1.SelectionColor = Color.Green;
                                richTextBox1.AppendText(output);
                                richTextBox1.SelectionColor = Color.Black;
                                output = "";
                                index++;
                            }
                            else
                            {
                                output = "";
                                output += message[index];
                                richTextBox1.AppendText(output);
                            }
                        }
                    }
                    message = Staffing.byteStaffing(message);
                    ComPort.WriteLine(message);
                    InputBox.Text = "";
                }
                catch (TimeoutException) { }
            }
            else
            {
                richTextBox1.AppendText("\nSelect com port");
            }
        }

        private void Form1_Load(object sender, EventArgs e){
        }
        
        private void OutputBox_SelectedIndexChanged(object sender, EventArgs e){
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e){
        }

        public void Com_FormClosing(object sender, FormClosingEventArgs e){
            ReadThread.Abort();
            if(PortNumber != "port not selected")
            {
                ComPort.Close();
            }
            System.Environment.Exit(0);
            System.Environment.FailFast("Exit Error!");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void InputBox_TextChanged(object sender, EventArgs e) {                 //Send message if click enter 
            if (PortNumber == "port not selected")
            {
                InputBox.Enabled = true;
            }
            if (InputBox.Text.Length > 0 && PortNumber != "port not selected") {
                if (InputBox.Text[InputBox.Text.Length - 1] == '\n' && InputBox.Text.TrimEnd('\r', '\n') != "") {
                    try {
                        richTextBox1.AppendText("\nsend message: ");
                        string message = Convert.ToString(InputBox.Text.TrimEnd('\r', '\n'));
                        staffing = new Staffing();
                        message = Staffing.byteStaffing(message);
                        for (int index = 0; index < message.Length; index++)
                        {
                            string output = "";
                            if ((message[index] == '@') && (message[index + 1] == '+'))
                            {
                                output = "@+";
                                richTextBox1.SelectionColor = Color.Red;
                                richTextBox1.AppendText(output);
                                richTextBox1.SelectionColor = Color.Black;
                                output = "";
                                index++;
                            }
                            else
                            {
                                if ((message[index] == '$'))
                                {
                                    output = "$";
                                    richTextBox1.SelectionColor = Color.Green;
                                    richTextBox1.AppendText(output);
                                    richTextBox1.SelectionColor = Color.Black;
                                    output = "";
                                    index++;
                                }
                                else
                                {
                                    output = "";
                                    output += message[index];
                                    richTextBox1.AppendText(output);
                                }
                            }
                        }
                        ComPort.WriteLine(message);
                        InputBox.Text = null;
                    } catch (InvalidOperationException) { richTextBox1.AppendText("\nPort is Close. Select another port"); }
                }
            }
        }
    }
}