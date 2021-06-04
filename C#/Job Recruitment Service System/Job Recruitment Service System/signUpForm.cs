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
    public partial class signUpForm : Form
    {

        string picPath = "";

        string userType = "";

        public signUpForm()
        {
            InitializeComponent();
        }




        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");






        private void submitButton_Click(object sender, EventArgs e)
        {

            String user = userTextBox.Text.ToString();
            String pass = passwordTextBox.Text.ToString();

            String name = nameTextBox.Text.ToString();
            String address = addressTextBox.Text.ToString();
            String email = emailTextBox.Text.ToString();
            String cell = cellTextBox.Text.ToString();

            String userType = userTypeComboBox.SelectedItem.ToString();



            //email checking

            int length = email.Length;

            bool valid = true;

            char[] ch = email.ToCharArray();


            if (length < 7 || !(ch[length - 4] == '.' && ch[length - 3] == 'c' && ch[length - 2] == 'o' && ch[length - 1] == 'm'))
                valid = false;


            bool correct_order = true;
            int count = 0;

            for (int x = 0; x < length - 4; x++)
            {
                if (ch[x] == '@') count++;

                if (ch[x] == '@')
                {
                    if (x == 0 || ch[x + 1] == '.')
                        correct_order = false;
                }

                if ((ch[x] >= 32 && ch[x] <= 45) || (ch[x] >= 58 && ch[x] <= 63) || (ch[x] >= 91 && ch[x] <= 94) || ch[x] == '{' || ch[x] == '}' || ch[x] == '/')
                    correct_order = false;


            }


            if (!correct_order || count != 1) valid = false;

            //email checking sesh







            SqlDataAdapter sda = new SqlDataAdapter("select * from LoginData where Username='" + user + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Username already exists !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto End;
            }






            if (user.Equals("") || pass.Equals("") || name.Equals("") || address.Equals("") || email.Equals("") || cell.Equals(""))
                MessageBox.Show("Fill all empty places !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (picPath.Equals(""))
                MessageBox.Show("Image not Selected !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (!valid || cell.Length != 11)
            {
                if (!valid)
                    MessageBox.Show("Invalid E-mail Address !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cell.Length != 11)
                    MessageBox.Show("Invalid Cell number !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            else
            {
                con.Open();

                String query =
                    "insert into LoginData values( '" + user + "','" + pass + "','" + name + "','" + address + "','" + email + "','" + cell + "', '" + picPath + "', '" + userType + "'  )"; 

                SqlCommand scmd = new SqlCommand(query, con);

                try
                {
                    int i = scmd.ExecuteNonQuery();
                    if (i == 1) MessageBox.Show("Registration Succesfull\n");

                    userTextBox.Text = passwordTextBox.Text = nameTextBox.Text = addressTextBox.Text = emailTextBox.Text = cellTextBox.Text = "";
                    userTypeComboBox.Text = "";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Query Failed\n"+ex);
                }


                con.Close();


                pictureBox.ImageLocation = "";

            }

            
            //kaj sesh

            End:
            {
                con.Close();
            }


        }






        //cell textBox er kaj
        //cell textBox er kaj
        private void cellTextBox_TextChanged(object sender, EventArgs e)
        {

            String str = cellTextBox.Text.ToString();
            int length = str.Length;

            if (length > 0 && !(str[length - 1] >= '0' && str[length - 1] <= '9'))
            {
                MessageBox.Show("No Letters or Symbols are allowed !!\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                char[] ch = str.ToCharArray();  //converting string to char array
                ch[length - 1] = '\0';          //dismissing last typed  char or symbol or other
                str = new String(ch);           //now converting edited char arrayto string

                cellTextBox.Text = str;
            }

            if (length == 1 && !str[0].Equals('0'))
            {
                MessageBox.Show("Cell must start with ' 0 ' \n", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //kaj sesh


        }



        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files(*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picPath = dlg.FileName;

                pictureBox.ImageLocation = picPath;
            }


        }


        private void addressLabel_Click(object sender, EventArgs e)
        {

        }

        private void emailLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void signUpForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}
