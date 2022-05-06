using NotionHashRemoverUI.Helpers;

namespace NotionHashRemoverUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectZipPath_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new();

            dlg.Filter = "Notion-generated Zipfile (.zip) | *.zip";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dlg.FileName;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilePath.Text))
            {
                try
                {
                    if (ZipHelpers.ExtractZipDirectory(txtFilePath.Text))
                    {
                        FileHelpers.Rename();
                    }

                    ShowSuccess();
                }
                catch (Exception ex)
                {
                    ShowError();
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                btnSelectZipPath.PerformClick();
            }
        }

        private void ShowError()
        {
            tslStatus.Text = "Error";
            tslStatus.BackColor = Color.Red;
        }

        private void ShowSuccess()
        {
            tslStatus.Text = "Success";
            tslStatus.BackColor = Color.Green;
        }
    }
}