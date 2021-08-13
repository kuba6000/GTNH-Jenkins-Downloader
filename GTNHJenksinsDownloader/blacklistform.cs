using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTNHJenksinsDownloader
{
    public partial class blacklistform : Form
    {
        public blacklistform()
        {
            InitializeComponent();
        }

        private void addmanual_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && !Settings.options.blacklist.Contains(textBox1.Text))
            {
                Settings.options.blacklist.Add(textBox1.Text);
                blacklistlist.Items.Add(textBox1.Text);
                Settings.Save();
            }
            textBox1.Text = "";
        }

        private void blacklistform_Load(object sender, EventArgs e)
        {
            foreach (string item in Settings.options.blacklist)
                blacklistlist.Items.Add(item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.options.blacklist.Clear();
            Settings.Save();
            blacklistlist.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] recommended = { 
                "BuildCraft",
                "BuildCraftCompat",
                "Ender-IO",
                "Ender-Storage",
                "Tainted-Magic",
                "ThaumcraftMobAspects"
            };

            foreach (string recom in recommended)
            {
                if(!Settings.options.blacklist.Contains(recom))
                {
                    Settings.options.blacklist.Add(recom);
                    blacklistlist.Items.Add(recom);
                }
            }

            Settings.Save();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(blacklistlist.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in blacklistlist.SelectedItems)
                {
                    Settings.options.blacklist.Remove(item.Text);
                    item.Remove();
                }
                Settings.Save();
            }
        }
    }
}
