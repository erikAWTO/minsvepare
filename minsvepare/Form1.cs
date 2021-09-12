using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using minsvepare.Properties;

namespace minsvepare
{
    public partial class Form1 : Form
    {

        public static Button[,] btn;

        public Random rand = new Random();

        public Field field;

        public static Label lbl;

        public static int timer = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object send, EventArgs e)
        {
            //Laddar in inställningar och skapar spelfält.
            int rows = Settings.Default.Width;
            int collums = Settings.Default.Height;
            int mines = Settings.Default.Mines;

            field = new Field(rows, collums, mines);

            btn = new Button[field.cellVector.GetLength(0), field.cellVector.GetLength(1)];

            for (int x = 0; x < field.cellVector.GetLength(0); x++)
            {
                for (int y = 0; y < field.cellVector.GetLength(1); y++)
                {
                    btn[x, y] = new Button();

                    btn[x, y].Left = x * 40 + 80;
                    btn[x, y].Top = y * 40 + 80;
                    btn[x, y].Width = 40;
                    btn[x, y].Height = 40;
                    Controls.Add(btn[x, y]);
                    btn[x, y].Click += B_Click;
                }
            }

                lbl = new Label();
                lbl.Top = 40;
                lbl.Left = 60;
                lbl.Width = 40;
                lbl.Height = 40;

        }


        //Händelsehanterare för knapptryckningar
        public void B_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            //Om spelet är över slutar vi ta input.
            if(field.gameOver)
            {
                return;
            }

            //Ta reda på vilken rad och kollumn som har tryckts ned. Dela med knappstorleken för att få reda på vilken som är nedtryckt. 
            //X = Rad, Y = Kollumn
            int y = (btn.Top - 80) / 40;
            int x = (btn.Left - 80) / 40;

            //Kolla rutan
            
            if(x >= 0 && y >= 0)
            field.CheckCell(x, y);
           
        }

        public void OnEnter(object sender, EventArgs e)
        {
            
        }

        private void btnNewGame2_Click(object sender, EventArgs e)
        {
            NewGame Game = new NewGame();
            this.Hide();
            Game.ShowDialog();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            lbl.Text = timer.ToString();

            Console.WriteLine(timer);
        }

        public static void updateLabel(string text)
        {
            
        }
    }
}
