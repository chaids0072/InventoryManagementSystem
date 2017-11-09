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

        private DataTable GetUserList() {
            DataTable dtUser = new DataTable();

            string connString = ConfigurationManager.ConnectionStrings["ims_userDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString)) {
                using (SqlCommand cmd = new SqlCommand("select * from [ims_user].[dbo].[iUM]", con)) {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtUser.Load(reader);
                }
            }

                return dtUser;
        }
    }
}
