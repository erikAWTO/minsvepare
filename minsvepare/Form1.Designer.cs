
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
            tmrScore = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnNewGame2
            // 
            this.btnNewGame2.Location = new System.Drawing.Point(50, 12);
            this.btnNewGame2.Name = "btnNewGame2";
            this.btnNewGame2.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame2.TabIndex = 0;
            this.btnNewGame2.Text = "New Game";
            this.btnNewGame2.UseVisualStyleBackColor = true;
            this.btnNewGame2.Click += new System.EventHandler(this.btnNewGame2_Click);
            // 
            // tmrScore
            // 
            tmrScore.Interval = 1000;
            tmrScore.Tick += new System.EventHandler(this.tmrScore_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(240, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "0";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNewGame2);
            this.Name = "Form1";
            this.Text = "Minsvep by Erik";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame2;
        public System.Windows.Forms.Label label1;
        public static System.Windows.Forms.Timer tmrScore;
    }
}

