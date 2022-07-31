using System;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsCalculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            txtInput.GotFocus += (s, e) => HideCaret(txtInput.Handle);
        }

        double operand1, operand2;
        string @operator = null;
        LastSet lastSet = LastSet.None;


        private void Numbers_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btnNum)
                {
                    if (lastSet == LastSet.None)
                        lastSet = LastSet.FirstOperand;
                    else if (lastSet == LastSet.Operator)
                    {
                        txtInput.Clear();
                        lastSet = LastSet.SecondOperand;
                    }

                    if (txtInput.Text.Length == 16)
                        return;

                    if (txtInput.Text == "0")
                    {
                        if (btnNum == btnNum0)
                            return;
                        txtInput.Clear();
                    }
                    txtInput.Text += btnNum?.Text;

                    if (lastSet == LastSet.FirstOperand)
                        operand1 = /*operand2 = */double.Parse(txtInput.Text);
                    else if (lastSet == LastSet.SecondOperand)
                        operand2 = double.Parse(txtInput.Text);

                    this.ActiveControl = lblStatus;
                }
            }
            catch (Exception ex) {
                CatchException(ex);
            }
        }

        public double Calculate()
        {
            try
            {
                double result = 0;
                switch (@operator)
                {
                    case "+": result = operand1 + operand2; break;
                    case "-": result = operand1 - operand2; break;
                    case "*": result = operand1 * operand2; break;
                    case "/": result = operand1 / operand2; break;
                }
                return operand1 = result;
            }
            catch (Exception ex) {
                CatchException(ex);
                return 0;
            }
        }

        public void CatchException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Calculator", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Operators_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btnOpr)
                {
                    if (lastSet == LastSet.Operator)
                    {
                        lblStatus.Text = lblStatus.Text.Substring(0, lblStatus.Text.Length - 2) + btnOpr?.Text.ToLower() + " ";
                        @operator = btnOpr?.Text;
                        return;
                    }

                    lblStatus.Text += $"{txtInput.Text} {btnOpr?.Text} ";

                    if (lastSet == LastSet.SecondOperand && lastSet != LastSet.Answer)
                        txtInput.Text = Calculate().ToString();

                    operand2 = operand1;
                    @operator = btnOpr?.Tag != null ? btnOpr.Tag.ToString() : btnOpr?.Text;
                    lastSet = LastSet.Operator;
                    
                    this.ActiveControl = lblStatus;
                }
            }
            catch (Exception ex) {
                CatchException(ex);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.D0:
                    case Keys.NumPad0:
                        btnNum0.PerformClick();
                        break;
                    case Keys.D1:
                    case Keys.NumPad1:
                        btnNum1.PerformClick();
                        break;
                    case Keys.D2:
                    case Keys.NumPad2:
                        btnNum2.PerformClick();
                        break;
                    case Keys.D3:
                    case Keys.NumPad3:
                        btnNum3.PerformClick();
                        break;
                    case Keys.D4:
                    case Keys.NumPad4:
                        btnNum4.PerformClick();
                        break;
                    case Keys.D5:
                    case Keys.NumPad5:
                        btnNum5.PerformClick();
                        break;
                    case Keys.D6:
                    case Keys.NumPad6:
                        btnNum6.PerformClick();
                        break;
                    case Keys.D7:
                    case Keys.NumPad7:
                        btnNum7.PerformClick();
                        break;
                    case Keys.D8:
                    case Keys.NumPad8:
                        btnNum8.PerformClick();
                        break;
                    case Keys.D9:
                    case Keys.NumPad9:
                        btnNum9.PerformClick();
                        break;
                    case Keys.Add:
                    case Keys.Oemplus:
                        btnPlus.PerformClick();
                        break;
                    case Keys.Subtract:
                    case Keys.OemMinus:
                        btnMinus.PerformClick();
                        break;
                    case Keys.Divide:
                        btnDivide.PerformClick();
                        break;
                    case Keys.Multiply:
                        btnMultiply.PerformClick();
                        break;
                    case Keys.Back:
                        btnBackspace.PerformClick();
                        break;
                    case Keys.Oemcomma:
                        btnDecimalPoint.PerformClick();
                        break;
                    case Keys.Enter:
                        btnEquality.PerformClick();
                        break;
                }
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            try
            {
                txtInput.Text = "0";
                operand1 = 0;
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                operand1 = operand2 = 0;
                @operator = null;
                lastSet = LastSet.None;

                txtInput.Text = "0";
                lblStatus.Text = string.Empty;
            }
            catch (Exception ex) {
                CatchException(ex);
            }
        }

        private void btnBackspace_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (lastSet == LastSet.FirstOperand || lastSet == LastSet.SecondOperand)
                {
                    txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
                    if (string.IsNullOrEmpty(txtInput.Text))
                        txtInput.Text = "0";

                    if (lastSet == LastSet.FirstOperand)
                        operand1 = /*operand2 = */double.Parse(txtInput.Text);
                    else if (lastSet == LastSet.SecondOperand)
                        operand2 = double.Parse(txtInput.Text);
                }
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
        }

        private void btnDecimalPoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (lastSet == LastSet.Operator)
                    btnNum0.PerformClick();

                string sep = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                if (!txtInput.Text.Contains(sep))
                    txtInput.Text += sep;
            }
            catch (Exception ex) {
                CatchException(ex);
            }
        }

        private void btnEquality_Click(object sender, EventArgs e)
        {
            if (lastSet != LastSet.None && lastSet != LastSet.FirstOperand)
            {
                txtInput.Text = Calculate().ToString();
                lblStatus.Text = string.Empty;
                lastSet = LastSet.Answer;
            }
        }

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            try
            {
                if (lastSet == LastSet.FirstOperand)
                {
                    operand1 = -operand1;
                    txtInput.Text = operand1.ToString();
                }
                else if (lastSet == LastSet.SecondOperand)
                {
                    operand2 = -operand2;
                    txtInput.Text = operand2.ToString();
                }
                else if (lastSet == LastSet.Operator)
                {
                    // write to status label
                    operand2 = -operand2;
                    txtInput.Text = operand2.ToString();
                }
                else if (lastSet == LastSet.Answer)
                {
                    // write to status label
                    operand1 = -operand1;
                    txtInput.Text = operand1.ToString();
                }
            }
            catch (Exception ex) {
                CatchException(ex);
            }
        }


        enum LastSet { None, FirstOperand, SecondOperand, Operator, Answer }


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
    }
}
