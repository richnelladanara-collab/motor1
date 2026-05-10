using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ahhh
{
    public partial class Motors : Form
    {
        private readonly string rentalsFile;

        public Motors()
        {
            InitializeComponent();
            rentalsFile = Path.Combine(Application.StartupPath, "rentals.txt");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RentMotor("NMAXX", button7);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RentMotor("ADV", button1);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            RentMotor("MIO", button8);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RentMotor("AEROX", button3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RentMotor("PCX", button4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            RentMotor("CLICK", button6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            RentMotor("FAZZIO", button9);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            RentMotor("GEAR", button10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RentMotor("ADV", button1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RentMotor("NMAXX", button7);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RentMotor("AEROX", button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RentMotor("PCX", button4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RentMotor("CLICK", button6);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RentMotor("MIO", button8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RentMotor("FAZZIO", button9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RentMotor("GEAR", button10);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Motors_Load(object sender, EventArgs e)
        {
            // On load, read existing rentals (if any) and disable rented items so they cannot be rented again
            if (!File.Exists(rentalsFile)) return;

            try
            {
                var lines = File.ReadAllLines(rentalsFile);
                var rented = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split(',');
                    if (parts.Length >= 1)
                    {
                        rented.Add(parts[0].Trim());
                    }
                }

                if (rented.Contains("ADV")) DisableButton(button1);
                if (rented.Contains("NMAXX")) DisableButton(button7);
                if (rented.Contains("AEROX")) DisableButton(button3);
                if (rented.Contains("PCX")) DisableButton(button4);
                if (rented.Contains("CLICK")) DisableButton(button6);
                if (rented.Contains("MIO")) DisableButton(button8);
                if (rented.Contains("FAZZIO")) DisableButton(button9);
                if (rented.Contains("GEAR")) DisableButton(button10);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read rentals file: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Helper: centralize rental flow
        private void RentMotor(string motorName, Button sourceButton)
        {
            if (sourceButton != null && !sourceButton.Enabled)
            {
                MessageBox.Show($"{motorName} is already rented.", "Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Do you want to rent \"{motorName}\"?", "Confirm Rental", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                File.AppendAllText(rentalsFile, $"{motorName},{DateTime.Now:o}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save rental: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sourceButton != null) DisableButton(sourceButton);

            MessageBox.Show($"You have successfully rented \"{motorName}\".", "Rented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisableButton(Button btn)
        {
            if (btn == null) return;
            btn.Enabled = false;
            btn.Text = "Rented";
        }
    }
}
