using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    public partial class Registration : Form
    {
        private System.Drawing.Point _start_point = new System.Drawing.Point(0, 0);
        private bool _mouseDown;

        public Registration()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Registration_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void Registration_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _start_point = new System.Drawing.Point(e.X, e.Y);
        }

        private void Registration_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                System.Drawing.Point P = PointToScreen(e.Location);
                Location = new System.Drawing.Point(P.X - _start_point.X, P.Y - _start_point.Y);
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetUserList();
        }

        private DataTable GetUserList()
        {
            DataTable dtUser = new DataTable();

            string connString = ConfigurationManager.ConnectionStrings["ims_userDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from [ims_user].[dbo].[iUM]", con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtUser.Load(reader);
                    con.Close();
                }
            }

            return dtUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["ims_userDB"].ConnectionString;


            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please check the details again.");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Passwords don't match.");
            }
            else
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserAdd", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@Username", textBox1.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Password", textBox2.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("User has been created successfully.");
                    clear();
                    con.Close();
                }
                dataGridView1.DataSource = GetUserList();
            }
        }

        void clear() {
            textBox1.Text = textBox2.Text = textBox3.Text =  "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 2)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                string selectedUsr = row.Cells["Username"].Value.ToString();
                string selectedPwd = row.Cells["Password"].Value.ToString();

                string connString = ConfigurationManager.ConnectionStrings["ims_userDB"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from [ims_user].[dbo].[iUM] where Username = '" + selectedUsr + "' and Password = '" + selectedPwd + "'", con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(rowIndex);
            }
            else
            {
                MessageBox.Show("One user must be left.");
            }
        }
    }
}
