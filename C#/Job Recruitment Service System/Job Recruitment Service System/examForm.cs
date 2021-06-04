using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Job_Recruitment_Service_System
{
    public partial class examForm : Form
    {

        string jobID = "";
        string user = "";


        int q1 = 0, q2 = 0, q3 = 0, q4 = 0, q5 = 0;

        public examForm(string user, string jobID)
        {
            InitializeComponent();

            this.jobID = jobID;
            this.user = user;

            q1TextBox.Text = "    yes 1a";
            q2TextBox.Text = "    language yes";
            q3TextBox.Text = "    microprocessor";

            q4_Option1_RadioButton.Checked = true;
            q5_Option2_RadioButton.Checked = true;


            //string[] ax = new string[10];
            //int length = ax.Length;

        }





        void Q1_ans()
        {
            int totalCorrect = 0;

            string str = q1TextBox.Text;
            str = str.ToLower();
            string[] words = str.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries);

            bool checked1 = false;
            bool checked2 = false;

            foreach (string word in words)
            {
                if (checked1 == false)
                {
                    if (word == "yes" || word == "possible" || word == "possible" || word == "affirmative" )
                    {
                        totalCorrect++;
                        checked1 = true;
                    }
                }

                if (checked2 == false)
                {
                    if (word == "1a")
                    {
                        totalCorrect++;
                        checked2 = true;
                    }
                }

                if (word == "no" || word == "not") totalCorrect--;


            }


            //if (totalCorrect >= 2) return 1;
            //else return 0;

            //if (totalCorrect >= 2) q1 = 1;
            //else q1 = 0;

            q1 = totalCorrect;

            //end
        }




        void Q2_ans()
        {
            int totalCorrect = 0;

            string str = q2TextBox.Text;
            str = str.ToLower();
            string[] words = str.Split(new char[] { ' ', '.', ',', '?' }, StringSplitOptions.RemoveEmptyEntries);

            bool checked1 = false;
            bool checked2 = false;

            foreach (string word in words)
            {
                if (checked1 == false)
                {
                    if (word == "yes" || word == "independent")
                    {
                        totalCorrect++;
                        checked1 = true;
                    }
                }

                if (checked2 == false)
                {
                    if (word == "language")
                    {
                        totalCorrect++;
                        checked2 = true;
                    }
                }

                if (word == "no" || word == "not") totalCorrect--;


            }


            //if (totalCorrect >= 2) return 1;
            //else return 0;

            //if (totalCorrect >= 2) q2 = 1;
            //else q2 = 0;

            q2 = totalCorrect;


            //end
        }




        void Q3_ans()
        {
            int totalCorrect = 0;

            string str = q3TextBox.Text;
            str = str.ToLower();
            string[] words = str.Split(new char[] { ' ', '.', ',', '-', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {

                if (word == "microprocessor" || word == "processor") totalCorrect++;

            }
            //micro processor

            //if (totalCorrect >= 1) return 1;
            //else return 0;

            //if (totalCorrect >= 1) q3 = 1;
            //else q3 = 0;

            q3 = totalCorrect;


        }






        void Q4_ans()
        {

            if (q4_Option1_RadioButton.Checked == true) q4 = 1;
            else q4 = 0;


        }



        void Q5_ans()
        {

            if (q5_Option2_RadioButton.Checked == true) q5 = 1;
            else q5 = 0;


        }





        private void submitButton_Click(object sender, EventArgs e)
        {



            //int q1 = Q1_ans();
            //Console.WriteLine(q1);

            //int q2 = Q2_ans();
            //Console.WriteLine(q2);

            //int q3 = Q3_ans();
            //Console.WriteLine(q3);


            Thread t1 = new Thread(Q1_ans);
            t1.Start();

            Thread t2 = new Thread(Q2_ans);
            t2.Start();

            Thread t3 = new Thread(Q3_ans);
            t3.Start();

            Thread t4 = new Thread(Q4_ans);
            t4.Start();

            Thread t5 = new Thread(Q5_ans);
            t5.Start();


            if ( t1.IsAlive || t2.IsAlive || t3.IsAlive )
            {
                Thread.Sleep(500);
            }


            Application.DoEvents();

            examResultForm ob = new examResultForm(q1,q2,q3,q4,q5,user,jobID);
            ob.Show();

            this.Dispose();
            //this.Dispose();
        }






    }
}
