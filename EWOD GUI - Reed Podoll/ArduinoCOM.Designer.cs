namespace EWOD_GUI___Reed_Podoll
{
    partial class ArduinoCOM
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.uxCOM = new System.Windows.Forms.TextBox();
            this.uxEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(138, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Enter Arduino COM #: ";
            // 
            // uxCOM
            // 
            this.uxCOM.Location = new System.Drawing.Point(156, 33);
            this.uxCOM.Name = "uxCOM";
            this.uxCOM.Size = new System.Drawing.Size(100, 22);
            this.uxCOM.TabIndex = 1;
            // 
            // uxEnter
            // 
            this.uxEnter.Location = new System.Drawing.Point(262, 32);
            this.uxEnter.Name = "uxEnter";
            this.uxEnter.Size = new System.Drawing.Size(75, 23);
            this.uxEnter.TabIndex = 2;
            this.uxEnter.Text = "Enter";
            this.uxEnter.UseVisualStyleBackColor = true;
            this.uxEnter.Click += new System.EventHandler(this.uxEnter_Click);
            // 
            // ArduinoCOM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 86);
            this.Controls.Add(this.uxEnter);
            this.Controls.Add(this.uxCOM);
            this.Controls.Add(this.textBox1);
            this.Name = "ArduinoCOM";
            this.Text = "ArduinoCOM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox uxCOM;
        private System.Windows.Forms.Button uxEnter;
    }
}