namespace QuanlyquanCoffe
{
    partial class FormCheckOut
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
            this.txbCheckOut = new System.Windows.Forms.RichTextBox();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbCheckOut
            // 
            this.txbCheckOut.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbCheckOut.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbCheckOut.Location = new System.Drawing.Point(45, 12);
            this.txbCheckOut.Name = "txbCheckOut";
            this.txbCheckOut.ReadOnly = true;
            this.txbCheckOut.Size = new System.Drawing.Size(513, 451);
            this.txbCheckOut.TabIndex = 0;
            this.txbCheckOut.Text = " private void richTextBox1_TextChanged(";
            this.txbCheckOut.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCheckOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckOut.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCheckOut.Location = new System.Drawing.Point(391, 469);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(167, 49);
            this.btnCheckOut.TabIndex = 2;
            this.btnCheckOut.Text = "THANH TOÁN";
            this.btnCheckOut.UseVisualStyleBackColor = false;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // FormCheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 529);
            this.Controls.Add(this.btnCheckOut);
            this.Controls.Add(this.txbCheckOut);
            this.Name = "FormCheckOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh Toán Hóa đơn";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txbCheckOut;
        private System.Windows.Forms.Button btnCheckOut;
    }
}