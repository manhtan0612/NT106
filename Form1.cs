using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        int x = -1;
        int y = -1;
        int result = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            y = 3;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            x = 3;
        }

        public void button15_Click(object sender, EventArgs e)
        {
            x = 4;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
           if(x != -1)
            {
                result = x*10 + result;
                x = -1;
            }
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            x = 8;        }

        private void button1_Click(object sender, EventArgs e)
        {
            x = 7;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            x = 9;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            x = 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            x = 6;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            x = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            x = 2;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            y = 0;   
        }

        private void button9_Click(object sender, EventArgs e)
        {
            y = 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            y = 2;
        }
    }
}
