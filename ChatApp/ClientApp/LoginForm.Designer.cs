namespace ClientApp
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BannerLabel = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.TextBoxUsername = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.TextBoxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BannerLabel
            // 
            this.BannerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BannerLabel.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BannerLabel.Location = new System.Drawing.Point(0, 0);
            this.BannerLabel.Name = "BannerLabel";
            this.BannerLabel.Size = new System.Drawing.Size(434, 62);
            this.BannerLabel.TabIndex = 0;
            this.BannerLabel.Text = "Login";
            this.BannerLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(124, 292);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 1;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Location = new System.Drawing.Point(201, 233);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = '*';
            this.TextBoxPassword.Size = new System.Drawing.Size(100, 23);
            this.TextBoxPassword.TabIndex = 2;
            // 
            // TextBoxUsername
            // 
            this.TextBoxUsername.Location = new System.Drawing.Point(201, 187);
            this.TextBoxUsername.Name = "TextBoxUsername";
            this.TextBoxUsername.Size = new System.Drawing.Size(100, 23);
            this.TextBoxUsername.TabIndex = 3;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameLabel.Location = new System.Drawing.Point(124, 185);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(55, 21);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(100, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(226, 292);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(75, 23);
            this.RegisterButton.TabIndex = 6;
            this.RegisterButton.Text = "Register";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // TextBoxIP
            // 
            this.TextBoxIP.Location = new System.Drawing.Point(79, 79);
            this.TextBoxIP.Name = "TextBoxIP";
            this.TextBoxIP.Size = new System.Drawing.Size(100, 23);
            this.TextBoxIP.TabIndex = 3;
            this.TextBoxIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(47, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP:";
            // 
            // TextBoxPort
            // 
            this.TextBoxPort.Location = new System.Drawing.Point(247, 79);
            this.TextBoxPort.Name = "TextBoxPort";
            this.TextBoxPort.Size = new System.Drawing.Size(100, 23);
            this.TextBoxPort.TabIndex = 3;
            this.TextBoxPort.Text = "51510";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(201, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port:";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 386);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxIP);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.TextBoxUsername);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.BannerLabel);
            this.Name = "LoginForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BannerLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.TextBox TextBoxUsername;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox TextBoxIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxPort;
        private System.Windows.Forms.Label label3;
    }
}

