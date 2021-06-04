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
    public partial class viewApplicantsForm : Form
    {
        string jobID = "";
        string user = "";

        public viewApplicantsForm(string jobID)
        {
            InitializeComponent();

            this.jobID = jobID;



            this.show();


            

        }



        //ConnectionBlock
        SqlConnection con = new SqlConnection(@"Data Source=PC\SQLEXPRESS;Initial Catalog=Job_Recruitment_Service_System_Data;Integrated Security=True");



        public void show()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from ApplicantData where Job_ID='" + jobID + "' ", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);


            string applicant_str = "";//////

            foreach (DataRow dr in dt.Rows)
            {

                applicant_str = dr[1].ToString();
            }

            con.Close();



            string[] ss = applicant_str.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> applicants = new List<string>();
            for (int x = 0; x < ss.Length; x++)
            {
                applicants.Add(ss[x]);
            }

            

            for (int x = 0; x < applicants.Count; x++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[x].Cells[0].Value = jobID;
                dataGridView.Rows[x].Cells[1].Value = applicants[x].ToString();
            }



            if(dt.Rows.Count > 0) this.user = dataGridView.Rows[0].Cells["Applicants"].Value.ToString();

            if (dt.Rows.Count == 0) viewButton.Visible = false;

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                jobID = dataGridView.Rows[e.RowIndex].Cells["Job_ID"].Value.ToString();
                user = dataGridView.Rows[e.RowIndex].Cells["Applicants"].Value.ToString();

                //Console.WriteLine(jobID+"\n");
                //Console.WriteLine(user + "\n");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
            }
        }

        private void viewButton_Click(object sender, EventArgs e)
        {

            viewApplicantsInfoForm oobb = new viewApplicantsInfoForm(jobID,user);
            oobb.Show();

        }
    }
}
