using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class HomeForm : Form
    {

        private Client client;
        public HomeForm(Client client, List<string> chatLog)
        {
            InitializeComponent();
            Shown += HomeForm_Shown;
            this.client = client;
            this.client.OnChatReceived += Client_OnChatReceived;

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
    }
}
