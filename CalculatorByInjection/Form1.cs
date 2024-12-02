using IOCServiceCollection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        public ILogger<Form1> Logger { get; set; }
        Operator operate = null;
        //public Form1()
        //{
        //    InitializeComponent();
        //}

        public Form1(ILogger<Form1> logger)
        {
            this.InitializeComponent();
            this.Logger = logger;
            this.Logger.LogDebug("建構元有被呼叫");
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

            operate = Program.provider.GetService<Operator>();
            anserLab.Text = operate.Caculate(Num1, Num2).ToString();

            // HW: 為什麼有注入了11個東西，但 Logger 是null
            this.Logger.LogDebug("Caculate");
        }
    }
}
