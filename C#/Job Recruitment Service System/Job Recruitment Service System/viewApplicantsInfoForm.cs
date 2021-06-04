using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Job_Recruitment_Service_System
{
    public partial class viewApplicantsInfoForm : Form
    {
        string jobID = "";
        string user = "";

        string pictureLocation = "";
        string cvLocation = "";


        public viewApplicantsInfoForm(string jobID, string user)
        {
            InitializeComponent();

            this.jobID = jobID;
            this.user = user;

            this.show();
            this.getCVLocation();

        }




        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");



        public void show()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from LoginData where Username='" + user + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            //


            foreach (DataRow dr in dt.Rows)
            {
                userTextBox.Text = dr[0].ToString();

                nameTextBox.Text = dr[2].ToString();
                addressTextBox.Text = dr[3].ToString();
                emailTextBox.Text = dr[4].ToString();
                cellTextBox.Text = dr[5].ToString();

                pictureLocation = dr[6].ToString();/////////

                Console.WriteLine(dr[0].ToString() + "\n");
                Console.WriteLine(dr[2].ToString() + "\n");
                Console.WriteLine(dr[3].ToString() + "\n");
                Console.WriteLine(dr[4].ToString() + "\n");
                Console.WriteLine(dr[5].ToString() + "\n");
                Console.WriteLine(dr[6].ToString() + "\n");
            }



            //
            pictureBox.ImageLocation = pictureLocation;



            con.Close();



        }



        public void getCVLocation()
        {

            con.Open();
            SqlDataAdapter sda = 
                new SqlDataAdapter("select * from ApplicantCVData where Job_ID='" + jobID + "' and Applicant='" + user + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            //


            foreach (DataRow dr in dt.Rows)
            {

                cvLocation = dr[2].ToString();
                
            }



            //
            cvTextBox.Text = cvLocation;



            con.Close();


        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            string file =cvLocation;
            //@"C:\"
            Process.Start(file);
        }



        private void inviteButton_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select * from InterviewData where Job_ID='" + jobID + "' and Selected_Applicant='" + user + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("This applicant has been Invited already !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto @End;    

            }



            String query = "insert into InterviewData values('" + jobID + "', '" + user + "' )";

            SqlCommand scmd = new SqlCommand(query, con);

            try
            {
                int i = scmd.ExecuteNonQuery();
                MessageBox.Show("This applicant has been Invited for Interview Succesfully\n", "Succes");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Query Failed\n" + ex);
            }



            con.Close();



            

            @End:
            {
                con.Close();
            }



        }
    }
}
