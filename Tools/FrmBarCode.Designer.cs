namespace Tools
{
    partial class FrmBarCode
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
            this.chkSimplePay = new System.Windows.Forms.CheckBox();
            this.chkAutoPromotion = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chkSimplePay
            // 
            this.chkSimplePay.AutoSize = true;
            this.chkSimplePay.Location = new System.Drawing.Point(54, 28);
            this.chkSimplePay.Name = "chkSimplePay";
            this.chkSimplePay.Size = new System.Drawing.Size(78, 16);
            this.chkSimplePay.TabIndex = 0;
            this.chkSimplePay.Text = "精简 模式";
            this.chkSimplePay.UseVisualStyleBackColor = true;
            this.chkSimplePay.CheckedChanged += new System.EventHandler(this.chkSimplePay_CheckedChanged);
            // 
            // chkAutoPromotion
            // 
            this.chkAutoPromotion.AutoSize = true;
            this.chkAutoPromotion.Location = new System.Drawing.Point(175, 28);
            this.chkAutoPromotion.Name = "chkAutoPromotion";
            this.chkAutoPromotion.Size = new System.Drawing.Size(96, 16);
            this.chkAutoPromotion.TabIndex = 1;
            this.chkAutoPromotion.Text = "自动匹配优惠";
            this.chkAutoPromotion.UseVisualStyleBackColor = true;
            this.chkAutoPromotion.CheckedChanged += new System.EventHandler(this.chkAutoPromotion_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(54, 50);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(407, 233);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // FrmBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 346);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.chkAutoPromotion);
            this.Controls.Add(this.chkSimplePay);
            this.Name = "FrmBarCode";
            this.Text = "FrmBarCode";
            this.Load += new System.EventHandler(this.FrmBarCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSimplePay;
        private System.Windows.Forms.CheckBox chkAutoPromotion;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}