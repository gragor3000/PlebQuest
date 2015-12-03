using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlebQuest
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            CExecuteur exe = new CExecuteur();

            if(exe.ConnexionValide())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(Int32));
                dt.Columns.Add("Name", typeof(string));
                exe.ExecView("view_Characters", dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string line = "";
                    line += dt.Rows[i][0];
                    line += " " + dt.Rows[i][1];

                    lstChar.Items.Add(line);
                }
            }
            else
            {
                MessageBox.Show("could not connect");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(lstChar.SelectedIndex > -1)
            {
                string[] data = lstChar.Items[lstChar.SelectedIndex].ToString().Split(' ');

                Form1 GUI = new Form1(int.Parse(data[0]));
                this.Close();
                GUI.Show();
            }
            else
            {
                MessageBox.Show("Please select a character.");
            }
        }

        private void lstChar_DoubleClick(object sender, EventArgs e)
        {
            if(lstChar.SelectedIndex > -1)
            {
                string[] data = lstChar.Items[lstChar.SelectedIndex].ToString().Split(' ');

                Form1 GUI = new Form1(int.Parse(data[0]));
                this.Hide();
                GUI.Show();
            }
                
        }
    }
}
