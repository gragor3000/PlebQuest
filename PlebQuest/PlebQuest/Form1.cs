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
        DataTable sheet = new DataTable();
        DataTable raceDt = new DataTable();

        public Form1(int idPerso)
        {
            this.idPerso = idPerso;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pgbAction.ForeColor = Color.FromArgb(6030325);
            pgbExp.ForeColor = Color.FromArgb(6030325);

            CExecuteur exe = new CExecuteur();

            if (exe.ConnexionValide())
            {
                
                sheet.Columns.Add("ID", typeof(Int32));
                sheet.Columns.Add("Name", typeof(string));
                sheet.Columns.Add("CurrentHP", typeof(Int32));
                sheet.Columns.Add("MaxHP", typeof(Int32));
                sheet.Columns.Add("Gold", typeof(Int32));
                sheet.Columns.Add("Level", typeof(Int32));
                sheet.Columns.Add("CurrentExp", typeof(Int32));
                sheet.Columns.Add("NextExp", typeof(Int32));
                sheet.Columns.Add("MaxItem", typeof(Int32));
                sheet.Columns.Add("Str", typeof(Int32));
                sheet.Columns.Add("Dex", typeof(Int32));
                sheet.Columns.Add("Int", typeof(Int32));
                sheet.Columns.Add("Con", typeof(Int32));
                sheet.Columns.Add("Cha", typeof(Int32));
                sheet.Columns.Add("ClassID", typeof(Int32));
                sheet.Columns.Add("RaceID", typeof(Int32));
                exe.ExecPsParams("sp_GetCharacterInfo", sheet, idPerso);

                //SetToolTip
                
                raceDt.Columns.Add("ID", typeof(Int32));
                raceDt.Columns.Add("Name", typeof(string));
                raceDt.Columns.Add("Desc", typeof(string));
                raceDt.Columns.Add("Str", typeof(Int32));
                raceDt.Columns.Add("Dex", typeof(Int32));
                raceDt.Columns.Add("Int", typeof(Int32));
                raceDt.Columns.Add("Con", typeof(Int32));
                raceDt.Columns.Add("Cha", typeof(Int32));
                exe.ExecPsParams("sp_RaceStats", raceDt, sheet.Rows[0][15]);

                DataTable classDt = new DataTable();
                classDt.Columns.Add("ID", typeof(Int32));
                classDt.Columns.Add("Name", typeof(string));
                classDt.Columns.Add("Desc", typeof(string));
                exe.ExecPsParams("sp_GetClassStats", classDt, sheet.Rows[0][14]);

                ListViewItem rowName = new ListViewItem("Name");
                rowName.SubItems.Add(sheet.Rows[0][1].ToString());
                ListViewItem rowRace = new ListViewItem("Race");
                rowRace.SubItems.Add(raceDt.Rows[0][1].ToString());
                ListViewItem rowClass = new ListViewItem("Class");
                rowClass.SubItems.Add(classDt.Rows[0][1].ToString());
                ListViewItem rowHP = new ListViewItem("HP");
                rowHP.SubItems.Add(sheet.Rows[0][2].ToString() + "/" + sheet.Rows[0][3].ToString());
                ListViewItem rowGold = new ListViewItem("Gold");
                rowGold.SubItems.Add(sheet.Rows[0][4].ToString());
                ListViewItem rowLevel = new ListViewItem("Level");
                rowLevel.SubItems.Add(sheet.Rows[0][5].ToString());
                ListViewItem rowExp = new ListViewItem("Exp");
                rowExp.SubItems.Add(sheet.Rows[0][6].ToString() + "/" + sheet.Rows[0][7].ToString());
                ListViewItem rowStr = new ListViewItem("Strength");
                int strChar = (Int32)sheet.Rows[0][9] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][3]);
                int strEquip = 0;   //add les stat d'equipment
                rowStr.SubItems.Add(strChar.ToString());
                ListViewItem rowDext = new ListViewItem("Dexterity");
                int dexChar = (Int32)sheet.Rows[0][10] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][4]);
                int dexEquip = 0;   //add les stat d'equipment
                rowDext.SubItems.Add(dexChar.ToString());
                ListViewItem rowInt = new ListViewItem("Intel");
                int intChar = (Int32)sheet.Rows[0][11] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][5]);
                int intEquip = 0;   //add les stat d'equipment
                rowInt.SubItems.Add(intChar.ToString());
                ListViewItem rowCon = new ListViewItem("Constitution");
                int conChar = (Int32)sheet.Rows[0][12] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][6]);
                int conEquip = 0;   //add les stat d'equipment
                rowCon.SubItems.Add(conChar.ToString());
                ListViewItem rowCha = new ListViewItem("Charisma");
                int chaChar = (Int32)sheet.Rows[0][13] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][7]);
                int chaEquip = 0;   //add les stat d'equipment
                rowCha.SubItems.Add(chaChar.ToString());


                lstCharSheet.Items.AddRange(new ListViewItem[] { rowName, rowRace, rowClass, rowHP, rowGold, rowExp, rowLevel, rowStr, rowDext, rowInt, rowCon, rowCha });
            }
            else
            {
                MessageBox.Show("could not connect");
            }

            /*
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
            */
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
