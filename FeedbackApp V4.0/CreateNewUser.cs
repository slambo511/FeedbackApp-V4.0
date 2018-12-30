using System;
using System.Windows.Forms;

namespace FeedbackApp_V4._0
{
    public partial class CreateNewUser : Form
    {
        public CreateNewUser()
        {
            InitializeComponent();
        }

        private void CreateNewUser_Load(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "Teacher")
            {
                chkIsTeacher.Enabled = true;
            }

            if (txtFirstName.Text == "Admin")
            {
                chkIsAdmin.Enabled = true;
            }
        }
    }
}
