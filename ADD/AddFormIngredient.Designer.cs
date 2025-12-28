namespace QuanlyquanCoffe.ADD
{
    partial class AddFormIngredient
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
            this.titleAddIngredient = new System.Windows.Forms.Label();
            this.labelNguyenLieu = new System.Windows.Forms.Label();
            this.txtTenNguyenLieu = new System.Windows.Forms.TextBox();
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.labelDonvi = new System.Windows.Forms.Label();
            this.btnAddIngredient = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleAddIngredient
            // 
            this.titleAddIngredient.AutoSize = true;
            this.titleAddIngredient.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.titleAddIngredient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.titleAddIngredient.Location = new System.Drawing.Point(226, 9);
            this.titleAddIngredient.Name = "titleAddIngredient";
            this.titleAddIngredient.Size = new System.Drawing.Size(384, 52);
            this.titleAddIngredient.TabIndex = 9;
            this.titleAddIngredient.Text = "Thêm nguyên liệu";
            // 
            // labelNguyenLieu
            // 
            this.labelNguyenLieu.AutoSize = true;
            this.labelNguyenLieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNguyenLieu.Location = new System.Drawing.Point(58, 106);
            this.labelNguyenLieu.Name = "labelNguyenLieu";
            this.labelNguyenLieu.Size = new System.Drawing.Size(207, 29);
            this.labelNguyenLieu.TabIndex = 16;
            this.labelNguyenLieu.Text = "Tên nguyên liệu:";
            // 
            // txtTenNguyenLieu
            // 
            this.txtTenNguyenLieu.Location = new System.Drawing.Point(271, 109);
            this.txtTenNguyenLieu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenNguyenLieu.Name = "txtTenNguyenLieu";
            this.txtTenNguyenLieu.Size = new System.Drawing.Size(358, 26);
            this.txtTenNguyenLieu.TabIndex = 17;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Location = new System.Drawing.Point(271, 173);
            this.txtDonVi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.Size = new System.Drawing.Size(358, 26);
            this.txtDonVi.TabIndex = 18;
            // 
            // labelDonvi
            // 
            this.labelDonvi.AutoSize = true;
            this.labelDonvi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDonvi.Location = new System.Drawing.Point(58, 169);
            this.labelDonvi.Name = "labelDonvi";
            this.labelDonvi.Size = new System.Drawing.Size(100, 29);
            this.labelDonvi.TabIndex = 19;
            this.labelDonvi.Text = "Đơn vị :";
            // 
            // btnAddIngredient
            // 
            this.btnAddIngredient.BackColor = System.Drawing.Color.PeachPuff;
            this.btnAddIngredient.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddIngredient.Location = new System.Drawing.Point(482, 345);
            this.btnAddIngredient.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddIngredient.Name = "btnAddIngredient";
            this.btnAddIngredient.Size = new System.Drawing.Size(128, 61);
            this.btnAddIngredient.TabIndex = 20;
            this.btnAddIngredient.Text = "Thêm";
            this.btnAddIngredient.UseVisualStyleBackColor = false;
            this.btnAddIngredient.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.PeachPuff;
            this.btnExit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(637, 345);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(128, 61);
            this.btnExit.TabIndex = 21;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AddFormIngredient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddIngredient);
            this.Controls.Add(this.labelDonvi);
            this.Controls.Add(this.txtDonVi);
            this.Controls.Add(this.txtTenNguyenLieu);
            this.Controls.Add(this.labelNguyenLieu);
            this.Controls.Add(this.titleAddIngredient);
            this.Name = "AddFormIngredient";
            this.Text = "Thêm nguyên liệu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleAddIngredient;
        private System.Windows.Forms.Label labelNguyenLieu;
        private System.Windows.Forms.TextBox txtTenNguyenLieu;
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.Label labelDonvi;
        private System.Windows.Forms.Button btnAddIngredient;
        private System.Windows.Forms.Button btnExit;
    }
}