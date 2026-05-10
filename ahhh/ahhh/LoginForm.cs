using System;
using System.IO;
using System.Windows.Forms;

namespace ahhh
{
    public partial class LoginForm : Form
    {
        string filePath = System.IO.Path.Combine(Application.StartupPath, "users.txt");

        public LoginForm()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("No registered users found.");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                bool loginSuccess = false;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split(',');

                    if (parts.Length >= 2 &&
                        parts[0].Trim() == username &&
                        parts[1].Trim() == password)
                    {
                        loginSuccess = true;
                        break;
                    }
                }

                if (loginSuccess)
                {
                    MessageBox.Show("Login successful! Welcome " + username);
                    Motors motorsForm = new Motors();
                    motorsForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.");
                    textBox2.Clear();
                    textBox2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading database: " + ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}