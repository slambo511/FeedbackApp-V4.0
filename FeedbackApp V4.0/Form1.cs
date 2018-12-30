using System;
using System.Windows.Forms;

namespace FeedbackApp_V4._0
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnRegister.Enabled = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LogMessage(@"Attempted login by user '" + txtUserName.Text + "'.");
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" && txtPassword.Text == "")
            {
                btnRegister.Enabled = true;
                btnLogin.Enabled = false;
            }
            else if (txtPassword.Text == "")
            {
                btnRegister.Enabled = false;
                btnLogin.Enabled = false;
            }
            else
            {
                btnRegister.Enabled = false;
                btnLogin.Enabled = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" && txtUserName.Text == "")
            {
                btnRegister.Enabled = true;
                btnLogin.Enabled = false;
            }
            else if (txtUserName.Text == "")
            {
                btnRegister.Enabled = false;
                btnLogin.Enabled = false;
            }
            else
            {
                btnRegister.Enabled = false;
                btnLogin.Enabled = true;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private static void ExitProgram()
        {
            if (MessageBox.Show(@"Are you sure you wish to exit?", @"Exit Program", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                Environment.Exit(Environment.ExitCode);
            }
            else
            {
                return;
            }
        }

        private void LogMessage(string message)
        {
            try
            {
                // Code to append system log
                // Consider making Async for efficiency
                var _Message = DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + ", " +
                               message;
                // Log _Message to appropriate place
                MessageBox.Show(_Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(@"The program cannot write to logs because, " + e + @" and so will exit.", @"Error", MessageBoxButtons.OK);
                ExitProgram();
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            LogMessage(@"New registration attempt started.");
        }
    }
}
