using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Addmin_setup
{
    public partial class Form1 : Form
    {

        private int maxAttempts = 3;
        private int currentAttempts = 0;
        private DateTime unlockTime = DateTime.MinValue;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FormLogin_FormClosing);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            login.Enabled = false;

        }


        public void cek()
        {
            if (textBox1.Text == "")
            {
                login.Enabled = false;
            }
            else if (textBox2.Text == "")
            {
                login.Enabled = false;
            }
            else if (checkBox1.Checked == false)
            {
                login.Enabled = false;
            }
            else
            {
                login.Enabled=true;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            cek();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            cek();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            cek();
            // Memeriksa apakah CheckBox dicentang atau tidak
            if (checkBox1.Checked)
            {
                // Menampilkan MessageBox dengan ikon
                string message = "Privacy Policy\n\n" +
                  "Information We Collect:\n" +
                  "- Username: For user identification within the app.\n" +
                  "- Password: For account security.\n\n" +
                  "How We Use Your Information:\n" +
                  "- To process your login and ensure account security.\n\n" +
                  "How We Share Your Information:\n" +
                  "- We don't share your username and password with third parties.\n\n" +
                  "Security:\n" +
                  "- We take security measures, including encrypting sensitive data.\n\n" +
                  "Changes to This Privacy Policy:\n" +
                  "- This policy may be updated; you'll be notified of changes here.\n\n" +
                  "Contact Us:\n" +
                  "- Questions or suggestions? Email us at [your contact email].";

                MessageBox.Show(message, "Privacy Policy", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (IsLockedOut())
            {
                MessageBox.Show("Akun Anda terkunci. Silakan tunggu hingga timer berakhir.");
                return;
            }

            string username = textBox1.Text;
            string password = textBox2.Text;

            // Tambahkan logika validasi username dan password di sini.
            // Contoh sederhana:
            if (username == "user" && password == "password")
            {
                MessageBox.Show("Login berhasil!");
                currentAttempts = 0; // Reset jumlah percobaan
            }
            else
            {
                currentAttempts++;

                if (currentAttempts >= maxAttempts)
                {
                    unlockTime = DateTime.Now.AddMinutes(5); // Mengunci akun selama 5 menit
                    MessageBox.Show("Anda telah mencapai batas percobaan. Akun Anda terkunci selama 5 menit.");
                }
                else
                {
                    MessageBox.Show("Username atau password salah. Sisa percobaan: " + (maxAttempts - currentAttempts));
                }
            }
        }

        private bool IsLockedOut()
        {
            return unlockTime > DateTime.Now;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (IsLockedOut())
            {
                TimeSpan remainingTime = unlockTime - DateTime.Now;
                lblTimer.Text = "Waktu tersisa: " + remainingTime.ToString(@"mm\:ss");
            }
            else
            {
                lblTimer.Text = "Login Akun terbuka.";
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLockedOut())
            {
                e.Cancel = true; // Mencegah penutupan aplikasi
                MessageBox.Show("Aplikasi tidak dapat ditutup selama akun terkunci.");
            }
            else
            {
                e.Cancel = false; // Memungkinkan penutupan aplikasi
            }
        }
    }
}
