
namespace GTNHJenksinsDownloader
{
    partial class SettingsForm
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
            this.savebutton = new System.Windows.Forms.Button();
            this.quitonclosechk = new System.Windows.Forms.CheckBox();
            this.autoupdatechk = new System.Windows.Forms.CheckBox();
            this.autoupdateinterval = new System.Windows.Forms.ComboBox();
            this.autostartchk = new System.Windows.Forms.CheckBox();
            this.startintraychk = new System.Windows.Forms.CheckBox();
            this.modpathtextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.jenksinspathtxt = new System.Windows.Forms.TextBox();
            this.modpathbutton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.clientchk = new System.Windows.Forms.CheckBox();
            this.serverchk = new System.Windows.Forms.CheckBox();
            this.servermodpathbutton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.servermodpathtxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // savebutton
            // 
            this.savebutton.Location = new System.Drawing.Point(468, 243);
            this.savebutton.Name = "savebutton";
            this.savebutton.Size = new System.Drawing.Size(79, 27);
            this.savebutton.TabIndex = 0;
            this.savebutton.Text = "Save";
            this.savebutton.UseVisualStyleBackColor = true;
            this.savebutton.Click += new System.EventHandler(this.savebutton_Click);
            // 
            // quitonclosechk
            // 
            this.quitonclosechk.AutoSize = true;
            this.quitonclosechk.Location = new System.Drawing.Point(12, 12);
            this.quitonclosechk.Name = "quitonclosechk";
            this.quitonclosechk.Size = new System.Drawing.Size(297, 23);
            this.quitonclosechk.TabIndex = 1;
            this.quitonclosechk.Text = "Quit on close instead of hiding in tray";
            this.quitonclosechk.UseVisualStyleBackColor = true;
            // 
            // autoupdatechk
            // 
            this.autoupdatechk.AutoSize = true;
            this.autoupdatechk.Location = new System.Drawing.Point(12, 41);
            this.autoupdatechk.Name = "autoupdatechk";
            this.autoupdatechk.Size = new System.Drawing.Size(249, 23);
            this.autoupdatechk.TabIndex = 2;
            this.autoupdatechk.Text = "Automatically check for updates";
            this.autoupdatechk.UseVisualStyleBackColor = true;
            // 
            // autoupdateinterval
            // 
            this.autoupdateinterval.FormattingEnabled = true;
            this.autoupdateinterval.Items.AddRange(new object[] {
            "Only on startup",
            "Every 10 minutes"});
            this.autoupdateinterval.Location = new System.Drawing.Point(267, 39);
            this.autoupdateinterval.Name = "autoupdateinterval";
            this.autoupdateinterval.Size = new System.Drawing.Size(165, 27);
            this.autoupdateinterval.TabIndex = 3;
            this.autoupdateinterval.Text = "Only on startup";
            // 
            // autostartchk
            // 
            this.autostartchk.AutoSize = true;
            this.autostartchk.Location = new System.Drawing.Point(12, 70);
            this.autostartchk.Name = "autostartchk";
            this.autostartchk.Size = new System.Drawing.Size(87, 23);
            this.autostartchk.TabIndex = 4;
            this.autostartchk.Text = "Autostart";
            this.autostartchk.UseVisualStyleBackColor = true;
            this.autostartchk.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // startintraychk
            // 
            this.startintraychk.AutoSize = true;
            this.startintraychk.Location = new System.Drawing.Point(12, 99);
            this.startintraychk.Name = "startintraychk";
            this.startintraychk.Size = new System.Drawing.Size(162, 23);
            this.startintraychk.TabIndex = 5;
            this.startintraychk.Text = "Always start in tray";
            this.startintraychk.UseVisualStyleBackColor = true;
            // 
            // modpathtextbox
            // 
            this.modpathtextbox.Enabled = false;
            this.modpathtextbox.Location = new System.Drawing.Point(170, 150);
            this.modpathtextbox.Name = "modpathtextbox";
            this.modpathtextbox.Size = new System.Drawing.Size(262, 23);
            this.modpathtextbox.TabIndex = 6;
            this.modpathtextbox.TextChanged += new System.EventHandler(this.modpathtextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(12, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Client mods path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Jenkins path";
            // 
            // jenksinspathtxt
            // 
            this.jenksinspathtxt.Location = new System.Drawing.Point(170, 243);
            this.jenksinspathtxt.Name = "jenksinspathtxt";
            this.jenksinspathtxt.Size = new System.Drawing.Size(262, 23);
            this.jenksinspathtxt.TabIndex = 9;
            this.jenksinspathtxt.Text = "http://jenkins.usrv.eu:8080/";
            // 
            // modpathbutton
            // 
            this.modpathbutton.Enabled = false;
            this.modpathbutton.Location = new System.Drawing.Point(436, 147);
            this.modpathbutton.Name = "modpathbutton";
            this.modpathbutton.Size = new System.Drawing.Size(79, 29);
            this.modpathbutton.TabIndex = 10;
            this.modpathbutton.Text = "Select";
            this.modpathbutton.UseVisualStyleBackColor = true;
            this.modpathbutton.Click += new System.EventHandler(this.modpathbutton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("OpenSymbol", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(521, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 30);
            this.label3.TabIndex = 11;
            this.label3.Text = "!!!";
            this.toolTip1.SetToolTip(this.label3, "Wrong path");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("OpenSymbol", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(521, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 30);
            this.label4.TabIndex = 16;
            this.label4.Text = "!!!";
            this.toolTip1.SetToolTip(this.label4, "Wrong path");
            // 
            // clientchk
            // 
            this.clientchk.AutoSize = true;
            this.clientchk.Location = new System.Drawing.Point(12, 128);
            this.clientchk.Name = "clientchk";
            this.clientchk.Size = new System.Drawing.Size(65, 23);
            this.clientchk.TabIndex = 12;
            this.clientchk.Text = "Client";
            this.clientchk.UseVisualStyleBackColor = true;
            this.clientchk.CheckedChanged += new System.EventHandler(this.clientchk_CheckedChanged);
            // 
            // serverchk
            // 
            this.serverchk.AutoSize = true;
            this.serverchk.Location = new System.Drawing.Point(12, 176);
            this.serverchk.Name = "serverchk";
            this.serverchk.Size = new System.Drawing.Size(71, 23);
            this.serverchk.TabIndex = 17;
            this.serverchk.Text = "Server";
            this.serverchk.UseVisualStyleBackColor = true;
            this.serverchk.CheckedChanged += new System.EventHandler(this.serverchk_CheckedChanged);
            // 
            // servermodpathbutton
            // 
            this.servermodpathbutton.Enabled = false;
            this.servermodpathbutton.Location = new System.Drawing.Point(436, 195);
            this.servermodpathbutton.Name = "servermodpathbutton";
            this.servermodpathbutton.Size = new System.Drawing.Size(79, 29);
            this.servermodpathbutton.TabIndex = 15;
            this.servermodpathbutton.Text = "Select";
            this.servermodpathbutton.UseVisualStyleBackColor = true;
            this.servermodpathbutton.Click += new System.EventHandler(this.servermodpathbutton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(12, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Server mods path";
            // 
            // servermodpathtxt
            // 
            this.servermodpathtxt.Enabled = false;
            this.servermodpathtxt.Location = new System.Drawing.Point(170, 198);
            this.servermodpathtxt.Name = "servermodpathtxt";
            this.servermodpathtxt.Size = new System.Drawing.Size(262, 23);
            this.servermodpathtxt.TabIndex = 13;
            this.servermodpathtxt.TextChanged += new System.EventHandler(this.servermodpathtxt_TextChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 282);
            this.Controls.Add(this.serverchk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.servermodpathbutton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.servermodpathtxt);
            this.Controls.Add(this.clientchk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.modpathbutton);
            this.Controls.Add(this.jenksinspathtxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modpathtextbox);
            this.Controls.Add(this.startintraychk);
            this.Controls.Add(this.autostartchk);
            this.Controls.Add(this.autoupdateinterval);
            this.Controls.Add(this.autoupdatechk);
            this.Controls.Add(this.quitonclosechk);
            this.Controls.Add(this.savebutton);
            this.Font = new System.Drawing.Font("OpenSymbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button savebutton;
        private System.Windows.Forms.CheckBox quitonclosechk;
        private System.Windows.Forms.CheckBox autoupdatechk;
        private System.Windows.Forms.ComboBox autoupdateinterval;
        private System.Windows.Forms.CheckBox autostartchk;
        private System.Windows.Forms.CheckBox startintraychk;
        private System.Windows.Forms.TextBox modpathtextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox jenksinspathtxt;
        private System.Windows.Forms.Button modpathbutton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox clientchk;
        private System.Windows.Forms.CheckBox serverchk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button servermodpathbutton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox servermodpathtxt;
    }
}