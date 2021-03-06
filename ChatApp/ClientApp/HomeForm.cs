﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class HomeForm : Form
    {

        private Client client;
        private LoginForm loginForm;
        public HomeForm(Client client, List<string> chatLog, LoginForm loginForm)
        {
            InitializeComponent();
            Shown += HomeForm_Shown;

            this.loginForm = loginForm;
            
            this.client = client;
            this.client.setForms(this, loginForm);
            this.client.OnChatReceived += Client_OnChatReceived;
            this.client.OnConnectionLost += Client_OnConnectionLost;

            foreach (string message in chatLog)
            {
                textBoxChatLog.Text += message + Environment.NewLine;
            }
        }

        private void HomeForm_Shown(Object sender, EventArgs e)
        {
            textBoxChatLog.SelectionStart = textBoxChatLog.Text.Length;
            textBoxChatLog.ScrollToCaret();
        }

        public void Client_OnChatReceived(string message)
        {
            this.Invoke((Action)delegate
            {
                textBoxChatLog.Text += message;
                textBoxChatLog.SelectionStart = textBoxChatLog.Text.Length;
                textBoxChatLog.ScrollToCaret();
            });
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            buttonSend_Click();
        }

        private void TextBoxChatMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonSend_Click();
                e.Handled = true;
            }
        }

        private void buttonSend_Click()
        {
            this.client.SendChatMessage(textBoxChatMessage.Text);
            textBoxChatMessage.Clear();
            textBoxChatMessage.Focus();
        }

        public void Client_OnConnectionLost()
        {
            this.Invoke((Action)delegate
            {
                this.Close();
                loginForm.Show();
            });
        }
    }
}
