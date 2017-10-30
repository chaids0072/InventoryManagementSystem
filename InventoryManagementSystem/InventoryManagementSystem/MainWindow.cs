using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class MainWindow : Form
    {
        private System.Drawing.Point _start_point = new System.Drawing.Point(0, 0);
        private bool _mouseDown;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _start_point = new System.Drawing.Point(e.X, e.Y);
        }

        private void MainWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                System.Drawing.Point P = PointToScreen(e.Location);
                Location = new System.Drawing.Point(P.X - _start_point.X, P.Y - _start_point.Y);
            }
        }
    }
}
