namespace minsvepare
{
    //Klass för rutor på spelplanen
    public class Cell
    {
        private bool Used;
        public bool mine { get; set; }
        public bool flag { get; set; }
        public bool firstClick { get; set; }
        public int nearMines { get; set; }

        public static int usedNum;
        public bool used
        {
            get
            {
                return Used;
            }
            set
            {
                if (value)
                {
                    Used = value;
                    usedNum += 1;
                }
                else
                {
                    Used = value;
                }
            }
        }

        public Cell()
        {
            Used = false;
            mine = false;
            flag = false;
            nearMines = 0;
        }
    }
}
