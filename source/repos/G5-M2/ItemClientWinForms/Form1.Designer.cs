namespace ItemClientWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvItems = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            txtName = new TextBox();
            txtCode = new TextBox();
            txtBrand = new TextBox();
            txtUnitPrice = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // dgvItems
            // 
            dgvItems.BackgroundColor = Color.OldLace;
            dgvItems.BorderStyle = BorderStyle.Fixed3D;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.GridColor = Color.OldLace;
            dgvItems.Location = new Point(24, 158);
            dgvItems.Margin = new Padding(4);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersWidth = 51;
            dgvItems.Size = new Size(982, 210);
            dgvItems.TabIndex = 0;
            dgvItems.CellContentClick += dgvItems_CellContentClick;
            dgvItems.SelectionChanged += dgvItems_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Linen;
            btnAdd.Location = new Point(430, 423);
            btnAdd.Margin = new Padding(4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 31);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.Linen;
            btnUpdate.Location = new Point(430, 473);
            btnUpdate.Margin = new Padding(4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(130, 31);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Linen;
            btnDelete.Location = new Point(694, 387);
            btnDelete.Margin = new Padding(4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(130, 31);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Linen;
            btnRefresh.Location = new Point(855, 387);
            btnRefresh.Margin = new Padding(4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(130, 31);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // txtName
            // 
            txtName.BackColor = Color.Linen;
            txtName.Location = new Point(188, 403);
            txtName.Margin = new Padding(4);
            txtName.Name = "txtName";
            txtName.Size = new Size(170, 28);
            txtName.TabIndex = 5;
            // 
            // txtCode
            // 
            txtCode.BackColor = Color.Linen;
            txtCode.Location = new Point(188, 454);
            txtCode.Margin = new Padding(4);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(170, 28);
            txtCode.TabIndex = 6;
            // 
            // txtBrand
            // 
            txtBrand.BackColor = Color.Linen;
            txtBrand.Location = new Point(188, 499);
            txtBrand.Margin = new Padding(4);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(170, 28);
            txtBrand.TabIndex = 7;
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.BackColor = Color.Linen;
            txtUnitPrice.Location = new Point(188, 551);
            txtUnitPrice.Margin = new Padding(4);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(170, 28);
            txtUnitPrice.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.BurlyWood;
            label1.Location = new Point(45, 406);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(54, 21);
            label1.TabIndex = 9;
            label1.Text = "Name";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 455);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 21);
            label2.TabIndex = 10;
            label2.Text = "Code";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 506);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(65, 21);
            label3.TabIndex = 11;
            label3.Text = "Brand";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 558);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(120, 21);
            label4.TabIndex = 12;
            label4.Text = "Unit Price";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.FlatStyle = FlatStyle.Popup;
            label5.Font = new Font("Courier New", 35F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(36, 41);
            label5.Name = "label5";
            label5.Size = new Size(958, 79);
            label5.TabIndex = 13;
            label5.Text = "ITEM MANAGEMENT SYSTEM";
            label5.TextAlign = ContentAlignment.TopCenter;
            label5.Click += label5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOliveGreen;
            ClientSize = new Size(1030, 622);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtUnitPrice);
            Controls.Add(txtBrand);
            Controls.Add(txtCode);
            Controls.Add(txtName);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dgvItems);
            Font = new Font("Courier New", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.Tan;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Item Manager";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvItems;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private TextBox txtName;
        private TextBox txtCode;
        private TextBox txtBrand;
        private TextBox txtUnitPrice;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
