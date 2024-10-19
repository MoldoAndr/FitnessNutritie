using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;

namespace FitnessNutritionClient
{
    public partial class MainWindow : Window
    {
        private const string ServerIp = "127.0.0.1"; // Server IP address
        private const int ServerPort = 7025; // Server port

    

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            
            string username = SignUpUsername.Text;
            string password = SignUpPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                SignUpResult.Text = "Username and password cannot be empty.";
                return;
            }

            try
            {
                using (TcpClient client = new TcpClient(ServerIp, ServerPort))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    writer.Write("SIGNUP");
                    writer.Write(username);
                    writer.Write(password);

                    bool success = reader.ReadBoolean();
                    SignUpResult.Text = success ? "Signup successful!" : "Signup failed. Username may already be taken.";
                }
            }
            catch (Exception ex)
            {
                SignUpResult.Text = "Error: " + ex.Message;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUsername.Text;
            string password = LoginPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                LoginResult.Text = "Username and password cannot be empty.";
                return;
            }

            try
            {
                using (TcpClient client = new TcpClient(ServerIp, ServerPort))
                using (NetworkStream stream = client.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    writer.Write("LOGIN");
                    writer.Write(username);
                    writer.Write(password);

                    bool authenticated = reader.ReadBoolean();
                    LoginResult.Text = authenticated ? "Login successful!" : "Login failed. Check your credentials.";
                }
            }
            catch (Exception ex)
            {
                LoginResult.Text = "Error: " + ex.Message;
            }
        }
    }
}