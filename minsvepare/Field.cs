using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minsvepare
{   //Klass för spelplanen
    public class Field
    {
        private int rows;
        private int cols;
        private int mines;

        private int firstClickRow;
        private int firstClickCol;

        private bool firstClick;

        public bool gameOver; 

        public Cell[,] cellVector;

        // ***************
        // * Konstruktor *
        // ***************
        public Field(int x, int y, int mines)
        {
            //För över argumenten till privata variablar i klassen.
            rows = x;
            cols = y;
            this.mines = mines;

            //Nollställer low-score räknaren.
            Form1.timer = 0;

            //Sätter gameOver = false varje gång en ny spelplan skapas.
            gameOver = false;

            //Nollställer räknare för antal visade rutor.
            Cell.usedNum = 0;

            //Man har ännu inte klickat på någon ruta.
            firstClick = true;

            //Sätter första klicket positionen init till något som inte finns på spelplanen
            firstClickRow = rows + 1;
            firstClickCol = cols + 1;

            //Skapar en vektor av rutor som spelplan.
            cellVector = new Cell[x, y];

            //Befolkar vektorn.
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    cellVector[i, j] = new Cell();
                }
            }
            //Sätter ut minorna.
            //CreateMines(mines);

            Console.WriteLine("första click " + firstClick);
        }

        #region Privata metoder för minor
        // ***********************************
        // * Funktioner för att skapa minor. *
        // ***********************************
        //Skapa minor
        private void CreateMines(int mines)
        {
            //Slumpa x och y-värden, så länge placerade minor < antal minor, sätt in minor.
            Random rand = new Random(Environment.TickCount);

            int placedMines = 0;

            while(placedMines < mines)
            {
                int x = rand.Next(rows);
                int y = rand.Next(cols);

                //Om slump-positionen är samma som första klickade positionen, slumpa ny position.
                if(x == firstClickRow && y == firstClickCol)
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

            //Gå igenom vektorn och räkna närliggande minor för varje ruta
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    CountNearMines(i, j);

                    //Console.WriteLine(" = " + cellVector[i, j].nearMines);
                    //Console.Write(i + "," + j);
                    
                }
            }
        }

        //Räknar alla minor runt om en ruta (förutom minor).
        private void CountNearMines(int x, int y)
        {
            if(cellVector[x,y].mine)
            {
                cellVector[x, y].nearMines = -1;
                return;
            }

            int total = 0;

            for(int xoff = -1; xoff <= 1; xoff++)
            {
                for (int yoff = -1; yoff <= 1; yoff++)
                {
                    int i = x + xoff;
                    int j = y + yoff;


                    if (i > -1 && i < rows && j > -1 && j < cols)
                    {

                        if (cellVector[i, j].mine)
                        {
                            total++;
                        }
                    }
                }
            }
            cellVector[x,y].nearMines = total;
        }
        #endregion
        
        //*************************************************
        //* Ritar spelplanen, ritar endast använda rutor. *
        //*************************************************
        public void DrawField()
        {
            //Gå igenom CellVektorn och kolla om den är använd. Om den är använd skall det värdet skrivas till knappen.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (cellVector[i, j].used)
                    {
                        Form1.btn[i, j].Text = null;

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
                       
                        //Console.WriteLine("Krav: " + (rows * cols - mines));
                        //Console.WriteLine("Använda: " + Cell.usedNum);
                    }
                }
            }

            //Om alla rutor är visade (förutom minor), har spelaren vunnit.
            if ((rows * cols - mines) == Cell.usedNum && !gameOver)
            {
                Won();
            }
        }

        #region Kolla rutan
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
                //Lagrar postionen för första klicket. För att inte hamna på mina.
                firstClickRow = x;
                firstClickCol = y;

                //Skapar minor
                CreateMines(mines);
            }

            if (cellVector[x, y].nearMines == 0)
            {
                FloodFill(x,y);
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

            /*Console.WriteLine("första click " + firstClick);
            Console.WriteLine("x: " + firstClickRow);
            Console.WriteLine("y: " + firstClickCol);*/
        }

        public void FlagCell(int x, int y)
        {
            if(cellVector[x, y].flag)
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
       
        public void FloodFill(int x, int y)
        {
            //Markera rutan och alla omgivande som använd (ej minor), om tom ruta fortsätt då med rekursiva anrop.
            //Brytvillkor alla omgivande rutor är markerade.
            for (int xoff = -1; xoff <= 1; xoff++)
            {
                for (int yoff = -1; yoff <= 1; yoff++)
                {
                    int i = x + xoff;
                    int j = y + yoff;

                    if (i > -1 && i < rows && j > -1 && j < cols)
                    {
                        var nearCell = cellVector[i, j];

                        if (!nearCell.mine && !nearCell.used)
                        {
                            nearCell.used = true;
                            DrawField();

                            if(nearCell.nearMines == 0)
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
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                   if(cellVector[i,j].mine)
                   {
                        cellVector[i, j].used = true;
                        DrawField();
                   }
                }
            }
            MessageBox.Show("Game Over!");
        }

        //Spelaren har vunnit, visa alla rutor.
        private void Won()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (cellVector[i, j].mine)
                    {
                        cellVector[i, j].used = true;
                        DrawField();
                    }
                }
            }
            MessageBox.Show("Win!");
        }

        //Rutan har nummer 
        /*private string IsNumber(int x, int y)
        {
            //Markera rutan som använd.
            return;
           
        }*/
        #endregion

       
    }
}
