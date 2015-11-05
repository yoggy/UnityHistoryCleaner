using System;
using System.Windows.Forms;

using Microsoft.Win32;

namespace UnityHistoryCleaner
{
    public partial class UnityHistoryCleanerForm : Form
    {
        public UnityHistoryCleanerForm()
        {
            InitializeComponent();
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            DialogResult rv = MessageBox.Show("本当に消しますか？", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (rv == DialogResult.Cancel) return;

            ClearHistory();

            MessageBox.Show("消しました！", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearHistory()
        {
            string basePath = @"SOFTWARE\Unity Technologies\Unity Editor 5.x\";
            string targetName = @"RecentlyUsedProjectPaths";

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(basePath, true);
            string[] names = reg.GetValueNames();

            foreach (string n in names)
            {
                if (n.Length >= targetName.Length && n.Substring(0, targetName.Length) == targetName)
                {
                    reg.DeleteValue(n);
                }
            }

            reg.Close();
        }
    }
}
