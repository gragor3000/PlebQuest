namespace PlebQuest
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pgbAction = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstCharSheet = new System.Windows.Forms.ListView();
            this.colTrait = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pgbExp = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstSpell = new System.Windows.Forms.ListView();
            this.colSpell = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNbSpell = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstEquip = new System.Windows.Forms.ListView();
            this.colEquip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEquipName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstLoot = new System.Windows.Forms.ListView();
            this.colItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNbItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lstQuest = new System.Windows.Forms.ListView();
            this.colQuest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSlower = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnFaster = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // pgbAction
            // 
            this.pgbAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.pgbAction.Location = new System.Drawing.Point(0, 539);
            this.pgbAction.Name = "pgbAction";
            this.pgbAction.Size = new System.Drawing.Size(656, 23);
            this.pgbAction.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstCharSheet);
            this.groupBox1.Location = new System.Drawing.Point(6, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 224);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character sheet";
            // 
            // lstCharSheet
            // 
            this.lstCharSheet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTrait,
            this.colValue});
            this.lstCharSheet.Location = new System.Drawing.Point(7, 20);
            this.lstCharSheet.Name = "lstCharSheet";
            this.lstCharSheet.Size = new System.Drawing.Size(187, 197);
            this.lstCharSheet.TabIndex = 0;
            this.lstCharSheet.UseCompatibleStateImageBehavior = false;
            this.lstCharSheet.View = System.Windows.Forms.View.Details;
            // 
            // colTrait
            // 
            this.colTrait.Text = "Stats";
            this.colTrait.Width = 80;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 102;
            // 
            // pgbExp
            // 
            this.pgbExp.Location = new System.Drawing.Point(6, 276);
            this.pgbExp.Name = "pgbExp";
            this.pgbExp.Size = new System.Drawing.Size(200, 13);
            this.pgbExp.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstSpell);
            this.groupBox2.Location = new System.Drawing.Point(6, 295);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 238);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grimoire";
            // 
            // lstSpell
            // 
            this.lstSpell.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSpell,
            this.colNbSpell});
            this.lstSpell.Location = new System.Drawing.Point(7, 20);
            this.lstSpell.Name = "lstSpell";
            this.lstSpell.Size = new System.Drawing.Size(187, 212);
            this.lstSpell.TabIndex = 0;
            this.lstSpell.UseCompatibleStateImageBehavior = false;
            this.lstSpell.View = System.Windows.Forms.View.Details;
            // 
            // colSpell
            // 
            this.colSpell.Text = "Scroll";
            this.colSpell.Width = 80;
            // 
            // colNbSpell
            // 
            this.colNbSpell.Text = "Scroll left";
            this.colNbSpell.Width = 102;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstEquip);
            this.groupBox3.Location = new System.Drawing.Point(212, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 142);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Character equipment";
            // 
            // lstEquip
            // 
            this.lstEquip.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colEquip,
            this.colEquipName});
            this.lstEquip.Location = new System.Drawing.Point(6, 19);
            this.lstEquip.Name = "lstEquip";
            this.lstEquip.Size = new System.Drawing.Size(299, 115);
            this.lstEquip.TabIndex = 1;
            this.lstEquip.UseCompatibleStateImageBehavior = false;
            this.lstEquip.View = System.Windows.Forms.View.Details;
            // 
            // colEquip
            // 
            this.colEquip.Text = "Equipment";
            this.colEquip.Width = 70;
            // 
            // colEquipName
            // 
            this.colEquipName.Text = "Name";
            this.colEquipName.Width = 225;
            // 
            // lstLoot
            // 
            this.lstLoot.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colItem,
            this.colNbItem});
            this.lstLoot.Location = new System.Drawing.Point(6, 19);
            this.lstLoot.Name = "lstLoot";
            this.lstLoot.Size = new System.Drawing.Size(299, 333);
            this.lstLoot.TabIndex = 1;
            this.lstLoot.UseCompatibleStateImageBehavior = false;
            this.lstLoot.View = System.Windows.Forms.View.Details;
            // 
            // colItem
            // 
            this.colItem.Text = "Item";
            this.colItem.Width = 225;
            // 
            // colNbItem
            // 
            this.colNbItem.Text = "Qty";
            this.colNbItem.Width = 70;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstLoot);
            this.groupBox4.Location = new System.Drawing.Point(212, 175);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(311, 358);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Loot";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lstQuest);
            this.groupBox5.Location = new System.Drawing.Point(529, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(243, 506);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Plebest Storyline";
            // 
            // lstQuest
            // 
            this.lstQuest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colQuest});
            this.lstQuest.Location = new System.Drawing.Point(6, 19);
            this.lstQuest.Name = "lstQuest";
            this.lstQuest.Size = new System.Drawing.Size(231, 481);
            this.lstQuest.TabIndex = 1;
            this.lstQuest.UseCompatibleStateImageBehavior = false;
            this.lstQuest.View = System.Windows.Forms.View.Details;
            // 
            // colQuest
            // 
            this.colQuest.Text = "Quests";
            this.colQuest.Width = 227;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Experience";
            // 
            // btnSlower
            // 
            this.btnSlower.BackgroundImage = global::PlebQuest.Properties.Resources.glyphicons_173_rewind;
            this.btnSlower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSlower.Location = new System.Drawing.Point(662, 539);
            this.btnSlower.Name = "btnSlower";
            this.btnSlower.Size = new System.Drawing.Size(23, 23);
            this.btnSlower.TabIndex = 11;
            this.btnSlower.UseVisualStyleBackColor = true;
            this.btnSlower.Click += new System.EventHandler(this.btnSlower_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = global::PlebQuest.Properties.Resources.glyphicons_174_play;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlay.Location = new System.Drawing.Point(720, 539);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(23, 23);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnFaster
            // 
            this.btnFaster.BackgroundImage = global::PlebQuest.Properties.Resources.glyphicons_177_forward;
            this.btnFaster.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFaster.Location = new System.Drawing.Point(749, 539);
            this.btnFaster.Name = "btnFaster";
            this.btnFaster.Size = new System.Drawing.Size(23, 23);
            this.btnFaster.TabIndex = 3;
            this.btnFaster.UseVisualStyleBackColor = true;
            this.btnFaster.Click += new System.EventHandler(this.btnFaster_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = global::PlebQuest.Properties.Resources.glyphicons_175_pause;
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Location = new System.Drawing.Point(691, 539);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(23, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSlower);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pgbExp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnFaster);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.pgbAction);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PlebQuest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ProgressBar pgbAction;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnFaster;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstCharSheet;
        private System.Windows.Forms.ProgressBar pgbExp;
        private System.Windows.Forms.ColumnHeader colTrait;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstSpell;
        private System.Windows.Forms.ColumnHeader colSpell;
        private System.Windows.Forms.ColumnHeader colNbSpell;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lstEquip;
        private System.Windows.Forms.ColumnHeader colEquip;
        private System.Windows.Forms.ColumnHeader colEquipName;
        private System.Windows.Forms.ListView lstLoot;
        private System.Windows.Forms.ColumnHeader colItem;
        private System.Windows.Forms.ColumnHeader colNbItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListView lstQuest;
        private System.Windows.Forms.ColumnHeader colQuest;
        private System.Windows.Forms.Button btnSlower;
        private System.Windows.Forms.Label label1;
    }
}

