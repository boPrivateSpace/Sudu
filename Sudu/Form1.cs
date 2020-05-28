using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        TextBox[,] tbArray = new TextBox[9, 9];

        int error = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            // 
            // textBox
            // 
            int i = 0;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    i++;
                    TextBox textBox = new TextBox();
                    tbArray[x, y] = textBox;
                    tbArray[x, y].Name = x +"," + y;
                    tbArray[x, y].Text = "";
                    tbArray[x, y].Location = new Point(12 + x * 30 + x * 3, 12 + y * 30 + y * 3);
                    tbArray[x, y].Size = new Size(30, 30);
                    tbArray[x, y].Multiline = true;
                    tbArray[x, y].MaxLength = 1;
                    tbArray[x, y].Font = new Font("宋体", 16F);
                    tbArray[x, y].TextAlign = HorizontalAlignment.Center;
                    tbArray[x, y].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
                    tbArray[x, y].Leave += new System.EventHandler(this.textBox_Leave);
                    tbArray[x, y].ReadOnly = true;
                    tbArray[x, y].BackColor = System.Drawing.SystemColors.Info;
                    if (x >= 3 && x < 6&&y >= 3 && y < 6 || x >= 0 && x < 3 && y >= 0 && y < 3 || x >= 6 && x < 9 && y >= 0 && y < 3 || x >= 0 && x < 3 && y >= 6 && y < 9 || x >= 6 && x < 9 && y >= 6 && y < 9)
                    {
                        tbArray[x, y].BackColor = System.Drawing.SystemColors.Window;
                    }
                    //将textBox添加到页面上
                    this.Controls.Add(tbArray[x, y]);
                }
            }
            printArray(SuDu.getSuDu());
        }

        /// <summary>
        /// 输入时间校验输入值为1-9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar > '0' && e.KeyChar <= '9') || e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.comboBox1.Text))
            {
                MessageBox.Show("请选择难度");
            }
            else
            {
                clear();
                error = 0;
                this.label2.Text = "错误次数:" + error;   
                printArray(SuDu.getSuDu());
            }
        }

        /// <summary>
        /// 生成数独选择难度
        /// </summary>
        /// <param name="a"></param>
        private void printArray(int[][] a)
        {
            int length = 4;
            switch (this.comboBox1.Text)
            {
                case "简单":
                    length = 4;
                    break;
                case "中等":
                    length = 5;
                    break;
                case "困难":
                    length = 6;
                    break;
            }
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int randomNum = random.Next(9);
                    if (randomNum > length)
                    {
                        tbArray[i, j].Text = a[i][j].ToString();
                    }
                    else
                    {
                        tbArray[i, j].ReadOnly = false;
                    }

                }
            }
        }

        /// <summary>
        /// 清空textbox
        /// </summary>
        public void clear()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    tbArray[x, y].Text = "";
                    tbArray[x, y].ReadOnly = true;
                    tbArray[x, y].ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void but2_Click(object sender, EventArgs e)
        {
            if (rowCheck() && cellCheck() && palaceCheck())
            {
                MessageBox.Show("恭喜过关");
                clear();
                error = 0;
                this.label2.Text = "错误次数:" + error;
                printArray(SuDu.getSuDu());
            }
        }

        /// <summary>
        /// 校验行
        /// </summary>
        public bool rowCheck()
        {
            for (int y = 0; y < 9; y++)
            {
                List<int> lines = new List<int>();
                for (int x = 0; x < 9; x++)
                {
                    string value = tbArray[x, y].Text;
                    if (!String.IsNullOrEmpty(value))
                    {
                        lines.Add(Convert.ToInt32(value));
                    }
                    else
                    {
                        MessageBox.Show("请确认是否填写完毕");
                        return false;
                    }
                }
                if (lines.Distinct().Count() < 9)
                {
                    MessageBox.Show("验证失败,请检查完重新提交");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 校验列
        /// </summary>
        public bool cellCheck()
        {
            for (int y = 0; y < 9; y++)
            {
                List<int> lines = new List<int>();
                for (int x = 0; x < 9; x++)
                {
                    string value = tbArray[y, x].Text;
                    if (!string.IsNullOrEmpty(value))
                    {
                        lines.Add(Convert.ToInt32(value));
                    }
                    else
                    {
                        MessageBox.Show("请确认是否填写完毕");
                        return false;
                    }
                }
                if (lines.Distinct().Count() < 9)
                {
                    MessageBox.Show("验证失败,请检查完重新提交");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 校验九宫格每一宫是否有重复
        /// </summary>
        public bool palaceCheck()
        {
            int[,] newArrary = block();
            for (int y = 0; y < 9; y++)
            {
                List<int> lines = new List<int>();
                for (int x = 0; x < 9; x++)
                {
                    lines.Add(newArrary[x, y]);
                }
                if (lines.Distinct().Count() < 9)
                {
                    MessageBox.Show("验证失败,请检查完重新提交");
                    return false;
                }
            }
            return true;
        }

        //将每块的数字保存至一个二维数组
        public int[,] block()
        {
            int[,] b = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    //将数独从左至右从上至下分为9块，求该单元格属于第几块，将该块数字保存至b第几行
                    int rowOfB = i / 3 * 3 + j / 3;
                    //每块有9个数字，求该数字属于第几个，保存至b第几列
                    int columnOfB = i % 3 * 3 + j % 3;
                    b[rowOfB, columnOfB] = Convert.ToInt32(tbArray[i, j].Text);
                }

            return b;
        }

        private void label1_Click(object sender, EventArgs e)
        {
      
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }


        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string[] name = textBox.Name.Split(',');
            int x =Convert.ToInt32(name[0]);
            int y = Convert.ToInt32(name[1]);
            int plate=SuDu.arrray1[x][y];
            if (!string.IsNullOrEmpty(textBox.Text) && plate != Convert.ToInt32(textBox.Text))
            {
                error++;
                textBox.ForeColor = System.Drawing.Color.Red;
                this.label2.Text = "错误次数:" + error;
            }
            else
            {
                textBox.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}
