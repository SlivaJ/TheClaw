using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace ClawControl
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            serialPort1 = new SerialPort();
            serialPort1.PortName = "COM8";
            serialPort1.BaudRate = 9600;

            try
            {
                serialPort1.Open();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

            resetClaw();

        }

        private void pinchControl_tb_Scroll(object sender, EventArgs e)
        {
            //pinch Servo is init as 0 in Arduino Code
            String pinchCommand, pinchValue;

            pinchValue = pinchControl_tb.Value.ToString();
            pinchCommand = "0:" + pinchValue + "*";

            pinchControl_Value_label.Text = "Value: " + pinchControl_tb.Value.ToString();

            serialPort1.Write(pinchCommand);

        }

        private void rotateControl_tb_scroll(object sender, EventArgs e)
        {
            //claw rotation servo is init as 1 in Arduino code
            String rotateCommand, rotateValue;

            rotateValue = rotateControl_tb.Value.ToString();
            rotateCommand = "1:" + rotateValue + "*";

            rotationControl_Value_label.Text = "Value: " + rotateControl_tb.Value.ToString();

            serialPort1.Write(rotateCommand);
        }

       

        private void clawJointControl_tb_scroll(object sender, EventArgs e)
        {
             //claw Joint servo is init as 2 in Arduino code
             String clawJointCommand, clawJointValue;

             clawJointValue = clawJointControl_tb.Value.ToString();
             clawJointCommand = "2:" + clawJointValue + "*";

             clawJoint_Value_label.Text = "Value: " + clawJointControl_tb.Value.ToString();

             serialPort1.Write(clawJointCommand);

            

        }

        private void centerJointControl_tb_scroll(object sender, EventArgs e)
        {
            //center Joint servo is init as 3 in Arduino code
            String centerJointCommand, centerJointValue;

            centerJointValue = centerJointControl_tb.Value.ToString();
            centerJointCommand = "3:" + centerJointValue + "*";

            centerJoint_Value_label.Text = "Value: " + centerJointControl_tb.Value.ToString();

            serialPort1.Write(centerJointCommand);

        }

        private void baseJointControl_tb_scroll(object sender, EventArgs e)
        {
            //base Joint servo is init as 4 in Arduino code
            //this controls only the left motor. Arduino code controls the opposite motor based on input
            String baseJointCommand_L, baseJointValue_L, baseJointCommand_R, baseJointValue_R;

            baseJointValue_L = baseJointControl_tb.Value.ToString();
            baseJointCommand_L = "4:" + baseJointValue_L + "*";

            int inverted;
            inverted = Convert.ToInt32(baseJointValue_L);
            inverted = (inverted * -1) + 180;
            baseJointValue_R = inverted.ToString();
            baseJointCommand_R = "5:" + baseJointValue_R + "*";

            baseJoint_Value_label.Text = "Value: " + baseJointControl_tb.Value.ToString();

            serialPort1.Write(baseJointCommand_L);
            serialPort1.Write(baseJointCommand_R);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void resetButton_click(object sender, EventArgs e)
        {
            resetClaw();

        }

        private void resetClaw() 
        {
            //reset all servos to default position by command

            pinchControl_tb.Value = 75;
            rotateControl_tb.Value = 85;
            clawJointControl_tb.Value = 61;
            centerJointControl_tb.Value = 53;
            baseJointControl_tb.Value = 101;

            serialPort1.Write("0:75*");
            serialPort1.Write("1:85*");
            serialPort1.Write("2:61*");
            serialPort1.Write("3:53*");
            serialPort1.Write("4:90*");
            serialPort1.Write("5:90*");

        }

        private void rotateBaseControl_L_btn_Click(object sender, EventArgs e)
        {
           
            serialPort1.Write("7:100*");
            
            
        }

        private void rotateBaseControl_R_btn_Click(object sender, EventArgs e)
        {
            
            serialPort1.Write("8:100*");
        }
    }
}

