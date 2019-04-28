using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace niires
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<int> RedY = new List<int>();
        List<int> BlueX = new List<int>();
        List<int> BlueY = new List<int>();
        List<int> RedX = new List<int>();



        int counter = 1;
        int num = 0;

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
         //   Draw myClass = new Draw();

            if (e.Button == MouseButtons.Right)
            {
                 Graphics g = Graphics.FromHwnd(this.Handle);
                 Pen redBrush = new Pen(new SolidBrush(Color.Blue), 2f);
                 g.DrawRectangle(redBrush, e.X - 1, e.Y - 1, 2, 2);

                 int x, y;
                 x = e.Location.X;
                 y = e.Location.Y;
                 BlueX.Add(x);
                 BlueY.Add(y);
                 textBox1.Text =  x + ", " + y ;
                 counter++;

                
            }
            else
            {
                Graphics g = Graphics.FromHwnd(this.Handle);
                Pen redBrush = new Pen(new SolidBrush(Color.Red), 2f);
                g.DrawRectangle(redBrush, e.X - 1, e.Y - 1, 2, 2);

                int x, y;
                x = e.Location.X;
                y = e.Location.Y;
               RedX.Add(x);
                RedY.Add(y);
                textBox1.Text = x + ", " + y;
                counter++;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int q, w;
            q = e.Location.X;
            w = e.Location.Y;
            textBox1.Text =  q + "; " + w + "\n";
        }

        private void button1_Click(object sender, EventArgs e)          //   кнопка
        {
            if (RedX.Count != BlueX.Count)
            {
                MessageBox.Show("Red = " + RedX.Count + "\nBlue = " + BlueX.Count, "Not equal number of dots!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Console.WriteLine("Not equal number of dots");
                return;
            }

            try
            {
                Console.WriteLine(num + 1);
                Console.Write("RedX " + RedX[num]);
                Console.WriteLine(" RedY " + RedY[num]);
                Console.Write("BlueX " + BlueX[num]);
                Console.WriteLine(" BlueY " + BlueY[num]);
                num++;
            }
            catch
            {
                Console.WriteLine("No more dots");
            }

            var result = ShowAllCombinations(BlueX);
            var result2 = ShowAllCombinations(BlueY);


           

            for (int c = 0; c != (result.Count); c++)
            {
                List<int> New_BlueX = result[c];
                List<int> New_BlueY = result2[c];
                double temp = 0;

                for (int q = RedX.Count - 1 ; q != -1; q--)
                {
                    temp += Math.Sqrt((Math.Pow((New_BlueX[q] - RedX[q]), 2) + Math.Pow((New_BlueY[q] - RedY[q]), 2)));
                    Console.WriteLine(temp);

                   // Paint(RedX[q], RedY[q], New_BlueX[q], New_BlueY[q]);   РИСУЕТ ВООБЩЕ ВСЁ)
                }

                if (StatClass.min_result == 0)
                {
                    StatClass.min_result = temp;
                    StatClass.Min_BlueX = New_BlueX;
                    StatClass.Min_BlueY = New_BlueY;
                }
                else if (StatClass.min_result >= temp)  
                {
                    StatClass.min_result = temp;
                    StatClass.Min_BlueX = New_BlueX;
                    StatClass.Min_BlueY = New_BlueY;
                }



            }
           for (int q = 0; q != RedX.Count; q++)
            {
                Paint(RedX[q], RedY[q], StatClass.Min_BlueX[q], StatClass.Min_BlueY[q]);
            }


        }

        public static List<List<T>> ShowAllCombinations<T>(IList<T> arr, List<List<T>> list = null, List<T> current = null)
        {
            {
                if (list == null) list = new List<List<T>>();
                if (current == null) current = new List<T>();
                if (arr.Count == 0) 
                {
                    list.Add(current);
                    return list;
                }
                for (int i = 0; i < arr.Count; i++) 
                {
                    List<T> lst = new List<T>(arr);
                    lst.RemoveAt(i);
                    var newlst = new List<T>(current);
                    newlst.Add(arr[i]);
                    ShowAllCombinations(lst, list, newlst);
                }
                return list;
            }

        }





       
         

        private void Paint(int RedX, int RedY, int BlueX, int BlueY)
        {

          //  int i;
            var g = CreateGraphics();
           g.DrawLine(Pens.Black, RedX, RedY, BlueX, BlueY);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            Color col = Color.WhiteSmoke;
            g.Clear(col);
            StatClass.Min_BlueX = null;
            StatClass.Min_BlueY = null;
            StatClass.min_result = 0;
         RedY.Clear();
            RedX.Clear();
            BlueX.Clear();
            BlueY.Clear();
        }
    }
}
