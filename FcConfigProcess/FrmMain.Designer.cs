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
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbOrganization = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbProductName = new System.Windows.Forms.TextBox();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置文件:";
            // 
            // tbFilePath
            // 
            this.tbFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.tbFilePath.Location = new System.Drawing.Point(99, 20);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.ReadOnly = true;
            this.tbFilePath.Size = new System.Drawing.Size(395, 21);
            this.tbFilePath.TabIndex = 1;
            this.tbFilePath.Text = "D:\\我的文档\\Desktop\\qsjm20180119new.ini";
            // 
            // btnSelFile
            // 
            this.btnSelFile.Location = new System.Drawing.Point(500, 18);
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
            this.label2.Location = new System.Drawing.Point(42, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "客户号(12位):";
            // 
            // tbClientID
            // 
            this.tbClientID.Location = new System.Drawing.Point(131, 36);
            this.tbClientID.MaxLength = 12;
            this.tbClientID.Name = "tbClientID";
            this.tbClientID.Size = new System.Drawing.Size(135, 21);
            this.tbClientID.TabIndex = 7;
            this.tbClientID.Text = "111111111111";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "营业部代码(ccccnn):";
            // 
            // tbYYB
            // 
            this.tbYYB.Location = new System.Drawing.Point(131, 66);
            this.tbYYB.MaxLength = 6;
            this.tbYYB.Name = "tbYYB";
            this.tbYYB.Size = new System.Drawing.Size(135, 21);
            this.tbYYB.TabIndex = 9;
            this.tbYYB.Text = "aaaa01";
            // 
            // tbStockHolder
            // 
            this.tbStockHolder.Location = new System.Drawing.Point(422, 36);
            this.tbStockHolder.MaxLength = 6;
            this.tbStockHolder.Name = "tbStockHolder";
            this.tbStockHolder.Size = new System.Drawing.Size(103, 21);
            this.tbStockHolder.TabIndex = 11;
            this.tbStockHolder.Text = "aaa_01";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(292, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "股东账号别名(ccc_nn):";
            // 
            // tbStockHolderReference
            // 
            this.tbStockHolderReference.Location = new System.Drawing.Point(131, 165);
            this.tbStockHolderReference.Name = "tbStockHolderReference";
            this.tbStockHolderReference.Size = new System.Drawing.Size(135, 21);
            this.tbStockHolderReference.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "参考股东账号别名:";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(684, 395);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(84, 27);
            this.btnExecute.TabIndex = 14;
            this.btnExecute.Text = "添加产品";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(131, 101);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(351, 21);
            this.textBox7.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 101);
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
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.tbClientID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbYYB);
            this.groupBox1.Controls.Add(this.tbStockHolderReference);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbStockHolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 251);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增营业部参数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(292, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "机构简称:";
            // 
            // tbOrganization
            // 
            this.tbOrganization.Location = new System.Drawing.Point(357, 66);
            this.tbOrganization.Name = "tbOrganization";
            this.tbOrganization.Size = new System.Drawing.Size(86, 21);
            this.tbOrganization.TabIndex = 18;
            this.tbOrganization.Text = "XX投资";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(466, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "产品名称:";
            // 
            // tbProductName
            // 
            this.tbProductName.Location = new System.Drawing.Point(531, 66);
            this.tbProductName.Name = "tbProductName";
            this.tbProductName.Size = new System.Drawing.Size(140, 21);
            this.tbProductName.TabIndex = 20;
            this.tbProductName.Text = "XXX5号";
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 365);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(634, 115);
            this.tbLog.TabIndex = 19;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 551);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnSelFile);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "分仓配置文件增加产品";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbProductName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbOrganization;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbLog;
    }
}

