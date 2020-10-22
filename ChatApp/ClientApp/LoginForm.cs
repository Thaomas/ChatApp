using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class LoginForm : Form
    {

        private Client _client;

        public LoginForm()
        {
            InitializeComponent();
            this._client = new Client();
            this._client.OnLogin += Client_OnLogin;
            this._client.OnRegister += Client_OnRegister;
        }
        private void Client_OnLogin(bool status)
        {
            this.Invoke((Action)delegate {
                if (status)
                {
                    HomeForm homePageForm = new HomeForm();
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
            this.Invoke((Action)delegate {
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
            if (!(TextBoxIP.Text == null) || !(TextBoxPort.Text == null) && !((IPEndPoint)_client.RemoteEndPoint).Address.ToString().Equals(TextBoxIP.Text) || !((IPEndPoint)_client.RemoteEndPoint).Port.Equals(TextBoxPort.Text))
            {
                _client.ConnectAsync(IPAddress.Parse(TextBoxIP.Text), Convert.ToInt32(TextBoxPort.Text));

            }
            bool connected = true;
            if (connected)
            {
                HomeForm home = new HomeForm();
                home.Show();
                this.Hide();
            }

            //TextBoxUsername.Text  TextBoxPassword.Text
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            //TextBoxUsername.Text TextBoxPassword.Text

        }

    }
}
