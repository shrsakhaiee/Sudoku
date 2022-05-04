using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace sudoku
{
    public partial class Form1 : Form
    {
        TextBox[,] cells;
        public Form1()
        {
            InitializeComponent();
            cells = new TextBox[9, 9];

            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    cells[i,j] = new TextBox();
                    cells[i,j].Multiline = true;
                    cells[i,j].TextAlign = HorizontalAlignment.Center;
                    cells[i,j].Font = new Font("tahoma", 20);
                    cells[i, j].BackColor = Color.LightPink;
                    cells[i,j].Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
                    tableLayoutPanel1.Controls.Add(cells[i,j] , i, j);
                    cells[i, j].TextChanged += new System.EventHandler(this.cells_TextChanged);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i < 3 && j < 3
                        ||
                        i > 5 && j > 5
                        ||
                        i < 3 && j > 5 || i > 5 && j < 3
                        ||
                        i > 4 && i < 6 && j > 4 && j < 6
                        ||
                        i > 3 && i < 6 && j > 4 && j < 6
                        ||
                        i > 2 && i < 6 && j > 4 && j < 6
                        ||
                        i > 4 && i < 6 && j > 3 && j < 6
                        ||
                        i > 4 && i < 6 && j > 2 && j < 6
                        ||
                        i > 3 && i < 6 && j > 2 && j < 6
                        ||
                        i > 2 && i < 6 && j > 2 && j < 6)
                    {

                        cells[i, j].BackColor = Color.LightSkyBlue;
                    }
                }
            }
        }

        private void cells_TextChanged(object sender, EventArgs e)
        {
            string t = this.ActiveControl.Text.ToString();
            if (t != "1" && t != "2" && t != "3" && t != "4" && t != "5" &&
                t != "6" && t != "7" && t != "8" && t != "9" && t != string.Empty)
            {
                this.ActiveControl.Text = string.Empty;
                MessageBox.Show("لطفا فقط اعداد 1 تا 9 را وارد کنید\n" , "Enter InCorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.ActiveControl.Text = string.Empty;
            }
        }

        private void btn_new_game_Click(object sender, EventArgs e)
        {
            reset();
            OpenFileDialog s = new OpenFileDialog();
            if(s.ShowDialog()==DialogResult.OK)
            {
                string file_path = s.FileName;
                //MessageBox.Show(file_path);
                StreamReader my_file_reader = new StreamReader(file_path);

                string big_text = my_file_reader.ReadToEnd();
                //MessageBox.Show(big_text);

                char[] my_seperatos = { ' ', '\r' };

                big_text = big_text.Replace("\n", "");

                String[] numbers = big_text.Split(my_seperatos);

                int index = 0;
                for(int i=0;i<9;i++)
                {
                    for(int j = 0; j<9;j++)
                    {
                        if(numbers [index]!="0")
                        {
                            cells[j,i].ReadOnly = true;
                            cells[j,i].Text = numbers[index];

                        }
                        index++;
                    }
                }
            }
        }

        private void reset()
        {
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    cells[j, i].ReadOnly = false;
                    cells[j, i].Text = "";
                }
            }
        }

        private void btn_chek_Click(object sender, EventArgs e)
        {
            int list;
            int[] Row, Column, Square;

            for (int i = 0; i < 9; i++)
            {
                Row = new int[9];
                Column = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].Text != "" && cells[i, j].Text != "")
                    {
                        int cellrow = Convert.ToInt16(cells[j, i].Text);
                        int cellcolumn = Convert.ToInt16(cells[i, j].Text);
                        if (0 < cellrow && cellrow < 10 && !Row.Contains(cellrow) &&
                            0 < cellcolumn && cellcolumn < 10 && !Column.Contains(cellcolumn))
                        {
                            Row[j] = cellrow;
                            Column[i] = cellcolumn;
                            continue;
                        }
                    }
                    MessageBox.Show("دوباره تلاش کن");
                    return;
                }
            }
            for (int m = 1, a = 0, b = 3, c = 0, d = 3; m <= 9; m++, c += 3, d += 3)

            {
                if ((m - 1) % 3 == 0)

                {
                    c = 0;
                    d = 3;
                }
                Square = new int[9];
                list = 0;

                for (int i = a; i < b; i++)

                {
                    for (int j = c; j < d; j++)

                    {
                        int cell = Convert.ToInt16(cells[j, i].Text);

                        if (!Square.Contains(cell))

                        {
                            Square[list++] = cell;

                            continue;
                        }

                        MessageBox.Show("دوباره تلاش کن");

                        return;

                    }

                }

                if (m % 3 == 0)

                {
                    a += 3;

                    b += 3;
                }
            }

            MessageBox.Show("آفرین درست حل کردی");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
