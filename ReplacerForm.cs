using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TTMPLReplacer.Enums;

namespace TTMPLReplacer
{
    public partial class ReplacerForm : Form
    {
        public ReplacerForm()
        {
            InitializeComponent();
        }
        
        private void ReplacerForm_Load(object sender, EventArgs e)
        {
            txtVersion.Text = Program.VersionNumber;
            cBoxConversion.DataSource = Enum.GetValues(typeof(ConvertType));
            cBoxSkin.DataSource = Enum.GetValues(typeof(SkinSlot));
            cBoxPube.DataSource = Enum.GetValues(typeof(PubeSlot));
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            Program.ConvertType = Enum.Parse<ConvertType>(cBoxConversion.SelectedValue.ToString() ?? string.Empty);
            Program.SkinSlot = Enum.Parse<SkinSlot>(cBoxSkin.SelectedValue.ToString() ?? string.Empty);
            Program.PubeSlot = Enum.Parse<PubeSlot>(cBoxPube.SelectedValue.ToString() ?? string.Empty);
            string[] selectedFiles;
            using (OpenFileDialog fileDialog = new())
            {
                fileDialog.InitialDirectory = Program.BasePath;
                fileDialog.Filter = "ttmp2 files (*.ttmp2)|*.ttmp2";
                fileDialog.Multiselect = true;
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                selectedFiles = fileDialog.FileNames.ToArray();
            }

            Stopwatch sw = Stopwatch.StartNew();
            int successfulConversion = 0;
            int failedConversions = 0;
            foreach (string file in selectedFiles)
            {
                if (MPLReplacer.ConvertFile(file)) successfulConversion++;
                else failedConversions++;
            }
            sw.Stop();
            StringBuilder sb = new();
            if (successfulConversion > 0) sb.AppendLine($"Successfully converted {successfulConversion} file(s)!");
            if (failedConversions > 0) sb.AppendLine($"Failed to convert {failedConversions} file(s)!");
            sb.AppendLine($"Conversions took {sw.ElapsedMilliseconds:n0}ms.");
            MessageBox.Show(sb.ToString(), "Conversion Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}