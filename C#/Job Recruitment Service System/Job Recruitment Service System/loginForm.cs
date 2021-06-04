using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Job_Recruitment_Service_System
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();

            this.userTextBox.Text = "saima";
            this.passwordTextBox.Text = "12345";
            
            this.userTypeComboBox.SelectedIndex = userTypeComboBox.Items.IndexOf("Job Seeker");
        }



        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");




        private void signUpButton_Click(object sender, EventArgs e)
        {
            signUpForm obj = new signUpForm();
            obj.Show();
            //this.Hide();
        }

        private void userNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

        private void enterButton_Click(object sender, EventArgs e)
        {


            con.Open();

            try
            {
                String user = userTextBox.Text.ToString();
                String pass = passwordTextBox.Text.ToString();
                String userType = userTypeComboBox.SelectedItem.ToString();

                Console.WriteLine("user = "+user + "\npass = " + pass+"\n");
                Console.WriteLine(userType);

                SqlDataAdapter sda
                    = new SqlDataAdapter("select * from LoginData where Username='" + user + "' and Password='" + pass + "' and userNameType = '" + userType + "' ", con);

                DataTable dt = new DataTable();

                sda.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    //nsole.WriteLine("passed\n");
                    //
                    jobCircularForm ob = new jobCircularForm(user,userType);
                    ob.Show();
                    this.Hide();
                }
                else
                {

                    if (user.Equals("") || pass.Equals(""))
                        MessageBox.Show("Fill all empty places !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show("Wrong Username or Password !!\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        userTextBox.Text = passwordTextBox.Text = "";
                    }

                }

            }

            catch (Exception)
            {
                Console.WriteLine("Query error\n");
            }


            con.Close();
            //kaj sesh



        }

        private void userTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userTypeComboBox.SelectedIndex.Equals(0))
                userTextBox.Text = "Priyoshop.Com";
            else
                userTextBox.Text = "saima";
        }
    }
}
