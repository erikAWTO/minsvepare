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

        //Laddar in inställningar och skapar spelfält.
        public int rows = Settings.Default.Width;
        public int columns = Settings.Default.Height;
        public int mines = Settings.Default.Mines;

        public static int timer = 0;

        public Form1()
        {
            InitializeComponent();

            //this.Size = new Size(rows * 40 + 115, columns * 40 + 130);
        }

        private void Form1_Load(object send, EventArgs e)
        {
            field = new Field(rows, columns, mines);

            btn = new Button[field.cellVector.GetLength(0), field.cellVector.GetLength(1)];

            for (int x = 0; x < field.cellVector.GetLength(0); x++)
            {
                for (int y = 0; y < field.cellVector.GetLength(1); y++)
                {
                    btn[x, y] = new Button();

                    btn[x, y].Font = new Font("Arial", 18);
                    btn[x, y].Left = x * 40 + 50;
                    btn[x, y].Top = y * 40 + 50;
                    btn[x, y].Width = 40;
                    btn[x, y].Height = 40;
                    //btn[x, y].
                    Controls.Add(btn[x, y]);
                    btn[x, y].Click += B_Click;
                }
            }

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
            int y = (btn.Top - 50) / 40;
            int x = (btn.Left - 50) / 40;

            //Kolla rutan
            
            if(x >= 0 && y >= 0)
            field.CheckCell(x, y);
           
        }

        private void btnNewGame2_Click(object sender, EventArgs e)
        {
            NewGame Game = new NewGame();
            this.Hide();
            Game.ShowDialog();
            this.Close();
        }
    }
}
