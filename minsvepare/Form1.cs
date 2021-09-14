using minsvepare.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace minsvepare
{
    public partial class Form1 : Form
    {

        //Laddar in inställningar.
        public int width = Settings.Default.Width;
        public int height = Settings.Default.Height;
        public int mines = Settings.Default.Mines;

        public static int score = 0;

        public static Button[,] btn;

        public Random rand = new Random();

        public Field field;


        public Form1()
        {
            InitializeComponent();

            //Gör fönstret propotionellt mot rader och kollumner.
            this.Size = new Size(width * 40 + 115, height * 40 + 130);
        }

        private void Form1_Load(object send, EventArgs e)
        {
            tmrScore.Enabled = true;

            field = new Field(width, height, mines);

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
                    Controls.Add(btn[x, y]);

                    //Eventhanterare för klick på knapparna
                    btn[x, y].MouseUp += (s, args) =>
                    {
                        Button btn = (Button)s;

                        //Ta reda på vilken rad och kollumn som har tryckts ned. Dela med knappstorleken för att få reda på vilken som är nedtryckt. 
                        //X = Rad, Y = Kollumn
                        int xPos = (btn.Left - 50) / 40;
                        int yPos = (btn.Top - 50) / 40;

                        if (args.Button == MouseButtons.Left)
                        {
                            if (field.gameOver)
                            {
                                return;
                            }

                            if (field.cellVector[xPos, yPos].flag)
                            {
                                return;
                            }

                            field.CheckCell(xPos, yPos);

                        }

                        if (args.Button == MouseButtons.Right)
                        {
                            if (field.gameOver)
                            {
                                return;
                            }

                            if (field.cellVector[xPos, yPos].used)
                            {
                                return;
                            }

                            field.FlagCell(xPos, yPos);

                        }
                    };

                }
            }
        }

        //Eventhanterare för klick på "New Game" Knapp
        private void btnNewGame2_Click(object sender, EventArgs e)
        {
            NewGame Game = new NewGame();

            tmrScore.Enabled = false;

            this.Hide();
            Game.ShowDialog();
            this.Close();
        }

        private void tmrScore_Tick(object sender, EventArgs e)
        {
            score++;

            label1.Text = score.ToString();
        }
    }
}
