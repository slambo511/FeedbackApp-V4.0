#region Includes

using System;
using System.Security.Authentication.ExtendedProtection;
using System.Windows.Forms;

#endregion Includes

namespace FeedbackApp_V4._0
{
    public partial class frmLogin : Form
    {
        protected internal static bool loggedIn = false;
        protected internal static bool registered = false;

        #region Initiliaser Handler

        public frmLogin()
        {
            InitializeComponent();
        }

        #endregion Initialiser Handler  

        #region Form Load Handler

        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnRegister.Enabled = true;
        }

        #endregion Form Load Handler

        #region Login Button Handler

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LogMessage(@"Attempted login by user '" + txtUserName.Text + "'.");
        }

        #endregion Login Button Handler

        #region User Name Textbox Changing Text Handler

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

        #endregion User Name Textbox Changing Text Handler

        #region Password Textbox Changing Text Handler

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

        #endregion Password Textbox Changing Text Handler

        #region Register Button Handler

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            ShowNewUserRegistrationForm();
        }

        #endregion Register Button Handler

        #region ExitProgram() Subroutine Handler

        private void ExitProgram()
        {
            if (MessageBox.Show(@"Are you sure you wish to exit?", @"Exit Program", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                if (!loggedIn && !registered)
                    LogMessage(@"Exit occured without valid login or registration.");
                else if (!loggedIn)
                    LogMessage(@"User registered but did not login.");
                Environment.Exit(Environment.ExitCode);
            }
            else
            {
                return;
            }
        }

        #endregion ExitProgram() Subroutine Handler

        #region LogMessage(string message) Subroutine Handler

        private void LogMessage(string message)
        {
            try
            {
                // Code to append system log
                // Consider making Async for efficiency
                var logMessage = DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + ", " +
                               message;
                // Log _Message to appropriate place
                MessageBox.Show(logMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(@"The program cannot write to logs because, " + e + @" and so will exit.", @"Error", MessageBoxButtons.OK);
                ExitProgram();
            }
        }

        #endregion LogMessage(string message) Subroutine Handler

        #region Exit Button Handler

        private void btnExit_Click_1(object sender, EventArgs e)
        {

            ExitProgram();
        }

        #endregion Exit Button Handler

        public void ShowNewUserRegistrationForm()
        {

            LogMessage(@"New registration attempt started.");

            var newUserForm = new CreateNewUser();

            LogMessage(@"Registration form opened.");

            if (newUserForm.ShowDialog(this) == DialogResult.OK)
            {
                var userName = newUserForm.txtUserName.Text;
                var firstName = newUserForm.txtFirstName.Text;
                var lastName = newUserForm.txtLastName.Text;
                var password = newUserForm.txtPassword.Text;
                var isPupil = newUserForm.chkIsPupil.Checked;
                var isTeacher = newUserForm.chkIsTeacher.Checked;
                var isAdmin = newUserForm.chkIsAdmin.Checked;

                registered = true;

                // MessageBox.Show(firstName + ", " + lastName + ", " + userName + ", " + password + ", isPupil " +
                //                isPupil + ", isTeacher " + isTeacher + ", isAdmin" + isAdmin);
            }

        }

    }
}
