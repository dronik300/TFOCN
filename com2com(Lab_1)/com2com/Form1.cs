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
            Debug.Text = "";                                            //Add ports in ComboBox
            ComboBox.Items.Add("port not selected");
            ComboBox.Items.AddRange(Ports);
            ComboBox.SelectedItem = "port not selected";
            Change--; //отвечает за изменение пользователем 1-пользователь, 0-изменение в коде
            ReadThread = new Thread(read);
            ReadThread.Start();
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {        //if received messasge in port, signal to read it
            Debug.Text = ("got message:\n");
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
                            Debug.Invoke((MethodInvoker)delegate
                            {
                                Debug.Text += message;
                            });
                            message = message.Replace("@+","g");
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

        //portName - хранит текущий порт, tmp_port - старый порт 
        //PortName сохраняет имя нового порта, отключаемся от старого порта
        //и пытемся подключится к новому. если не получается, то подключаемся к старому
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
                        Debug.Text = "Select " + PortNumber;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        ComPort.Close();
                        ComPort = new SerialPort(PreviousName);
                        ComPort.Encoding = Encoding.Unicode;
                        ComPort.Open();
                        Debug.Text = "Com Port is Closed. \nConnect to " + PreviousName;
                        Change--;
                        ComboBox.SelectedItem = PreviousName;
                    }
                    catch (IOException) { Debug.Text = "The port response time has expired. \nConnect to " + PreviousName; Change--; ComboBox.SelectedItem = PreviousName; }
                    finally { 
                        ComPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived); }
                }
            }
            if(PortNumber == "port not selected" && PreviousName != null) { ComPort.Close(); }
        }

        private void SendButton_Click(object sender, EventArgs e){
            if (PortNumber != "port not selected")
            {
                try
                {
                    string writeLine = Convert.ToString(InputBox.Text);
                    staffing = new Staffing();
                    writeLine = writeLine.Replace("g", "@+");
                    ComPort.WriteLine(writeLine);
                    InputBox.Text = "";
                    Debug.Text = "send message";
                }
                catch (TimeoutException) { }
            }
            else
            {
                Debug.Text="Select com port";
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
                        string message = Convert.ToString(InputBox.Text.TrimEnd('\r', '\n'));
                        message = message.Replace("g","@+");
                        ComPort.WriteLine(message);
                        InputBox.Text = null;
                        Debug.Text = "send message";
                    } catch (InvalidOperationException) { Debug.Text = "Port is Close. Select another port"; }
                }
            }
        }


    }
}