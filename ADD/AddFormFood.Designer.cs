namespace QuanlyquanCoffe
{
    partial class AddFormFood
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
            this.btnAddFood = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbFoodName = new System.Windows.Forms.TextBox();
            this.nmFoodPrice = new System.Windows.Forms.NumericUpDown();
            this.cbFoodCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rdoRecipeYes = new System.Windows.Forms.RadioButton();
            this.rdoRecipeNo = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cbNguyenLieu = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nmDinhLuong = new System.Windows.Forms.NumericUpDown();
            this.dtgvCongThuc = new System.Windows.Forms.DataGridView();
            this.lblDonVi = new System.Windows.Forms.Label();
            this.btnThemNguyenLieu = new System.Windows.Forms.Button();
            this.panelCongThuc = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nmFoodPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDinhLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCongThuc)).BeginInit();
            this.panelCongThuc.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddFood
            // 
            this.btnAddFood.BackColor = System.Drawing.Color.PeachPuff;
            this.btnAddFood.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFood.Location = new System.Drawing.Point(634, 485);
            this.btnAddFood.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(103, 51);
            this.btnAddFood.TabIndex = 3;
            this.btnAddFood.Text = "Thêm";
            this.btnAddFood.UseVisualStyleBackColor = false;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PeachPuff;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(497, 485);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 51);
            this.button1.TabIndex = 4;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tên món ăn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Giá bán:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Danh mục:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(208, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(302, 52);
            this.label4.TabIndex = 8;
            this.label4.Text = "Thêm món ăn";
            // 
            // txbFoodName
            // 
            this.txbFoodName.Location = new System.Drawing.Point(279, 93);
            this.txbFoodName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbFoodName.Name = "txbFoodName";
            this.txbFoodName.Size = new System.Drawing.Size(358, 26);
            this.txbFoodName.TabIndex = 9;
            // 
            // nmFoodPrice
            // 
            this.nmFoodPrice.Location = new System.Drawing.Point(278, 193);
            this.nmFoodPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nmFoodPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmFoodPrice.Name = "nmFoodPrice";
            this.nmFoodPrice.Size = new System.Drawing.Size(359, 26);
            this.nmFoodPrice.TabIndex = 10;
            // 
            // cbFoodCategory
            // 
            this.cbFoodCategory.FormattingEnabled = true;
            this.cbFoodCategory.Location = new System.Drawing.Point(280, 145);
            this.cbFoodCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbFoodCategory.Name = "cbFoodCategory";
            this.cbFoodCategory.Size = new System.Drawing.Size(358, 28);
            this.cbFoodCategory.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(53, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Công thức:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // rdoRecipeYes
            // 
            this.rdoRecipeYes.AutoSize = true;
            this.rdoRecipeYes.Location = new System.Drawing.Point(280, 251);
            this.rdoRecipeYes.Name = "rdoRecipeYes";
            this.rdoRecipeYes.Size = new System.Drawing.Size(54, 24);
            this.rdoRecipeYes.TabIndex = 13;
            this.rdoRecipeYes.Text = "Có";
            this.rdoRecipeYes.UseVisualStyleBackColor = true;
            this.rdoRecipeYes.CheckedChanged += new System.EventHandler(this.rdoRecipe_CheckedChanged);
            // 
            // rdoRecipeNo
            // 
            this.rdoRecipeNo.AutoSize = true;
            this.rdoRecipeNo.Checked = true;
            this.rdoRecipeNo.Location = new System.Drawing.Point(398, 251);
            this.rdoRecipeNo.Name = "rdoRecipeNo";
            this.rdoRecipeNo.Size = new System.Drawing.Size(80, 24);
            this.rdoRecipeNo.TabIndex = 14;
            this.rdoRecipeNo.TabStop = true;
            this.rdoRecipeNo.Text = "Không";
            this.rdoRecipeNo.UseVisualStyleBackColor = true;
            this.rdoRecipeNo.CheckedChanged += new System.EventHandler(this.rdoRecipe_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Nguyên liệu";
            // 
            // cbNguyenLieu
            // 
            this.cbNguyenLieu.FormattingEnabled = true;
            this.cbNguyenLieu.Location = new System.Drawing.Point(161, 14);
            this.cbNguyenLieu.Name = "cbNguyenLieu";
            this.cbNguyenLieu.Size = new System.Drawing.Size(217, 28);
            this.cbNguyenLieu.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Định lượng";
            // 
            // nmDinhLuong
            // 
            this.nmDinhLuong.Location = new System.Drawing.Point(161, 63);
            this.nmDinhLuong.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmDinhLuong.Name = "nmDinhLuong";
            this.nmDinhLuong.Size = new System.Drawing.Size(89, 26);
            this.nmDinhLuong.TabIndex = 18;
            // 
            // dtgvCongThuc
            // 
            this.dtgvCongThuc.AllowUserToAddRows = false;
            this.dtgvCongThuc.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtgvCongThuc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgvCongThuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvCongThuc.Location = new System.Drawing.Point(30, 97);
            this.dtgvCongThuc.Name = "dtgvCongThuc";
            this.dtgvCongThuc.RowHeadersVisible = false;
            this.dtgvCongThuc.RowHeadersWidth = 62;
            this.dtgvCongThuc.RowTemplate.Height = 28;
            this.dtgvCongThuc.Size = new System.Drawing.Size(415, 170);
            this.dtgvCongThuc.TabIndex = 19;
            this.dtgvCongThuc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvCongThuc_CellContentClick);
            // 
            // lblDonVi
            // 
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDonVi.Location = new System.Drawing.Point(256, 61);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new System.Drawing.Size(67, 25);
            this.lblDonVi.TabIndex = 20;
            this.lblDonVi.Text = "Đơn vị";
            // 
            // btnThemNguyenLieu
            // 
            this.btnThemNguyenLieu.Location = new System.Drawing.Point(387, 55);
            this.btnThemNguyenLieu.Name = "btnThemNguyenLieu";
            this.btnThemNguyenLieu.Size = new System.Drawing.Size(66, 40);
            this.btnThemNguyenLieu.TabIndex = 21;
            this.btnThemNguyenLieu.Text = "Thêm";
            this.btnThemNguyenLieu.UseVisualStyleBackColor = true;
            this.btnThemNguyenLieu.Click += new System.EventHandler(this.btnThemNguyenLieu_Click);
            // 
            // panelCongThuc
            // 
            this.panelCongThuc.Controls.Add(this.btnThemNguyenLieu);
            this.panelCongThuc.Controls.Add(this.dtgvCongThuc);
            this.panelCongThuc.Controls.Add(this.lblDonVi);
            this.panelCongThuc.Controls.Add(this.nmDinhLuong);
            this.panelCongThuc.Controls.Add(this.label7);
            this.panelCongThuc.Controls.Add(this.cbNguyenLieu);
            this.panelCongThuc.Controls.Add(this.label6);
            this.panelCongThuc.Location = new System.Drawing.Point(28, 290);
            this.panelCongThuc.Name = "panelCongThuc";
            this.panelCongThuc.Size = new System.Drawing.Size(463, 270);
            this.panelCongThuc.TabIndex = 22;
            this.panelCongThuc.Visible = false;
            // 
            // AddFormFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 572);
            this.Controls.Add(this.panelCongThuc);
            this.Controls.Add(this.rdoRecipeNo);
            this.Controls.Add(this.rdoRecipeYes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbFoodCategory);
            this.Controls.Add(this.nmFoodPrice);
            this.Controls.Add(this.txbFoodName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddFood);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddFormFood";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "n";
            this.Load += new System.EventHandler(this.AddFormFood_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmFoodPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmDinhLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvCongThuc)).EndInit();
            this.panelCongThuc.ResumeLayout(false);
            this.panelCongThuc.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbFoodName;
        private System.Windows.Forms.NumericUpDown nmFoodPrice;
        private System.Windows.Forms.ComboBox cbFoodCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdoRecipeYes;
        private System.Windows.Forms.RadioButton rdoRecipeNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbNguyenLieu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nmDinhLuong;
        private System.Windows.Forms.DataGridView dtgvCongThuc;
        private System.Windows.Forms.Label lblDonVi;
        private System.Windows.Forms.Button btnThemNguyenLieu;
        private System.Windows.Forms.Panel panelCongThuc;
    }
}