
namespace FruitFightLauncher
{
    partial class FruitFightLauncher
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
            this.statusText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statusText
            // 
            this.statusText.BackColor = System.Drawing.Color.Transparent;
            this.statusText.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusText.ForeColor = System.Drawing.Color.White;
            this.statusText.Location = new System.Drawing.Point(12, 184);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(660, 38);
            this.statusText.TabIndex = 0;
            this.statusText.Text = "Starting";
            this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FruitFightLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FruitFightLauncher.Properties.Resources.BackgroundImage;
            this.ClientSize = new System.Drawing.Size(684, 231);
            this.Controls.Add(this.statusText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FruitFightLauncher";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Fruit Fight Launcher";
            this.Load += new System.EventHandler(this.FruitFightLauncherLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label statusText;
    }
}

