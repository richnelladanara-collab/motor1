using System;
using System.IO;
using System.Windows.Forms;

namespace ahhh
{
    public partial class RegisterForm : Form
    {
        private readonly string filePath = Path.Combine(Application.StartupPath, "users.txt");

        public RegisterForm()
        {
            InitializeComponent();

            // Make password fields masked
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string confirmPassword = textBox3.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Clear();
                textBox3.Focus();
                return;
            }

            // Check for duplicate username
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(',');
                        if (parts.Length >= 1 && parts[0].Trim() == username)
                        {
                            MessageBox.Show("This username is already taken.", "Duplicate Username",
                                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBox1.Focus();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading user file: " + ex.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Save new user
            try
            {
                File.AppendAllText(filePath, $"{username},{password}{Environment.NewLine}");

                MessageBox.Show("Registration successful!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Login Form and close current
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Added the methods below to fix the Designer errors ---

        private void label1_Click(object sender, EventArgs e)
        {
            // This fixes the error on line 48
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // This fixes the error on line 61
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // This fixes the error on line 87
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // This fixes the error on line 100
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}