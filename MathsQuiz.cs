using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Maths_Quiz
{
    public partial class FormSymbolDivision : Form
    {
        //Object To Generate Random Numbers For Quiz And Manage Countdown Timer
        Random _RandomNumberGenerator = new Random();
        System.Windows.Forms.Timer _QuizTimer = new System.Windows.Forms.Timer();
        
        //Store Values For Sum Question And Countdown Timer
        int _AddEnd1 = 0;
        int _AddEnd2 = 0;
        static int _TimeRemaining = 0;

        public FormSymbolDivision()
        {
            InitializeComponent();
        }

        public void StartQuiz()
        {
            //Generate The Numbers And Assign to Input Values For Addition
            _AddEnd1 = _RandomNumberGenerator.Next(101);
            _AddEnd2 = _RandomNumberGenerator.Next(101);

            //When Quiz Starts, Set First Row Values. Spinner Set To 0 As Default Value For Continuity. 
            labelLeftSumValue.Text = _AddEnd1.ToString();
            labelRightSumValue.Text = _AddEnd2.ToString();
            numericUpDownSum.Value = 0;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //Start Quiz And Disable Function To Prevent Quiz Reset.
            StartQuiz();
            _TimeRemaining = 30;

            labelTime.Text = "30 Seconds";
            buttonStart.Enabled = false;

            //Set Timer Interval And Determine The Action To Be Taken Each Tick
            _QuizTimer.Tick += new EventHandler(TimerEvent);
            _QuizTimer.Interval = 1000;
            _QuizTimer.Start();
        }

        void TimerEvent (object sender, EventArgs e)
        {
            if (_TimeRemaining > 0)
            {
                _TimeRemaining--;
                labelTime.Text = _TimeRemaining + " Seconds";
            }

            else
            {
                //Stop Quiz And Notify User That Time Is Up Via Label And Dialog Box.
                _QuizTimer.Stop();
                labelTime.Text = "Time Expired";
                MessageBox.Show("Out Of Time!", "Ok");

                /*Once Dialog Box Is Clicked Add Totals Together And Display In Respective Box.
                 *Re-enable The Start Button To Restart Quiz */
                numericUpDownSum.Value = _AddEnd1 + _AddEnd2;
                buttonStart.Enabled = true;
            }
        }
    }
}
