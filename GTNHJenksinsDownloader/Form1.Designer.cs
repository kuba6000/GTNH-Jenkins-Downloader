
namespace GTNHJenksinsDownloader
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.totalstatusbar = new System.Windows.Forms.ToolStripProgressBar();
            this.totalstatuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statuspercentage = new System.Windows.Forms.ToolStripProgressBar();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolstripbreak = new System.Windows.Forms.ToolStripStatusLabel();
            this.versionlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientmodslistview = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updatesstrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToBlacklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Updatebutton = new System.Windows.Forms.Button();
            this.upgrademods = new System.Windows.Forms.Button();
            this.clientupdates = new System.Windows.Forms.GroupBox();
            this.serverupdates = new System.Windows.Forms.GroupBox();
            this.servermodslistview = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.afterload = new System.Windows.Forms.Timer(this.components);
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blacklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.updatesstrip.SuspendLayout();
            this.clientupdates.SuspendLayout();
            this.serverupdates.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalstatusbar,
            this.totalstatuslabel,
            this.statuspercentage,
            this.statuslabel,
            this.toolstripbreak,
            this.versionlabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 594);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1323, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // totalstatusbar
            // 
            this.totalstatusbar.Name = "totalstatusbar";
            this.totalstatusbar.Size = new System.Drawing.Size(100, 16);
            this.totalstatusbar.Visible = false;
            // 
            // totalstatuslabel
            // 
            this.totalstatuslabel.Name = "totalstatuslabel";
            this.totalstatuslabel.Size = new System.Drawing.Size(23, 17);
            this.totalstatuslabel.Text = "0%";
            this.totalstatuslabel.Visible = false;
            // 
            // statuspercentage
            // 
            this.statuspercentage.Name = "statuspercentage";
            this.statuspercentage.Size = new System.Drawing.Size(100, 16);
            this.statuspercentage.Visible = false;
            // 
            // statuslabel
            // 
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(51, 17);
            this.statuslabel.Text = "Cheking";
            // 
            // toolstripbreak
            // 
            this.toolstripbreak.Name = "toolstripbreak";
            this.toolstripbreak.Size = new System.Drawing.Size(1133, 17);
            this.toolstripbreak.Spring = true;
            // 
            // versionlabel
            // 
            this.versionlabel.Name = "versionlabel";
            this.versionlabel.Size = new System.Drawing.Size(22, 17);
            this.versionlabel.Text = "0.1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.hideToTrayToolStripMenuItem,
            this.quitToolStripMenuItem,
            this.blacklistToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1323, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.fileToolStripMenuItem.Text = "Settings";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // hideToTrayToolStripMenuItem
            // 
            this.hideToTrayToolStripMenuItem.Name = "hideToTrayToolStripMenuItem";
            this.hideToTrayToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.hideToTrayToolStripMenuItem.Text = "Hide to tray";
            this.hideToTrayToolStripMenuItem.Click += new System.EventHandler(this.hideToTrayToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // clientmodslistview
            // 
            this.clientmodslistview.CheckBoxes = true;
            this.clientmodslistview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.clientmodslistview.ContextMenuStrip = this.updatesstrip;
            this.clientmodslistview.FullRowSelect = true;
            this.clientmodslistview.GridLines = true;
            this.clientmodslistview.HideSelection = false;
            this.clientmodslistview.Location = new System.Drawing.Point(6, 22);
            this.clientmodslistview.Name = "clientmodslistview";
            this.clientmodslistview.Size = new System.Drawing.Size(632, 491);
            this.clientmodslistview.TabIndex = 2;
            this.clientmodslistview.UseCompatibleStateImageBehavior = false;
            this.clientmodslistview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "NAME";
            this.columnHeader1.Width = 262;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Leatest version";
            this.columnHeader2.Width = 162;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Your version";
            this.columnHeader3.Width = 175;
            // 
            // updatesstrip
            // 
            this.updatesstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToBlacklistToolStripMenuItem,
            this.toggleToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toggleAllToolStripMenuItem});
            this.updatesstrip.Name = "updatesstrip";
            this.updatesstrip.Size = new System.Drawing.Size(231, 92);
            this.updatesstrip.Opening += new System.ComponentModel.CancelEventHandler(this.updatesstrip_Opening);
            // 
            // addToBlacklistToolStripMenuItem
            // 
            this.addToBlacklistToolStripMenuItem.Name = "addToBlacklistToolStripMenuItem";
            this.addToBlacklistToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.addToBlacklistToolStripMenuItem.Text = "Add to/remove from blacklist";
            this.addToBlacklistToolStripMenuItem.Click += new System.EventHandler(this.addToBlacklistToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "GTNH Jenkins Downloader";
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // Updatebutton
            // 
            this.Updatebutton.Location = new System.Drawing.Point(12, 27);
            this.Updatebutton.Name = "Updatebutton";
            this.Updatebutton.Size = new System.Drawing.Size(133, 27);
            this.Updatebutton.TabIndex = 4;
            this.Updatebutton.Text = "Update list";
            this.Updatebutton.UseVisualStyleBackColor = true;
            this.Updatebutton.Click += new System.EventHandler(this.Updatebutton_Click);
            // 
            // upgrademods
            // 
            this.upgrademods.Enabled = false;
            this.upgrademods.Location = new System.Drawing.Point(526, 27);
            this.upgrademods.Name = "upgrademods";
            this.upgrademods.Size = new System.Drawing.Size(133, 27);
            this.upgrademods.TabIndex = 5;
            this.upgrademods.Text = "Update mods";
            this.upgrademods.UseVisualStyleBackColor = true;
            this.upgrademods.Click += new System.EventHandler(this.upgrademods_Click);
            // 
            // clientupdates
            // 
            this.clientupdates.Controls.Add(this.clientmodslistview);
            this.clientupdates.Location = new System.Drawing.Point(12, 60);
            this.clientupdates.Name = "clientupdates";
            this.clientupdates.Size = new System.Drawing.Size(647, 523);
            this.clientupdates.TabIndex = 9;
            this.clientupdates.TabStop = false;
            this.clientupdates.Text = "Client updates";
            // 
            // serverupdates
            // 
            this.serverupdates.Controls.Add(this.servermodslistview);
            this.serverupdates.Location = new System.Drawing.Point(665, 60);
            this.serverupdates.Name = "serverupdates";
            this.serverupdates.Size = new System.Drawing.Size(647, 523);
            this.serverupdates.TabIndex = 10;
            this.serverupdates.TabStop = false;
            this.serverupdates.Text = "Server updates";
            // 
            // servermodslistview
            // 
            this.servermodslistview.CheckBoxes = true;
            this.servermodslistview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.servermodslistview.ContextMenuStrip = this.updatesstrip;
            this.servermodslistview.FullRowSelect = true;
            this.servermodslistview.GridLines = true;
            this.servermodslistview.HideSelection = false;
            this.servermodslistview.Location = new System.Drawing.Point(6, 22);
            this.servermodslistview.Name = "servermodslistview";
            this.servermodslistview.Size = new System.Drawing.Size(632, 491);
            this.servermodslistview.TabIndex = 2;
            this.servermodslistview.UseCompatibleStateImageBehavior = false;
            this.servermodslistview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "NAME";
            this.columnHeader9.Width = 262;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Leatest version";
            this.columnHeader10.Width = 162;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Your version";
            this.columnHeader11.Width = 175;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Olive;
            this.label1.Location = new System.Drawing.Point(281, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "Make sure the game is closed !";
            this.label1.Visible = false;
            // 
            // afterload
            // 
            this.afterload.Enabled = true;
            this.afterload.Interval = 1;
            this.afterload.Tick += new System.EventHandler(this.afterload_Tick);
            // 
            // toggleToolStripMenuItem
            // 
            this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
            this.toggleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleToolStripMenuItem.Text = "Toggle";
            this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.selectAllToolStripMenuItem.Text = "Check all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toggleAllToolStripMenuItem
            // 
            this.toggleAllToolStripMenuItem.Name = "toggleAllToolStripMenuItem";
            this.toggleAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.toggleAllToolStripMenuItem.Text = "Toggle all";
            this.toggleAllToolStripMenuItem.Click += new System.EventHandler(this.toggleAllToolStripMenuItem_Click);
            // 
            // blacklistToolStripMenuItem
            // 
            this.blacklistToolStripMenuItem.Name = "blacklistToolStripMenuItem";
            this.blacklistToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.blacklistToolStripMenuItem.Text = "Blacklist";
            this.blacklistToolStripMenuItem.Click += new System.EventHandler(this.blacklistToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 616);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverupdates);
            this.Controls.Add(this.clientupdates);
            this.Controls.Add(this.Updatebutton);
            this.Controls.Add(this.upgrademods);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("OpenSymbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GTNH Jenksins Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.updatesstrip.ResumeLayout(false);
            this.clientupdates.ResumeLayout(false);
            this.serverupdates.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statuslabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToTrayToolStripMenuItem;
        private System.Windows.Forms.ListView clientmodslistview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripStatusLabel toolstripbreak;
        private System.Windows.Forms.ToolStripStatusLabel versionlabel;
        private System.Windows.Forms.Button Updatebutton;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button upgrademods;
        private System.Windows.Forms.GroupBox clientupdates;
        private System.Windows.Forms.GroupBox serverupdates;
        private System.Windows.Forms.ListView servermodslistview;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer afterload;
        private System.Windows.Forms.ToolStripProgressBar statuspercentage;
        private System.Windows.Forms.ToolStripProgressBar totalstatusbar;
        private System.Windows.Forms.ToolStripStatusLabel totalstatuslabel;
        private System.Windows.Forms.ContextMenuStrip updatesstrip;
        private System.Windows.Forms.ToolStripMenuItem addToBlacklistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blacklistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

