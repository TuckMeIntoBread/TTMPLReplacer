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
            cBoxSkinBibo.DataSource = Enum.GetValues(typeof(SlotType));
            cBoxSkinGen3.DataSource = Enum.GetValues(typeof(SlotType));
            cBoxPubeBibo.DataSource = Enum.GetValues(typeof(SlotType));
            cBoxPubeGen3.DataSource = Enum.GetValues(typeof(SlotType));
        }

        public static SlotType BiboSkin { get; private set; }

        public static SlotType Gen3Skin { get; private set; }

        public static SlotType BiboPube { get; private set; }

        public static SlotType Gen3Pube { get; private set; }
        
        public static bool CorrectMatA { get; private set; }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            BiboSkin = Enum.Parse<SlotType>(cBoxSkinBibo.SelectedValue.ToString() ?? string.Empty);
            Gen3Skin = Enum.Parse<SlotType>(cBoxSkinGen3.SelectedValue.ToString() ?? string.Empty);
            BiboPube = Enum.Parse<SlotType>(cBoxPubeBibo.SelectedValue.ToString() ?? string.Empty);
            Gen3Pube = Enum.Parse<SlotType>(cBoxPubeGen3.SelectedValue.ToString() ?? string.Empty);
            CorrectMatA = cBoxMatA.Checked;
            Program.Log($"BiboSkin: {BiboSkin}, BiboPube: {BiboPube}, Gen3Skin: {Gen3Skin}, Gen3Pube: {Gen3Pube}");
            string[] selectedFiles;
            using (OpenFileDialog fileDialog = new())
            {
                fileDialog.InitialDirectory = Program.BasePath;
                fileDialog.Filter = "ttmp2 files (*.ttmp2)|*.ttmp2";
                fileDialog.Multiselect = true;
                if (fileDialog.ShowDialog() != DialogResult.OK) return;
                selectedFiles = fileDialog.FileNames.ToArray();
            }

            if (selectedFiles.Length == 0) return;

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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new();
            sb.AppendLine("Written by Bread#9902 with great assistance by Bizu for actually knowing what's getting replaced with what." + Environment.NewLine);
            sb.AppendLine("If you have issues feel free to DM TuckMeIntoBread#9902 on Discord with an attached Output.log file and a detailed description of the issue." + Environment.NewLine);
            sb.AppendLine("Additionally, make sure you're using the latest version: 'github.com/TuckMeIntoBread/TTMPLReplacer'");
            MessageBox.Show(sb.ToString(), "Help!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}