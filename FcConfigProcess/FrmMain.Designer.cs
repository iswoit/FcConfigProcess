namespace FcConfigProcess
{
    partial class FrmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.btnSelFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbClientID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYYB = new System.Windows.Forms.TextBox();
            this.tbStockHolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbStockHolderReference = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tbDestPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbProductName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbOrganization = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDelExecute = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbDelStockHolder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbDelFilePath = new System.Windows.Forms.TextBox();
            this.btnSelDelFile = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tbCheckFilePath = new System.Windows.Forms.TextBox();
            this.btnSelCheckFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置文件(请注意备份):";
            // 
            // tbFilePath
            // 
            this.tbFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.tbFilePath.Location = new System.Drawing.Point(185, 18);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.ReadOnly = true;
            this.tbFilePath.Size = new System.Drawing.Size(395, 21);
            this.tbFilePath.TabIndex = 1;
            // 
            // btnSelFile
            // 
            this.btnSelFile.Location = new System.Drawing.Point(586, 16);
            this.btnSelFile.Name = "btnSelFile";
            this.btnSelFile.Size = new System.Drawing.Size(50, 23);
            this.btnSelFile.TabIndex = 2;
            this.btnSelFile.Text = "...";
            this.btnSelFile.UseVisualStyleBackColor = true;
            this.btnSelFile.Click += new System.EventHandler(this.btnSelFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "客户号(12位):";
            // 
            // tbClientID
            // 
            this.tbClientID.Location = new System.Drawing.Point(159, 27);
            this.tbClientID.MaxLength = 12;
            this.tbClientID.Name = "tbClientID";
            this.tbClientID.Size = new System.Drawing.Size(135, 21);
            this.tbClientID.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "营业部代码(ccccnn):";
            // 
            // tbYYB
            // 
            this.tbYYB.Location = new System.Drawing.Point(159, 95);
            this.tbYYB.MaxLength = 6;
            this.tbYYB.Name = "tbYYB";
            this.tbYYB.Size = new System.Drawing.Size(135, 21);
            this.tbYYB.TabIndex = 9;
            this.tbYYB.TextChanged += new System.EventHandler(this.GeneratePathChanged);
            // 
            // tbStockHolder
            // 
            this.tbStockHolder.Location = new System.Drawing.Point(159, 61);
            this.tbStockHolder.MaxLength = 6;
            this.tbStockHolder.Name = "tbStockHolder";
            this.tbStockHolder.Size = new System.Drawing.Size(103, 21);
            this.tbStockHolder.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "股东账号别名(ccc_nn):";
            // 
            // tbStockHolderReference
            // 
            this.tbStockHolderReference.Location = new System.Drawing.Point(203, 234);
            this.tbStockHolderReference.Name = "tbStockHolderReference";
            this.tbStockHolderReference.Size = new System.Drawing.Size(135, 21);
            this.tbStockHolderReference.TabIndex = 13;
            this.tbStockHolderReference.Text = "hfz_03";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "参考股东号别名(建议就这个不改):";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(552, 345);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(84, 27);
            this.btnExecute.TabIndex = 14;
            this.btnExecute.Text = "添加产品";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tbDestPath
            // 
            this.tbDestPath.Location = new System.Drawing.Point(159, 180);
            this.tbDestPath.Name = "tbDestPath";
            this.tbDestPath.Size = new System.Drawing.Size(351, 21);
            this.tbDestPath.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "生成路径:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbProductName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbOrganization);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbDestPath);
            this.groupBox1.Controls.Add(this.tbClientID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbYYB);
            this.groupBox1.Controls.Add(this.tbStockHolderReference);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbStockHolder);
            this.groupBox1.Location = new System.Drawing.Point(26, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 278);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增营业部参数";
            // 
            // tbProductName
            // 
            this.tbProductName.Location = new System.Drawing.Point(159, 152);
            this.tbProductName.Name = "tbProductName";
            this.tbProductName.Size = new System.Drawing.Size(140, 21);
            this.tbProductName.TabIndex = 20;
            this.tbProductName.TextChanged += new System.EventHandler(this.GeneratePathChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(90, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "产品名称:";
            // 
            // tbOrganization
            // 
            this.tbOrganization.Location = new System.Drawing.Point(159, 125);
            this.tbOrganization.Name = "tbOrganization";
            this.tbOrganization.Size = new System.Drawing.Size(86, 21);
            this.tbOrganization.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "机构简称:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(657, 406);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.tbFilePath);
            this.tabPage1.Controls.Add(this.btnExecute);
            this.tabPage1.Controls.Add(this.btnSelFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(649, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "产品添加";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btnDelExecute);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.tbDelStockHolder);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.tbDelFilePath);
            this.tabPage2.Controls.Add(this.btnSelDelFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(649, 380);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "产品删除";
            // 
            // btnDelExecute
            // 
            this.btnDelExecute.Location = new System.Drawing.Point(548, 345);
            this.btnDelExecute.Name = "btnDelExecute";
            this.btnDelExecute.Size = new System.Drawing.Size(84, 27);
            this.btnDelExecute.TabIndex = 15;
            this.btnDelExecute.Text = "删除产品";
            this.btnDelExecute.UseVisualStyleBackColor = true;
            this.btnDelExecute.Click += new System.EventHandler(this.btnDelExecute_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "股东账号别名(ccc_nn，多行):";
            // 
            // tbDelStockHolder
            // 
            this.tbDelStockHolder.Location = new System.Drawing.Point(202, 69);
            this.tbDelStockHolder.Multiline = true;
            this.tbDelStockHolder.Name = "tbDelStockHolder";
            this.tbDelStockHolder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDelStockHolder.Size = new System.Drawing.Size(340, 253);
            this.tbDelStockHolder.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "配置文件(请注意备份):";
            // 
            // tbDelFilePath
            // 
            this.tbDelFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.tbDelFilePath.Location = new System.Drawing.Point(202, 23);
            this.tbDelFilePath.Name = "tbDelFilePath";
            this.tbDelFilePath.ReadOnly = true;
            this.tbDelFilePath.Size = new System.Drawing.Size(356, 21);
            this.tbDelFilePath.TabIndex = 4;
            // 
            // btnSelDelFile
            // 
            this.btnSelDelFile.Location = new System.Drawing.Point(582, 21);
            this.btnSelDelFile.Name = "btnSelDelFile";
            this.btnSelDelFile.Size = new System.Drawing.Size(50, 23);
            this.btnSelDelFile.TabIndex = 5;
            this.btnSelDelFile.Text = "...";
            this.btnSelDelFile.UseVisualStyleBackColor = true;
            this.btnSelDelFile.Click += new System.EventHandler(this.btnSelDelFile_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.btnCheck);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.tbCheckFilePath);
            this.tabPage3.Controls.Add(this.btnSelCheckFile);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(649, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "检查配置文件格式合法性";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(434, 82);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(84, 27);
            this.btnCheck.TabIndex = 19;
            this.btnCheck.Text = "开始检查";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(41, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "配置文件;";
            // 
            // tbCheckFilePath
            // 
            this.tbCheckFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.tbCheckFilePath.Location = new System.Drawing.Point(106, 24);
            this.tbCheckFilePath.Name = "tbCheckFilePath";
            this.tbCheckFilePath.ReadOnly = true;
            this.tbCheckFilePath.Size = new System.Drawing.Size(356, 21);
            this.tbCheckFilePath.TabIndex = 17;
            // 
            // btnSelCheckFile
            // 
            this.btnSelCheckFile.Location = new System.Drawing.Point(468, 22);
            this.btnSelCheckFile.Name = "btnSelCheckFile";
            this.btnSelCheckFile.Size = new System.Drawing.Size(50, 23);
            this.btnSelCheckFile.TabIndex = 18;
            this.btnSelCheckFile.Text = "...";
            this.btnSelCheckFile.UseVisualStyleBackColor = true;
            this.btnSelCheckFile.Click += new System.EventHandler(this.btnSelCheck_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 406);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "分仓配置文件增加&删除产品";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Button btnSelFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbClientID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbYYB;
        private System.Windows.Forms.TextBox tbStockHolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbStockHolderReference;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.TextBox tbDestPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbProductName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOrganization;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDelExecute;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbDelStockHolder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbDelFilePath;
        private System.Windows.Forms.Button btnSelDelFile;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbCheckFilePath;
        private System.Windows.Forms.Button btnSelCheckFile;
    }
}

