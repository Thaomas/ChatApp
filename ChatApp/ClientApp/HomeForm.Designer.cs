﻿namespace ClientApp
{
    partial class HomeForm
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
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxChatMessage = new System.Windows.Forms.TextBox();
            this.panelSendMessage = new System.Windows.Forms.Panel();
            this.textBoxChatLog = new System.Windows.Forms.TextBox();
            this.panelSendMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSend.Location = new System.Drawing.Point(725, 0);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 36);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxChatMessage
            // 
            this.textBoxChatMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxChatMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxChatMessage.Location = new System.Drawing.Point(0, 0);
            this.textBoxChatMessage.Multiline = true;
            this.textBoxChatMessage.Name = "textBoxChatMessage";
            this.textBoxChatMessage.Size = new System.Drawing.Size(725, 36);
            this.textBoxChatMessage.TabIndex = 1;
            this.textBoxChatMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxChatMessage_KeyPress);
            // 
            // panelSendMessage
            // 
            this.panelSendMessage.Controls.Add(this.textBoxChatMessage);
            this.panelSendMessage.Controls.Add(this.buttonSend);
            this.panelSendMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSendMessage.Location = new System.Drawing.Point(0, 414);
            this.panelSendMessage.Name = "panelSendMessage";
            this.panelSendMessage.Size = new System.Drawing.Size(800, 36);
            this.panelSendMessage.TabIndex = 2;
            // 
            // textBoxChatLog
            // 
            this.textBoxChatLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxChatLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxChatLog.Multiline = true;
            this.textBoxChatLog.Name = "textBoxChatLog";
            this.textBoxChatLog.ReadOnly = true;
            this.textBoxChatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxChatLog.Size = new System.Drawing.Size(800, 414);
            this.textBoxChatLog.TabIndex = 35;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxChatLog);
            this.Controls.Add(this.panelSendMessage);
            this.Name = "HomeForm";
            this.Text = "Chat";
            this.panelSendMessage.ResumeLayout(false);
            this.panelSendMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxChatMessage;
        private System.Windows.Forms.Panel panelSendMessage;
        private System.Windows.Forms.TextBox textBoxChatLog;
    }
}