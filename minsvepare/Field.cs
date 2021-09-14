using System;
using System.Windows.Forms;

namespace minsvepare
{   //Klass för spelplanen
    public class Field
    {
        private int width;
        private int height;
        private int mines;

        private int firstClickX;
        private int firstClickY;

        private bool firstClick;

        public bool gameOver;

        public Cell[,] cellVector;

        // ***************
        // * Konstruktor *
        // ***************
        public Field(int width, int height, int mines)
        {
            //För över argumenten till privata variablar i klassen.
            this.width = width;
            this.height = height;
            this.mines = mines;

            //Nollställer low-score räknaren.
            Form1.score = 0;

            //Sätter gameOver = false varje gång en ny spelplan skapas.
            gameOver = false;

            //Nollställer räknare för antal visade rutor.
            Cell.usedNum = 0;

            //Man har ännu inte klickat på någon ruta.
            firstClick = true;

            //Sätter första klicket positionen init till något som inte finns på spelplanen
            firstClickX = width + 1;
            firstClickY = height + 1;

            //Skapar en vektor av rutor som spelplan.
            cellVector = new Cell[width, height];

            //Befolkar vektorn.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cellVector[i, j] = new Cell();
                }
            }
        }


        // **************************
        // * Funktioner för  minor. *
        // **************************
        //Skapa minor
        private void CreateMines(int mines)
        {
            //Slumpa x och y-värden, så länge placerade minor < antal minor, sätt in minor.
            Random rand = new Random(Environment.TickCount);

            int placedMines = 0;

            while (placedMines < mines)
            {
                int x = rand.Next(width);
                int y = rand.Next(height);

                //Om slump-positionen är samma som första klickade positionen, slumpa ny position.
                if (x == firstClickX && y == firstClickY)
                {
                    continue;
                }

                if (cellVector[x, y].mine)
                {
                    continue;
                }
                else
                {
                    cellVector[x, y].mine = true;
                    placedMines++;
                }
            }

            //Gå igenom vektorn och räkna närliggande minor för varje ruta.          Kunde lika gärna inkrementrera alla rutor runt om minan :DDD
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    CountNearMines(i, j);
                }
            }
        }

        //Räknar alla minor runt om en ruta (förutom minor).
        private void CountNearMines(int x, int y)
        {
            if (cellVector[x, y].mine)
            {
                cellVector[x, y].nearMines = -1;
                return;
            }

            int total = 0;

            //Inkrementera alla rutor runt om minan
            for (int xoff = -1; xoff <= 1; xoff++)
            {
                for (int yoff = -1; yoff <= 1; yoff++)
                {
                    int i = x + xoff;
                    int j = y + yoff;

                    if (i > -1 && i < width && j > -1 && j < height)
                    {

                        if (cellVector[i, j].mine)
                        {
                            total++;
                        }
                    }
                }
            }

            cellVector[x, y].nearMines = total;
        }


        //*************************************************
        //* Ritar spelplanen, ritar endast använda rutor. *
        //*************************************************
        public void DrawField()
        {
            //Gå igenom CellVektorn och kolla om den är använd. Om den är använd skall det värdet skrivas till knappen.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cellVector[i, j].used)
                    {
                        Form1.btn[i, j].Text = "";

                        if (cellVector[i, j].nearMines == 0)
                        {
                            Form1.btn[i, j].BackColor = System.Drawing.Color.Green;
                        }

                        if (cellVector[i, j].nearMines > 0)
                        {
                            Form1.btn[i, j].Text = Convert.ToString(cellVector[i, j].nearMines);
                            Form1.btn[i, j].BackColor = System.Drawing.Color.Yellow;
                        }

                        if (cellVector[i, j].mine)
                        {
                            Form1.btn[i, j].Text = "💣";

                            if (!gameOver)
                            {
                                Form1.btn[i, j].BackColor = System.Drawing.Color.LightGreen;
                            }
                            else
                            {
                                Form1.btn[i, j].BackColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
            }

            //Om alla rutor är visade (förutom minor), har spelaren vunnit.
            if ((width * height - mines) == Cell.usedNum && !gameOver)
            {
                onWin();
            }
        }


        // *****************************
        // * Kolla rutan funktionerna. *
        // *****************************

        //Kollar vad rutan innehåller och kallar därefter på rätt metod.
        public void CheckCell(int x, int y)
        {
            if (cellVector[x, y].used)
            {
                return;
            }

            if (firstClick)
            {
                Form1.tmrScore.Start();

                //Lagrar postionen för första klicket.
                firstClickX = x;
                firstClickY = y;

                CreateMines(mines);
            }

            if (cellVector[x, y].nearMines == 0)
            {
                FloodFill(x, y);
            }
            else if (cellVector[x, y].nearMines > 0)
            {
                cellVector[x, y].used = true;
                DrawField();
            }
            else if (cellVector[x, y].mine)
            {
                GameOver(x, y);
            }

            firstClick = false;
        }

        public void FlagCell(int x, int y)
        {
            if (cellVector[x, y].flag)
            {
                cellVector[x, y].flag = false;
                Form1.btn[x, y].Text = "";

            }
            else
            {
                cellVector[x, y].flag = true;
                Form1.btn[x, y].Text = "⚑";
            }
        }

        //Markera rutan och alla omgivande som använd (ej minor), om tom ruta fortsätt då med rekursiva anrop.
        //Brytvillkor alla omgivande rutor är markerade.
        public void FloodFill(int x, int y)
        {
            for (int xoff = -1; xoff <= 1; xoff++)
            {
                for (int yoff = -1; yoff <= 1; yoff++)
                {
                    int i = x + xoff;
                    int j = y + yoff;

                    if (i > -1 && i < width && j > -1 && j < height)
                    {
                        var nearCell = cellVector[i, j];

                        if (!nearCell.mine && !nearCell.used)
                        {
                            nearCell.used = true;
                            DrawField();

                            if (nearCell.nearMines == 0)
                            {
                                FloodFill(i, j);
                            }
                        }
                    }
                }
            }
        }

        //Gick på mina.
        private void GameOver(int x, int y)
        {
            gameOver = true;

            //Sätt alla minor som använda och visa dem.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cellVector[i, j].mine)
                    {
                        cellVector[i, j].used = true;
                        DrawField();
                    }
                }
            }
            MessageBox.Show("Game Over!");
        }

        //Spelaren har vunnit, visa alla rutor.
        private void onWin()
        {
            Form1.tmrScore.Stop();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cellVector[i, j].mine)
                    {
                        cellVector[i, j].used = true;
                        DrawField();
                    }
                }
            }
            MessageBox.Show("Win! \n" + "Score: " + Form1.score);
        }
    }
}
