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
    public partial class jobCircularForm : Form
    {

        string jobID = "";
        string user = "", userType = "";
        
        

        public jobCircularForm(String user,String userType)
        {
            InitializeComponent();
            this.user = user;
            this.userType = userType;


            this.show();

            if(dataGridView.Rows.Count > 0)this.jobID = dataGridView.Rows[0].Cells["Job_ID"].Value.ToString();



            if (this.userType == "Job Seeker")
            {
                createButton.Visible = false;
                applicantsButton.Visible = false;

                this.checkNotification();
            }


            if (this.userType == "Authority-Creator") notificationPictureBox.Visible = false;



        }



        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");


        //datagrid 
        public void show()
        {
            string cmp = user;
            SqlDataAdapter sda;

            if (this.userType == "Authority-Creator")
                sda = new SqlDataAdapter("select * from JobCircularData where Company='" + cmp + "'", con);
            else
                sda = new SqlDataAdapter("select * from JobCircularData", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count < 1)
            {
                Console.WriteLine(dt.Rows.Count);
                dataGridView.Rows.Add("null",cmp.ToString(),"null","null");
                goto End;
            }

            dataGridView.Rows.Clear();

            int n = 0;

            foreach (DataRow dr in dt.Rows)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = dr[0].ToString();
                dataGridView.Rows[n].Cells[1].Value = dr[1].ToString();
                dataGridView.Rows[n].Cells[2].Value = dr[2].ToString();
                dataGridView.Rows[n].Cells[3].Value = dr[3].ToString();


                n++;
            }


            End:
            { }

        }




        //change notification icon
        public void checkNotification()
        {
            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select * from InterviewData where Selected_Applicant='" + user + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                notificationPictureBox.Image = Properties.Resources.notification;
        

            }

            else
            {
                notificationPictureBox.Image = Properties.Resources.basic;

            }


            con.Close();


        }



        private void notificationPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            
            notificationPictureBox.Image = Properties.Resources.basic;


            con.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select * from InterviewData where Selected_Applicant='" + user + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            string str = "";

            if (dt.Rows.Count > 0)
            {
                int n = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    str += "          You have been invited for Job with Job ID" + dr[0].ToString() +". You are requested to come to the main office address next Saturday. \n\n";

                    n++;
                }

            }

           
            con.Close();


            MessageBox.Show( str, "Notification");


        }







        private void viewButton_Click(object sender, EventArgs e)
        {
            jobCircularDetailForm objt = new jobCircularDetailForm(user,userType,jobID);
            objt.Show();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            jobCircularDetailForm objt = new jobCircularDetailForm(user, userType, "");
            objt.Show();
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            loginForm ob = new loginForm();
            ob.Show();
            this.Dispose();
        }

        private void applicantsButton_Click(object sender, EventArgs e)
        {
            viewApplicantsForm obj = new viewApplicantsForm(jobID);
            obj.Show();
        }




        private void refreshButton_Click(object sender, EventArgs e)
        {
            show();
        }



        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                jobID = dataGridView.Rows[e.RowIndex].Cells["Job_ID"].Value.ToString();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
            }
        }


                                                                                                                                                   
    }
}
