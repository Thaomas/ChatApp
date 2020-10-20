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

        private TcpClient _client;

        public LoginForm()
        {
            InitializeComponent();
            _client = new TcpClient();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if( !(TextBoxIP.Text == null) || !(TextBoxPort.Text == null)  && !((IPEndPoint)_client.Client.RemoteEndPoint).Address.ToString().Equals(TextBoxIP.Text) || !((IPEndPoint)_client.Client.RemoteEndPoint).Port.Equals(TextBoxPort.Text))
            {
                _client.ConnectAsync(IPAddress.Parse( TextBoxIP.Text), Convert.ToInt32(TextBoxPort.Text));

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
