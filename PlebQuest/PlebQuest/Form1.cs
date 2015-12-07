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
        int idPerso;        //id du perso
        DataTable sheet;    //table du character
        DataTable raceDt;   //table de race (ne change jms)
        CExecuteur exe;     //executeur qui communique avec la BD
        Timer timer;        //le timer qui compte les ticks pour les actions

        public Form1(int idPerso)
        {
            this.idPerso = idPerso;
            InitializeComponent();
            exe = new CExecuteur();
            timer = new Timer();
            timer.Interval = 50;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pgbAction.ForeColor = Color.FromArgb(6030325);
            pgbExp.ForeColor = Color.FromArgb(6030325);

            DisplaySheet();
            FillEquip();
            FillSpell();
            FillLoot();
            FillQuest();

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// event declencher a chaque tick du timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            pgbAction.Increment(1);
            //si la progress bar est pleine
            if(pgbAction.Value == pgbAction.Maximum)
            {
                pgbAction.Value = 0;
                DataTable tabTemp = new DataTable();
                exe.ExecPsParams("sp_Action", tabTemp, idPerso);

                DisplaySheet();
                FillEquip();
                FillSpell();
                FillLoot();
                FillQuest();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Close();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// rempli le character sheet
        /// </summary>
        private void DisplaySheet()
        {
            //si lappel a la BD a foncitonner
            if (exe.ConnexionValide())
            {
                lstCharSheet.Items.Clear();

                sheet = new DataTable();
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
                raceDt = new DataTable();
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
                equipIdDt.Columns.Add("EquipID", typeof(Int32));
                equipIdDt.Columns.Add("CharID", typeof(Int32));
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
                
                //compte les stats que l'armure apporte au character
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

                //ecrit dans la listview
                ListViewItem rowName = new ListViewItem("Name");
                rowName.SubItems.Add(sheet.Rows[0]["Name"].ToString());
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
                int strChar = (Int32)sheet.Rows[0]["Str"] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0]["Str"]);
                rowStr.SubItems.Add((strChar + strEquip).ToString());
                ListViewItem rowDext = new ListViewItem("Dexterity");
                int dexChar = (Int32)sheet.Rows[0]["Dex"] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0]["Dex"]);
                rowDext.SubItems.Add((dexChar + dexEquip).ToString());
                ListViewItem rowInt = new ListViewItem("Intel");
                int intChar = (Int32)sheet.Rows[0]["Int"] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0]["Int"]);
                rowInt.SubItems.Add((intChar + intEquip).ToString());
                ListViewItem rowCon = new ListViewItem("Constitution");
                int conChar = (Int32)sheet.Rows[0]["Con"] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0]["Con"]);
                rowCon.SubItems.Add((conChar + conEquip).ToString());
                ListViewItem rowCha = new ListViewItem("Charisma");
                int chaChar = (Int32)sheet.Rows[0]["Cha"] + ((Int32)sheet.Rows[0][5] * (Int32)raceDt.Rows[0]["Cha"]);
                rowCha.SubItems.Add((chaChar + chaEquip).ToString());

                lstCharSheet.Items.AddRange(new ListViewItem[] { rowName, rowRace, rowClass, rowHP, rowGold, rowExp, rowLevel, rowStr, rowDext, rowInt, rowCon, rowCha }); 

                pgbExp.Maximum = (Int32)sheet.Rows[0][7];
                pgbExp.Value = (Int32)sheet.Rows[0][6];
            }
            else
            {
                MessageBox.Show("could not connect");
            }
        }

        /// <summary>
        /// remplis la listview d'equipment
        /// </summary>
        public void FillEquip()
        {
            lstEquip.Items.Clear();
            string[] result = new string[5]; 

            DataTable equipIdDt = new DataTable();
            equipIdDt.Columns.Add("EquipID", typeof(Int32));
            equipIdDt.Columns.Add("CharID", typeof(Int32));
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

            for (int i = 0; i < equipIdDt.Rows.Count; i++)
            {
                exe.ExecPsParams("sp_GetEquipmentInfo", equipDt, (Int32)equipIdDt.Rows[i][0]);
                string[] data = equipDt.Rows[0][1].ToString().Split(' ');

                switch(data[1])
                {
                    case "helmet":
                    case "hat":
                    case "bandana":
                        result[0] = equipDt.Rows[0]["Name"].ToString();
                        break;
                    case "chest":
                    case "robe":
                    case "chestplate":
                        result[1] = equipDt.Rows[0]["Name"].ToString();
                        break;
                    case "pants":
                        result[2] = equipDt.Rows[0]["Name"].ToString();
                        break;
                    case "boots":
                        result[3] = equipDt.Rows[0]["Name"].ToString();
                        break;
                    default:
                        result[4] = equipDt.Rows[0]["Name"].ToString();
                        break;
                }
            }

            ListViewItem rowHead = new ListViewItem("Head");
            rowHead.SubItems.Add(result[0]);
            ListViewItem rowChest = new ListViewItem("Chest");
            rowChest.SubItems.Add(result[1]);
            ListViewItem rowPants = new ListViewItem("Pants");
            rowPants.SubItems.Add(result[2]);
            ListViewItem rowBoots = new ListViewItem("Boots");
            rowBoots.SubItems.Add(result[3]);
            ListViewItem rowWeapon = new ListViewItem("Weapon");
            rowWeapon.SubItems.Add(result[4]);

            lstEquip.Items.AddRange(new ListViewItem[] { rowHead, rowChest, rowPants, rowBoots, rowWeapon });
        }

        /// <summary>
        /// remplis la listview de scrolls
        /// </summary>
        private void FillSpell()
        {
            lstSpell.Items.Clear();

            DataTable spellDt = new DataTable();
            spellDt.Columns.Add("Id", typeof(Int32));
            spellDt.Columns.Add("Name", typeof(string));
            spellDt.Columns.Add("Qty", typeof(Int32));
            exe.ExecPsParams("sp_GetCharacterSpell", spellDt, idPerso);

            for (int i = 0; i < spellDt.Rows.Count; i++)
            {
                ListViewItem row = new ListViewItem(spellDt.Rows[i]["Name"].ToString());
                row.SubItems.Add(spellDt.Rows[i]["Qty"].ToString());

                lstSpell.Items.AddRange(new ListViewItem[] { row });
            }
        }

        /// <summary>
        /// remplis la listview de loot
        /// </summary>
        private void FillLoot()
        {
            lstLoot.Items.Clear();

            DataTable lootDt = new DataTable();
            lootDt.Columns.Add("Id", typeof(Int32));
            lootDt.Columns.Add("Name", typeof(string));
            lootDt.Columns.Add("Qty", typeof(Int32));
            exe.ExecPsParams("sp_GetCharacterItem", lootDt, idPerso);

            for (int i = 0; i < lootDt.Rows.Count; i++)
            {
                ListViewItem row = new ListViewItem(lootDt.Rows[i]["Name"].ToString());
                row.SubItems.Add(lootDt.Rows[i]["Qty"].ToString());

                lstLoot.Items.AddRange(new ListViewItem[] { row });
            }
        }

        /// <summary>
        /// remplis la listview de quest
        /// </summary>
        private void FillQuest()
        {
            lstQuest.Items.Clear();

            DataTable questDt = new DataTable();
            //questDt.Columns.Add("Id", typeof(Int32));
            questDt.Columns.Add("Quest", typeof(string));
            questDt.Columns.Add("Status", typeof(Int32));
            exe.ExecPsParams("sp_GetCharacterQuest", questDt, idPerso);

            for (int i = 0; i < questDt.Rows.Count; i++)
            {
                ListViewItem row = new ListViewItem(questDt.Rows[i]["Quest"].ToString());
                if ((int)questDt.Rows[i]["Status"] == 1)
                    row.ForeColor = Color.Green;

                lstQuest.Items.AddRange(new ListViewItem[] { row });
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            timer.Interval = 50;
            timer.Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void btnSlower_Click(object sender, EventArgs e)
        {
            timer.Interval = 80;
            timer.Start();
        }

        private void btnFaster_Click(object sender, EventArgs e)
        {
            timer.Interval = 10;
            timer.Start();
        }
    }
}
