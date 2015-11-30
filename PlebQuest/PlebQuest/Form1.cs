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
    public partial class Form1 : Form
    {
        int idPerso;

        public Form1(int idPerso)
        {
            this.idPerso = idPerso;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            ListViewItem rowName = new ListViewItem("Name");
            rowName.SubItems.Add("Dragonmost");

            ListViewItem rowRace = new ListViewItem("Race");
            rowRace.SubItems.Add("Dwarf");

            ListViewItem rowClass = new ListViewItem("Class");
            rowClass.SubItems.Add("Paladin");

            ListViewItem rowHP = new ListViewItem("HP");
            rowHP.SubItems.Add("200/200");

            ListViewItem rowGold= new ListViewItem("Gold");
            rowGold.SubItems.Add("10k");

            ListViewItem rowExp = new ListViewItem("Exp");
            rowExp.SubItems.Add("150/150");

            ListViewItem rowLevel = new ListViewItem("Level");
            rowLevel.SubItems.Add("100");

            ListViewItem rowStr = new ListViewItem("Strength");
            rowStr.SubItems.Add("150");

            ListViewItem rowDext = new ListViewItem("Dexterity");
            rowDext.SubItems.Add("150");

            ListViewItem rowInt = new ListViewItem("Intelligence");
            rowInt.SubItems.Add("150");

            ListViewItem rowCon = new ListViewItem("Constitution");
            rowCon.SubItems.Add("150");

            ListViewItem rowCha = new ListViewItem("Charisma");
            rowCha.SubItems.Add("150");

            listView1.Items.AddRange(new ListViewItem[] { rowName, rowRace, rowClass, rowHP, rowGold, rowExp, rowLevel, rowStr, rowDext, rowInt, rowCon, rowCha });

            ListViewItem rowWeap = new ListViewItem("Weapon");
            rowWeap.SubItems.Add("Lightbringer");

            ListViewItem rowHead = new ListViewItem("Head");
            rowHead.SubItems.Add("Lightsword Faceguard");

            ListViewItem rowChess = new ListViewItem("Chess");
            rowChess.SubItems.Add("Lightsworn Chessguard");

            ListViewItem rowPants = new ListViewItem("Pants");
            rowPants.SubItems.Add("Lightsworn Legguard");

            ListViewItem rowBoots = new ListViewItem("Boots");
            rowBoots.SubItems.Add("Lightsworn Boots");

            listView3.Items.AddRange(new ListViewItem[] { rowWeap, rowHead, rowChess, rowPants, rowBoots });

            progressBar1.ForeColor = Color.FromArgb(6030325);
            progressBar1.Value = 50;
            progressBar2.ForeColor = Color.FromArgb(6030325);
            progressBar2.Value = 50;
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }
    }
}
