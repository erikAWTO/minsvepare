
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
            this.btnNewGame2 = new System.Windows.Forms.Button();
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
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.btnNewGame2);
            this.Name = "Form1";
            this.Text = "Minsvep by Erik";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame2;
    }
}

