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
        int idPerso;                        //id du perso
        DataTable sheet = new DataTable();  //table du character
        DataTable raceDt = new DataTable(); //table de race (ne change jms)

        public Form1(int idPerso)
        {
            this.idPerso = idPerso;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pgbAction.ForeColor = Color.FromArgb(6030325);
            pgbExp.ForeColor = Color.FromArgb(6030325);

            DisplaySheet();
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplaySheet()
        {
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

                DataTable equipIdDt = new DataTable();
                equipIdDt.Columns.Add("ID", typeof(Int32));
                exe.ExecPsParams("sp_GetEquipment", equipIdDt, idPerso);
                DataTable equipDt = new DataTable();
                equipDt.Columns.Add("ID", typeof(Int32));
                equipDt.Columns.Add("Name", typeof(string));
                equipDt.Columns.Add("Value", typeof(Int32));
                equipDt.Columns.Add("Str", typeof(Int32));
                equipDt.Columns.Add("Dex", typeof(Int32));
                equipDt.Columns.Add("Int", typeof(Int32));
                equipDt.Columns.Add("Con", typeof(Int32));
                equipDt.Columns.Add("Cha", typeof(Int32));
                equipDt.Columns.Add("TypeID", typeof(Int32));


                int strEquip = 0;   //add les stat d'equipment
                int dexEquip = 0;   //add les stat d'equipment
                int intEquip = 0;   //add les stat d'equipment
                int conEquip = 0;   //add les stat d'equipment
                int chaEquip = 0;   //add les stat d'equipment
                for (int i = 0; i < equipIdDt.Rows.Count; i++)
                {
                    exe.ExecPsParams("sp_GetEquipmentInfo", equipDt, (Int32)equipIdDt.Rows[i][0]);

                    strEquip += (Int32)equipDt.Rows[0][3];
                    dexEquip += (Int32)equipDt.Rows[0][4];
                    intEquip += (Int32)equipDt.Rows[0][5];
                    conEquip += (Int32)equipDt.Rows[0][6];
                    chaEquip += (Int32)equipDt.Rows[0][7];
                }                

                for (int i = 0; i < equipDt.Rows.Count; i++)
                {
                    string a;
                    if (equipDt.Rows[i][1].ToString() != "")
                        a = equipDt.Rows[i][1].ToString();
                }

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
                rowStr.SubItems.Add((strChar + strEquip).ToString());
                ListViewItem rowDext = new ListViewItem("Dexterity");
                int dexChar = (Int32)sheet.Rows[0][10] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][4]);
                rowStr.SubItems.Add((dexChar + dexEquip).ToString());
                ListViewItem rowInt = new ListViewItem("Intel");
                int intChar = (Int32)sheet.Rows[0][11] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][5]);
                rowStr.SubItems.Add((intChar + intEquip).ToString());
                ListViewItem rowCon = new ListViewItem("Constitution");
                int conChar = (Int32)sheet.Rows[0][12] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][6]);
                rowStr.SubItems.Add((conChar + conEquip).ToString());
                ListViewItem rowCha = new ListViewItem("Charisma");
                int chaChar = (Int32)sheet.Rows[0][13] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0][7]);
                rowStr.SubItems.Add((chaChar + chaEquip).ToString());


                lstCharSheet.Items.AddRange(new ListViewItem[] { rowName, rowRace, rowClass, rowHP, rowGold, rowExp, rowLevel, rowStr, rowDext, rowInt, rowCon, rowCha });

                pgbExp.Maximum = (Int32)sheet.Rows[0][7];
                pgbExp.Value = (Int32)sheet.Rows[0][6];
            }
            else
            {
                MessageBox.Show("could not connect");
            }
        }
    }
}
