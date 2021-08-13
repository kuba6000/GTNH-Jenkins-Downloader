using System;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace GTNHJenksinsDownloader
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void modpathbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                modpathtextbox.Text = dialog.SelectedPath;
        }

        private void servermodpathbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                servermodpathtxt.Text = dialog.SelectedPath;
        }

        private void modpathtextbox_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = !Directory.Exists(modpathtextbox.Text);
        }
        private void servermodpathtxt_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = !Directory.Exists(servermodpathtxt.Text);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            quitonclosechk.Checked = Settings.options.quitonclose;
            autoupdatechk.Checked = Settings.options.autoupdates;
            autoupdateinterval.SelectedIndex = (int)Settings.options.autoupdateinterval;
            startintraychk.Checked = Settings.options.startintray;
            modpathtextbox.Text = Settings.options.modpath;
            jenksinspathtxt.Text = Settings.options.jenkinspath;
            clientchk.Checked = Settings.options.client;
            serverchk.Checked = Settings.options.server;
            servermodpathtxt.Text = Settings.options.servermodpath;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            autostartchk.Checked = rk.GetValue(Settings.appname) != null;
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            Settings.options.quitonclose = quitonclosechk.Checked;
            Settings.options.autoupdates = autoupdatechk.Checked;
            Settings.options.autoupdateinterval = (AutoUpdateSelector)autoupdateinterval.SelectedIndex;
            Settings.options.startintray = startintraychk.Checked;
            Settings.options.modpath = modpathtextbox.Text;
            Settings.options.jenkinspath = jenksinspathtxt.Text;
            Settings.options.client = clientchk.Checked;
            Settings.options.server = serverchk.Checked;
            Settings.options.servermodpath = servermodpathtxt.Text;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (autostartchk.Checked)
                rk.SetValue(Settings.appname, Settings.apppath);
            else
                rk.DeleteValue(Settings.appname, false);
            Settings.Save();
            Close();
        }

        private void clientchk_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = clientchk.Checked;
            modpathtextbox.Enabled = clientchk.Checked;
            modpathbutton.Enabled = clientchk.Checked;
        }

        private void serverchk_CheckedChanged(object sender, EventArgs e)
        {
            label5.Enabled = serverchk.Checked;
            servermodpathtxt.Enabled = serverchk.Checked;
            servermodpathbutton.Enabled = serverchk.Checked;
        }
    }
}
