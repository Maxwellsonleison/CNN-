using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNN_demo
{
    public partial class Form1 : Form
    {
        CNN mycnn = new CNN();
        CNN.Layer myly = new CNN.Layer(6, 6);
        double[,] kernel = new double[3, 3] { { 1,0,-1},{ 1,0,-1},{1,0,-1 } };
        public Form1()
        {
            InitializeComponent();
            myly.Elements = new double[6,6]{ {3,0,1,2,7,4},{ 1,5,8,9,3,1},
                {2,7,2,5,1,3 },{0,1,3,1,7,8},
               {4,2,1,6,2,8},{ 2,4,5,2,3,9} };
        }

        private void button1_Click(object sender, EventArgs e)
        {
           var r=  mycnn.Pooling(3, myly.Elements);
            string s = "";
            for(int c=0; c< r.GetLength(0); c++)
            {
                for(int j=0;j<r.GetLength(0);j++)
                {
                    s+=(r[c, j].ToString() + ",");
                }
                s += Environment.NewLine;
            }
            
            textBox1.AppendText("池化结果：" + Environment.NewLine);
            textBox1.AppendText(string.Format("{0}", s));
            string t = "";
            for (int c = 0; c < r.GetLength(0); c++)
            {
                for (int j = 0; j < r.GetLength(0); j++)
                {
                    var m=  mycnn.Activation(r[c,j],ActiveFunType.Sigmod);
                    t += (m.ToString() + ",");
                }
                t += Environment.NewLine;
            }
            textBox1.AppendText("激活输出：" + Environment.NewLine);
            textBox1.AppendText(string.Format("{0}", t));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var a = myly.Elements;
            string s = "";
            for (int c = 0; c < a.GetLength(0); c++)
            {
                for (int j = 0; j < a.GetLength(0); j++)
                {
                    s += (a[c, j].ToString() + ",");
                }
                s += Environment.NewLine;
            }
            textBox1.AppendText("原始矩阵：" + Environment.NewLine);
            textBox1.AppendText(string.Format("{0}", s));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var cul= mycnn.Convolution(myly,kernel,1);
            string s = "";
            for (int c = 0; c < cul.GetLength(0); c++)
            {
                for (int j = 0; j < cul.GetLength(0); j++)
                {
                    s += (cul[c, j].ToString() + ",");
                }
                s += Environment.NewLine;
            }

            textBox1.AppendText("卷积结果：" + Environment.NewLine);
            textBox1.AppendText(string.Format("{0}", s));
        }
    }
}
