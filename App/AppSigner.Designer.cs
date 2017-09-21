namespace App
{
    partial class AppSigner
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
            this.txtApkPath = new System.Windows.Forms.TextBox();
            this.txtKeystorePath = new System.Windows.Forms.TextBox();
            this.txtRSAPath = new System.Windows.Forms.TextBox();
            this.btnsSingerAok = new System.Windows.Forms.Button();
            this.btnApkPath = new System.Windows.Forms.Button();
            this.btnRSAPath = new System.Windows.Forms.Button();
            this.btnKeystorePath = new System.Windows.Forms.Button();
            this.txtKeystorePassword = new System.Windows.Forms.TextBox();
            this.btnPasswordPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtApkPath
            // 
            this.txtApkPath.Location = new System.Drawing.Point(12, 47);
            this.txtApkPath.Name = "txtApkPath";
            this.txtApkPath.ReadOnly = true;
            this.txtApkPath.Size = new System.Drawing.Size(455, 21);
            this.txtApkPath.TabIndex = 0;
            // 
            // txtKeystorePath
            // 
            this.txtKeystorePath.Location = new System.Drawing.Point(12, 140);
            this.txtKeystorePath.Name = "txtKeystorePath";
            this.txtKeystorePath.ReadOnly = true;
            this.txtKeystorePath.Size = new System.Drawing.Size(455, 21);
            this.txtKeystorePath.TabIndex = 1;
            // 
            // txtRSAPath
            // 
            this.txtRSAPath.Location = new System.Drawing.Point(12, 269);
            this.txtRSAPath.Name = "txtRSAPath";
            this.txtRSAPath.ReadOnly = true;
            this.txtRSAPath.Size = new System.Drawing.Size(455, 21);
            this.txtRSAPath.TabIndex = 2;
            // 
            // btnsSingerAok
            // 
            this.btnsSingerAok.Location = new System.Drawing.Point(165, 430);
            this.btnsSingerAok.Name = "btnsSingerAok";
            this.btnsSingerAok.Size = new System.Drawing.Size(119, 43);
            this.btnsSingerAok.TabIndex = 3;
            this.btnsSingerAok.Text = "签名";
            this.btnsSingerAok.UseVisualStyleBackColor = true;
            this.btnsSingerAok.Click += new System.EventHandler(this.btnsSingerAok_Click);
            // 
            // btnApkPath
            // 
            this.btnApkPath.Location = new System.Drawing.Point(309, 74);
            this.btnApkPath.Name = "btnApkPath";
            this.btnApkPath.Size = new System.Drawing.Size(147, 23);
            this.btnApkPath.TabIndex = 4;
            this.btnApkPath.Text = "点击选择需要签名app";
            this.btnApkPath.UseVisualStyleBackColor = true;
            this.btnApkPath.Click += new System.EventHandler(this.btnApkPath_Click);
            // 
            // btnRSAPath
            // 
            this.btnRSAPath.Location = new System.Drawing.Point(309, 296);
            this.btnRSAPath.Name = "btnRSAPath";
            this.btnRSAPath.Size = new System.Drawing.Size(147, 23);
            this.btnRSAPath.TabIndex = 5;
            this.btnRSAPath.Text = "点击选择解密密钥";
            this.btnRSAPath.UseVisualStyleBackColor = true;
            this.btnRSAPath.Click += new System.EventHandler(this.btnRSAPath_Click);
            // 
            // btnKeystorePath
            // 
            this.btnKeystorePath.Location = new System.Drawing.Point(309, 167);
            this.btnKeystorePath.Name = "btnKeystorePath";
            this.btnKeystorePath.Size = new System.Drawing.Size(147, 23);
            this.btnKeystorePath.TabIndex = 6;
            this.btnKeystorePath.Text = "点击选择签名文件";
            this.btnKeystorePath.UseVisualStyleBackColor = true;
            this.btnKeystorePath.Click += new System.EventHandler(this.btnKeystorePath_Click);
            // 
            // txtKeystorePassword
            // 
            this.txtKeystorePassword.Location = new System.Drawing.Point(12, 196);
            this.txtKeystorePassword.Name = "txtKeystorePassword";
            this.txtKeystorePassword.ReadOnly = true;
            this.txtKeystorePassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtKeystorePassword.Size = new System.Drawing.Size(455, 21);
            this.txtKeystorePassword.TabIndex = 7;
            // 
            // btnPasswordPath
            // 
            this.btnPasswordPath.Location = new System.Drawing.Point(309, 223);
            this.btnPasswordPath.Name = "btnPasswordPath";
            this.btnPasswordPath.Size = new System.Drawing.Size(147, 23);
            this.btnPasswordPath.TabIndex = 8;
            this.btnPasswordPath.Text = "点击选择密码文件";
            this.btnPasswordPath.UseVisualStyleBackColor = true;
            this.btnPasswordPath.Click += new System.EventHandler(this.btnPasswordPath_Click);
            // 
            // AppSigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 573);
            this.Controls.Add(this.btnPasswordPath);
            this.Controls.Add(this.txtKeystorePassword);
            this.Controls.Add(this.btnKeystorePath);
            this.Controls.Add(this.btnRSAPath);
            this.Controls.Add(this.btnApkPath);
            this.Controls.Add(this.btnsSingerAok);
            this.Controls.Add(this.txtRSAPath);
            this.Controls.Add(this.txtKeystorePath);
            this.Controls.Add(this.txtApkPath);
            this.Name = "AppSigner";
            this.Text = "AppSigner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtApkPath;
        private System.Windows.Forms.TextBox txtKeystorePath;
        private System.Windows.Forms.TextBox txtRSAPath;
        private System.Windows.Forms.Button btnsSingerAok;
        private System.Windows.Forms.Button btnApkPath;
        private System.Windows.Forms.Button btnRSAPath;
        private System.Windows.Forms.Button btnKeystorePath;
        private System.Windows.Forms.TextBox txtKeystorePassword;
        private System.Windows.Forms.Button btnPasswordPath;
    }
}

