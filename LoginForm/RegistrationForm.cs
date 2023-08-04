using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using SocialMedia_BITC.Controllers;
using SocialMedia_BITC.Middleware;

namespace LoginForm
{
    public partial class frmRegistrationForm : Form
    {
        public frmRegistrationForm()
        {
            InitializeComponent();
        }
        UserController User = new UserController();
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!Validation.IsUsername(txtLogin.Text))
            {
                MessageBox.Show("Invalid Username", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (!Validation.IsPassword(txtPassword.Text))
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                User.AddUser(txtLogin.Text, txtPassword.Text, dtBirth.Value.Date, IsMale.Checked);
                MessageBox.Show("Registration Successful", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
