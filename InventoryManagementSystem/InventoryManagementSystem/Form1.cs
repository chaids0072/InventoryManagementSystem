using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class Form1 : Form
    {
        private System.Drawing.Point _start_point = new System.Drawing.Point(0, 0);
        private bool _mouseDown;

        public Form1()
        {
            InitializeComponent();
        }

        private void userTextBox_Click(object sender, EventArgs e)
        {
            if (userTextBox.Text == "Username")
            {
                userTextBox.Text = "";
            }
        }

        private void passwordTextBox_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "Password")
            {
                passwordTextBox.Text = "";
                passwordTextBox.PasswordChar = '*';
            }
        }

        private void userTextBox_Leave(object sender, EventArgs e)
        {
            if (userTextBox.Text == "")
            {
                userTextBox.Text = "Username";
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Text = "Password";
                passwordTextBox.PasswordChar = '\0';
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            SqlDataReader reader = null;
            try
            {
                con = new SqlConnection(@"Server = .\SQLEXPRESS; Database = ims_user; Integrated Security = True");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = string.Format("select * from [ims_user].[dbo].[iUM] where Username = @Username and Password = @Password;");
                cmd.Parameters.AddWithValue("@Username", userTextBox.Text);
                cmd.Parameters.AddWithValue("@Password", passwordTextBox.Text);
                cmd.Connection = con;

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    MainWindow otherForm = new MainWindow();
                    //otherForm.FormClosed += new FormClosedEventHandler(otherForm_FormClosed);
                    this.Hide();
                    otherForm.Show();
                } else {
                    MessageBox.Show("Incorrect combination");
                }
            }
            finally
            {
                reader.Close();
                con.Close();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                System.Drawing.Point P = PointToScreen(e.Location);
                Location = new System.Drawing.Point(P.X - _start_point.X, P.Y - _start_point.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _start_point = new System.Drawing.Point(e.X, e.Y);
        }
    }
}
