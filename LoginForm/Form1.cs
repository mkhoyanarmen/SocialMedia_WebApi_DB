using SocialMedia_BITC.Middleware;
using SocialMedia_BITC.Controllers;
namespace LoginForm
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        UserController User { get; set; } = new UserController();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Validation.IsUsername(txtLogin.Text))
            {
                MessageBox.Show("Invalid Login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Validation.IsPassword(txtPassword.Text))
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (User.CheckUser(txtLogin.Text, txtPassword.Text))
                    MessageBox.Show("Login Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("User not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            frmRegistrationForm frmReg = new frmRegistrationForm();
            frmReg.Show();
        }
    }
}