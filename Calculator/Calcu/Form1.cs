using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
// 6:2x4 = 4.000 ?

namespace Calcu
{
    public partial class Form1 : Form
    {
        string sumstr = "";
        bool ok = false;
        Stack<char> keystack = new Stack<char>(); // luu cac phim da bam
        Queue<char> keyqueue= new Queue<char>();
        bool next = false; // sau khi an '=' va tiep tuc tinh toan voi ket qua do 
        char firstkey;
        bool isButtonClicked = false; // phai nhap cac so hoac '-' dau tien
        List<char> keylist = new List<char>(); // luu cac phep tinh
        decimal[] nums = new decimal [20]; // luu cac so 
        decimal val = 0;
        string temp = "", temp1 = "";
        int i = 0;
        decimal r = 1;
        decimal sum = 0;
        bool comma = false; // dau . chua duoc bat len 
        int count = 0;

        public Form1()
        {
            InitializeComponent();

        }
        // so 0
        private void button18_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 0 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 0 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;



        }
        // so 1
        private void button19_Click(object sender, EventArgs e)
        {
            if(comma == false)
            {
                val = val * 10 + 1 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 1*r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;     
            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }
        // phep +
        private void button16_Click(object sender, EventArgs e)
        {
            if (isButtonClicked == true)
            {

                nums[i] = val; // them so can tinh vao trong mang
                i++;
                val = 0;
                if(ok == true)
                {
                    temp += sumstr;
                    ok = false;
                }
                temp = " + " + temp1 + temp;
                temp1 = "";
                textBox2.Text = temp + temp1;
                keylist.Add('+');
                comma = false;
                r = 1;
            }
            isButtonClicked = false;
        }

        // dau .
        private void button17_Click(object sender, EventArgs e)
        {
            textBox2.Text = '.' + val.ToString() + temp;
            keystack.Push('.');
            comma = true; // dau . da bat
        }

        // dau =
        private void button15_Click(object sender, EventArgs e)
        {
            if(keylist.Count == nums.Length)
            {
                nums[0] = -nums[0];
                for (int j = 0; j < keylist.Count - 1; j++)
                {
                    keylist[j] = keylist[j + 1];
                }
                keylist.RemoveAt(keylist.Count - 1);
            }
            nums[i] = val;
            i++;
            for (int j = 0; j < keylist.Count; j++) // vector chua dau
            {
                if (keylist[j] == '/')
                {
                    for (int t = j; t < keylist.Count - 1; t++)
                    {
                        keylist[t] = keylist[t + 1];
                    }
                    keylist.RemoveAt(keylist.Count - 1); // xoa '/' ra khoi keylist
                    nums[j] /= nums[j + 1];
                    nums[j + 1] = 0;

                    for (int t = j + 1; t < nums.Length - 1; t++)
                    {
                        nums[t] = nums[t + 1];
                        nums[t + 1] = 0;
                    }
                    j--;
                }

                }
                for (int j = 0; j < keylist.Count; j++) // vector chua dau
                {
                    if (keylist[j] == '×')
                {
                    for (int t = j; t < keylist.Count - 1; t++)
                    {
                        keylist[t] = keylist[t + 1];
                    }
                    keylist.RemoveAt(keylist.Count - 1); // xoa '/' ra khoi keylist

                    nums[j] *= nums[j + 1];
                    nums[j + 1] = 0;

                    for (int t = j + 1; t < nums.Length - 1; t++)
                    {
                        nums[t] = nums[t + 1];
                        nums[t + 1] = 0;
                    }
                    j--;
                }
            }
            for(int j = 0; j < keylist.Count; j++)
            {
                if (keylist[j] == '+')
                {
                     nums[j] += nums[j + 1];
                    nums[j+1] = 0;
                    sum += nums[j];
                    count++;
                }

                if (keylist[j] == '-')
                {
                    nums[j] -= nums[j + 1];
                    nums[j + 1] = 0;
                    sum += nums[j];
                    count++;
                }
            }
            if(count == 0) sum = nums[0]; // truong hop chi co phep nhan va phep chia

            int sum1 = Convert.ToInt32(sum); // ep kieu cho truong hop 3:2x2 = 3.0 -> 3
            if(sum == sum1) sum = sum1;
            temp = sum.ToString(); // truong hop sau khi an '=' va muon thuc hien tinh toan tiep 
            if (sum < 0) // truong hop ket qua la so am : 1- -> -1
            {
                sumstr = (-sum).ToString() + '-';
                textBox2.Text = sumstr;
                ok = true;
                temp = temp1 = "";
                for (int j = 0; j < keylist.Count; j++)
                {
                    keylist.RemoveAt(keylist.Count - 1);
                }
            } 
            else textBox2.Text = sum.ToString();

            
            for(int j = 0; j < nums.Length; j++) // gian cac gia tri cua nums = 0 
            {
                nums[j] = 0;
            }
            for(int j = 0; j < keylist.Count; j++)
            {
                keylist.RemoveAt(keylist.Count - 1);
            }
            val = sum;
            sum = 0;
            i = 0;     
            count = 0;
            temp1 = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(isButtonClicked == true)
            {
                nums[i] = val; // them so can tinh vao trong mang
                i++;
                val = 0;
                if (ok == true)
                {
                    temp += sumstr;
                    ok = false;
                }
                temp = " × " + temp1 + temp;
                temp1 = "";
                textBox2.Text = temp + temp1;


                keylist.Add('×');
                comma = false;
                r = 1;
            }
            isButtonClicked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 2 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 2 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;
            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 3 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 3 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(keyqueue.Count == 0)
            {
                
                    nums[i] = val; // them so can tinh vao trong mang
                    i++;
                    val = 0;
                if (ok == true)
                {
                    temp += sumstr;
                    ok = false;
                }
                temp = " - " + temp1 + temp;
                    temp1 = "";
                    textBox2.Text = temp1 + temp;


                    keylist.Add('-');
                    comma = false;
                    r = 1;
            }
            else
            {
                if (isButtonClicked == true)
                {
                    nums[i] = val; // them so can tinh vao trong mang
                    i++;
                    val = 0;
                    if (ok == true)
                    {
                        temp += sumstr;
                        ok = false;
                    }
                    temp = " - " + temp1 + temp;
                    temp1 = "";
                    textBox2.Text = temp + temp1;


                    keylist.Add('-');
                    comma = false;
                    r = 1;
                }
                isButtonClicked = false;
            }

            keyqueue.Enqueue('-');
        }

        

        private void button10_Click(object sender, EventArgs e)
        {
            if(isButtonClicked == true)
            {
                nums[i] = val; // them so can tinh vao trong mang
                i++;
                val = 0;
                if (ok == true)
                {
                    temp += sumstr;
                    ok = false;
                }
                temp = " ÷ " + temp1 + temp;
                temp1 = "";
                textBox2.Text = temp + temp1;

                keylist.Add('/');
                comma = false;
                r = 1;
            }
            isButtonClicked = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 4 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 4 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 5 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 5 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 6 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 6 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 7 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 7 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }



        private void button8_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 8 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 8 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comma == false)
            {
                val = val * 10 + 9 * r;
            }
            else
            {
                r = 0.1m * r;
                val = val + 9 * r;
            }
            temp1 = val.ToString();
            textBox2.Text = temp1 + temp;

            keyqueue.Enqueue('n');
            isButtonClicked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            

                    
            
        }
    }
}
