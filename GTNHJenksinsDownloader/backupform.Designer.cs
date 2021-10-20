
namespace GTNHJenksinsDownloader
{
    partial class backupform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.backuplist = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteall = new System.Windows.Forms.Button();
            this.deleteold = new System.Windows.Forms.CheckBox();
            this.deleteafter = new System.Windows.Forms.CheckBox();
            this.backupdir = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backuplist
            // 
            this.backuplist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.backuplist.ContextMenuStrip = this.contextMenuStrip1;
            this.backuplist.FullRowSelect = true;
            this.backuplist.GridLines = true;
            this.backuplist.HideSelection = false;
            this.backuplist.Location = new System.Drawing.Point(12, 12);
            this.backuplist.Name = "backuplist";
            this.backuplist.Size = new System.Drawing.Size(365, 243);
            this.backuplist.TabIndex = 4;
            this.backuplist.UseCompatibleStateImageBehavior = false;
            this.backuplist.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "DATE";
            this.columnHeader1.Width = 163;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "MODS";
            this.columnHeader2.Width = 92;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "TYPE";
            this.columnHeader3.Width = 90;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.extractToToolStripMenuItem,
            this.detailsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // extractToToolStripMenuItem
            // 
            this.extractToToolStripMenuItem.Name = "extractToToolStripMenuItem";
            this.extractToToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.extractToToolStripMenuItem.Text = "Extract to...";
            this.extractToToolStripMenuItem.Click += new System.EventHandler(this.extractToToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "*Right click to see options";
            // 
            // deleteall
            // 
            this.deleteall.Location = new System.Drawing.Point(396, 12);
            this.deleteall.Name = "deleteall";
            this.deleteall.Size = new System.Drawing.Size(146, 28);
            this.deleteall.TabIndex = 7;
            this.deleteall.Text = "Delete all";
            this.deleteall.UseVisualStyleBackColor = true;
            this.deleteall.Click += new System.EventHandler(this.deleteall_Click);
            // 
            // deleteold
            // 
            this.deleteold.AutoSize = true;
            this.deleteold.Location = new System.Drawing.Point(397, 47);
            this.deleteold.Name = "deleteold";
            this.deleteold.Size = new System.Drawing.Size(199, 23);
            this.deleteold.TabIndex = 8;
            this.deleteold.Text = "Delete backups 1 hr old";
            this.deleteold.UseVisualStyleBackColor = true;
            this.deleteold.CheckedChanged += new System.EventHandler(this.deleteold_CheckedChanged);
            // 
            // deleteafter
            // 
            this.deleteafter.AutoSize = true;
            this.deleteafter.Location = new System.Drawing.Point(397, 76);
            this.deleteafter.Name = "deleteafter";
            this.deleteafter.Size = new System.Drawing.Size(211, 42);
            this.deleteafter.TabIndex = 9;
            this.deleteafter.Text = "Delete oldest backup after\r\n2 backups";
            this.deleteafter.UseVisualStyleBackColor = true;
            this.deleteafter.CheckedChanged += new System.EventHandler(this.deleteafter_CheckedChanged);
            // 
            // backupdir
            // 
            this.backupdir.Location = new System.Drawing.Point(397, 124);
            this.backupdir.Name = "backupdir";
            this.backupdir.Size = new System.Drawing.Size(199, 28);
            this.backupdir.TabIndex = 10;
            this.backupdir.Text = "Open backups directory";
            this.backupdir.UseVisualStyleBackColor = true;
            this.backupdir.Click += new System.EventHandler(this.backupdir_Click);
            // 
            // backupform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 288);
            this.Controls.Add(this.backupdir);
            this.Controls.Add(this.deleteafter);
            this.Controls.Add(this.deleteold);
            this.Controls.Add(this.deleteall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backuplist);
            this.Font = new System.Drawing.Font("OpenSymbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "backupform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "backupform";
            this.Load += new System.EventHandler(this.backupform_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView backuplist;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.Button deleteall;
        private System.Windows.Forms.CheckBox deleteold;
        private System.Windows.Forms.CheckBox deleteafter;
        private System.Windows.Forms.Button backupdir;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}