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
    public partial class jobCircularDetailForm : Form
    {
        string jobID = "";
        string user = "", userType = "";

        public jobCircularDetailForm(string user, string userType, string jobID)
        {
            InitializeComponent();

            this.jobID = jobID;
            this.user = user;
            this.userType = userType;

            this.show();


            if (this.userType != "Job Seeker") applyButton.Visible = false;
            if (this.userType != "Authority-Creator")
            {
                doneButton.Visible = false;
                updateButton.Visible = false;

            }

            //when click on create then theres no Job_ID number
            if (jobID == "")
            {
                companyTextBox.Text = user;
                getJobID();

            }

        }




        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");


        
        public void show()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from JobCircularData where Job_ID='" + jobID + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            

            foreach (DataRow dr in dt.Rows)
            {
             
                jobIDTextBox.Text = dr[0].ToString();
                companyTextBox.Text = dr[1].ToString();
                postTextBox.Text = dr[2].ToString();
                deadlineTextBox.Text = dr[3].ToString();
                addressTextBox.Text = dr[4].ToString();
                detailsTextBox.Text = dr[5].ToString();
                

            }

            con.Close();

        }



        public void getJobID()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from JobCircularData ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            List<String> all_jobID = new List<string>();
            all_jobID.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                string temp_jobID = dr[0].ToString();
                all_jobID.Add(temp_jobID);

            }

            con.Close();

            int n = all_jobID.Count;


            string str = all_jobID[n-1];
            string[] ss = str.Split(new char[] { ' ', '.', ',', '-' }, StringSplitOptions.RemoveEmptyEntries);

            jobID = ss[0] + "-" + (Int32.Parse(ss[1]) + 1).ToString();

            //
            jobIDTextBox.Text = jobID;

        }







        /*
        private void viewButton_Click(object sender, EventArgs e)
        {
            jobCircularDetailForm objt = new jobCircularDetailForm(user,userType,jobID);
            objt.Show();
        }
        */
        


        private void doneButton_Click(object sender, EventArgs e)
        {

            con.Open();

            string jid = jobIDTextBox.Text;
            string cmp = companyTextBox.Text;
            string pst = postTextBox.Text;
            string dl = deadlineTextBox.Text;
            string adrs = addressTextBox.Text;
            string dtls = detailsTextBox.Text;

            if (jid.Equals("") || cmp.Equals("") || pst.Equals("") || dl.Equals("") || adrs.Equals("") || dtls.Equals(""))
            {
                MessageBox.Show("Fill all empty places !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto @End;
            }

            
            SqlDataAdapter sda = new SqlDataAdapter("select * from JobCircularData where Job_ID='" + jobID + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Job ID already exists !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto @End;
            }


            String query =
                    "insert into JobCircularData values( '" + jid + "','" + cmp + "','" + pst + "','" + dl + "','" + adrs + "','" + dtls + "'  )";

            SqlCommand scmd = new SqlCommand(query, con);

            try
            {
                int i = scmd.ExecuteNonQuery();
                if (i == 1) MessageBox.Show("Created Succesfully\n");

                jobIDTextBox.Text = companyTextBox.Text = postTextBox.Text = addressTextBox.Text = deadlineTextBox.Text = detailsTextBox.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query Failed\n" + ex);
            }





            @End:
            con.Close();



        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            con.Open();

            string jid = jobIDTextBox.Text;
            string cmp = companyTextBox.Text;
            string pst = postTextBox.Text;
            string dl = deadlineTextBox.Text;
            string adrs = addressTextBox.Text;
            string dtls = detailsTextBox.Text;

            if (jid.Equals("") || cmp.Equals("") || pst.Equals("") || dl.Equals("") || adrs.Equals("") || dtls.Equals(""))
            {
                MessageBox.Show("Fill all empty places !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto @End;
            }


            String query = "update JobCircularData set Company='" + cmp + "' , Post='" + pst + "' , Deadline_Date='" + dl + "' , Address='"+adrs+"' , Details='" + dtls + "'   where Job_ID='" + jid + "'  ";
            SqlCommand scmd = new SqlCommand(query, con);

            try
            {
                int i = scmd.ExecuteNonQuery();
                if (i == 1) MessageBox.Show("Updated Succesfully\n");

                jobIDTextBox.Text = companyTextBox.Text = postTextBox.Text = addressTextBox.Text = deadlineTextBox.Text = detailsTextBox.Text = "";

            }
            catch (Exception)
            {
                Console.WriteLine("Query Failed\n");
            }




            @End:
            con.Close();



        }



        private void applyButton_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select * from ApplicantData where Job_ID='" + jobID + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            string applicant_str = "";//////

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    applicant_str = dr[1].ToString();
                }


                string[] ss = applicant_str.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> applicants = new List<string>();
                for (int x = 0; x < ss.Length; x++)
                {
                    applicants.Add(ss[x]);
                }

                if (applicants.Contains(user))
                {
                    MessageBox.Show("You have applied already !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto @End;
                }

            }


            con.Close();


            examForm obj = new examForm(user,jobID);
            obj.Show();

            this.Dispose();




            @End:
            {
                con.Close();
            }

        }


    }
}
