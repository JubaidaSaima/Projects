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
    public partial class examResultForm : Form
    {
        string jobID = "";
        string user = "";

        string cvLocation = "";

        public examResultForm(int q1, int q2, int q3, int q4, int q5, string user, string jobID)
        {
            InitializeComponent();

            this.jobID = jobID;
            this.user = user;
            

            //if (q1 == 1) label2.Text = "Correct";
            //if (q2 == 1) label4.Text = "Correct";
            //if (q3 == 1) label6.Text = "Correct";

            q1MarksLabel.Text = q1.ToString();
            q2MarksLabel.Text = q2.ToString();
            q3MarksLabel.Text = q3.ToString();
            q4MarksLabel.Text = q4.ToString();
            q5MarksLabel.Text = q5.ToString();

            label10.Text = (q1 + q2 + q3 + q4 + q5).ToString();

            applyButton.Visible = true;

            Function(q1+q2+q3+q4+q5);


        }




        public void Function(int val)
        {
            double value = (double)val / 7.00;
            value = value * 100.00;

            //Console.WriteLine(value);

            if (value < 70.00)
            {
                panel.Visible = false;
                applyButton.Visible = false;
                label11.Text = "You got less than 70% marks in the exam. You are not eligible to apply.";
                label11.ForeColor = Color.Red;
            }

            else
            {
                label11.Text = "You got 70% + marks in the exam. You are eligible to apply.";
                label11.ForeColor = Color.Green;
                
            }

        }



        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");



        private void applyButton_Click(object sender, EventArgs e)
        {

            if (cvTextBox.Text == "")
            {
                MessageBox.Show(" Please select your Curriculam Vitae first.", "CV Not Selected");
                goto End;
            }



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


            }

            
            


            applicant_str += (user+" ");


            String query = "";

            if(dt.Rows.Count == 0) query = "insert into ApplicantData values('" + jobID + "', '" + applicant_str + "' )";
            else query = "update ApplicantData set Applicant='" +applicant_str+ "' ";

            //Console.WriteLine(dt.Rows.Count + "\n");
            //Console.WriteLine(query + "\n");/////////


            SqlCommand scmd = new SqlCommand(query, con);

            try
            {
                int i = scmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Query Failed\n" + ex);
            }









            //cv insert to ApplicantCVData
            query = "insert into ApplicantCVData values('" + jobID + "', '" + user + "', '" + cvLocation + "' )";
            SqlCommand sqlcmd = new SqlCommand(query, con);

            try
            {
                int i = sqlcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Query Failed\n" + ex);
            }





            MessageBox.Show(" You have Applied Succesfully\n", "Succes");

            DialogResult result = MessageBox.Show(" Do you want to close the window?\n", "Close Window Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
            else applyButton.Visible = false;



            @End:
            con.Close();


        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose Your CV";
            //ofd.Filter = "pdf files (*.pdf)|*.pdf|doc files (*.doc)|*.doc|All files (*.*)|*.*";
            ofd.Filter = "All files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cvLocation = ofd.FileName;
                cvTextBox.Text = cvLocation;
                
            }
        }
    }
}
