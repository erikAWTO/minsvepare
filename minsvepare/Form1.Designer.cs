
namespace minsvepare
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnNewGame2 = new System.Windows.Forms.Button();
            this.timerScore = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnNewGame2
            // 
            this.btnNewGame2.Location = new System.Drawing.Point(13, 13);
            this.btnNewGame2.Name = "btnNewGame2";
            this.btnNewGame2.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame2.TabIndex = 0;
            this.btnNewGame2.Text = "New Game";
            this.btnNewGame2.UseVisualStyleBackColor = true;
            this.btnNewGame2.Click += new System.EventHandler(this.btnNewGame2_Click);
            // 
            // timerScore
            // 
            this.timerScore.Interval = 1000;
            this.timerScore.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(171, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnNewGame2);
            this.Name = "Form1";
            this.Activated += new System.EventHandler(this.OnEnter);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame2;
        public System.Windows.Forms.Timer timerScore;
        private System.Windows.Forms.TextBox textBox1;
    }
}

