using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorByInjection
{
    public partial class Form1 : Form
    {
        Operator operate = null;
        public Form1(Operator operate)
        {
            InitializeComponent();
            this.operate = operate;
        }
        public string GetSelectedOperator()
        {
            if (radioButton1.Checked)
            {
                return "Plus";
            }
            else if (radioButton2.Checked)
            {
                return "Minus";
            }
            else if (radioButton3.Checked)
            {
                return "Multi";
            }
            else
            {
                return "Divide";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int Num1 = int.Parse(textBox1.Text);
            int Num2 = int.Parse(textBox2.Text);

            anserLab.Text = operate.Caculate(Num1, Num2).ToString();
        }
    }
}
