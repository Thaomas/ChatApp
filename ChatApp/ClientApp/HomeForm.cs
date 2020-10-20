using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        public void addMessageToChat(string message)
        {
            this.Invoke((Action)delegate
            {
                textBoxChatLog.Text += message;
                textBoxChatLog.SelectionStart = textBoxChatLog.Text.Length;
                textBoxChatLog.ScrollToCaret();
            });
        }

        private void textBoxChatMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {

                textBoxChatMessage.Text = "";
            }
        }
    }
}
