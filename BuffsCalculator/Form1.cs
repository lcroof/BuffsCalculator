using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BuffsCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calculate calculate = new Calculate();
            this.textBox1.Text = "gq82HBPvhapDLdtc";
            calculate.FightCode = this.textBox1.Text;
            calculate.FightID = Convert.ToDecimal(this.textBox2.Text);
            calculate.GetJsonString();
        }
    }
}
