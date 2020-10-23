using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class LoginForm : Form
    {

        private Client _client;
        private string ip = "";
        private string port = "";

        public LoginForm()
        {
            InitializeComponent();
            this._client = new Client();
            this._client.OnLogin += Client_OnLogin;
            this._client.OnRegister += Client_OnRegister;
        }

        private void Client_OnLogin(bool status, List<string> chatlog)
        {
            this.Invoke((Action)delegate
            {
                if (status)
                {
                    HomeForm homePageForm = new HomeForm(_client, chatlog);
                    homePageForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Password or Username", "Error");
                }
            });

        }

        private void Client_OnRegister(bool status)
        {
            this.Invoke((Action)delegate
            {
                if (status)
                {
                    MessageBox.Show("Registration succesfull");
                }
                else
                {
                    MessageBox.Show("Username already in use", "Error");
                }
            });

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            if (!(TextBoxIP.Text == null || TextBoxPort.Text == null) && (TextBoxIP.Text != this.ip || TextBoxPort.Text != this.port))
            {
                _client.Disconnect();
                _client.ConnectAsync(IPAddress.Parse(TextBoxIP.Text), Convert.ToInt32(TextBoxPort.Text));
                this.ip = TextBoxIP.Text;
                this.port = TextBoxPort.Text;
            }
            _client.SendLogin(TextBoxUsername.Text, TextBoxPassword.Text);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (!(TextBoxIP.Text == null || TextBoxPort.Text == null) && (TextBoxIP.Text != this.ip || TextBoxPort.Text != this.port))
            {
                _client.Disconnect();
                _client.ConnectAsync(IPAddress.Parse(TextBoxIP.Text), Convert.ToInt32(TextBoxPort.Text));
                this.ip = TextBoxIP.Text;
                this.port = TextBoxPort.Text;
            }

            _client.SendRegister(TextBoxUsername.Text, TextBoxPassword.Text);

        }

        public void TextBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxPassword.Focus();
                e.Handled = true;
            }

        }

        public void TextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Login();
                e.Handled = true;
            }
        }
    }
}
