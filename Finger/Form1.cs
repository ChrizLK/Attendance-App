using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Finger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = DateTime.Now.ToString("hh:mm");
        }

        //sql connecton db 
        SqlConnection Con = new SqlConnection(@"Data Source=.;Initial Catalog=InOutApp;Persist Security Info=True;User ID=sa;Password=RaySmartSoft;MultipleActiveResultSets=True");


        public void btncheck()
        {

            DateTime? dbDate = null;

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Date From Register where Date = @Date)", Con);
                cmd.Parameters.AddWithValue("@DATE", DateTime.Today);

                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                   button1.Enabled = false;
                }

               
            }

            catch (Exception ex) 
                {

                    MessageBox.Show("Day END Failed: " + ex.Message, "Failed");

                }

            finally 
            {

                
               Con.Close();

            }

        }


        public void CheckButtonStatus()
        {
            
            DateTime lastClickedDate = DateTime.Today;

           
            if (lastClickedDate.Date == DateTime.Today)
            {
             
                button1.Enabled = false;
            }
        }

        public void varz()
        {
            
            try
            {
                Con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Register (id,InTime,OutTime,Employee,Remark,OT,Date) VALUES(@id, @InTime, @OutTime, @Employee, @Remark, @OT , @Date)", Con);

                cmd.Parameters.AddWithValue("@id",1);
                cmd.Parameters.AddWithValue("@InTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@OutTime",DBNull.Value);
                cmd.Parameters.AddWithValue("@Employee", "Chrishan");
                cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
                cmd.Parameters.AddWithValue("@OT",DBNull.Value);
                cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Intime Recorded Successfully", "IN Time");

            }

            catch(Exception ex)
            {

                MessageBox.Show("Day Start Failed: " + ex.Message, "Failed");

            }

            finally
            {
                Con.Close();
                
            }

        }


        public void end() 
        {

            try
            {
                Con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE dbo.Register SET OutTime=@OutTime WHERE @Date = @Date", Con);

               
                cmd.Parameters.AddWithValue("@OutTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@Date", DateTime.Today);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Day End Successfully", "Out Time");

            }

            catch (Exception ex)
            {

                MessageBox.Show("Day END Failed: " + ex.Message, "Failed");

            }

            finally
            {
                Con.Close();

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //button1.Enabled = false;
            CheckButtonStatus();
            varz();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            end();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
