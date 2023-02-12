
namespace form
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
            this.galaga1 = new gg.galaga();
            this.SuspendLayout();
            // 
            // galaga1
            // 
            this.galaga1.EnemyBulletColor = System.Drawing.Color.Yellow;
            this.galaga1.EnemyColor = System.Drawing.Color.Green;
            this.galaga1.EnemyCount = 30;
            this.galaga1.Location = new System.Drawing.Point(321, 89);
            this.galaga1.Name = "galaga1";
            this.galaga1.PlayerBulletColor = System.Drawing.Color.White;
            this.galaga1.PlayerColor = System.Drawing.Color.Red;
            this.galaga1.Size = new System.Drawing.Size(354, 531);
            this.galaga1.SpaceColor = System.Drawing.Color.Black;
            this.galaga1.TabIndex = 0;
            this.galaga1.Text = "galaga1";
            this.galaga1.Click += new System.EventHandler(this.galaga1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 643);
            this.Controls.Add(this.galaga1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private gg.galaga galaga1;
    }
}

