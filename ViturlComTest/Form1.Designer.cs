namespace ViturlComTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbSendCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendComControl = new System.Windows.Forms.Button();
            this.labSendComStatus = new System.Windows.Forms.Label();
            this.txtSendLog = new System.Windows.Forms.RichTextBox();
            this.txtReceiveLog = new System.Windows.Forms.RichTextBox();
            this.labReceiveComStatus = new System.Windows.Forms.Label();
            this.btnReceiveComControl = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbReceiveCom = new System.Windows.Forms.ComboBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.chkLed = new System.Windows.Forms.CheckBox();
            this.chkLedReceive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbSendCom
            // 
            this.cmbSendCom.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSendCom.FormattingEnabled = true;
            this.cmbSendCom.Location = new System.Drawing.Point(184, 60);
            this.cmbSendCom.Name = "cmbSendCom";
            this.cmbSendCom.Size = new System.Drawing.Size(121, 28);
            this.cmbSendCom.TabIndex = 0;
            this.cmbSendCom.TextChanged += new System.EventHandler(this.cmbSendCom_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(99, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "发送串口：";
            // 
            // btnSendComControl
            // 
            this.btnSendComControl.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendComControl.Location = new System.Drawing.Point(311, 60);
            this.btnSendComControl.Name = "btnSendComControl";
            this.btnSendComControl.Size = new System.Drawing.Size(80, 28);
            this.btnSendComControl.TabIndex = 2;
            this.btnSendComControl.Text = "打开串口";
            this.btnSendComControl.UseVisualStyleBackColor = true;
            this.btnSendComControl.Click += new System.EventHandler(this.btnSendComControl_Click);
            // 
            // labSendComStatus
            // 
            this.labSendComStatus.AutoSize = true;
            this.labSendComStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSendComStatus.Location = new System.Drawing.Point(182, 91);
            this.labSendComStatus.Name = "labSendComStatus";
            this.labSendComStatus.Size = new System.Drawing.Size(79, 20);
            this.labSendComStatus.TabIndex = 3;
            this.labSendComStatus.Text = "串口未选择";
            // 
            // txtSendLog
            // 
            this.txtSendLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSendLog.Location = new System.Drawing.Point(33, 158);
            this.txtSendLog.Name = "txtSendLog";
            this.txtSendLog.Size = new System.Drawing.Size(381, 241);
            this.txtSendLog.TabIndex = 4;
            this.txtSendLog.Text = "";
            // 
            // txtReceiveLog
            // 
            this.txtReceiveLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtReceiveLog.Location = new System.Drawing.Point(525, 158);
            this.txtReceiveLog.Name = "txtReceiveLog";
            this.txtReceiveLog.Size = new System.Drawing.Size(341, 241);
            this.txtReceiveLog.TabIndex = 10;
            this.txtReceiveLog.Text = "";
            // 
            // labReceiveComStatus
            // 
            this.labReceiveComStatus.AutoSize = true;
            this.labReceiveComStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labReceiveComStatus.Location = new System.Drawing.Point(657, 91);
            this.labReceiveComStatus.Name = "labReceiveComStatus";
            this.labReceiveComStatus.Size = new System.Drawing.Size(79, 20);
            this.labReceiveComStatus.TabIndex = 9;
            this.labReceiveComStatus.Text = "串口未选择";
            // 
            // btnReceiveComControl
            // 
            this.btnReceiveComControl.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReceiveComControl.Location = new System.Drawing.Point(786, 60);
            this.btnReceiveComControl.Name = "btnReceiveComControl";
            this.btnReceiveComControl.Size = new System.Drawing.Size(80, 28);
            this.btnReceiveComControl.TabIndex = 8;
            this.btnReceiveComControl.Text = "打开串口";
            this.btnReceiveComControl.UseVisualStyleBackColor = true;
            this.btnReceiveComControl.Click += new System.EventHandler(this.btnReceiveComControl_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(574, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "接收串口：";
            // 
            // cmbReceiveCom
            // 
            this.cmbReceiveCom.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbReceiveCom.FormattingEnabled = true;
            this.cmbReceiveCom.Location = new System.Drawing.Point(659, 60);
            this.cmbReceiveCom.Name = "cmbReceiveCom";
            this.cmbReceiveCom.Size = new System.Drawing.Size(121, 28);
            this.cmbReceiveCom.TabIndex = 6;
            this.cmbReceiveCom.TextChanged += new System.EventHandler(this.cmbReceiveCom_TextChanged);
            // 
            // txtSendData
            // 
            this.txtSendData.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSendData.Location = new System.Drawing.Point(186, 118);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(119, 26);
            this.txtSendData.TabIndex = 12;
            // 
            // btnSendData
            // 
            this.btnSendData.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSendData.Location = new System.Drawing.Point(311, 118);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(80, 28);
            this.btnSendData.TabIndex = 13;
            this.btnSendData.Text = "发送数据";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // chkLed
            // 
            this.chkLed.AutoSize = true;
            this.chkLed.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkLed.Location = new System.Drawing.Point(96, 120);
            this.chkLed.Name = "chkLed";
            this.chkLed.Size = new System.Drawing.Size(84, 24);
            this.chkLed.TabIndex = 14;
            this.chkLed.Text = "客显格式";
            this.chkLed.UseVisualStyleBackColor = true;
            // 
            // chkLedReceive
            // 
            this.chkLedReceive.AutoSize = true;
            this.chkLedReceive.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkLedReceive.Location = new System.Drawing.Point(578, 120);
            this.chkLedReceive.Name = "chkLedReceive";
            this.chkLedReceive.Size = new System.Drawing.Size(112, 24);
            this.chkLedReceive.TabIndex = 15;
            this.chkLedReceive.Text = "客显格式接收";
            this.chkLedReceive.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 521);
            this.Controls.Add(this.chkLedReceive);
            this.Controls.Add(this.chkLed);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.txtSendData);
            this.Controls.Add(this.txtReceiveLog);
            this.Controls.Add(this.labReceiveComStatus);
            this.Controls.Add(this.btnReceiveComControl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbReceiveCom);
            this.Controls.Add(this.txtSendLog);
            this.Controls.Add(this.labSendComStatus);
            this.Controls.Add(this.btnSendComControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSendCom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "虚拟客显测试工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSendCom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendComControl;
        private System.Windows.Forms.Label labSendComStatus;
        private System.Windows.Forms.RichTextBox txtSendLog;
        private System.Windows.Forms.RichTextBox txtReceiveLog;
        private System.Windows.Forms.Label labReceiveComStatus;
        private System.Windows.Forms.Button btnReceiveComControl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbReceiveCom;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.CheckBox chkLed;
        private System.Windows.Forms.CheckBox chkLedReceive;
    }
}

