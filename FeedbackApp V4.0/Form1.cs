#region Includes

using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Data;


#endregion Includes

namespace FeedbackApp_V4._0
{
    public partial class frmLogin : Form
    {
        #region Local Variable Declarations

        protected internal static bool loggedIn = false;
        protected internal static bool registered = false;
        protected internal static int recordCount;
        protected internal static int uID;

        #endregion Local Variable Declarations

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

            // Find last user_id and place it into a variable for creating new records.
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FeedbackApp_V4._0.Properties.Settings.FeedbackConnectionString"].ConnectionString);
            var cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "SELECT MAX(user_id) FROM user_details",
                Connection = conn
            };

            try
            {
                // Attempt connection as listed in App.Config
                conn.Open();
            }
            catch
            {
                // If no connection available run the appropriate Function to handle exception
                NoSuchConn(ConfigurationManager
                    .ConnectionStrings["FeedbackApp_V4._0.Properties.Settings.FeedbackConnectionString"]
                    .ConnectionString);
            }

            try
            {
                uID = (int)cmd.ExecuteScalar();
            }
            catch
            {
                NoSuchTable(cmd.CommandText);
            }

            conn.Close();

            MessageBox.Show(@"Max id = " + uID);
            uID += 1;
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

        #region ShowNewUserRegistrationForm Handler

        public void ShowNewUserRegistrationForm()
        {

            LogMessage(@"New registration attempt started.");

            var newUserForm = new CreateNewUser();

            LogMessage(@"Registration form opened.");

            if (newUserForm.ShowDialog(this) == DialogResult.OK)
            {
                var uN = newUserForm.txtUserName.Text;
                var fN = newUserForm.txtFirstName.Text;
                var lN = newUserForm.txtLastName.Text;
                var pw = newUserForm.txtPassword.Text;
                var iP = newUserForm.chkIsPupil.Checked;
                var iT = newUserForm.chkIsTeacher.Checked;
                var iA = newUserForm.chkIsAdmin.Checked;

                // not sure what is going on here.

                registered = true;

                // MessageBox.Show(firstName + ", " + lastName + ", " + userName + ", " + password + ", isPupil " +
                //                isPupil + ", isTeacher " + isTeacher + ", isAdmin" + isAdmin);

                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FeedbackApp_V4._0.Properties.Settings.FeedbackConnectionString"].ConnectionString);

                var cmd = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    CommandText =
                    "INSERT INTO user_details (user_id, user_userName, user_firstName, user_lastName, isPupil, isTeacher, isAdmin) VALUES (uID, uN, fN,lN, iP, iT, iA)",
                    Connection = conn
                };

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }

        #endregion ShowNewUserResistrationForm Handler

        #region NoSuchConn(connectionString) Method Handler

        private void NoSuchConn(string connectionString)
        {
            MessageBox.Show(@"No such database exists, " + connectionString);
            LogMessage("Requested database does not exist.");
        }

        #endregion NoSuchConn(connectionString) Method Handler

        #region NoSuchTable(commandText) Method Handler

        private void NoSuchTable(string commandText)
        {
            MessageBox.Show(@"No such table exists, " + commandText);
            LogMessage("Requested table does not exist.");
        }

        #endregion NoSuchTable(commandText) Method Handler

    }
}