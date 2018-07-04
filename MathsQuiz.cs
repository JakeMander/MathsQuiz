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
        
        //Store Values For Artithmetic Questions
        int _AddEnd1 = 0;
        int _AddEnd2 = 0;

        int _Subtract1 = 0;
        int _Subtract2 = 0;

        int _Multiply1 = 0;
        int _Multiply2 = 0;

        int _Divide1 = 0;
        int _Divide2 = 0;
        int _TempQuotient = 0;

        static int _TimeRemaining = 0;

        private void FormSymbolDivision_Load(object sender, EventArgs e)
        {
            //Set Timer Interval And Determine The Action To Be Taken Each Tick
            _QuizTimer.Tick += new EventHandler(TimerEvent);
            _QuizTimer.Interval = 1000;
        }

        public FormSymbolDivision()
        {
            InitializeComponent();
        }

        public void StartQuiz()
        {
            //Generate The Numbers For Each Arithmetic Question
            _AddEnd1 = _RandomNumberGenerator.Next(101);
            _AddEnd2 = _RandomNumberGenerator.Next(101);

            _Subtract1 = _RandomNumberGenerator.Next(1, 101);
            _Subtract2 = _RandomNumberGenerator.Next(1, _Subtract1);

            _Multiply1 = _RandomNumberGenerator.Next(1, 11);
            _Multiply2 = _RandomNumberGenerator.Next(1, 11);

            /*Ensure Dividend Is Always A Factor Of The Divisor By Multiplying The Divsor By A Random Number
              And Assigning Value To Dividend*/
            _Divide2 = _RandomNumberGenerator.Next(2,11);
            _Divide1 = _Divide2 * _RandomNumberGenerator.Next(2, 11);

            //When Quiz Starts, Set First Row Values. Spinner Set To 0 As Default Value For Continuity. 
            labelLeftSumValue.Text = _AddEnd1.ToString();
            labelRightSumValue.Text = _AddEnd2.ToString();
            labelLeftDifferenceValue.Text = _Subtract1.ToString();
            labelRightDifferenceValue.Text = _Subtract2.ToString();
            labelLeftProductValue.Text = _Multiply1.ToString();
            labelRightProductValue.Text = _Multiply2.ToString();
            labelLeftDivisionValue.Text = _Divide1.ToString();
            labelRightDivisionValue.Text = _Divide2.ToString();
            numericUpDownSum.Value = 0;
            numericUpDownDifference.Value = 0;

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //Start Quiz And Disable Function To Prevent Quiz Reset.
            StartQuiz();
            _TimeRemaining = 30;

            labelTime.Text = "30 Seconds";
            buttonStart.Enabled = false;
            _QuizTimer.Start();
        }

        void TimerEvent (object sender, EventArgs e)
        { 
            if (CheckTheAnswer())
            {
                _QuizTimer.Stop();
                MessageBox.Show("Congratulations! You Got The Answers Correct!");
                buttonStart.Enabled = true;
            }
            else if (_TimeRemaining > 0)
            {
                _TimeRemaining--;
                labelTime.Text = _TimeRemaining + " Seconds";
            }

            else
            {
                //Stop Quiz And Notify User That Time Is Up Via Label And Message Box.
                _QuizTimer.Stop();
                labelTime.Text = "Time Expired";
                MessageBox.Show("Out Of Time!", "Ok");

                /*Once Message Box Is Clicked Add Totals Together And Display In Respective Box.
                 *Re-enable The Start Button To Restart Quiz */
                numericUpDownSum.Value = _AddEnd1 + _AddEnd2;
                numericUpDownDifference.Value = _Subtract1 - _Subtract2;
                numericUpDownDivision.Value = _Divide1 / _Divide2;
                numericUpDownProduct.Value = _Multiply1 * _Multiply2;
                buttonStart.Enabled = true;
            }
        }

        private void numericUpDown_Enter(object sender, EventArgs e)
        {
            //Convert Sender 'Object' To Type An Instance Of The NumericUpDown Class.
            NumericUpDown answerBox = sender as NumericUpDown;

            //Verify Cast. Check To See answerBox Is Valid. An Invalid Element Would Return Null.
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        //On Click, Select The Enitre Contents Of The NumericUpDown Box Ready For Editing. 
        private void numericUpDownSum_MouseClick(object sender, MouseEventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private bool CheckTheAnswer()
        {
            if ((_AddEnd1 + _AddEnd2 == numericUpDownSum.Value)
                && (_Subtract1 - _Subtract2 == numericUpDownDifference.Value)
                    && (_Multiply1 * _Multiply2 == numericUpDownProduct.Value)
                        &&(_Divide1/_Divide2 == numericUpDownDivision.Value))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
