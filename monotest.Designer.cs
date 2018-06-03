namespace mono_test
{
    partial class monotest
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
            // 
            this.close = new System.Windows.Forms.Button();
            
            this.SuspendLayout();
            
            this.close.Location = new System.Drawing.Point(50, 50);
            this.close.Name = "Close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 0;
            this.close.Text = "Exit";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.onExit_Click);
            
            // 
            this.ClientSize = new System.Drawing.Size(400, 400);
            
            this.Controls.Add(this.close);
            
            this.Name = "App";
            this.Text = "Mono Test App";
            
            this.Load += new System.EventHandler(this.onForm_Load);            
            
            //
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        
        private System.Windows.Forms.Button close;
    }
}